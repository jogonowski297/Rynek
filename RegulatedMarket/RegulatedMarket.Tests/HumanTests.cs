using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulatedMarket.Bank;
using RegulatedMarket.Humans;
using RegulatedMarket.Markets;
using RegulatedMarket.Products;
using Xunit;

namespace RegulatedMarket.Tests
{
    public class HumanTests
    {
        [Fact]
        public void TestAddBuyer()
        {
            var kupujacy = new List<Buyer>()
            {
                new Buyer("Adam", "Nowak", 7000, "Gitara", 7000, "Gitara")
            };
            Assert.Equal("Adam", kupujacy[0].name);
            Assert.Equal("Nowak", kupujacy[0].surname);
            Assert.Equal("Gitara", kupujacy[0].interestingThings[0]);
        }

        [Fact]
        public void TestBuyproduct()
        {
            var kupujacy = new List<Buyer>()
            {
                new Buyer("Adam", "Nowak", 7000, "Gitara", 7000, "Gitara")
            };

            var market = new Market();

            var bank = new CentalBank(0);

            var sprzedawca1 = new Seller("Sprzedawca1", "Sprzedawca1", 0, 10);

            var towar = new List<Product>();
            towar.Add(new Product("Gitara", 3000, 200));

            market.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(sprzedawca1);

            bank.SubscribeMarket(market);

            sprzedawca1.addBank(bank);
            sprzedawca1.joinToMarket(market);
            sprzedawca1.addProducts(towar);

            Assert.Equal("Gitara", kupujacy[0].boughtProducts[0].productName);

        }

        [Fact]
        public void TestBuyproductAccountBalance()
        {
            var kupujacy = new List<Buyer>()
            {
                new Buyer("Adam", "Nowak", 7000, "Gitara", 7000, "Gitara")
            };

            var market = new Market();

            var bank = new CentalBank(0);

            var sprzedawca1 = new Seller("Sprzedawca1", "Sprzedawca1", 0, 10);

            var towar = new List<Product>();
            towar.Add(new Product("Gitara", 3000, 200));

            market.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(sprzedawca1);

            bank.SubscribeMarket(market);

            sprzedawca1.addBank(bank);
            sprzedawca1.joinToMarket(market);
            sprzedawca1.addProducts(towar);

            Assert.Equal(3480, kupujacy[0].accountBalance);

        }

        [Fact]
        public void TestInflationKnown()
        {
            var kupujacy = new List<Buyer>()
            {
                new Buyer("Adam", "Nowak", 7000, "Gitara", 7000, "Gitara")
            };

            var market = new Market();

            var bank = new CentalBank(0);

            var sprzedawca1 = new Seller("Sprzedawca1", "Sprzedawca1", 0, 10);

            var towar = new List<Product>();
            towar.Add(new Product("Gitara", 3000, 20));
            towar.Add(new Product("Gitara", 3000, 30));
            towar.Add(new Product("Gitara", 3000, 40));


            market.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(kupujacy.ToArray());
            bank.SubscribeHuman(sprzedawca1);

            bank.SubscribeMarket(market);

            sprzedawca1.addBank(bank);
            sprzedawca1.joinToMarket(market);
            sprzedawca1.addProducts(towar);

            Assert.Equal(25, kupujacy[0].inflation);

        }


    }
}
