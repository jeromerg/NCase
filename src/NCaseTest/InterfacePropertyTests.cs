using System.Collections.Generic;
using Autofac;
using NCase.Api;
using NCase.Autofac;
using NUnit.Framework;
using NVisitor.Api.Lazy;

namespace NCaseTest
{
    [TestFixture]
    public class InterfacePropertyTests
    {
        public interface ITestvalues
        {
            string Name { get; set; }
            int Age { get; set; }
            string City { get; set; }
        }

        [Test]
        public void Test_Properties_1Contrib()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<NCaseModule>();
            IContainer container = builder.Build();
            var caseBuilder = container.Resolve<ICaseBuilder>();

            var o = caseBuilder.GetContributor<ITestvalues>("o");

            CaseSet caseSet = caseBuilder.CreateSet("Environment");
            using (caseSet.Define())
            {
                o.Name = "Raoul";
                {
                    o.Age = 22;
                    {
                        o.City = "Munich";
                        o.City = "Berlin";
                    }
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
            IEnumerator<Pause> enumerator = caseBuilder.GetAllCases(caseSet).GetEnumerator();

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
            var builder = new ContainerBuilder();
            builder.RegisterModule<NCaseModule>();
            IContainer container = builder.Build();
            var caseBuilder = container.Resolve<ICaseBuilder>();

            var m = caseBuilder.GetContributor<ITestvalues>("man");
            var w = caseBuilder.GetContributor<ITestvalues>("woman");

            CaseSet caseSet = caseBuilder.CreateSet("children");
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

