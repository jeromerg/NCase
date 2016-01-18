using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using NDocUtil;
using NUnit.Framework;
using NUtil.Linq;
using NUtil.Math.Combinatorics.Pairwise;

namespace NUtil.Doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    [SuppressMessage("ReSharper", "RedundantExplicitArrayCreation")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public class Readme
    {
        // ReSharper disable once InconsistentNaming
        [NotNull] private readonly DocUtil docu = new DocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void Doc_PairwiseGenerator()
        {
            int i = 0;

            docu.BeginRecordConsole("PairwiseGenerator_Console");
            //# PairwiseGenerator
            var setCardinals = new int[] {3, 2, 2};
 
            IEnumerable<int[]> tuples = new PairwiseGenerator().Generate(setCardinals);
            
            foreach (int[] t in tuples)
                Console.WriteLine("Tuple #{0}:  {1}, {2}, {3}", i++, t[0], t[1], t[2]);
            //#
            docu.StopRecordConsole();

        }

        void SendToServer(int i) { }
        
        [Test]
        public void Doc_LinqForEachExtensions()
        {
            {
                docu.BeginRecordConsole("LinqForEachExtensions1_Console");
                //# LinqForEachExtensions1
                var set = Enumerable.Range(0, 10);
            
                set.ForEach(v => Console.Write(v));
                
                //#
                docu.StopRecordConsole();
            }

            Console.WriteLine();

            {
                docu.BeginRecordConsole("LinqForEachExtensions2_Console");
                //# LinqForEachExtensions2
                var set = Enumerable.Range(0, 10);
                
                set.ForEach(v => Console.Write(v), () => Console.Write(", "));
                
                //#
                docu.StopRecordConsole();
            }

            Console.WriteLine();

            {
                //# LinqForEachExtensions3
                var set = Enumerable.Range(0, 10);
                
                set.ForEach(v => SendToServer(v), () => Thread.Sleep(10));
                
                //#
            }

        }

        [Test]
        public void Doc_QuadraticExtensions()
        {
            docu.BeginRecordConsole("QuadraticExtensions_Console");
            //# QuadraticExtensions
            var set1 = new [] {"a", "b"};
            var set2 = new [] {0, 1};

            var product = set1.CartesianProduct(set2, (s1, s2) => new { s1, s2 });
            
            foreach (var pair in product)
                Console.WriteLine("({0}, {1})", pair.s1, pair.s2);
            //#
            docu.StopRecordConsole();

        }

        [Test]
        public void Doc_TriangularProductWithoutDiagonal()
        {
            docu.BeginRecordConsole("TriangularProductWithoutDiagonal_Console");
            //# TriangularProductWithoutDiagonal
            var set1 = new [] {0, 1, 2};

            var product = set1.TriangularProductWithoutDiagonal((s1, s2) => new { s1, s2 });
            
            foreach (var pair in product)
                Console.WriteLine("({0}, {1})", pair.s1, pair.s2);
            //#
            docu.StopRecordConsole();
        }

        [Test]
        public void Doc_CascadeExtensions()
        {

            //# CascadeExtensions_Def
            var stats = new Dictionary<string,                // Country
                                Dictionary<string,            // City
                                        HashSet<int>>>();     // Year of Visit
            //#

            {
                //# CascadeExtensions_CascadeAdd
                stats.CascadeAdd("FR").CascadeAdd("Paris").Add(2010);
                stats.CascadeAdd("FR").CascadeAdd("Paris").Add(2011);
                stats.CascadeAdd("DE").CascadeAdd("Mainz").Add(2013);
                //#
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_Indexer_Console");
                //# CascadeExtensions_Indexer
                var years = stats["FR"]["Paris"];

                Console.WriteLine("years = {0}", string.Join(", ", years));
                //#
                docu.StopRecordConsole();
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_CascadeGetOrDefault_Console");
                //# CascadeExtensions_CascadeGetOrDefault
                var yearsSafe1  = stats.CascadeGetOrDefault("FR")
                                       .CascadeGetOrDefault("Paris");

                Console.WriteLine("yearsSafe1 = {0}", string.Join(", ", yearsSafe1));

                var yearsSafe2 = stats.CascadeGetOrDefault("FR")
                                      .CascadeGetOrDefault("Lyon");

                Console.WriteLine("yearsSafe2 is null? {0} (no exception)", yearsSafe2 == null ? "true" : "false");
                //#
                docu.StopRecordConsole();
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_CascadeTryFirst_Console");
                //# CascadeExtensions_CascadeTryFirst
                string country,city;
                int year;
                bool ok = stats.CascadeTryFirst(out country)
                               .CascadeTryFirst(out city)
                               .CascadeTryFirst(out year);

                Console.WriteLine("CascadeTryFirst: ok={0}, country={1}, city={2}, year={3}", 
                                  ok, country, city, year);
                //#
                docu.StopRecordConsole();
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_CascadeRemove_Console");
                //# CascadeExtensions_CascadeRemove
                bool isRemoved1 = stats
                    .CascadeRemove("FR")
                    .CascadeRemove("Paris")
                    .CascadeRemove(2011);

                Console.WriteLine("isRemoved1= {0}", isRemoved1);

                bool isRemoved2 = stats
                    .CascadeRemove("FR")
                    .CascadeRemove("Paris")
                    .CascadeRemove(2052);

                Console.WriteLine("isRemoved2= {0}", isRemoved2);
                //#
                docu.StopRecordConsole();
            }
        }
    }

}