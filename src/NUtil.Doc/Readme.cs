using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using NDocUtilLibrary;
using NUnit.Framework;
using NUtil.Linq;
using NUtil.Math.Combinatorics.Pairwise;
using NUtil.Text;

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
        [NotNull] private readonly NDocUtil docu = new NDocUtil("docu");

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
                
                set.ForEach( v => Console.Write(v), 
                            () => Console.Write(", "));                
                //#
                docu.StopRecordConsole();
            }

            Console.WriteLine();

            {
                //# LinqForEachExtensions3
                var set = Enumerable.Range(0, 10);
                
                set.ForEach( v => SendToServer(v), 
                            () => Thread.Sleep(10));                
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
            var model = new Dictionary<string,                // Country
                                Dictionary<string,            // City
                                        HashSet<string>>>();  // Street
            //#

            {
                //# CascadeExtensions_CascadeAdd
                model.CascadeAdd("FR").CascadeAdd("Paris").Add("Rue de la paix");
                model.CascadeAdd("FR").CascadeAdd("Paris").Add("Rue de Paradis");
                model.CascadeAdd("DE").CascadeAdd("Mainz").Add("Gutenbergplatz");
                //#
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_Indexer_Console");
                //# CascadeExtensions_Indexer
                var streets1 = model["FR"]["Paris"];

                Console.WriteLine("Street in Paris: {0}", string.Join(", ", streets1));

                try
                {
                    var streets2 = model["Switzerland"]["Lausanne"]; // UNREGISTERED COUNTRY!
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine("Streets in Lausanne: KeyNotFoundException has been thrown");
                }
                //#
                docu.StopRecordConsole();
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_CascadeGetOrDefault_Console");
                //# CascadeExtensions_CascadeGetOrDefault
                var safeStreets1  = model.CascadeGetOrDefault("FR")
                                         .CascadeGetOrDefault("Paris");

                Console.WriteLine("Streets in Paris : {0}", string.Join(", ", safeStreets1));

                var safeStreets2 = model.CascadeGetOrDefault("Switzerland") // UNREGISTERED COUNTRY!
                                        .CascadeGetOrDefault("Lausanne"); 

                if(safeStreets2 == null)
                    Console.WriteLine("No street found in Lausanne");
                //#
                docu.StopRecordConsole();
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_CascadeTryFirst_Console");
                //# CascadeExtensions_CascadeTryFirst
                string country,city, street;
                bool ok = model.CascadeTryFirst(out country)
                               .CascadeTryFirst(out city)
                               .CascadeTryFirst(out street);

                Console.WriteLine("CascadeTryFirst: ok={0}, country={1}, city={2}, street={3}", 
                                  ok, country, city, street);
                //#
                docu.StopRecordConsole();
            }

            {
                docu.BeginRecordConsole("CascadeExtensions_CascadeRemove_Console");
                //# CascadeExtensions_CascadeRemove
                bool isRemoved1 = model
                    .CascadeRemove("FR")
                    .CascadeRemove("Paris")
                    .CascadeRemove("Rue de la paix");

                Console.WriteLine("isRemoved1= {0}", isRemoved1);

                bool isRemoved2 = model
                    .CascadeRemove("FR")
                    .CascadeRemove("Paris")
                    .CascadeRemove("Trafalgar Square");

                Console.WriteLine("isRemoved2= {0}", isRemoved2);
                //#
                docu.StopRecordConsole();
            }
        }

        [Test]
        public void Doc_TextExtensions()
        {
            {
                //# TextExtensions_Lines_JoinLines
                string txt = "one line\nand a second line";
                IEnumerable<string> lines = txt.Lines();
                string rejoinedLines = lines.JoinLines();
                //#
            }

            {
                docu.BeginRecordConsole("TextExtensions_Desindent_Console");
                //# TextExtensions_Desindent
                string txt = "    I was originally indented!";

                string desindentedTxt = txt.Desindent(tabIndentation:4);

                Console.WriteLine("Before: {0}\nAfter :{1}", txt, desindentedTxt);
                //#
                docu.StopRecordConsole();
            }            
        }

    }

}