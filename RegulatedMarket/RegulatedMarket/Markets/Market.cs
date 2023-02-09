using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulatedMarket.Products;
using RegulatedMarket.Humans;
using RegulatedMarket.Bank;

namespace RegulatedMarket.Markets
{
    interface IObservableHuman<T>
    {
        void SubscribeHuman(params T[] observer);
        bool NofifyBuyerNewProducts(Seller s, Product p);
        void NotifyHumanNewSeller();
    }


    public class Market : IObservableHuman<Buyer>
    {
        public float inflation { get; set; }

        public List<Human> sellersInMarket = new List<Human>();

        public Dictionary<Human, List<Product>> sellerItems = new Dictionary<Human, List<Product>>();

        private readonly List<Buyer> observers = new List<Buyer>();

        public void addSellerToMarket(Seller seller)
        {
            //sellerItems.Add(seller, seller.listProducts);
            sellersInMarket.Add(seller);
            NotifyHumanNewSeller();
        }

        public void addProductsFromSeller(Seller seller, Product newProduct)
        {           
            
            if (sellerItems.ContainsKey(seller))
            {
                
                sellerItems[seller].Add(newProduct);                                       

            }
            else
            {
                sellerItems.Add(seller, new List<Product>());
                sellerItems[seller].Add(newProduct);
                
 
            }
            includeInflation(newProduct, this.inflation);

        }

        public void buyProduct(Product product, Seller seller, Buyer buyer)
        {
            foreach(var i in sellerItems[seller])
            {
                if(i.productName == product.productName && i.price == product.price)
                {
                    seller.accountBalance += i.price - i.priceFromSeller;
                    buyer.accountBalance -= i.price;

                    sellerItems[seller].Remove(product);
                    break;
                }
            }
        }

        public void UpdateMarket(float inflation)
        {
            this.inflation = inflation;
        }

        private void includeInflation(Product product, float inflation)
        {
            foreach (var i in sellerItems.Keys)
            {
                for (int j=sellerItems[i].Count -1; j >= 0; j--)
                {
                    if (inflation < 0)
                    {
                        sellerItems[i][j].price = sellerItems[i][j].priceWithOutInflation - sellerItems[i][j].priceWithOutInflation * ((inflation * (-1)) * 0.01f);
                    }
                    else if (inflation > 0)
                    {

                        sellerItems[i][j].price = sellerItems[i][j].priceWithOutInflation + sellerItems[i][j].priceWithOutInflation * (inflation * 0.01f);
                    }
                    if (NofifyBuyerNewProducts(sellerItems[i][j].owner, sellerItems[i][j]))
                    {
                        continue;
                    }


                }
            }                    

        }

        public void showProducts()
        {
            foreach(var i in sellerItems)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine(i.Key.name);
                foreach (var j in i.Value)
                {
                    Console.WriteLine($"{j.productName}  o cenie  {j.price}");
                }
                
            }
        }

        public void NotifyHumanNewSeller()
        {
            observers.ForEach(observers => observers.UpdateMarket(this));
        }

        public bool NofifyBuyerNewProducts(Seller seller, Product newProduct)
        {
            //observers.ForEach(observers => observers.UpdateMarketNewProduct(seller, newProduct, this));
            foreach (var observers in observers)
            {
                if( observers.UpdateMarketNewProduct(seller, newProduct, this))
                {
                    return true;
                }
            }
            return false;
        }

        public void SubscribeHuman(params Buyer[] newObservers)
        {
            observers.AddRange(newObservers);
        }





    }
}
 