using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NCase.Front.Ui;
using NUnit.Framework;

namespace NCase.Test
{
    [TestFixture]
    public class PairwiseProductTests
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
        }

        private class Val : IMyTestvalues
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public Val(string name, int age)
            {
                Name = name;
                Age = age;
            }

            protected bool Equals(Val other)
            {
                return string.Equals(Name, other.Name) && Age == other.Age;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Val) obj);
            }

            public override int GetHashCode()
            {
                unchecked { return ((Name != null ? Name.GetHashCode() : 0)*397) ^ Age; }
            }
        }

        [Test]
        public void TestPairwiseProduct_TwoDimensions()
        {
            // Create a new builder
            IBuilder builder = CaseBuilder.Create();

            // create a case contributor
            var o = builder.CreateContributor<IMyTestvalues>("o");

            var allPersonsAllAges = builder.CreatePairwise("allPersonsAllAges");

            using (allPersonsAllAges.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";

                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            var expected = new HashSet<Val>
                           {
                               new Val("Raoul", 20),
                               new Val("Raoul", 25),
                               new Val("Raoul", 30),
                               new Val("Philip", 20),
                               new Val("Philip", 25),
                               new Val("Philip", 30),
                               new Val("Wilhelm", 20),
                               new Val("Wilhelm", 25),
                               new Val("Wilhelm", 30),
                           };

            // Then you can iterate through the cases defined by the tree, by calling ParseAndGenerate() 
            foreach (var @case in allPersonsAllAges.Cases().Replay())
            {
                bool removed = expected.Remove(new Val(o.Name, o.Age));
                Assert.AreEqual(true, removed);
            }
            Assert.AreEqual(0, expected.Count);
        }

        [Test]
        public void Test_CartesianProduct_with_ref()
        {
            // Create a new builder
            IBuilder builder = CaseBuilder.Create();

            // create a case contributor
            var o = builder.CreateContributor<IMyTestvalues>("o");

            // define a first set of cases
            var names = builder.CreateTree("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";
            }

            // transplant the first set into a second one
            var ages = builder.CreateTree("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            var allPersonsAllAges = builder.CreateProd("allPersonsAllAges");

            using (allPersonsAllAges.Define())
            {
                names.Ref();
                ages.Ref();
            }

            // Then you can iterate through the cases defined by the tree, by calling ParseAndGenerate() 
            IEnumerator<ICase> enumerator = allPersonsAllAges.Cases().Replay().GetEnumerator();

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