using System;
using NCase;
using NCase.Api;
using NUnit.Framework;

namespace NCaseTest
{
    [TestFixture]
    public class DemoTests
    {
        public enum Curr { EUR, USD, YEN }
        public enum Card { Visa, Mastercard, Maestro }
        public interface ITransfer
        {
            Curr Currency { get; set; }
            double Amount { get; set; }
            double BalanceUsd { get; set; }
            bool Accepted { get; set; }

            string DestBank { get; set; }
            Card Card { get; set; }
        }

        [Test]
        public void SimpleTreeTest()
        {
            IBuilder builder = Case.CreateBuilder();
            ITransfer t = builder.CreateContributor<ITransfer>("t");

            ITree transfers = builder.CreateSet<ITree>("transfers");
            using (transfers.Define())
            {
                t.Currency = Curr.USD;
                    t.BalanceUsd = 100.00;
                        t.Amount =   0.01;  t.Accepted = true;
                        t.Amount = 100.00;  t.Accepted = true;
                        t.Amount = 100.01;  t.Accepted = false;
                    t.BalanceUsd =   0.00;  
                        t.Amount =   0.01;  t.Accepted = false;
                t.Currency = Curr.YEN;      
                    t.BalanceUsd =   0.00;  
                        t.Amount =   0.01;  t.Accepted = false;
                t.Currency = Curr.EUR;      
                    t.BalanceUsd = 100.00;  
                        t.Amount =   0.01;  t.Accepted = true;
                        t.Amount =  89.39;  t.Accepted = true;
                        t.Amount =  89.40;  t.Accepted = false;
            }

            Console.WriteLine("CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
            Console.WriteLine("---------|-------------|--------|---------");
            foreach (var cas in transfers.Cases.Replay())
            {
                Console.WriteLine("{0,8} | {1,11:000.00} | {2,6:000.00} | {3,-8}",
                    t.Currency,
                    t.BalanceUsd,
                    t.Amount,
                    t.Accepted);
            }

            // Console Output: 
            // CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED
            // ---------|-------------|--------|---------
            //      USD |      100,00 | 000,01 | True    
            //      USD |      100,00 | 100,00 | True    
            //      USD |      100,00 | 100,01 | False   
            //      USD |      000,00 | 000,01 | False   
            //      YEN |      000,00 | 000,01 | False   
            //      EUR |      100,00 | 000,01 | True    
            //      EUR |      100,00 | 089,39 | True    
            //      EUR |      100,00 | 089,40 | False           
        }

        [Test]
        public void SimpleCartesianProductTest()
        {
            IBuilder builder = Case.CreateBuilder();
            ITransfer t = builder.CreateContributor<ITransfer>("t");

            IProd cardsAndBanks = builder.CreateSet<IProd>("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            Console.WriteLine("DEST_BANK     | CARD       ");
            Console.WriteLine("--------------|------------");
            foreach (var cas in cardsAndBanks.Cases.Replay())
            {
                Console.WriteLine("{0,-13} | {1,-10}", t.DestBank, t.Card);
            }
            // Console Output:
            // DEST_BANK     | CARD       
            // --------------|------------
            // HSBC          | Visa      
            // HSBC          | Mastercard
            // HSBC          | Maestro   
            // Unicredit     | Visa      
            // Unicredit     | Mastercard
            // Unicredit     | Maestro   
            // Bank of China | Visa      
            // Bank of China | Mastercard
            // Bank of China | Maestro   
        }

        [Test]
        public void ReferenceTest()
        {
            IBuilder builder = Case.CreateBuilder();
            ITransfer t = builder.CreateContributor<ITransfer>("t");

            ITree transfers = builder.CreateSet<ITree>("transfers");
            using (transfers.Define())
            {
                t.Currency = Curr.USD;
                    t.BalanceUsd = 100.00;
                        t.Amount =   0.01;  t.Accepted = true;
                        t.Amount = 100.00;  t.Accepted = true;
                        t.Amount = 100.01;  t.Accepted = false;
                    t.BalanceUsd =   0.00;  
                        t.Amount =   0.01;  t.Accepted = false;
                t.Currency = Curr.YEN;      
                    t.BalanceUsd =   0.00;  
                        t.Amount =   0.01;  t.Accepted = false;
                t.Currency = Curr.EUR;      
                    t.BalanceUsd = 100.00;  
                        t.Amount =   0.01;  t.Accepted = true;
                        t.Amount =  89.39;  t.Accepted = true;
                        t.Amount =  89.40;  t.Accepted = false;
            }

            IProd cardsAndBanks = builder.CreateSet<IProd>("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            IProd transfersForAllcardsAndBanks = builder.CreateSet<IProd>("transferForAllcardsAndBanks");
            using (transfersForAllcardsAndBanks.Define())
            {
                transfers.Ref();
                cardsAndBanks.Ref();
            }

            Console.WriteLine("DEST_BANK     | CARD       | CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
            Console.WriteLine("--------------|------------|----------|-------------|--------|---------");
            foreach (var cas in transfersForAllcardsAndBanks.Cases.Replay())
            {
                Console.WriteLine("{0,-13} | {1,-10} | {2,8} | {3,11:000.00} | {4,6:000.00} | {5,-8}",
                    t.DestBank, t.Card, t.Currency, t.BalanceUsd, t.Amount, t.Accepted);
            }

            // Console Output:
            //
            // DEST_BANK     | CARD       | CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED
            // --------------|------------|----------|-------------|--------|---------
            // HSBC          | Visa       |      USD |      100,00 | 000,01 | True    
            // HSBC          | Mastercard |      USD |      100,00 | 000,01 | True    
            // HSBC          | Maestro    |      USD |      100,00 | 000,01 | True    
            // Unicredit     | Visa       |      USD |      100,00 | 000,01 | True    
            // Unicredit     | Mastercard |      USD |      100,00 | 000,01 | True    
            // Unicredit     | Maestro    |      USD |      100,00 | 000,01 | True    
            // Bank of China | Visa       |      USD |      100,00 | 000,01 | True    
            // Bank of China | Mastercard |      USD |      100,00 | 000,01 | True    
            // Bank of China | Maestro    |      USD |      100,00 | 000,01 | True    
            // HSBC          | Visa       |      USD |      100,00 | 100,00 | True    
            // HSBC          | Mastercard |      USD |      100,00 | 100,00 | True    
            // HSBC          | Maestro    |      USD |      100,00 | 100,00 | True    
            // Unicredit     | Visa       |      USD |      100,00 | 100,00 | True    
            // Unicredit     | Mastercard |      USD |      100,00 | 100,00 | True    
            // Unicredit     | Maestro    |      USD |      100,00 | 100,00 | True    
            // Bank of China | Visa       |      USD |      100,00 | 100,00 | True    
            // Bank of China | Mastercard |      USD |      100,00 | 100,00 | True    
            // Bank of China | Maestro    |      USD |      100,00 | 100,00 | True    
            // HSBC          | Visa       |      USD |      100,00 | 100,01 | False   
            // HSBC          | Mastercard |      USD |      100,00 | 100,01 | False   
            // HSBC          | Maestro    |      USD |      100,00 | 100,01 | False   
            // Unicredit     | Visa       |      USD |      100,00 | 100,01 | False   
            // Unicredit     | Mastercard |      USD |      100,00 | 100,01 | False   
            // Unicredit     | Maestro    |      USD |      100,00 | 100,01 | False   
            // Bank of China | Visa       |      USD |      100,00 | 100,01 | False   
            // Bank of China | Mastercard |      USD |      100,00 | 100,01 | False   
            // Bank of China | Maestro    |      USD |      100,00 | 100,01 | False   
            // HSBC          | Visa       |      USD |      000,00 | 000,01 | False   
            // HSBC          | Mastercard |      USD |      000,00 | 000,01 | False   
            // HSBC          | Maestro    |      USD |      000,00 | 000,01 | False   
            // Unicredit     | Visa       |      USD |      000,00 | 000,01 | False   
            // Unicredit     | Mastercard |      USD |      000,00 | 000,01 | False   
            // Unicredit     | Maestro    |      USD |      000,00 | 000,01 | False   
            // Bank of China | Visa       |      USD |      000,00 | 000,01 | False   
            // Bank of China | Mastercard |      USD |      000,00 | 000,01 | False   
            // Bank of China | Maestro    |      USD |      000,00 | 000,01 | False   
            // HSBC          | Visa       |      YEN |      000,00 | 000,01 | False   
            // HSBC          | Mastercard |      YEN |      000,00 | 000,01 | False   
            // HSBC          | Maestro    |      YEN |      000,00 | 000,01 | False   
            // Unicredit     | Visa       |      YEN |      000,00 | 000,01 | False   
            // Unicredit     | Mastercard |      YEN |      000,00 | 000,01 | False   
            // Unicredit     | Maestro    |      YEN |      000,00 | 000,01 | False   
            // Bank of China | Visa       |      YEN |      000,00 | 000,01 | False   
            // Bank of China | Mastercard |      YEN |      000,00 | 000,01 | False   
            // Bank of China | Maestro    |      YEN |      000,00 | 000,01 | False   
            // HSBC          | Visa       |      EUR |      100,00 | 000,01 | True    
            // HSBC          | Mastercard |      EUR |      100,00 | 000,01 | True    
            // HSBC          | Maestro    |      EUR |      100,00 | 000,01 | True    
            // Unicredit     | Visa       |      EUR |      100,00 | 000,01 | True    
            // Unicredit     | Mastercard |      EUR |      100,00 | 000,01 | True    
            // Unicredit     | Maestro    |      EUR |      100,00 | 000,01 | True    
            // Bank of China | Visa       |      EUR |      100,00 | 000,01 | True    
            // Bank of China | Mastercard |      EUR |      100,00 | 000,01 | True    
            // Bank of China | Maestro    |      EUR |      100,00 | 000,01 | True    
            // HSBC          | Visa       |      EUR |      100,00 | 089,39 | True    
            // HSBC          | Mastercard |      EUR |      100,00 | 089,39 | True    
            // HSBC          | Maestro    |      EUR |      100,00 | 089,39 | True    
            // Unicredit     | Visa       |      EUR |      100,00 | 089,39 | True    
            // Unicredit     | Mastercard |      EUR |      100,00 | 089,39 | True    
            // Unicredit     | Maestro    |      EUR |      100,00 | 089,39 | True    
            // Bank of China | Visa       |      EUR |      100,00 | 089,39 | True    
            // Bank of China | Mastercard |      EUR |      100,00 | 089,39 | True    
            // Bank of China | Maestro    |      EUR |      100,00 | 089,39 | True    
            // HSBC          | Visa       |      EUR |      100,00 | 089,40 | False   
            // HSBC          | Mastercard |      EUR |      100,00 | 089,40 | False   
            // HSBC          | Maestro    |      EUR |      100,00 | 089,40 | False   
            // Unicredit     | Visa       |      EUR |      100,00 | 089,40 | False   
            // Unicredit     | Mastercard |      EUR |      100,00 | 089,40 | False   
            // Unicredit     | Maestro    |      EUR |      100,00 | 089,40 | False   
            // Bank of China | Visa       |      EUR |      100,00 | 089,40 | False   
            // Bank of China | Mastercard |      EUR |      100,00 | 089,40 | False   
            // Bank of China | Maestro    |      EUR |      100,00 | 089,40 | False   

        }

    }
}

