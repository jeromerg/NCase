﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NCase.Front.Ui;
using NUnit.Framework;

namespace NCase.Test
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
        public void NoTest_ExtractBlocks()
        {
            ConsoleAndBlockExtractor.DumpAllExtractOfThisFile();
        }

        [Test]
        public void SimpleTreeTest()
        {
            //BeginExtract SimpleTree
            IBuilder builder = CaseBuilder.Create();
            var t = builder.CreateContributor<ITransfer>("t");

            var transfers = builder.CreateTree("transfers");
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
            //EndExtract

            ConsoleAndBlockExtractor.WriteLine("CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
            ConsoleAndBlockExtractor.WriteLine("---------|-------------|--------|---------");
            foreach (ICase cas in transfers.Cases().Replay())
            {
                ConsoleAndBlockExtractor.WriteLine(string.Format("{0,8} | {1,11:000.00} | {2,6:000.00} | {3,-8}",
                                  t.Currency,
                                  t.BalanceUsd,
                                  t.Amount,
                                  t.Accepted));
            }
        }

        [Test]
        public void SimpleCartesianProductTest()
        {
            IBuilder builder = CaseBuilder.Create();
            var t = builder.CreateContributor<ITransfer>("t");

            var cardsAndBanks = builder.CreateProd("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            ConsoleAndBlockExtractor.WriteLine("DEST_BANK     | CARD       ");
            ConsoleAndBlockExtractor.WriteLine("--------------|------------");
            foreach (ICase cas in cardsAndBanks.Cases().Replay())
            {
                ConsoleAndBlockExtractor.WriteLine(string.Format("{0,-13} | {1,-10}", t.DestBank, t.Card));
            }
        }

        [Test]
        public void ReferenceTest()
        {
            IBuilder builder = CaseBuilder.Create();
            var t = builder.CreateContributor<ITransfer>("t");

            var transfers = builder.CreateTree("transfers");
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

            var cardsAndBanks = builder.CreateProd("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            var transfersForAllcardsAndBanks = builder.CreateProd("transferForAllcardsAndBanks");
            using (transfersForAllcardsAndBanks.Define())
            {
                transfers.Ref();
                cardsAndBanks.Ref();
            }

            ConsoleAndBlockExtractor.WriteLine("DEST_BANK     | CARD       | CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
            ConsoleAndBlockExtractor.WriteLine("--------------|------------|----------|-------------|--------|---------");
            foreach (ICase cas in transfersForAllcardsAndBanks.Cases().Replay())
            {
                ConsoleAndBlockExtractor.WriteLine(string.Format("{0,-13} | {1,-10} | {2,8} | {3,11:000.00} | {4,6:000.00} | {5,-8}",
                                  t.DestBank,
                                  t.Card,
                                  t.Currency,
                                  t.BalanceUsd,
                                  t.Amount,
                                  t.Accepted));
            }
        }

        [Test]
        public void PrintDef()
        {
            IProd prod = GetTipicalMixOfTreeAndProd();

            WriteLine("# DEFAULT OPTIONS: prod.PrintDef()");
            ConsoleAndBlockExtractor.WriteLine(prod.PrintDef());

            WriteLine("# OPTION prod.PrintDef(isRecursive : true)");
            ConsoleAndBlockExtractor.WriteLine(prod.PrintDef(isRecursive: true));

            WriteLine("# OPTION: prod.PrintDef(includeFileInfo : true)");
            ConsoleAndBlockExtractor.WriteLine(prod.PrintDef(true));

            WriteLine("# OPTION: prod.PrintDef(isRecursive : true, includeFileInfo : true)");
            ConsoleAndBlockExtractor.WriteLine(prod.PrintDef(isRecursive: true, isFileInfo: true));
        }

        [Test]
        public void PrintTable()
        {
            ITree transfers = GetTypicalTreeWithReferences();

            WriteLine("# DEFAULT OPTION: transfers.PrintTable()");
            ConsoleAndBlockExtractor.WriteLine(transfers.PrintTable());

            WriteLine("# OPTION: transfers.PrintTable(isRecursive:true)");
            ConsoleAndBlockExtractor.WriteLine(transfers.PrintTable(isRecursive:true));
        }

        #region Helpers

        private static ITree GetTypicalTreeWithReferences()
        {
            IBuilder builder = CaseBuilder.Create();
            var t = builder.CreateContributor<ITransfer>("t");

            var cardsAndBanks = builder.CreateProd("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            var transfers = builder.CreateTree("transfers");
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

            var transfers = builder.CreateTree("transfers");
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

            var cardsAndBanks = builder.CreateProd("cardsAndBank");
            using (cardsAndBanks.Define())
            {
                t.DestBank = "HSBC";
                t.DestBank = "Unicredit";
                t.DestBank = "Bank of China";

                t.Card = Card.Visa;
                t.Card = Card.Mastercard;
                t.Card = Card.Maestro;
            }

            var testCasesDef = builder.CreateProd("transferForAllcardsAndBanks");
            using (testCasesDef.Define())
            {
                transfers.Ref();
                cardsAndBanks.Ref();
            }

            return testCasesDef;
        }
        #endregion
               
        private static void WriteLine(string txt, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null)
        {
            // ReSharper disable ExplicitCallerInfoArgument
            ConsoleAndBlockExtractor.WriteLine(txt, filePath, memberName);
            // ReSharper restore ExplicitCallerInfoArgument
        }
    }
}