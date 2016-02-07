
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Moq;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDsl.Front.Ui;
using NUnit.Framework;
using NDocUtilLibrary;
    
namespace NCaseFramework.Doc.intern
{    
    // remark: must remain here, in order to redirect NCase calls to the doc-specific implementation
    using Shared;
    
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ReadmeCaroussel
    {
        // ReSharper disable once InconsistentNaming
        [NotNull] private readonly NDocUtil docu = new NDocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.SaveSnippetsAsImage(ImageFormat.Emf, leftBorder:5, path:".");
            docu.SaveSnippetsAsRaw(path:".");
        }

        //# TodoInterface
        public interface ITodo
        {
            string Title { get; set; }
            DateTime DueDate { get; set; }
            bool IsDone { get; set; }
        }
        //#

        //# UserInterface
        public interface IUser
        {
            bool IsActive { get; set; }
            string Email { get; set; }
        }
        //#

        public class TodoManager
        {
            public bool AddTodo(ITodo todo)
            {
                mTodos.Add(todo);
                return true;
            }

            private readonly List<ITodo> mTodos = new List<ITodo>();
            public IEnumerable<ITodo> Todos
            {
                get { return mTodos; }
            }
        }

        DateTime now = new DateTime(2011, 11, 11, 0, 0, 0);
        DateTime yesterday = new DateTime(2011, 11, 10, 0, 0, 0);
        DateTime tomorrow = new DateTime(2011, 11, 12, 0, 0, 0);
        DateTime invalidLocalTime = new DateTime(2011, 11, 12, 0, 0, 0);
        DateTime ambiguousLocalTime = new DateTime(2011, 11, 12, 0, 0, 0);

        [Test]
        public void Slide1()
        {
            var builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            var todoSet = builder.NewCombinationSet("todoSet");

            //# Slide1
            using (todoSet.Define())
            {
                todo.Title = "Forget NCase";
                todo.Title = "Remember";
                todo.Title = "Love!";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;

                todo.IsDone = false;
                todo.IsDone = true;
            }
            //#

            docu.BeginRecordConsole("Slide1_Console");
            Console.WriteLine(todoSet.PrintCasesAsTable());
            docu.StopRecordConsole();
        }

        [Test]
        public void Slide2()
        {
            var builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            var todoSet = builder.NewCombinationSet("todoSet");

            //# Slide2
            using (todoSet.Define())
            {
                todo.IsDone = true;
                    todo.DueDate = yesterday;
                        todo.Title = "Forget NCase";
                        todo.Title = "Remember";
                        todo.Title = "Love!";
                    todo.DueDate = now;
                        todo.Title = "Remember";
                    todo.DueDate = tomorrow;
                        todo.Title = "Remember";
                todo.IsDone = false;
                    todo.Title = "Love!";
                        todo.DueDate = tomorrow;
            }
            //#

            docu.BeginRecordConsole("Slide2_Console");
            Console.WriteLine(todoSet.PrintCasesAsTable());
            docu.StopRecordConsole();
        }

        [Test]
        public void Slide3()
        {
            var builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            //# Slide3
            var todoSet = builder.NewCombinationSet("todoSet", onlyPairwise: true);
            //#

            //# Slide3_2
            using (todoSet.Define())
            {
                todo.Title = "Forget NCase";
                todo.Title = "Remember";
                todo.Title = "Love!";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;

                todo.IsDone = false;
                todo.IsDone = true;
            }
            //#

            docu.BeginRecordConsole("Slide3_Console");
            Console.WriteLine(todoSet.PrintCasesAsTable());
            docu.StopRecordConsole();
        }

        [Test]
        public void Slide4()
        {
            var builder = NCase.NewBuilder();


            var todoSet = builder.NewCombinationSet("todoSet");

            //# Slide4
            var todo = builder.NewContributor<ITodo>("todo");

            var user = builder.NewContributor<IUser>("user");

            using (todoSet.Define())
            {
                todo.Title = "Forget NCase";
                todo.Title = "Remember";

                todo.DueDate = now;
                todo.DueDate = tomorrow;

                user.Email = "some@email.com";
                
                user.IsActive = true;
                user.IsActive = false;
            }
            //#

            docu.BeginRecordConsole("Slide4_Console");
            Console.WriteLine(todoSet.PrintCasesAsTable());
            docu.StopRecordConsole();
        }

