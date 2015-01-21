using NCase.Api;
using NCase.Api.Dev.Director;
using NUnit.Framework;

namespace NCaseTest
{
    [TestFixture]
    public class Tests
    {
        public interface ITestData
        {
            string Name { get; set; }    
            int Age { get; set; }    
            string City { get; set; }    
        }

        [Test]
        public void TestBuilderAndComponentAndDumpVisitor()
        {
            var testCaseBuilder = new CaseBuilder();

            var o = testCaseBuilder.CreateCaseComponent<ITestData>();

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

            string dump = testCaseBuilder.VisitAst<DumpDirector>().ToString();

            const string expected = @"ROOT
    set_Name(Jerome)
        set_Age(22)
            set_City(Munich)
            set_City(Berlin)
        set_Age(30)
            set_City(Munich)
            set_City(Berlin)
";
            Assert.AreEqual(expected, dump);
        }
    }
}
