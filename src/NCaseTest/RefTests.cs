using System.Collections.Generic;
using NCase.Api;
using NCase.Autofac;
using NUnit.Framework;
using NVisitor.Api.Lazy;

namespace NCaseTest
{
    [TestFixture]
    public class RefTests
    {
        public interface IMyTestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
        }

        [Test]
        public void Test_Properties_1Contrib()
        {
            // Create a new builder
            var caseBuilder = Case.GetBuilder();

            // create a case contributor
            var o = caseBuilder.GetContributor<IMyTestvalues>("o");
            
            // define a first set of cases
            var ages = caseBuilder.CreateSet<ITree>("age_set");
            using (ages.Define())
            {
                o.Age = 20;
                o.Age = 25;
            }

            // transplant the first set into a second one
            var names = caseBuilder.CreateSet<ITree>("person_set");
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

            // Then you can iterate through the cases defined by the tree, by calling GetAllCases() 
            IEnumerator<Pause> enumerator = caseBuilder.GetAllCases(names).GetEnumerator();
            
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

