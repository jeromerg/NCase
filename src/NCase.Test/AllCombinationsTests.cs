using System.Collections.Generic;
using System.Linq;
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
        [ExpectedException(typeof(InvalidSyntaxException))]
        public void UndefinedDef()
        {
            IBuilder builder = NCase.NewBuilder();
            var all = builder.NewDefinition<AllCombinations>("all");

            all.Cases().Replay().FirstOrDefault();
        }

        [Test]
        [ExpectedException(typeof(InvalidRecPlayStateException))]
        public void AssignmentOutsideDef()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            v.Age = 10;
            using (allPersonsAllAges.Define())
            {
            }

            allPersonsAllAges.Cases().Replay().FirstOrDefault();
        }

        [Test]
        [ExpectedException(typeof(InvalidSyntaxException))]
        public void AssignmentTwice()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            var allPersonsAllAges = builder.NewDefinition<AllCombinations>("all");

            using (allPersonsAllAges.Define())
            {
            }
            using (allPersonsAllAges.Define())
            {
            }

            allPersonsAllAges.Cases().Replay().FirstOrDefault();
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
        [ExpectedException(typeof(InvalidRecPlayStateException))]
        public void CallGetterNotInReplayMode()
        {
            IBuilder builder = NCase.NewBuilder();
            var v = builder.NewContributor<IMyTestvalues>("v");
            int age = v.Age;
        }

        [Test]
        [ExpectedException(typeof(InvalidRecPlayStateException))]
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
            v.Age = 20; // Age property is currently in replay mode
        }

        [Test]
        [ExpectedException(typeof(InvalidRecPlayStateException))]
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
            v.Age = 20; // Age property is currently in replay mode
        }

        [Test]
        [ExpectedException(typeof(InvalidRecPlayStateException))]
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
            v2.Age = 20;
        }

        [Test]
        [ExpectedException(typeof(InvalidRecPlayStateException))]
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

        // TODO: REDUCE TEST
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