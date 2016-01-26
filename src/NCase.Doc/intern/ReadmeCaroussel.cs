
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
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
    public class Readme
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
            public void AddTodo(ITodo todo) { }
        }

        DateTime now = new DateTime(2011, 11, 11, 0, 0, 0);
        DateTime yesterday = new DateTime(2011, 11, 10, 0, 0, 0);
        DateTime tomorrow = new DateTime(2011, 11, 12, 0, 0, 0);

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


   }

}