Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)]()


# NCase *(Pre-Alpha !!)*

NCase allows you to define compactly one or more sets of cases directly in C#. While you iterate the set of cases, each case is written into C# objects. You can freely use them to perform any further actions.

A typical usage is to inject the cases into a parametrized test. On a system under test (SUT) with a few inputs (dimensions) you quickly reach a few dozen of cases to test (i.e. 3 dimensions with 4 values generate 4 x 4 x 4 = 64 test cases). 64 unit tests are hard to write and to maintain. That's why test frameworks like NUnit support parametrized tests. But even parametrized are hard to maintain, because you have to define all cases one by one, repeating almost everything from one case to the other. That's where NCase comes to rescue: you can simplify the cases definition by using trees. You can combine multiple set of cases together and easily generate the cartesian product of them.

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
    Console.WriteLine("{0,-7} | {1, -6} | {2,2}", p.Country, p.Age, p.Gender);

// Console:
// France  |  0 | Female
// France  |  0 | Male   
// France  | 60 | Male   
// France  | 60 | Other  
// Germany | 10 | Male   
// Germany | 45 | Female
```

First you call `Case.GetBuilder()` to get a builder, the swiss-knife of NCase.

Then you call `builder.GetContributor<IPerson>()` in order to get a contributor of type `IPerson`. A contributor is a variable that will contribute to the construction of the cases. `IPerson` is a locally defined interface that contains three properties: `Country`, `Age`, `Gender`. Under the hood, you get a dynamic proxy of `IPerson`, which will have the ability to record/replay assignments similarly to mocks do in mocking frameworks.

Then you call `builder.CreateSet<ITree>("anything")` in order to create a set of type `ITree`. `ITree` enables you to define the cases via a tree structure. Each path from a leaf to the root represents a case.

Once you have created the `person` and the `tree`, you call `using (tree.Define())` and write the definition of the tree within the using block. The `ITree` case-set expects a well-defined syntax. Every time that a property of `tree` is assigned, it extends the current case with a new fact (the assignment itself). If the property was already defined on the way up to the root, then if performs a branching of the tree at the level where the last assignment was performed. As a result the assignment of a property are all siblings in the tree. In the example, we use indentation to highlight the tree structure.

That's it! You can now generate the cases one by one. For that purpose, you call `builder.GetAllCases(tree)` and iterate the returned enumerable. At each iteration the builder replays a single case, by settings the properties to the expected values. So you can read the properties and use them for whatever you need...

Enjoy! (But warning! It is a pre-alpha development)

### Generating all combinations of cases

When you need to generate cases, you quite often need first to generate all combinations between two sets, the so called cartesian product of two sets.

The most simple way to generate a cartesian product is by using the `IProd` case-set:

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
//    France  |  0 | Female
//    France  |  0 | Male   
//    France  |  0 | Other  
//    France  | 60 | Female
//    France  | 60 | Male   
//    France  | 60 | Other  
//    France  | 10 | Female
//    France  | 10 | Male   
//    France  | 10 | Other  
//    Germany |  0 | Female
//    Germany |  0 | Male   
//    Germany |  0 | Other  
//    Germany | 60 | Female
//    Germany | 60 | Male   
//    Germany | 60 | Other  
//    Germany | 10 | Female
//    Germany | 10 | Male   
//    Germany | 10 | Other  
```

`IProd` expects a well-defined syntax. The assignment to a property are grouped together. `IProd` performs the cartesian product between all these these groups.

## Roadmap Pre-Alpha
- Dev: Refactor AddChild() by using `PairVisitor`
- Documentation: add doc about References
- Improve dump functionality
- Documentation: show Dump functionality
- Implement pairwise set (similar to cartesian product)
- Support method-call of existing instance (of interface, or virtual method)
- Support permutation set
- Support pairwise permutation set
- Documentation: show how to use permutation with method-call of existing instance
- Improve tracing capability
- Improve Unit test coverage
- Implement alternative syntax based on operator-override
