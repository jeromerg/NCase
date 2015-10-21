using System;
using NCaseFramework.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Test
{
    [TestFixture]
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

            //BeginExtract AllCombinations
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");

            var all = builder.NewDefinition<AllCombinations>("all");
            using (all.Define())
            {
                todo.Task = "Don't forget to forget";
                todo.Task = "";
                todo.Task = "@()/&%$§ ß üäö ÖÄÜ éè";
                todo.Task = "@(SELECT * FROM USERS)";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;
                
                todo.IsDone = false;
                todo.IsDone = true;
            }

            Console.WriteLine(all.PrintCasesAsTable());
            //EndExtract
        }

        [Test]
        public void PairwiseCombinations()
        {            
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            DateTime tomorrow = now.AddDays(+1);

            //BeginExtract AllCombinations
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");

            var all = builder.NewDefinition<PairwiseCombinations>("all");
            using (all.Define())
            {
                todo.Task = "Don't forget to forget";
                todo.Task = "";
                todo.Task = "@()/&%$§ ß üäö ÖÄÜ éè";
                todo.Task = "@(SELECT * FROM USERS)";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;
                
                todo.IsDone = false;
                todo.IsDone = true;
            }

            Console.WriteLine(all.PrintCasesAsTable());
            //EndExtract
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