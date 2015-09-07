using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Autofac;
using NUnit.Framework;
using NVisitor.Api.Lazy;

namespace NCaseTest
{
    [TestFixture]
    public class DemoTests
    {
        public enum Gender {  Male, Female, Other }

        public interface IPerson
        {
            string Country { get; set; }
            int Age { get; set; }
            Gender Gender { get; set; }
        }

        [Test]
        public void SimpleTree()
        {
            ICaseBuilder builder = Case.GetBuilder();
            IPerson p = builder.GetContributor<IPerson>("p");
            
            ITree tree = builder.CreateSet<ITree>("tree");            
            using (tree.Define())
            {
                p.Country = "France";
                    p.Age = 0;
                        p.Gender = Gender.Female;
                        p.Gender = Gender.Male;
                    p.Age = 60;
                        p.Gender = Gender.Male;
                        p.Gender = Gender.Other;
                p.Country = "Germany";
                    p.Age = 10;
                        p.Gender = Gender.Male;
                    p.Age = 45;
                        p.Gender = Gender.Female;
            }


            foreach (Pause pause in builder.GetAllCases(tree))
                Console.WriteLine("{0,-7} | {1, -6} | {2,2}", p.Country, p.Gender, p.Age);
        }

        [Test]
        public void SimpleCardinalProduct()
        {
            ICaseBuilder builder = Case.GetBuilder();
            IPerson p = builder.GetContributor<IPerson>("p");

            var allCombinations = builder.CreateSet<IProd>("allCombinations");
            using (allCombinations.Define())
            {
                p.Country = "France";
                p.Country = "Germany";
                
                p.Age = 0;
                p.Age = 60;
                p.Age = 10;
                
                p.Gender = Gender.Female;
                p.Gender = Gender.Male;
                p.Gender = Gender.Other;
            }

            foreach (Pause pause in builder.GetAllCases(allCombinations))
                Console.WriteLine("{0,-7} | {1, -6} | {2,2}", p.Country, p.Gender, p.Age);
        }

    }
}

