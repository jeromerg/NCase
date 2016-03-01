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
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class SharedRecPlayTests
    {
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
            void Do();
        }

        public abstract class MyTestvalues : IMyTestvalues
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public abstract void Do();
        }

        public class TestData
        {
            public string Description { get; private set; }
            public CaseBuilder CaseBuilder { get; private set;}
            public IMyTestvalues Values { get; private set; }

            public TestData(string description, CaseBuilder caseBuilder, IMyTestvalues testValues)
            {
                Description = description;
                CaseBuilder = caseBuilder;
                Values = testValues;
            }

            public override string ToString()
            {
                return string.Format("{0}", Description);
            }
        }

        public IEnumerable<TestData> GenerateContributor()
        {
            {
                // INTERFACE CONTRIBUTOR
                CaseBuilder caseBuilder = NCase.NewBuilder();
                yield return new TestData("interface contributor", caseBuilder, caseBuilder.NewContributor<IMyTestvalues>("v"));
            }
            {
                // ABSTRACT CLASS CONTRIBUTOR
                CaseBuilder caseBuilder = NCase.NewBuilder();
                yield return new TestData("class contributor", caseBuilder, caseBuilder.NewContributor<MyTestvalues>("v"));                
            }
        }

        [Test, TestCaseSource("GenerateContributor")]
        public void AssignmentOutsideDef(TestData data)
        {
            IMyTestvalues v = data.Values;

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

        [Test, TestCaseSource("GenerateContributor")]
        public void CallGetterInReplayModeButNotRecorded(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");
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

        [Test, TestCaseSource("GenerateContributor")]
        public void CallGetterNotInReplayMode(TestData data)
        {
            IMyTestvalues v = data.Values;

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

        [Test, TestCaseSource("GenerateContributor")]
        public void CallGetterNotInReplayMode2(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");
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

        [Test, TestCaseSource("GenerateContributor")]
        public void CallSetterNotInRecordingMode(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");

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

        [Test, TestCaseSource("GenerateContributor")]
        public void CallSetterNotInRecordingMode2(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");

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

        [Test, TestCaseSource("GenerateContributor")]
        public void CallSetterNotInRecordingMode3(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var v2 = caseBuilder.NewContributor<IMyTestvalues>("v2");
            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");

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

        [Test, TestCaseSource("GenerateContributor")]
        public void GetUnsetProperty(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");

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

        [Test, TestCaseSource("GenerateContributor")]
        public void CallingUnimplementedMethid(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");

            using (allPersonsAllAges.Define())
            {
                v.Do();
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

        [Test, TestCaseSource("GenerateContributor")]
        public void RecPlay(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues v = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");

            using (allPersonsAllAges.Define())
            {
                v.Age = 10;
            }

            IEnumerator<Case> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(10, v.Age);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test, TestCaseSource("GenerateContributor")]
        public void TwoProperties(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues o = data.Values;

            var allPersonsAllAges = caseBuilder.NewCombinationSet("all");
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

        [Test, TestCaseSource("GenerateContributor")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void Ref(TestData data)
        {
            CaseBuilder caseBuilder = data.CaseBuilder;
            IMyTestvalues o = data.Values;

            var names = caseBuilder.NewCombinationSet("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";
            }

            var ages = caseBuilder.NewCombinationSet("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            var allPersonsAllAges = caseBuilder.NewCombinationSet("allPersonsAllAges");

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