﻿using System.Collections.Generic;
using NCase.Api;
using NCase.Autofac;
using NUnit.Framework;
using NVisitor.Api.Lazy;

namespace NCaseTest
{
    [TestFixture]
    public class InterfacePropertyTests
    {
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
            var caseBuilder = Case.GetBuilder();

            // create a case contributor
            var o = caseBuilder.GetContributor<IMyTestvalues>("o");
            
            // initialize a new case set of type ITree
            var tree = caseBuilder.CreateSet<ITree>("Environment");

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

            // Then you can iterate through the cases defined by the tree, by calling GetAllCases() 
            IEnumerator<Pause> enumerator = caseBuilder.GetAllCases(tree).GetEnumerator();
            
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
            var caseBuilder = Case.GetBuilder();

            // you can use multiple contributors, contributing to the definition of cases
            var m = caseBuilder.GetContributor<IMyTestvalues>("man");
            var w = caseBuilder.GetContributor<IMyTestvalues>("woman");

            ITree caseSet = caseBuilder.CreateSet<ITree>("children");
            using (caseSet.Define())
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
            IEnumerator<Pause> enumerator = caseBuilder.GetAllCases(caseSet).GetEnumerator();

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

    }
}

