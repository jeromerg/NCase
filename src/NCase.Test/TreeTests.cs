using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Ui;
using NDsl.Front.Api;
using NUnit.Framework;

namespace NCaseFramework.Test
{
    [TestFixture]
    public class TreeTests
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
            string City { get; set; }
        }

        [Test]
        public void Test_Properties_1Contrib()
        {
            // Create a new builder
            IBuilder builder = NCase.NewBuilder();

            // create a case contributor
            var o = builder.NewContributor<IMyTestvalues>("o");

            // initialize a new case set of type Tree
            var tree = builder.NewDefinition<Tree>("Environment");

            // define the content of the tree
            using (tree.Define())
            {
                o.Name = "Raoul";
                {
                    o.Age = 22;
                    {
                        o.City = "Munich";
                        o.City = "Berlin"; // successive use of a property results in a case fork
                    } // bracket have no syntax meaning, they just ease the automatic formatting

                    o.Age = 30;
                    {
                        o.City = "Paris";
                        o.City = "London";
                    }
                }
                o.Name = "Philip";
                {
                    o.Age = 34;
                    {
                        o.City = "Lyon";
                    }
                }
            }

            // Then you can iterate through the cases defined by the tree, by calling ParseAndGenerate() 
            IEnumerator<Case> enumerator = tree.Cases().Replay().GetEnumerator();

            // case 1
            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(22, o.Age);
            Assert.AreEqual("Munich", o.City);

            // case 2
            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(22, o.Age);
            Assert.AreEqual("Berlin", o.City);

            // case 3
            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(30, o.Age);
            Assert.AreEqual("Paris", o.City);

            // case 4
            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(30, o.Age);
            Assert.AreEqual("London", o.City);

            // case 5
            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(34, o.Age);
            Assert.AreEqual("Lyon", o.City);
        }

        [Test]
        public void Test_Properties_2Contribs()
        {
            // Create a new builder
            IBuilder builder = NCase.NewBuilder();

            // you can use multiple contributors, contributing to the definition of cases
            var m = builder.NewContributor<IMyTestvalues>("man");
            var w = builder.NewContributor<IMyTestvalues>("woman");

            var tree = builder.NewDefinition<Tree>("children");
            using (tree.Define())
            {
                {
                    m.Name = "Louis";
                    m.City = "Munich";
                    w.Name = "Saskia";
                    w.City = "Munich";
                    {
                        m.Age = 5;
                        w.Age = 3;
                    }
                    {
                        m.Age = 6;
                        w.Age = 4;
                    }
                    {
                        m.Age = 7;
                        w.Age = 5;
                    }
                }
            }
            IEnumerator<Case> enumerator = tree.Cases().Replay().GetEnumerator();

            // case 1
            enumerator.MoveNext();
            Assert.AreEqual("Louis", m.Name);
            Assert.AreEqual("Saskia", w.Name);
            Assert.AreEqual("Munich", m.City);
            Assert.AreEqual("Munich", w.City);
            Assert.AreEqual(5, m.Age);
            Assert.AreEqual(3, w.Age);

            // case 2
            enumerator.MoveNext();
            Assert.AreEqual("Louis", m.Name);
            Assert.AreEqual("Saskia", w.Name);
            Assert.AreEqual("Munich", m.City);
            Assert.AreEqual("Munich", w.City);
            Assert.AreEqual(6, m.Age);
            Assert.AreEqual(4, w.Age);

            // case 3
            enumerator.MoveNext();
            Assert.AreEqual("Louis", m.Name);
            Assert.AreEqual("Saskia", w.Name);
            Assert.AreEqual("Munich", m.City);
            Assert.AreEqual("Munich", w.City);
            Assert.AreEqual(7, m.Age);
            Assert.AreEqual(5, w.Age);
        }

        [Test]
        public void TestTreeWithRef()
        {
            // Create a new builder
            IBuilder builder = NCase.NewBuilder();

            // create a case contributor
            var o = builder.NewContributor<IMyTestvalues>("o");

            // define a first set of cases
            var ages = builder.NewDefinition<Tree>("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
            }

            // transplant the first set into a second one
            var names = builder.NewDefinition<Tree>("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                {
                    ages.Ref();
                }
                o.Name = "Philip";
                {
                    ages.Ref();
                }
            }

            // Then you can iterate through the cases defined by the tree, by calling ParseAndGenerate() 
            IEnumerator<Case> enumerator = names.Cases().Replay().GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Raoul", o.Name);
            Assert.AreEqual(25, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(20, o.Age);

            enumerator.MoveNext();
            Assert.AreEqual("Philip", o.Name);
            Assert.AreEqual(25, o.Age);
        }
    }
}