using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Ex;
using NDsl.Front.Api;
using NUnit.Framework;

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
        public void UndefinedDef()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var allCombi = caseBuilder.NewDefinition<AllCombinations>("allCombi");

            string line = GetLine(3);
            try
            {
                allCombi.Cases().Replay().GetEnumerator().MoveNext();
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidSyntaxException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("allCombi", s);
            }
        }

        [Test]
        public void AssignmentOutsideDef()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");

            string line = GetLine(3);
            try
            {
                v.Age = 10;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        public void AssignmentTwice()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("allPersonsAllAges");

            string line = GetLine(4);
            try
            {
                using (allPersonsAllAges.Define()) { }
                using (allPersonsAllAges.Define()) { }
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidSyntaxException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("allPersonsAllAges", s);
                StringAssert.Contains("AllCombinations", s);
                StringAssert.Contains("NotDefined", s);
            }
        }

        [Test]
        public void CallGetterInReplayModeButNotRecorded()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");
            using (allPersonsAllAges.Define())
            {
                v.Name = "myName";
            }

            allPersonsAllAges.Cases().Replay().GetEnumerator().MoveNext();

            string line = GetLine(3);
            try
            {
                v.Age = 10;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        public void CallGetterNotInReplayMode()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");

            string line = GetLine(3);
            try
            {
                int age = v.Age;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        public void CallGetterNotInReplayMode2()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");
            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            string line = GetLine(3);
            try
            {
                int age = v.Age;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        public void CallSetterNotInRecordingMode()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            string line = GetLine(3);
            try
            {
                v.Age = 20;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v.Age", s);
            }
        }

        [Test]
        public void CallSetterNotInRecordingMode2()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Name = "coucou";
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            string line = GetLine(3);
            try
            {
                v.Age = 20; 
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v.Age", s);
            }

        }

        [Test]
        public void CallSetterNotInRecordingMode3()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var v2 = caseBuilder.NewContributor<IMyTestvalues>("v2");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Name = "coucou";
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            string line = GetLine(3);
            try
            {
                v2.Age = 20;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v2.Age", s);
            }
        }

        [Test]
        public void GetUnsetProperty()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            allPersonsAllAges.Cases().First().Replay(true);

            Assert.AreEqual(10, v.Age);

            string line = GetLine(3);
            try
            {
                string name = v.Name;
                Assert.Fail("Act expected to throw an exception");
            }
            catch (InvalidRecPlayStateException e)
            {
                string s = e.ToString();
                StringAssert.Contains(line, s);
                StringAssert.Contains("v.Name", s);
            }

        }

        [Test]
        public void EmptyDef()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
            }

            Case @case = allPersonsAllAges.Cases().Replay().FirstOrDefault();
            Assert.IsNull(@case);
        }

        [Test]
        public void SingleAssignment()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

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
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var o = caseBuilder.NewContributor<IMyTestvalues>("o");

            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("allPersonsAllAges");
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
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var o = caseBuilder.NewContributor<IMyTestvalues>("o");

            var names = caseBuilder.NewDefinition<Tree>("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";
            }

            var ages = caseBuilder.NewDefinition<Tree>("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("allPersonsAllAges");

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
    
        private string GetLine(int offset, [CallerLineNumber] int callerLineNumber = -1)
        {
            return "line " + (offset + callerLineNumber);
        }

    }
}