using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulatedMarket.Bank;
using RegulatedMarket.Markets;
using RegulatedMarket.Products;

namespace RegulatedMarket.Humans
{
    public class Seller : Human
    {

        public int markUpInPercent { get; set; }
        private Market market { get; set; }
        private CentalBank bank { get; set; }

        public Seller(string name, string surname, float accountBalance, int markUpInPercent)
        {
            this.name = name;
            this.surname = surname;
            this.accountBalance = accountBalance;
            this.markUpInPercent = markUpInPercent;
        }

        public List<Product> listProducts = new List<Product>();


        public void addProducts(List<Product> pro)
        {
            pro.ForEach(b => b.SubscribeBank(bank));

            foreach (Product p in pro)
            {
                p.owner = this;
                p.market = this.market;
                p.price += p.creationCost; 
                p.priceFromSeller = p.price - (p.price * (markUpInPercent / 100f));
                p.price += (p.price * (markUpInPercent / 100f));
                p.priceWithOutInflation = p.price;

                listProducts.Add(p);

                p.NotifyBank();
                this.market.addProductsFromSeller(this, p);
            }

            
        }

        public void addBank(CentalBank bank)
        {
            this.bank = bank;
        }

        private void addToList(List<Product> list, Product p)
        {
            
            list.Add(p);
        }

        public void joinToMarket(Market market)
        {
            this.market = market;
            market.addSellerToMarket(this);
        }

        public int getAllProductsSize()
        {
            return listProducts.Count;
        }

    }
}
