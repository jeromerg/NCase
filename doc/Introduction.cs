﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Moq;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDocUtilLibrary;
using NDsl.Front.Ui;
using NUnit.Framework;
using NUtil.Generics;

namespace NCaseFramework.Doc
{
    // remark: must remain here, in order to redirect NCase calls to the doc-specific implementation
    using Shared;
    
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class Introduction
    {
        // ReSharper disable once InconsistentNaming
        [NotNull] private readonly NDocUtil docu = new NDocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
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
        DateTime daylightSavingTimeMissingTime = new DateTime(2011, 11, 12, 0, 0, 0);
        DateTime daylightSavingTimeAmbiguousTime = new DateTime(2011, 11, 12, 0, 0, 0);

        [Test]
        public void MoqExample1()
        {
            //# MoqExample1
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Title = "Don't forget to forget NCase";
            todo.DueDate = now;
            todo.IsDone = false;

            // ACT
            var todoManager = new TodoManager();
            todoManager.AddTodo(todo);

            // ASSERT
            //...
            //#
        }

        [Test]
        public void NCaseExample1()
        {
            //# NCaseExample1
            // ARRANGE
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var todoSet = builder.NewCombinationSet("todoSet");

            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget NCase";

                todo.DueDate = now;

                todo.IsDone = false;
            }

            todoSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.AddTodo(todo);

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

            todo.Title = "Don't forget to forget NCase";
            todo.DueDate = now;            
            todo.IsDone = false;

            // ACT ... ASSERT ...
        }

        [Test]
        public void MoqTest2()                                 // DUPLICATED TEST
        {
            // ARRANGE
            var mock = new Mock<ITodo>();
            mock.SetupAllProperties();
            ITodo todo = mock.Object;

            todo.Title = "Another todo to never forget NCase!"; // CHANGE
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

            todoSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.AddTodo(todo);

                // ASSERT
                //...
            });
            //#
        }

        [Test]
        public void NCaseExample2_AddedLine()
        {
            // ARRANGE
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var todoSet = builder.NewCombinationSet("todoSet");
            using (todoSet.Define())
            {
                //# NCaseExample2_AddedLine
                todo.Title = "Another todo to never forget NCase";
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
            var builder = NCase.NewBuilder();
            var todo = builder.NewContributor<ITodo>("todo");
            var todoSet = builder.NewCombinationSet("todoSet");
            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget NCase";
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

            todoSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.AddTodo(todo);

                // ASSERT
                //...
            });
            //#
        }


        [Test]
        public void NCaseCombiningContributors()
        {
            // ARRANGE
            var builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            //# NCaseCombiningContributors_VAR
            var user = builder.NewContributor<IUser>("user");
            //# 

            var wholeSet = builder.NewCombinationSet("wholeSet");

            //# NCaseCombiningContributors_DEF
            using (wholeSet.Define())
            {
                todo.Title = "Don't forget to forget NCase";
                //... alternatives

                todo.DueDate = yesterday;
                //... alternatives

                todo.IsDone = false;
                //... alternatives

                user.IsActive = true;
                //... alternatives

                user.Email = null;
                //... alternatives
            }
            //#

            wholeSet.Cases().Replay().ActAndAssert(ea =>
            {
                // ACT
                var todoManager = new TodoManager();
                todoManager.AddTodo(todo);

                // ASSERT
                //...
            });
        }

        [Test]
        public void NCaseCombiningSets()
        {
            // ARRANGE
            var builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");
            var user = builder.NewContributor<IUser>("user");

            //# NCaseCombiningSets_TODO_SET
            var todoSet = builder.NewCombinationSet("todoSet");
            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget NCase";
                //... and alternatives

                todo.DueDate = yesterday;
                //... and alternatives

                todo.IsDone = false;
                //... and alternatives
            }
            //#

            //# NCaseCombiningSets_USER_SET
            var userSet = builder.NewCombinationSet("userSet");
            using (userSet.Define())
            {
                user.IsActive = true;
                //... and alternatives

                user.Email = null;
                //... and alternatives

            }
            //#

            { 
                //# NCaseCombiningSets_ALL_SET
                var allSet = builder.NewCombinationSet("allSet");
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
                    todoManager.AddTodo(todo);

                    // ASSERT
                    //...
                });
            }

            { 
                //# NCaseCombiningSets_ALL_SET_Pairwise
                var allSet = builder.NewCombinationSet("allSet");
                using (allSet.Define())
                {
                    todoSet.Ref();    // Pairwise product!

                    userSet.Ref();    // Default cartesian product
                }
                //#
            }


        }

        [Test]
        public void NCasePairwiseCombinations()
        {
            // ARRANGE
            var builder = NCase.NewBuilder();

            var todo = builder.NewContributor<ITodo>("todo");

            //# NCasePairwiseCombinations
            var todoSet  = builder.NewCombinationSet("todoSet", onlyPairwise: true);
            using (todoSet.Define())
            {
                todo.Title = "Don't forget to forget NCase";
                //... alternatives

                todo.DueDate = yesterday;
                //... alternatives

                todo.IsDone = false;
                //... alternatives

            }
            //#

        }

        [Test]
        public void NCaseTree()
        {
            // ARRANGE
            var builder = NCase.NewBuilder();

            //# NCaseTree
            var todo = builder.NewContributor<ITodo>("todo");
            var isValid = builder.NewContributor<IHolder<bool>>("isValid");

            var todoSet  = builder.NewCombinationSet("todoSet");
            using (todoSet.Define())
            {
                todo.Title = "forget me";
                    isValid.Value = true;
                        todo.DueDate = tomorrow;
                            todo.IsDone = false;
                        todo.DueDate = yesterday;
                            todo.IsDone = false;
                            todo.IsDone = true;
                    isValid.Value = false;
                        todo.DueDate = tomorrow;
                            todo.IsDone = true;
            }
            //#

            //# Visualize_Def
            docu.BeginRecordConsole("Visualize_Def_Console");
            string def = todoSet.PrintDefinition(isFileInfo: true);

            Console.Write(def);
            docu.StopRecordConsole();
            //#                

            //# Visualize_Table
            docu.BeginRecordConsole("Visualize_Table_Console");
            string table = todoSet.PrintCasesAsTable();

            Console.Write(table);
            docu.StopRecordConsole();
            //#

            //# Visualize_Case
            docu.BeginRecordConsole("Visualize_Case_Console");
            string cas = todoSet.Cases().First().Print();

            Console.Write(cas);
            docu.StopRecordConsole();
            //#
        }

        [Test]
        public void NCaseTree2()
        {
            // ARRANGE
            var builder = NCase.NewBuilder();

            //# NCaseTree2
            var todo = builder.NewContributor<ITodo>("todo");
            var isValid = builder.NewContributor<IHolder<bool>>("isValid");

            var todoSet  = builder.NewCombinationSet("todoSet");
            using (var d = todoSet.Define())
            {
                todo.Title = "forget me";
                    isValid.Value = true;
                        todo.DueDate = tomorrow;
                            todo.IsDone = false;
                        todo.DueDate = yesterday;
                            todo.IsDone = false;
                            todo.IsDone = true;
                d.Branch();
                    todo.Title = "remember me";
                    todo.Title = "forgive me";

                    isValid.Value = false;
                        todo.DueDate = tomorrow;
                            todo.IsDone = true;
            }
            //#
        }

    }

}