        [Test]
        public void Slide5()
        {
            var builder = NCase.NewBuilder();

            var todoSet = builder.NewCombinationSet("todoSet");
            var todo = builder.NewContributor<ITodo>("todo");
            using (todoSet.Define())
            {
                todo.Title = "Forget NCase";
                todo.Title = "Remember";
                todo.Title = "Love!";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;

                todo.IsDone = false;
                todo.IsDone = true;
            }

            var user = builder.NewContributor<IUser>("user");

            var allSet = builder.NewCombinationSet("allSet");

            //# Slide5
            using (allSet.Define())
            {
                todoSet.Ref();

                user.Email = "some@email.com";
                
                user.IsActive = true;
                user.IsActive = false;
            }
            //#

            docu.BeginRecordConsole("Slide5_Console");
            Console.WriteLine(allSet.PrintCasesAsTable());
            docu.StopRecordConsole();

            docu.BeginRecordConsole("Slide5_Console2");
            Console.WriteLine(allSet.PrintCasesAsTable(isRecursive: true));
            docu.StopRecordConsole();
        }

        [Test]
        public void Slide6()
        {
            // ARRANGE
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var todoSet = builder.NewCombinationSet("todoSet");

            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget NCase";
                todo.Title = "Another todo to never forget NCase";  // SINGLE ADDITION!!!

                todo.DueDate = now;

                todo.IsDone = false;
            }

            //# Slide6
            todoSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var tm = new TodoManager();
                bool ok = tm.AddTodo(todo);

                // ASSERT
                Assert.IsTrue(ok);
                Assert.AreEqual(1, tm.Todos.Count());
            });
            //#
        }

        [Test]
        public void Slide7_Conventional_Compact()
        {
            //# Slide7_Conventional_Compact
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Title = "Remember to forget";

            todo.DueDate = now;

            todo.IsDone = false;

            // ACT
            var tm = new TodoManager();
            bool ok = tm.AddTodo(todo);

            // ASSERT
            Assert.IsTrue(ok);
            Assert.AreEqual(1, tm.Todos.Count());
            //#
        }

        [Test]
        public void Slide7_Conventional()
        {
            //# Slide7_Conventional
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;



            todo.Title = "Remember to forget";

            todo.DueDate = now;

            todo.IsDone = false;

            


            // ACT
            var tm = new TodoManager();
            bool ok = tm.AddTodo(todo);

            // ASSERT
            Assert.IsTrue(ok);
            Assert.AreEqual(1, tm.Todos.Count());
            //#
        }

        //# Slide7_Conventional_ParametrizedSolution
        [Test, Combinatorial]
        public void ParametrizedTest(
            [Values("Remember to forget", 
                    "forget to remember", 
                    "and so on...", "all...", 
                    "and everything ...")] string title,
            [Values("yesterday", 
                    "now", 
                    "invalidLocalTime", 
                    "ambiguousLocalTime")] string dueDate,
            [Values(false, true)] bool isDone
            )
        {
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Title = title;

            // Conversion due to attribute restrictions
            todo.DueDate = ConvertDueDate(dueDate);
            
            todo.IsDone = isDone;
           
            // ACT
            var tm = new TodoManager();
            bool ok = tm.AddTodo(todo);

            // ASSERT
            Assert.IsTrue(ok);
            Assert.AreEqual(1, tm.Todos.Count());
        }
        //#

        private DateTime ConvertDueDate(string dueDate)
        {
            DateTime dueDateValue;
            switch (dueDate)
            {
                case "yesterday":
                    dueDateValue = yesterday;
                    break;
                case "now":
                    dueDateValue = now;
                    break;
                case "invalidLocalTime":
                    dueDateValue = invalidLocalTime;
                    break;
                case "ambiguousLocalTime":
                    dueDateValue = ambiguousLocalTime;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("dueDate");
            }
            return dueDateValue;
        }

        [Test]
        public void Slide7_NCase()
        {
            //# Slide7_NCase
            // ARRANGE
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var todoSet = builder.NewCombinationSet("todoSet");

            using (todoSet.Define())
            {
                todo.Title = "Remember to forget";

                todo.DueDate = now;

                todo.IsDone = false;
            }

            todoSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var tm = new TodoManager();
                bool ok = tm.AddTodo(todo);

                // ASSERT
                Assert.IsTrue(ok);
                Assert.AreEqual(1, tm.Todos.Count());
            });
            //#
        }

        [Test]
        public void Slide7_NCase2()
        {
            //# Slide7_NCase2
            // ARRANGE
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var todoSet = builder.NewCombinationSet("todoSet");

            using (todoSet.Define())
            {
                todo.Title = "Remember to forget";
                todo.Title = "forget to remember";
                todo.Title = "and so on...";
                todo.Title = "all...";
                todo.Title = "and everything ...";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = invalidLocalTime;
                todo.DueDate = ambiguousLocalTime;

                todo.IsDone = false;
                todo.IsDone = true;
            }

            todoSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var tm = new TodoManager();
                bool ok = tm.AddTodo(todo);

                // ASSERT
                Assert.IsTrue(ok);
                Assert.AreEqual(1, tm.Todos.Count());
            });
            //#

            docu.BeginRecordConsole("Slide7_NCase2_Console");
            Console.Write(todoSet.PrintCasesAsTable());
            docu.StopRecordConsole();
        }
   }

}