using Autofac;
using NCase.Api;
using NCase.Autofac;
using NUnit.Framework;

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

            caseBuilder.CaseSet("Environment");
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

            foreach (var pause in caseBuilder.PlayAllCases())
            {
                
            }

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
