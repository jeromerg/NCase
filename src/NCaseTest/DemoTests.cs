using System;
using System.Diagnostics.CodeAnalysis;
using NCase.Front.Ui;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseTest
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DemoTests
    {
        public enum Curr
        {
            EUR,
            USD,
            YEN
        }

        public enum Card
        {
            Visa,
            Mastercard,
            Maestro
        }

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
            IBuilder builder = NCase.NCase.CreateBuilder();
            var t = builder.CreateContributor<ITransfer>("t");

            var transfers = builder.CreateDef<ITree>("transfers");
            using (transfers.Define())
            {
                t.Currency = Curr.USD;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                        t.Amount = 100.00;
                            t.Accepted = true;
                        t.Amount = 100.01;
                            t.Accepted = false;
                    t.BalanceUsd = 0.00;
                        t.Amount = 0.01;
                            t.Accepted = false;
                t.Currency = Curr.YEN;
                    t.BalanceUsd = 0.00;
                        t.Amount = 0.01;
                            t.Accepted = false;
                t.Currency = Curr.EUR;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                        t.Amount = 89.39;
                            t.Accepted = true;
                        t.Amount = 89.40;
                            t.Accepted = false;
            }

            Console.WriteLine("CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
            Console.WriteLine("---------|-------------|--------|---------");
            foreach (ICase cas in transfers.Cases().Replay())
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
            IBuilder builder = NCase.NCase.CreateBuilder();
            var t = builder.CreateContributor<ITransfer>("t");

            var cardsAndBanks = builder.CreateDef<IProd>("cardsAndBank");
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
            foreach (ICase cas in cardsAndBanks.Cases().Replay())
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
            IBuilder builder = NCase.NCase.CreateBuilder();
            var t = builder.CreateContributor<ITransfer>("t");

            var transfers = builder.CreateDef<ITree>("transfers");
            using (transfers.Define())
            {
                t.Currency = Curr.USD;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                        t.Amount = 100.00;
                            t.Accepted = true;
                        t.Amount = 100.01;
                            t.Accepted = false;
                    t.BalanceUsd = 0.00;
                        t.Amount = 0.01;
                            t.Accepted = false;
                t.Currency = Curr.YEN;
                    t.BalanceUsd = 0.00;
                        t.Amount = 0.01;
                            t.Accepted = false;
                t.Currency = Curr.EUR;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                        t.Amount = 89.39;
                            t.Accepted = true;
                        t.Amount = 89.40;
                            t.Accepted = false;
            }

            var cardsAndBanks = builder.CreateDef<IProd>("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            var transfersForAllcardsAndBanks = builder.CreateDef<IProd>("transferForAllcardsAndBanks");
            using (transfersForAllcardsAndBanks.Define())
            {
                transfers.Ref();
                cardsAndBanks.Ref();
            }

            Console.WriteLine("DEST_BANK     | CARD       | CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
            Console.WriteLine("--------------|------------|----------|-------------|--------|---------");
            foreach (ICase cas in transfersForAllcardsAndBanks.Cases().Replay())
            {
                Console.WriteLine("{0,-13} | {1,-10} | {2,8} | {3,11:000.00} | {4,6:000.00} | {5,-8}",
                                  t.DestBank,
                                  t.Card,
                                  t.Currency,
                                  t.BalanceUsd,
                                  t.Amount,
                                  t.Accepted);
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

        [Test]
        public void PrintDefinitionTest()
        {
            var testCasesDef = GetTipicalMixOfTreeAndProd();

            string defString = testCasesDef.PrintDefinition();
            Console.WriteLine(defString);

            WriteTitle("WITH OPTION IsRecursive");
            string defString2 = testCasesDef.PrintDefinition(isRecursive : true);
            Console.WriteLine(defString2);

            WriteTitle("WITH OPTION IncludeFileInfo");
            string defString3 = testCasesDef.PrintDefinition(includeFileInfo : true);
            Console.WriteLine(defString3);

            WriteTitle("WITH OPTIONS IsRecursive and IncludeFileInfo");
            string defString4 = testCasesDef.PrintDefinition(isRecursive : true, includeFileInfo : true);
            Console.WriteLine(defString4);
        }

        [Test]
        public void PrintCaseTableTest()
        {
            var testCasesDef = GetTipicalMixOfTreeAndProd();
            string defString = testCasesDef.PrintTable(isRecursive: true);
            Console.WriteLine(defString);
        }

        [Test]
        public void PrintCaseTableTest_NonRecursiveCases()
        {
            IBuilder builder = NCase.NCase.CreateBuilder();
            var t = builder.CreateContributor<ITransfer>("t");

            var cardsAndBanks = builder.CreateDef<IProd>("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            var transfers = builder.CreateDef<ITree>("transfers");
            using (transfers.Define())
            {
                t.Currency = Curr.USD;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                                cardsAndBanks.Ref();
                        t.Amount = 100.00;
                            t.Accepted = true;
                        t.Amount = 100.01;
                            t.Accepted = false;
                t.Currency = Curr.EUR;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                        t.Amount = 89.40;
                            t.Accepted = false;
                                cardsAndBanks.Ref();
            }


            WriteTitle("WITH DEFAULT OPTIONS");
            Console.WriteLine(transfers.PrintTable(isRecursive:false));

            WriteTitle("WITH OPTIONS IsRecursive");
            Console.WriteLine(transfers.PrintTable(isRecursive:true));
        }

        private static void WriteTitle(string title)
        {
            int totalLength = 64;
            var centeredString = title.PadLeft(((totalLength - title.Length) / 2) + title.Length).PadRight(totalLength);

            Console.WriteLine();
            Console.WriteLine(new string('-', totalLength + 4));
            Console.WriteLine(string.Format("--{0}--", centeredString));
            Console.WriteLine(new string('-', totalLength + 4));
            Console.WriteLine();
        }

        private IProd GetTipicalMixOfTreeAndProd()
        {
            IBuilder builder = NCase.NCase.CreateBuilder();
            var t = builder.CreateContributor<ITransfer>("t");

            var transfers = builder.CreateDef<ITree>("transfers");
            using (transfers.Define())
            {
                t.Currency = Curr.USD;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                        t.Amount = 100.00;
                            t.Accepted = true;
                        t.Amount = 100.01;
                            t.Accepted = false;
                    t.BalanceUsd = 0.00;
                        t.Amount = 0.01;
                            t.Accepted = false;
                t.Currency = Curr.YEN;
                    t.BalanceUsd = 0.00;
                        t.Amount = 0.01;
                            t.Accepted = false;
                t.Currency = Curr.EUR;
                    t.BalanceUsd = 100.00;
                        t.Amount = 0.01;
                            t.Accepted = true;
                        t.Amount = 89.39;
                            t.Accepted = true;
                        t.Amount = 89.40;
                            t.Accepted = false;
            }

            var cardsAndBanks = builder.CreateDef<IProd>("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            var testCasesDef = builder.CreateDef<IProd>("transferForAllcardsAndBanks");
            using (testCasesDef.Define())
            {
                transfers.Ref();
                cardsAndBanks.Ref();
            }

            return testCasesDef;
        }

    }
}