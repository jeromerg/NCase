using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NCaseFramework.Front.Ui;
using NCaseFramework.Test.Util;
using NDsl.Back.Api.Ex;
using NDsl.Front.Api;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Test
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class InterfaceRecPlayTests
    {
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
        }

        [Test]
        public void AssignmentOutsideDef()
        {
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");

            string line = LineUtil.GetLine(3);
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
        public void CallGetterInReplayModeButNotRecorded()
        {
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");
            using (allPersonsAllAges.Define())
            {
                v.Name = "myName";
            }

            allPersonsAllAges.Cases().Replay().GetEnumerator().MoveNext();

            string line = LineUtil.GetLine(3);
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
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");

            string line = LineUtil.GetLine(3);
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
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");
            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            string line = LineUtil.GetLine(3);
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
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            string line = LineUtil.GetLine(3);
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
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Name = "coucou";
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            string line = LineUtil.GetLine(3);
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
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var v2 = caseBuilder.NewContributor<IMyTestvalues>("v2");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Name = "coucou";
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());

            string line = LineUtil.GetLine(3);
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
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            allPersonsAllAges.Cases().First().Replay(true);

            Assert.AreEqual(10, v.Age);

            string line = LineUtil.GetLine(3);
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
        public void RecPlay()
        {
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
            var v = caseBuilder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(10, v.Age);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void TwoProperties()
        {
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
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
            CaseBuilder caseBuilder = NCaseLegacy.NewBuilder();
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
    }
}