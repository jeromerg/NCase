using System;
using NCaseFramework.Front.Ui;
using NDsl.Front.Api;
using NUnit.Framework;
using NUtil.Doc;

namespace NCaseFramework.doc
{
    [TestFixture]
    public class Readme
    {
        // ReSharper disable once InconsistentNaming
        private readonly ConsoleRecorder Console = new ConsoleRecorder();

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            new DocUtil().UpdateSnippetsOfAssociatedDocumentation(Console);
        }

        //# TodoInterface
        public interface ITodo
        {
            string Task { get; set; }
            bool IsDone { get; set; }
            DateTime DueDate { get; set; }
        }
        //#

        [Test]
        public void AllCombinations()
        {
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            DateTime tomorrow = now.AddDays(+1);

            //# AllCombinations
            IBuilder builder = NCase.NewBuilder();
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

            Console.WriteLine("//# AllCombinationsConsole");
            Console.WriteLine(all.PrintCasesAsTable());
            //#
        }

        [Test]
        public void PairwiseCombinations()
        {
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            DateTime tomorrow = now.AddDays(+1);

            //# PairwiseCombinations
            IBuilder builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");

            var allPairs = builder.NewDefinition<PairwiseCombinations>("allPairs");
            using (allPairs.Define())
            {
                todo.Task = "Don't forget to forget this";
                todo.Task = "";
                todo.Task = "@()/&%$§ ß üäö ÖÄÜ éè";
                todo.Task = "@(SELECT * FROM USERS)";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;

                todo.IsDone = false;
                todo.IsDone = true;
            }

            Console.WriteLine(allPairs.PrintCasesAsTable());
            //#
        }

        [Test]
        public void Tree()
        {
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            DateTime tomorrow = now.AddDays(+1);

            //# Tree
            IBuilder builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");

            var tree = builder.NewDefinition<Tree>("tree");
            using (tree.Define())
            {
                todo.Task = "Don't forget to forget";
                    todo.DueDate = yesterday;
                        todo.IsDone = true;
                        todo.IsDone = false;
                    todo.DueDate = now;
                        todo.IsDone = true;
                    todo.DueDate = tomorrow;
                        todo.IsDone = false;
            }
            //#
        }
    }
}