using _04_Observer;
using Product = RegulatedMarket.Products.Product;
using RegulatedMarket.Humans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics.CodeAnalysis;
using RegulatedMarket.Markets;

namespace RegulatedMarket.Bank
{
    interface IObserver<T>
    {
        void Update(T product);
    }

    interface IObservableHuman<T>
    {
        void SubscribeHuman(params T[] observer);
        void NotifyHuman();
    }
    interface IObservableMarket<T>
    {
        void SubscribeMarket(params T[] observer);
        void NotifyMarket();
    }

    public class CentalBank: IObserver<Product>, IObservableHuman<Human>, IObservableMarket<Market>
    {
        public float inflation { get; set; }

        private Dictionary<string, List<float>> inflationCreationCost = new Dictionary<string, List<float>>(); //Gitara:[4,8,8,6]

        private Dictionary<string, List<float>> inflationCreationCostAv = new Dictionary<string, List<float>>(); //Gitara:[4,6,6.6,6.5]

        private Dictionary<string, float> inflationProduct = new Dictionary<string, float>(); //Gitara:2.56

        public CentalBank(float inflation)
        {
            this.inflation = inflation;
        }

        private readonly List<Human> observersHumans = new List<Human>();
        private readonly List<Market> observersMarkets = new List<Market>();

        public void SubscribeHuman(params Human[] newObservers)
        {
            observersHumans.AddRange(newObservers);
        }

        public void NotifyHuman()
        {
            observersHumans.ForEach(observers => observers.UpdateHuman(this));
        }

        public void SubscribeMarket(params Market[] newObservers)
        {
            observersMarkets.AddRange(newObservers);
        }

        public void NotifyMarket()
        {
            observersMarkets.ForEach(observers => observers.UpdateMarket(this.inflation));
        }

        public void Update(Product product)
        {
            float sum = 0;

            if (inflationCreationCost.ContainsKey(product.productName))
            {
                inflationCreationCost[product.productName].Add(product.creationCost);
            }
            else
            {
                inflationCreationCost.Add(product.productName, new List<float>());
                inflationCreationCost[product.productName].Add(product.creationCost);
            }

            AvCoastInflationCreation(product);

            foreach (var lista in inflationCreationCostAv)
            {
                CheckInflation(product, lista.Value);
            }

            foreach (var inflation in inflationProduct)
            {
                sum += inflation.Value;
            }

            if (inflationProduct.Count > 0)
            {
                this.inflation = (sum / inflationProduct.Count);
            }


            NotifyMarket();
            NotifyHuman();
            
            Console.WriteLine($"Inflacja wynosi: {this.inflation}");

        }


        private void AvCoastInflationCreation(Product p)
        {
            if (inflationCreationCostAv.ContainsKey(p.productName))
            {
                inflationCreationCostAv[p.productName].Add(inflationCreationCost[p.productName].Average());
            }
            else
            {
                inflationCreationCostAv.Add(p.productName, new List<float>());
                inflationCreationCostAv[p.productName].Add(inflationCreationCost[p.productName].Average());
            }
        }

        private void CheckInflation(Product p, List<float> AvList)
        {
            if(AvList.Count >= 3)
            {
                var first = AvList.Take(AvList.Count - 1).Average();

               // var first = AvList[AvList.Count - 2];
                var second = AvList[AvList.Count-1];
                var sub = first - second;

                if (inflationProduct.ContainsKey(p.productName))
                {
                    inflationProduct[p.productName] = ((sub / second) * 100) * (-1);
                }
                else
                {
                    inflationProduct.Add(p.productName, ((sub / second) * 100)* (-1)) ;
                }
            }

            
        }
    }
}
