using System.Collections.Generic;
using Autofac;
using Castle.DynamicProxy;
using NCase.Api;
using NCase.Autofac;
using NDsl.Impl.Core;
using NDsl.Impl.RecPlay;
using NUnit.Framework;
using NVisitor.Api.Lazy;

namespace NCaseTest
{
    [TestFixture]
    public class InterfacePropertyTests
    {
        public interface ISimpleProperties
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

            var o = caseBuilder.GetContributor<ISimpleProperties>("o");

            caseBuilder.CaseSet("Jerome's life");
            o.Name = "Jerome";
            {
                o.Age = 22;
                {
                    o.City = "Munich";
                    o.City = "Berlin";
                }
                o.Age = 30;
                {
                    o.City = "Munich";
                    o.City = "Berlin";
                }
            }

            var pause = caseBuilder.GetAllCases();

            const string expected = @"ROOT
    set_Name(Jerome)
        set_Age(22)
            set_City(Munich)
            set_City(Berlin)
        set_Age(30)
            set_City(Munich)
            set_City(Berlin)
";
            //Assert.AreEqual(expected, dumpVisit.ToString());
        }

        public interface IIndexerProperties
        {
            string this[string name, int age, string city] { get; set; }
        }

        public interface IMethods
        {
            void Method();
        }
    }
}
