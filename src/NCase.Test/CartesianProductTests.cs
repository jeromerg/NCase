using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Ui;
using NDsl.Front.Api;
using NUnit.Framework;

namespace NCaseFramework.Test
{
    [TestFixture]
    public class CartesianProductTests
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
        }

        [Test]
        public void Test_CartesianProduct()
        {
            // Create a new builder
            IBuilder builder = NCase.NewBuilder();

            // create a case contributor
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

            // Then you can iterate through the cases defined by the tree, by calling ParseAndGenerate() 
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

        [Test]
        public void Test_CartesianProduct_with_ref()
        {
            // Create a new builder
            IBuilder builder = NCase.NewBuilder();

            // create a case contributor
            var o = builder.NewContributor<IMyTestvalues>("o");

            // define a first set of cases
            var names = builder.NewDefinition<Tree>("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";
            }

            // transplant the first set into a second one
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

            // Then you can iterate through the cases defined by the tree, by calling ParseAndGenerate() 
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