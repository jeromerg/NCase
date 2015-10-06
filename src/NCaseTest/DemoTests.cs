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
            IBuilder builder = CaseBuilder.Create();
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
        }

        [Test]
        public void SimpleCartesianProductTest()
        {
            IBuilder builder = CaseBuilder.Create();
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
        }

        [Test]
        public void ReferenceTest()
        {
            IBuilder builder = CaseBuilder.Create();
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
        }

        [Test]
        public void PrintDefinition()
        {
            IProd prod = GetTipicalMixOfTreeAndProd();

            WriteTitle("DEFAULT OPTIONS: prod.PrintDefinition()");
            Console.WriteLine(prod.PrintDefinition());

            WriteTitle("OPTION prod.PrintDefinition(isRecursive : true)");
            Console.WriteLine(prod.PrintDefinition(isRecursive: true));

            WriteTitle("OPTION: prod.PrintDefinition(includeFileInfo : true)");
            Console.WriteLine(prod.PrintDefinition(true));

            WriteTitle("OPTION: prod.PrintDefinition(isRecursive : true, includeFileInfo : true)");
            Console.WriteLine(prod.PrintDefinition(isRecursive: true, includeFileInfo: true));
        }

        [Test]
        public void PrintTable()
        {
            ITree transfers = GetTypicalTreeWithReferences();

            WriteTitle("DEFAULT OPTION: transfers.PrintTable(isRecursive:false)");
            Console.WriteLine(transfers.PrintTable(isRecursive:false));

            WriteTitle("OPTION: transfers.PrintTable(isRecursive:true)");
            Console.WriteLine(transfers.PrintTable(isRecursive:true));
        }

        #region Helpers

        private static ITree GetTypicalTreeWithReferences()
        {
            IBuilder builder = CaseBuilder.Create();
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
            return transfers;
        }

        private static IProd GetTipicalMixOfTreeAndProd()
        {
            IBuilder builder = CaseBuilder.Create();
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

        private static void WriteTitle(string title)
        {
            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine(new string('=', title.Length));
            Console.WriteLine();
        }

        #endregion
    }
}