using NCase.Api;
using NCase.Api.Dev.Dir;
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
        public void Test()
        {
            var testCaseBuilder = new CaseBuilder();

            var o = testCaseBuilder.CreateComponent<ITestData>();

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

            string dump = testCaseBuilder.VisitTestTree<DumpDirector>().ToString();
        }


        //interface IBaseInterface {}
        //interface IBaseInterface<T, U> : IBaseInterface {}
        //class DoubleClosedType : IBaseInterface<int, uint>, IBaseInterface<string, uint> { }

        //[Test]
        //public void TestInjectClassImplementingMultipleClosedTypes()
        //{
        //    var builder = new ContainerBuilder();

        //    var executingAssembly = Assembly.GetExecutingAssembly();
        //    builder.RegisterAssemblyTypes(executingAssembly)
        //        .Where(t => typeof(IBaseInterface).IsAssignableFrom(t))
        //        .AsClosedTypesOf(typeof(IBaseInterface<,>));

        //    var container = builder.Build();

        //    var visitors = container.Resolve<>();
        //}
    }
}
