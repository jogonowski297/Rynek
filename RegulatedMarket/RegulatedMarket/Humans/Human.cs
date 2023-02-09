using _04_Observer;
using RegulatedMarket.Bank;
using RegulatedMarket.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = RegulatedMarket.Products.Product;

namespace RegulatedMarket.Humans
{
    
    interface IObserverMarket<T>
    {
        void UpdateMarket(T product);
    }
    interface IObserverBank<T>
    {
        void UpdateHuman(T product);
    }

    public class Human : IObserverMarket<Market>, IObserverBank<CentalBank>
    {
        public string name { get; set; }
        public string surname { get; set; }
        public float accountBalance { get; set; }
        public float inflation { get; set; }

        public List<string> interestingThings = new List<string>();

        
        public void UpdateMarket(Market market)
        {
            Console.WriteLine($"Panie {name} dodano nowego sprzedawcę {market.sellersInMarket[market.sellersInMarket.Count -1].name}");
            //foreach(var seller in market.sellersInMarket)
            //{
            //    Console.WriteLine($"Panie {name} dodano nowego sprzedawcę {seller.name}");
            //}

        }

        public void UpdateHuman(CentalBank bank)
        {
            this.inflation = bank.inflation;
        }

    }
}
