using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NCaseFramework.Test.Util;
using NDsl.Back.Api.Ex;
using NDsl.Front.Api;
using NDsl.Front.Ui;
using NUnit.Framework;
using NUtil.Generics;

namespace NCaseFramework.Test
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class InterfaceRecPlayTests
    {
        [NotNull]
        // ReSharper disable once NotNullMemberIsNotInitialized
        private CaseBuilder mCaseBuilder = NCase.NewBuilder();

        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
        }

        public class MyTestvalues : IMyTestvalues
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [TearDown]
        public void Teardown()
        {
            mCaseBuilder = NCase.NewBuilder();
        }

        public class Wrapper<T>
        {
            public Wrapper(T value)
            {
                Value = value;
            }

            public T Value { get; private set; }
        }

        public IEnumerable<Wrapper<IMyTestvalues>> GenerateContributor()
        {
            yield return new Wrapper<IMyTestvalues>(mCaseBuilder.NewContributor<IMyTestvalues>("v"));
            yield return new Wrapper<IMyTestvalues>(mCaseBuilder.NewContributor<MyTestvalues>("v"));
        }

        [Test, TestCaseSource("GenerateContributor")]
        public void AssignmentOutsideDef(IMyTestvalues v)
        {
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
        public void CallGetterInReplayModeButNotRecorded(IMyTestvalues v)
        {
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");
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
        public void CallGetterNotInReplayMode(IMyTestvalues v)
        {
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
        public void CallGetterNotInReplayMode2(IMyTestvalues v)
        {
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");
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
        public void CallSetterNotInRecordingMode(IMyTestvalues v)
        {
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");

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
        public void CallSetterNotInRecordingMode2(IMyTestvalues v)
        {
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");

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
        public void CallSetterNotInRecordingMode3(IMyTestvalues v)
        {
            var v2 = mCaseBuilder.NewContributor<IMyTestvalues>("v2");
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");

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
        public void GetUnsetProperty(IMyTestvalues v)
        {
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");

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
        public void RecPlay(IMyTestvalues v)
        {
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");

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
        public void TwoProperties(IMyTestvalues o)
        {
            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("all");
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
        public void Ref(IMyTestvalues o)
        {
            var names = mCaseBuilder.NewCombinationSet("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";
            }

            var ages = mCaseBuilder.NewCombinationSet("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            var allPersonsAllAges = mCaseBuilder.NewCombinationSet("allPersonsAllAges");

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