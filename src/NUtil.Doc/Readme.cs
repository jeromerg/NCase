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
        public void PairwiseGenerator()
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
        public void LinqForEachExtensions()
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
        public void QuadraticExtensions()
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
        public void TriangularProductWithoutDiagonal()
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
    }

}