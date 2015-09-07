Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)]()


# NCase *[Pre-Alpha !! Under Construction]*

Case Builder for .Net: generates all cases from a tree of cases. Multiple sets of cases can be combined together with operators like the cardinal product operator, the pair-wise operator...

Here is an example (see unit test to get the code):

### Generating cases from a tree

```C#
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

// Console:
// France  | Female |  0
// France  | Male   |  0
// France  | Male   | 60
// France  | Other  | 60
// Germany | Male   | 10
// Germany | Female | 45
```

First you get a builder, the swiss-knife of NCase.

In order to generate cases, you need variables. In NCase, variables are called contributors, as they contribute to the construction of the cases. So you ask the builder to provide a contributor of type `IPerson`. `IPerson` is a locally defined interface that contains three properties: `Country`, `Age`, `Gender`. `GetContributor<IPerson>()` returns a dynamic proxy of `IPerson`, that will have the ability to record/replay similarly to mock in mocking frameworks.

Now you need to define a set of cases. In this example we want to define it via a tree, where each level corresponds to one property. So we create a set of type `ITree`. This is the most simple case-set currently. NCase has been designed from the ground up to enable adding easily new types of case-set and new operators... So new sets will come soon... You define the tree within a using block.

That's it! You can come back to the swiss-knife and ask him to generate the cases one by one. You call the `GetAllCases()` method, that parses, transforms the tree definition and finally generates the cases one by one. Actually, it doesn't generate anything, as `GetAllCases()` returns a dummy instance of type `Pause`. It plays the case, by setting the properties to the expected values, so that you can call them during the pause...

Enjoy! (But warning! It is a pre-alpha development)

### Generating cases from a cardinal product

When you need to generate cases, you quite often need first to generate all combinations between two sets... it is the so called cardinal product.

The most simple way to generate a cardinal product is by using the `IProd` case-set:

```C#
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

// Console:
//    France  | Female |  0
//    France  | Male   |  0
//    France  | Other  |  0
//    France  | Female | 60
//    France  | Male   | 60
//    France  | Other  | 60
//    France  | Female | 10
//    France  | Male   | 10
//    France  | Other  | 10
//    Germany | Female |  0
//    Germany | Male   |  0
//    Germany | Other  |  0
//    Germany | Female | 60
//    Germany | Male   | 60
//    Germany | Other  | 60
//    Germany | Female | 10
//    Germany | Male   | 10
//    Germany | Other  | 10
```

## Roadmap Pre-Alpha
- Support method-call of existing instance (of interface, or virtual method)
- Support indexed Items property
- Improve tracing capability
- re-factor some places missing visitors
- Unit test for all success and failing cases
- Improve debug information
- Implement alternative syntax based on operator-override
- think about NCase as alternative to mocking?
