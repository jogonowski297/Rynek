using RegulatedMarket.Bank;
using RegulatedMarket.Humans;
using RegulatedMarket.Markets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RegulatedMarket.Products
{

    interface IObservableBank<T>
    {
        void SubscribeBank(params T[] observer);
        void NotifyBank();
    }

    public class Product : IObservableBank<CentalBank>
    {

        public string productName { get; set; }
        public float price { get; set; }
        public float priceFromSeller { get; set; }
        public float creationCost { get; set; }
        public float priceWithOutInflation { get; set; }
        public Seller owner { get; set; }
        public Market market { get; set; } 
        public float costInMarket {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            } 
        }


        private readonly List<CentalBank> bank = new List<CentalBank>();

        public Product(string productName, float price, float creationCost)
        {
            this.productName = productName;
            this.price = price;
            this.creationCost = creationCost;
        }

        public void NotifyBank()
        {
            bank.ForEach(bank => bank.Update(this));
        }

        public void SubscribeBank(params CentalBank[] newBank)
        {
            bank.AddRange(newBank);
        }




    }
}
