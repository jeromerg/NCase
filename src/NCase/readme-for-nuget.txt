Congratulation, you have succesfully installed *NCase*
=========================================================

For more information, look at: 

    http://github.com/jeromerg/NCase

You can have a look to the demo unit tests:

    https://github.com/jeromerg/NCase/blob/master/src/NCaseTest/DemoTests.cs
	
If you are impatient, simply copy&paste the following code into an empty C# console application:

```
using System;
using NCase.Api;
using NCase.Autofac;
using NVisitor.Api.Lazy;

namespace NCase.Test
{
    public enum Gender {  Female, Male, Other }
    public interface IPerson 
    {
        string Country { get; set; }
        int    Age     { get; set; }
        Gender Gender  { get; set; }
    }

    class Program
    {        
        static void Main(string[] args)
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
                Console.WriteLine("{0,-7} | {1,2} | {2, -6}", p.Country, p.Age, p.Gender);
        }
    }
}
```