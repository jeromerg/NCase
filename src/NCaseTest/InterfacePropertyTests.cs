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
        public void TestSimpleProperties()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<NCaseModule>();
            IContainer container = builder.Build();
            var caseBuilder = container.Resolve<ICaseBuilder>();

            var o = caseBuilder.GetContributor<ITestvalues>("o");

            caseBuilder.CreateSet("Environment");
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

                    IEnumerator<Pause> enumerator =
                        caseBuilder.PlayAllCases().GetEnumerator();

                    enumerator.MoveNext();
                    Assert.AreEqual("Raoul", o.Name);
                    Assert.AreEqual(22, o.Age);
                    Assert.AreEqual("Munich", o.Age);

                    enumerator.MoveNext();
                    Assert.AreEqual("Raoul", o.Name);
                    Assert.AreEqual(22, o.Age);
                    Assert.AreEqual("Berlin", o.Age);

                    enumerator.MoveNext();
                    Assert.AreEqual("Raoul", o.Name);
                    Assert.AreEqual(30, o.Age);
                    Assert.AreEqual("Paris", o.Age);

                    enumerator.MoveNext();
                    Assert.AreEqual("Raoul", o.Name);
                    Assert.AreEqual(30, o.Age);
                    Assert.AreEqual("London", o.Age);

                    enumerator.MoveNext();
                    Assert.AreEqual("Philip", o.Name);
                    Assert.AreEqual(34, o.Age);
                    Assert.AreEqual("Lyon", o.Age);

                }

            }
        }
    }
}
