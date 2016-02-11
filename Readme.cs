using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NDocUtilLibrary;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Doc
{
    using Shared;
    // remark: must remain here, in order to redirect NCase calls to the doc-specific implementation
    
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class Readme
    {
        // ReSharper disable once InconsistentNaming
        [NotNull] private static readonly NDocUtil docu = new NDocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void DemoTest()
        {
            Main();
        }
 
        //# FIRST_UNIT_TEST
        public interface IVar
        {
            int X { get; set; }
            int Y { get; set; }
        }

        public static void Main()
        {
            var builder = NCase.NewBuilder();
            var v = builder.NewContributor<IVar>("v");
            var mySet = builder.NewCombinationSet("c");

            using (mySet.Define())
            {
                v.X = 0;
                v.X = 1;

                v.Y = 0;
                v.Y = 1;
            }

            string table = mySet.PrintCasesAsTable();

            docu.BeginRecordConsole("FIRST_UNIT_TEST_CONSOLE");
            Console.WriteLine(table);
            docu.StopRecordConsole();
        }
        //#

        [Test]
        public void DemoTest2()
        {
            var builder = NCase.NewBuilder();
            var v = builder.NewContributor<IVar>("v");
            var mySet = builder.NewCombinationSet("c");

            using (mySet.Define())
            {
                v.X = 0;
                v.X = 1;

                v.Y = 0;
                v.Y = 1;
            }

            docu.BeginRecordConsole("FIRST_UNIT_TEST_2_CONSOLE");
            //# FIRST_UNIT_TEST_2
            foreach (Case c in mySet.Cases().Replay())
                Console.WriteLine("{0} ^ {1} = {2}", v.X, v.Y, v.X ^ v.Y);
            //#
            docu.StopRecordConsole();
        }
 
   }

}