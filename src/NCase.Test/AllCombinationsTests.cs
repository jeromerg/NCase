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

            string line = LineUtil.GetLine(3);
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
        public void DefinedTwice()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var allPersonsAllAges = caseBuilder.NewDefinition<AllCombinations>("allPersonsAllAges");

            string line = LineUtil.GetLine(4);
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
            Assert.AreEqual(10, v.Age);
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
    }
}