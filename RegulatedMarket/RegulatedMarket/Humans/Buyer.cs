using _04_Observer;
using RegulatedMarket.Markets;
using RegulatedMarket.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = RegulatedMarket.Products.Product;

namespace RegulatedMarket.Humans
{

    public class Buyer : Human
    {
        public string need { get; set; }
        public int maxOneTransaction { get; set; }
        public List<Product> boughtProducts = new List<Product>();

        public Buyer(string name, string surname, float accountBalance, string need, int maxOneTransaction, params string[] interestingThings)
        {
            this.name = name;
            this.surname = surname;
            this.accountBalance = accountBalance;
            this.need = need;
            this.maxOneTransaction = maxOneTransaction;
            foreach (string i in interestingThings)
            {
                this.interestingThings.Add(i);
            }
        }

        public bool UpdateMarketNewProduct(Seller seller, Product newProduct, Market market)
        {
            if (interestingThings.Contains(newProduct.productName))
            {
                Console.WriteLine($"Istnieje produkt, który interesuje {name}, ten produkt to {newProduct.productName} o cenie {newProduct.price}");
                if (newProduct.price <= maxOneTransaction && accountBalance >= newProduct.price)
                {
                    market.buyProduct(newProduct, newProduct.owner, this);
                    boughtProducts.Add(newProduct);
                    Console.WriteLine($"{this.name} {this.surname} kupił {newProduct.productName}. Saldo po transakcji wynosi {this.accountBalance}");
                    return true;
                }
                return false;
            }
            return false;
           

        }



    }
}
