Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)]()


# NCase *[Pre-Alpha !! Under Construction]*

Case Builder for .Net: generates all cases from various case set definitions. Multiple sets of cases can be combined together with operators like the cardinal product operator.

Here is an example (see unit test to get the code).

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

First you need a case-builder. A case-builder is the swiss knife of NCase. You get it by calling `Case.GetBuilder()`.

In order to generate cases, you need variables. In NCase, variables are called contributors, as they contribute to the construction of the cases. You call `builder.GetContributor<IPerson>()` and you get a contributor of type `IPerson`. `IPerson` is a locally defined interface that contains three properties: `Country`, `Age`, `Gender`. Under the hood, the instance is a dynamic proxy of `IPerson`, which will have the ability to record/replay assignments similarly to mocks do in mocking frameworks.

Next you need to choose the kind of case-set you want to define. In this example we want to define a tree. You call `builder.CreateSet<ITree>("anything")` and you get an instance of `ITree` tree. `anything` is a simple display name used as a label of the tree.

Now you need to define the content of the tree. You call `using(tree.Define())` and you place the definition of the tree within the using block. The `ITree` case-set expects a well-defined syntax: here we only assign values to contributor's properties. Every time that a property is assigned, it extends the current case with a new fact (the assignment). If the property was already defined on the way up to the root, then if performs a branching of the tree at the level where the last assignment was performed. As a result the assignement of a property are all siblings in the tree. In the example, we use indentation to highlight the tree structure.

That's it! You can now generate the cases one by one. For that purpose, you call `builder.GetAllCases(tree)` and iterate the enumerable. At each iteration the builder replays a case, sets the properties to the expected values and gives you the hand. In the foreach loop you can read the contributor properties and use them for whatever you need...

Enjoy! (But warning! It is a pre-alpha development)

### Generating cases with the cardinal product operator

When you need to generate cases, you quite often need first to generate all combinations between two sets... it is the so called cardinal product.

To generate the cases of a cardinal product, you use `IProd`, another case-set, as following:

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

Again, `IProd` expects a well-defined syntax. Here we use it in combination with contributor's assignments. Each property build a set of assignments. And the `IProd` performs the cardinal product between all these sets pairwise.
