using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDsl.Front.Api;
using NUnit.Framework;
using NUtil.Doc;

namespace NCaseFramework.doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Readme
    {
        // ReSharper disable once InconsistentNaming
        private readonly ConsoleRecorder Console = new ConsoleRecorder();

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            new DocUtil().UpdateDocAssociatedToThisFile(Console);
        }

        //# TodoInterface
        public interface ITodo
        {
            string Task { get; set; }
            DateTime DueDate { get; set; }
            bool IsDone { get; set; }
        }
        //#

        public class TaskManager
        {
            public void CreateTask(string task, DateTime dueDate, bool isDone) { }
        }

        DateTime now = new DateTime(2011,11,11,11,11,11);
        DateTime yesterday = new DateTime(2011,11,10,11,11,11);
        DateTime tomorrow = new DateTime(2011,11,12,11,11,11);
        DateTime daylightSavingTimeMissingTime = new DateTime(2011,11,12,11,11,11);
        DateTime daylightSavingTimeAmbiguousTime = new DateTime(2011,11,12,11,11,11);

        [Test]
        public void MoqExample1()
        {
            //# MoqExample1
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Task = "Don't forget to forget";
            todo.DueDate = now;
            todo.IsDone = false;

            // ACT
            var taskManager = new TaskManager();
            taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

            // ASSERT
            //...
            //#
        }

        [Test]
        public void NCaseExample1()
        {
            //# NCaseExample1
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var set = builder.NewDefinition<AllCombinations>("set");
            using (set.Define())
            {
                todo.Task = "Don't forget to forget";
                todo.DueDate = now;
                todo.IsDone = false;
            }

            set.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var taskManager = new TaskManager();
                taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

                // ASSERT
                //...
            });
            //#
        }


        //# MoqExample2
        [Test]
        public void MoqTest1()
        {
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Task = "Don't forget to forget";
            todo.DueDate = now;
            todo.IsDone = false;

            // ACT ... ASSERT ...
        }

        [Test]
        public void MoqTest2()
        {
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Task = "Another task to forget";
            todo.DueDate = now;
            todo.IsDone = false;

            // ACT ... ASSERT ...
        }
        //#

        [Test]
        public void NCaseExample2()
        {
            //# NCaseExample2
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var set = builder.NewDefinition<AllCombinations>("set");
            using (set.Define())
            {
                todo.Task = "Don't forget to forget";
                todo.Task = "Another task to forget";

                todo.DueDate = now;
                todo.IsDone = false;
            }

            set.Cases().Replay().ActAndAssert( ea =>
            {
                // ACT
                var taskManager = new TaskManager();
                taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

                // ASSERT
                //...
            });
            //#
        }

        [Test]
        public void NCaseExample2_AddedLine()
        {
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var set = builder.NewDefinition<AllCombinations>("set");
            using (set.Define())
            {
                //# NCaseExample2_AddedLine
                todo.Task = "Another task to forget";
                //#

                todo.DueDate = now;
                todo.IsDone = false;
            }
        }

        [Test]
        public void NCaseExample3()
        {
            //# NCaseExample3
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var set = builder.NewDefinition<AllCombinations>("set");
            using (set.Define())
            {
                todo.Task = "Don't forget to forget";
                todo.Task = "";
                todo.Task = null;
                todo.Task = "ß üöä ÜÖÄ !§$%&/()=?";
                todo.Task = "SELECT USER_ID, PASSWORD FROM USER";
                todo.Task = "Another Task";

                todo.DueDate = yesterday;
                todo.DueDate = now;
                todo.DueDate = tomorrow;
                todo.DueDate = DateTime.MaxValue;
                todo.DueDate = DateTime.MinValue;
                todo.DueDate = daylightSavingTimeMissingTime;
                todo.DueDate = daylightSavingTimeAmbiguousTime;

                todo.IsDone = false;
                todo.IsDone = true;
            }

            set.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var taskManager = new TaskManager();
                taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);
 
                // ASSERT
                //...
            });
            //#
        }

    }
}