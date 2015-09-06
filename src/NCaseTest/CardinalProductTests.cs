using System.Collections.Generic;
using NCase.Api;
using NCase.Autofac;
using NUnit.Framework;
using NVisitor.Api.Lazy;

namespace NCaseTest
{
    [TestFixture]
    public class CardinalProductTests
    {
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
        }

        [Test]
        public void Test_CardinalProduct()
        {
            // Create a new builder
            var caseBuilder = Case.GetBuilder();

            // create a case contributor
            var o = caseBuilder.GetContributor<IMyTestvalues>("o");
            
            var allPersonsAllAges = caseBuilder.CreateSet<IProd>("allPersonsAllAges");

            using (allPersonsAllAges.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";

                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            // Then you can iterate through the cases defined by the tree, by calling GetAllCases() 
            IEnumerator<Pause> enumerator = caseBuilder.GetAllCases(allPersonsAllAges).GetEnumerator();
            
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
        public void Test_CardinalProduct_with_ref()
        {
            // Create a new builder
            var caseBuilder = Case.GetBuilder();

            // create a case contributor
            var o = caseBuilder.GetContributor<IMyTestvalues>("o");
            
            // define a first set of cases
            var names = caseBuilder.CreateSet<ITree>("person_set");
            using (names.Define())
            {
                o.Name = "Raoul";
                o.Name = "Philip";
                o.Name = "Wilhelm";
            }

            // transplant the first set into a second one
            var ages = caseBuilder.CreateSet<ITree>("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
                o.Age = 30;
            }

            var allPersonsAllAges = caseBuilder.CreateSet<IProd>("allPersonsAllAges");

            using (allPersonsAllAges.Define())
            {
                names.Ref();
                ages.Ref();
            }

            // Then you can iterate through the cases defined by the tree, by calling GetAllCases() 
            IEnumerator<Pause> enumerator = caseBuilder.GetAllCases(allPersonsAllAges).GetEnumerator();
            
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

