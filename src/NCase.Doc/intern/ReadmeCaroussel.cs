
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
            docu.SaveSnippetsAsImage(ImageFormat.Emf, leftBorder:5);
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
            string NotificationEmail { get; set; }
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
            //# Slide1
            var builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            var set = builder.NewCombinationSet("set");

            using (set.Define())
            {
                todo.Title = "Forget";
                todo.Title = "Remember";
                todo.Title = "Forgive";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;

                todo.IsDone = false;
                todo.IsDone = true;
            }
            //#

            docu.BeginRecordConsole("Slide1_Console");
            Console.WriteLine(set.PrintCasesAsTable());
            docu.StopRecordConsole();
        }


   }

}