using System;
using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Test
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DemoForReadMePage
    {
        [Test]
        public void ExtractSnippets()
        {
            new ConsoleAndBlockExtractor().DumpAllExtractOfThisFile();
        }

        //BeginExtract TodoInterface
        public interface ITodo
        {
            string Task { get; set; }
            bool IsDone { get; set; }
            DateTime DueDate { get; set; }
        }
        //EndExtract

        [Test]
        public void AllCombinations()
        {            
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            DateTime tomorrow = now.AddDays(+1);

            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");

            var pairwise = builder.NewDefinition<PairwiseCombinations>("pairwise");
            using (pairwise.Define())
            {
                todo.Task = "Don't forget to forget";
                todo.Task = "";
                todo.Task = "@()/&%$§ ß üäö ÖÄÜ 中华";
                todo.Task = "@(SELECT * FROM USERS)";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;
                
                todo.IsDone = false;
                todo.IsDone = true;
            }
        }

        [Test]
        public void Tree()
        {            
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            DateTime tomorrow = now.AddDays(+1);

            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");

            Tree tree = builder.NewDefinition<Tree>("tree");
            using (tree.Define())
            {
                todo.Task = "Don't forget to forget";
                    todo.DueDate = yesterday;
                        todo.IsDone = true;

            }
        }

    }
}