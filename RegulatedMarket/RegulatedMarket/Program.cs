using RegulatedMarket.Products;
using RegulatedMarket.Humans;
using RegulatedMarket.Bank;
using RegulatedMarket.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RegulatedMarket
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bank = new CentalBank(0);
            var market = new Market();

            var sprzedawca1 = new Seller("Sprzedawca1", "Sprzedawca1", 0, 10);

            var kupujacy = new List<Buyer>()
            {
                new Buyer("Adam", "Nowak", 7000, "Gitara", 7000, "Gitara"),
                //new Buyer("Piotr", "Kokos", 100, "Gitara2", 70, "Gitara2"),
                //new Buyer("Krzysztof", "Nosacz", 100, "Ukulele", 70, "Ukulele")
            };

            List<string> items = new List<string>() { "Gitara", "Komputer", "Tablet", "Telefon", "Klawiatura", "Zegarek" };
            Random rnd = new Random();

            market.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(sprzedawca1);

            bank.SubscribeMarket(market);



            sprzedawca1.addBank(bank);
            sprzedawca1.joinToMarket(market);

            for (int i = 0; i < 50; i++)
            {
                var towar = new List<Product>();
                for (int j = 0; j < 3; j++)
                {
                    string product = items[rnd.Next(items.Count - 1)];
                    int price = rnd.Next(400, 4000);
                    int creation_coast = rnd.Next(100, 800);

                    towar.Add(new Product(product, price, creation_coast));
                }


                sprzedawca1.addProducts(towar);

                // market.showProducts();


                Console.ReadLine();
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            //var bank = new CentalBank(0);
            //var market = new Market();

            //var sprzedawca1 = new Seller("Sprzedawca1", "Sprzedawca1", 0, 10);
            //var towar11 = new List<Product>()
            //    {
            //        new Product("Gitara", 4000, 234)

            //    };
            //var kupujacy = new List<Buyer>()
            //{
            //    new Buyer("Adam", "Nowak", 7000, "Gitara", 7000, "Gitara")

            //};


            ////var sprzedawca2 = new Seller("Sprzedawca2", "Sprzedawca2", 0, 10);
            ////var towar21 = new List<Product>()
            ////    {
            ////        new Product("Ukulele", 4000, 234),
            ////        new Product("Ukulele", 4000, 252),
            ////        new Product("Ukulele", 4000, 250)

            ////};



            ////var sprzedawca2 = new Seller("Sprzedawca2", "Sprzedawca2", 0, 10);
            ////var towar21 = new List<Product>()
            ////    {
            ////        new Product("Flet", 40, 4),
            ////        //new Product("Gitara", 40, 8),
            ////        //new Product("Gitara", 40, 8),
            ////        //new Product("Gitara", 40, 6),
            ////        new Product("Flet1", 40, 4)
            ////        //new Product("Gitara1", 40, 8),
            ////        //new Product("Gitara1", 40, 8),
            ////        //new Product("Gitara1", 40, 9)

            ////    };
            ////var towar22 = new List<Product>()
            ////        {
            ////            new Product("Flet2", 321, 32)
            ////            //new Product("Gitara2", 333, 18),
            ////            //new Product("Gitara2", 320, 22)


            ////        };




            //market.SubscribeHuman(kupujacy.ToArray());
            //bank.SubscribeHuman(kupujacy.ToArray());
            //bank.SubscribeHuman(sprzedawca1);
            ////bank.SubscribeHuman(sprzedawca2);
            //bank.SubscribeMarket(market);
            ////bank.SubscribeHuman(sprzedawca2);
            ////towar.ForEach(p => p.SubscribeHuman(kupujacy.ToArray()));
            ////towar11.ForEach(b => b.SubscribeBank(bank));
            ////towar12.ForEach(b => b.SubscribeBank(bank));


            //sprzedawca1.addBank(bank);
            //sprzedawca1.joinToMarket(market);
            //sprzedawca1.addProducts(towar11);


            ////sprzedawca2.addBank(bank);
            ////sprzedawca2.joinToMarket(market);
            ////sprzedawca2.addProducts(towar21);

            //market.showProducts();

            ////sprzedawca2.joinToMarket(market);
            ////sprzedawca2.addProducts(towar21);
            ////sprzedawca2.addProducts(towar22);


            //Console.ReadLine();

        }
    }
}

