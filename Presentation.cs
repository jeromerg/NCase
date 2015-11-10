using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDsl.Front.Api;
using NUnit.Framework;
using NUtil.Doc;
using NUtil.Generics;

namespace NCaseFramework.doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Presentation
    {
        // ReSharper disable once InconsistentNaming
        private readonly DocUtil mDocUtil = new DocUtil("mDocUtil");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            mDocUtil.UpdateDocAssociatedToThisFile();
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
            public void CreateTodo(ITodo todo) { }
        }

        DateTime now = new DateTime(2011, 11, 11, 11, 11, 11);
        DateTime yesterday = new DateTime(2011, 11, 10, 11, 11, 11);
        DateTime tomorrow = new DateTime(2011, 11, 12, 11, 11, 11);
        DateTime daylightSavingTimeMissingTime = new DateTime(2011, 11, 12, 11, 11, 11);
        DateTime daylightSavingTimeAmbiguousTime = new DateTime(2011, 11, 12, 11, 11, 11);

        [Test]
        public void MoqExample1()
        {
            //# MoqExample1
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Title = "Don't forget to forget";
            todo.DueDate = now;
            todo.IsDone = false;

            // ACT
            var todoManager = new TodoManager();
            todoManager.CreateTodo(todo);

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
                todo.Title = "Don't forget to forget";
                todo.DueDate = now;
                todo.IsDone = false;
            }

            // REPLAY TEST CASES
            set.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.CreateTodo(todo);

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

            todo.Title = "Don't forget to forget";
            todo.DueDate = now;
            todo.IsDone = false;

            // ACT ... ASSERT ...
        }

        [Test]
        public void MoqTest2()                     // DUPLICATE
        {
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Title = "Another todo to forget"; // CHANGE
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
                todo.Title = "Don't forget to forget";
                todo.Title = "Another todo to forget";  // CHANGE

                todo.DueDate = now;
                todo.IsDone = false;
            }

            set.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.CreateTodo(todo);

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
                todo.Title = "Another todo to forget";
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
                todo.Title = "Don't forget to forget";
                todo.Title = "";
                todo.Title = null;
                todo.Title = "ß üöä ÜÖÄ !§$%&/()=?";
                todo.Title = "SELECT USER_ID, PASSWORD FROM USER";
                todo.Title = "Another Title";

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
                var todoManager = new TodoManager();
                todoManager.CreateTodo(todo);

                // ASSERT
                //...
            });
            //#
        }


        [Test]
        public void NCaseCombiningContributors()
        {
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            //# NCaseCombiningContributors_VAR
            var user = builder.NewContributor<IUser>("user");
            //# 

            var set = builder.NewDefinition<AllCombinations>("set");

            //# NCaseCombiningContributors_DEF
            using (set.Define())
            {
                todo.Title = "Don't forget to forget";
                //...

                todo.DueDate = yesterday;
                //...

                todo.IsDone = false;
                //...

                user.IsActive = true;
                //...

                user.NotificationEmail = null;
                //...
            }
            //#

            set.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.CreateTodo(todo);

                // ASSERT
                //...
            });
        }

        [Test]
        public void NCaseCombiningSets()
        {
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");
            var user = builder.NewContributor<IUser>("user");

            //# NCaseCombiningSets_TODO_SET
            var todoSet = builder.NewDefinition<AllCombinations>("todoSet");
            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget";
                //...

                todo.DueDate = yesterday;
                //...

                todo.IsDone = false;
                //...
            }
            //#

            //# NCaseCombiningSets_USER_SET
            var userSet = builder.NewDefinition<AllCombinations>("userSet");
            using (userSet.Define())
            {
                user.IsActive = true;
                //...

                user.NotificationEmail = null;
                //...

            }
            //#

            //# NCaseCombiningSets_ALL_SET
            var allSet = builder.NewDefinition<AllCombinations>("allSet");
            using (allSet.Define())
            {
                todoSet.Ref();
                userSet.Ref();
            }
            //#

            allSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.CreateTodo(todo);

                // ASSERT
                //...
            });
        }

        [Test]
        public void NCasePairwiseCombinations()
        {
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            //# NCasePairwiseCombinations
            var todoSet = builder.NewDefinition<PairwiseCombinations>("todoSet");
            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget";
                //...

                todo.DueDate = yesterday;
                //...

                todo.IsDone = false;
                //...

            }
            //#

        }

        [Test]
        public void NCaseTree()
        {
            // ARRANGE
            IBuilder builder = NCase.NewBuilder();


            //# NCaseTree
            var todo = builder.NewContributor<ITodo>("todo");
            var isValid = builder.NewContributor<IHolder<bool>>("isValid");

            var todoSet = builder.NewDefinition<Tree>("todoSet");
            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget";
                    todo.DueDate = yesterday;
                        todo.IsDone = false;
                            isValid.Value = true;
                        todo.IsDone = true;
                            isValid.Value = false;
                    todo.DueDate = tomorrow;
                        isValid.Value = true;
                            todo.IsDone = false;
                            todo.IsDone = true;
                    todo.DueDate = now;
                        isValid.Value = true;
                            todo.IsDone = false;
                            todo.IsDone = true;                            
            }
            //#

        }

    }

}