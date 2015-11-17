using System.Collections.Generic;
using System.Linq;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Ex;
using NDsl.Front.Api;
using NUnit.Framework;
using NUtil.File;

namespace NCaseFramework.Test
{
    [TestFixture]
    public class AllCombinationsTests
    {
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
        }

        [Test]
        //[ExpectedException(typeof(InvalidSyntaxException))]
        public void UndefinedDef()
        {
            IBuilder builder = NCase.NewBuilder();
            var allCombi = builder.NewDefinition<AllCombinations>("allCombi");

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                allCombi.Cases().Replay().FirstOrDefault();
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidSyntaxException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("allCombi", s);
            }
        }

        [Test]
        //[ExpectedException(typeof(InvalidRecPlayStateException))]
        public void AssignmentOutsideDef()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                v.Age = 10;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        //[ExpectedException(typeof(InvalidSyntaxException))]
        public void AssignmentTwice()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("allPersonsAllAges");

            int line = 4 + CallerUtil.GetCallerLineNumber();
            try
            {
                using (allPersonsAllAges.Define()) { }
                using (allPersonsAllAges.Define()) { }
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidSyntaxException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("allPersonsAllAges", s);
                StringAssert.Contains("AllCombinations", s);
                StringAssert.Contains("NotDefined", s);
            }
        }

        [Test]
        //[ExpectedException(typeof(InvalidRecPlayStateException))]
        public void CallGetterInReplayModeButNotRecorded()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");
            using (allPersonsAllAges.Define())
            {
                v.Name = "myName";
            }

            allPersonsAllAges.Cases().Replay().GetEnumerator().MoveNext();

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                v.Age = 10;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        public void CallGetterNotInReplayMode()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                int age = v.Age;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        //[ExpectedException(typeof(InvalidRecPlayStateException))]
        public void CallGetterNotInReplayMode2()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");
            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                int age = v.Age;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        //[ExpectedException(typeof(InvalidRecPlayStateException))]
        public void CallSetterNotInRecordingMode()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                v.Age = 20;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        //[ExpectedException(typeof(InvalidRecPlayStateException))]
        public void CallSetterNotInRecordingMode2()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Name = "coucou";
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                v.Age = 20; 
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("v.Age", s);
            }

        }

        [Test]
        //[ExpectedException(typeof(InvalidRecPlayStateException))]
        public void CallSetterNotInRecordingMode3()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var v2 = builder.NewContributor<IMyTestvalues>("v2");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Name = "coucou";
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            int line = 3 + CallerUtil.GetCallerLineNumber();
            try
            {
                v2.Age = 20;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains("line " + line, s);
                StringAssert.Contains("v2.Age", s);
            }

        }

        [Test]
        //[ExpectedException(typeof(InvalidRecPlayStateException))]
        public void GetUnsetProperty()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            allPersonsAllAges.Cases().First().Replay(true);
            Assert.AreEqual(10, v.Age);


            string name = v.Name;
        }

        [Test]
        public void EmptyDef()
        {
            IBuilder builder = NCase.NewBuilder();
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
            }

            Case @case = allPersonsAllAges.Cases().Replay().FirstOrDefault();
            Assert.IsNull(@case);
        }

        [Test]
        public void SingleAssignment()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void TwoProperties()
        {
            IBuilder builder = NCase.NewBuilder();
            var o = builder.NewContributor<IMyTestvalues>("o");

            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("allPersonsAllAges");
            using (allPersonsAllAges.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";

                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(25, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(30, o.Age);


            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(25, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(30, o.Age);


            enumerator.MoveNext();
            Assert.AreEqual("Wilhelm", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Wilhelm", o.Name);
            Assert.AreEqual(25, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Wilhelm", o.Name);
            Assert.AreEqual(30, o.Age);

            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void Ref()
        {
            IBuilder builder = NCase.NewBuilder();
            var o = builder.NewContributor<IMyTestvalues>("o");

            var names = builder.NewDefinition<Tree>("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";
            }

            var ages = builder.NewDefinition<Tree>("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("allPersonsAllAges");

            using (allPersonsAllAges.Define())
            {
                names.Ref();
                ages.Ref();
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(25, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(30, o.Age);


            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(25, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(30, o.Age);


            enumerator.MoveNext();
            Assert.AreEqual("Wilhelm", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Wilhelm", o.Name);
            Assert.AreEqual(25, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Wilhelm", o.Name);
            Assert.AreEqual(30, o.Age);
        }
    }
}