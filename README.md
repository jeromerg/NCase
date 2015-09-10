Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)]()


NCase *(Pre-Alpha !!)*
======================

NCase allows you to define compact and readable test cases in C#.

When you write tests, you quickly reach a few dozen of combinations to test. 3 parameters having 4 remarkable states and you already have 64 test cases to test. That's why test frameworks like NUnit support parametrized tests. But even parametrized tests are hard to maintain. The tabular syntax used to define test parameters lacks of readability. That's where NCase comes to rescue: it provides several compact and readable syntaxes to write test cases. NCase use a record/replay mechanism of C# statements, so that you can directly use NCase in your C# code or unit tests.

Currently NCase supports following features:
- `ITree`: allows you to define test cases with a simplified tree syntax
- `IProd`: allows you to perform the cartesian product between multiple test dimensions
- Reference: allows you to inject test cases into other test cases, allowing to mix many `ITree` and `IProd` sets together, as well as to re-use test combinations
- Record/replay is supported on interface properties, including indexed properties

Let's see with an example how it works...

Example 1: Generating test cases from a tree
--------------------------------------------

All examples are from the [demo tests][demoTests].

Let's say, you want to test a function accepting or rejecting money transfer depending on:
- The balance of the source account in US Dollar
- The amount of the transfer 
- The currency of the transfer

So you want to create a few test cases testing the critical situations. Here is how you do it with NCase:

```C#
ICaseBuilder builder = Case.GetBuilder();
ITransfer t = builder.GetContributor<ITransfer>("t");

ITree allTransfers = builder.CreateSet<ITree>("transfers");
using (allTransfers.Define())
{
    t.Currency = Curr.USD;
        t.BalanceUsd = 100.00;
            t.Amount =   0.01;  t.Accepted = true;
            t.Amount = 100.00;  t.Accepted = true;
            t.Amount = 100.01;  t.Accepted = false;
        t.BalanceUsd =    0.0;
            t.Amount =   0.01;  t.Accepted = false;
    t.Currency = Curr.EUR;
        t.BalanceUsd =   0.00;
            t.Amount =   0.01;  t.Accepted = false;
    t.Currency = Curr.EUR;
        t.BalanceUsd = 100.00;
            t.Amount =   0.01;  t.Accepted = true;
            t.Amount =  89.39;  t.Accepted = true;
            t.Amount =  89.40;  t.Accepted = false;
}

Console.WriteLine("CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
Console.WriteLine("---------|-------------|--------|---------");
foreach (Pause pause in builder.GetAllCases(allTransfers))
{
    Console.WriteLine("{0,8} | {1,11:000.00} | {2,6:000.00} | {3,-8}", 
                      t.Currency, t.BalanceUsd, t.Amount, t.Accepted);
}

// Console Output: 
// CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED
// ---------|-------------|--------|---------
//      USD |      100,00 | 000,01 | True    
//      USD |      100,00 | 100,00 | True    
//      USD |      100,00 | 100,01 | False   
//      USD |      000,00 | 000,01 | False   
//      EUR |      000,00 | 000,01 | False   
//      EUR |      100,00 | 000,01 | True    
//      EUR |      100,00 | 089,39 | True    
//      EUR |      100,00 | 089,40 | False   
```

First you call `Case.GetBuilder()` to get a builder, the swiss-knife of NCase.

Then you call `builder.GetContributor<ITransfer>()` in order to get a contributor of type `ITransfer`. A contributor is a variable that will contribute to the construction of the test cases. `ITransfer` is a locally defined interface that contains four properties:

```C#
public enum Curr {  EUR, USD, YEN }
public interface ITransfer
{
    Curr Currency { get; set; }       // currency of the transfer
    double Amount { get; set; }       // amount of the transfer
    double BalanceUsd { get; set; }   // the balance of the customer account in USD
    bool Accepted { get; set; }       // Expected result of the function under test
}
```

Under the hood, you get a dynamic proxy of `ITransfer`, which will have the ability to record/replay assignments as mocks do in mocking frameworks.

Then you call `builder.CreateSet<ITree>("anything")` in order to create a set of type `ITree`. `ITree` enables you to define the test cases with a tree structure. Each a single test case is represented by the path between a leaf and the root. `"anything"` is simply a display name for the tree.

Once you have created the `ITree` instance, you call `using (tree.Define())` and define the tree within the using block. The `ITree` set expects a well-defined syntax: Every time that a contributor's property is assigned, it extends the current case with a new fact (the assignment itself). If the property was already defined on the way up to the root, then if performs a branching of the tree at the level where the last assignment was performed. As a result the assignment of a property are all siblings in the tree. In the example, we use indentation to highlight the tree structure.

You can now generate the cases one by one. For that purpose, you call `builder.GetAllCases(tree)` and iterate the returned enumerable. At each iteration, the builder replays a test case, by settings the contributor's properties back to the expected values... So that you can read the properties and use them for whatever you need...

Example 2: Generating all combinations (cartesian product)
--------------------------------------

If you want to systematically test all the combinations of different parameters, then you need `IProd`. `IProd` performs the cartesian product of all dimensions it finds in its definition. 

For the purpose of this test, we add two properties to the `ITransfer` interface as following:
```C#
public enum Card { Visa, Mastercard, Maestro }
public interface ITransfer
{
    (...)
    string DestBank { get; set; }  // The name of the destination bank
    Card Card { get; set; }        // the card type used to perform the payment
}
```

Now we combine a few banks with all types of credit cards:

```C#
IProd cardsAndBanks = builder.CreateSet<IProd>("cardsAndBank");
using (cardsAndBanks.Define())
{
    t.DestBank = "HSBC";
    t.DestBank = "Unicredit";
    t.DestBank = "Bank of China";

    t.Card = Card.Visa;
    t.Card = Card.Mastercard;
    t.Card = Card.Maestro;
}

Console.WriteLine("DEST_BANK     | CARD       ");
Console.WriteLine("--------------|------------");
foreach (Pause pause in builder.GetAllCases(cardsAndBanks))
{
    Console.WriteLine("{0,-13} | {1,-10}", t.DestBank, t.Card);
}

// DEST_BANK     | CARD       
// --------------|------------
// HSBC          | Visa      
// HSBC          | Mastercard
// HSBC          | Maestro   
// Unicredit     | Visa      
// Unicredit     | Mastercard
// Unicredit     | Maestro   
// Bank of China | Visa      
// Bank of China | Mastercard
// Bank of China | Maestro   
```

`IProd` expects a well-defined syntax. All assignments to a contributor's property are grouped together. `IProd` performs the cartesian product between all these these groups.

Example 3: References to set of cases
--------------------------

In Example 1, we defined a few tests: based on the balance of the account, the amount to transfer and the transfer currency, we tested whether the transfer function under test accepts or reject the transfer. 

In example 2, we defined a possible environment with 3 different banks and all possible credit cards. If you want to ensure that the destination bank `DestBank` and the credit card `Card` don't impact the asserts of the first test, then you can combine the two sets together with the cartesian product. NCase allows you to reuse the test cases by references, as following:

```C# 
IProd transferForAllcardsAndBanks = builder.CreateSet<IProd>("transferForAllcardsAndBanks");
using (transferForAllcardsAndBanks.Define())
{
    transfers.Ref();
    cardsAndBanks.Ref();
}
``` 

You get now 72 test cases, testing the same money transfer defined in *Example 1*, in all environments defined in *Example 2*!

To have fun, you can print out all cases:

```C#
Console.WriteLine("DEST_BANK     | CARD       | CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED");
Console.WriteLine("--------------|------------|----------|-------------|--------|---------");
foreach (Pause pause in builder.GetAllCases(transferForAllcardsAndBanks))
{
    Console.WriteLine("{0,-13} | {1,-10} | {2,8} | {3,11:000.00} | {4,6:000.00} | {5,-8}", 
        t.DestBank, t.Card, t.Currency, t.BalanceUsd, t.Amount, t.Accepted);
}

// Console Output:
//
// DEST_BANK     | CARD       | CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED
// --------------|------------|----------|-------------|--------|---------
// HSBC          | Visa       |      USD |      100,00 | 000,01 | True    
// HSBC          | Mastercard |      USD |      100,00 | 000,01 | True    
// HSBC          | Maestro    |      USD |      100,00 | 000,01 | True    
// Unicredit     | Visa       |      USD |      100,00 | 000,01 | True    
// Unicredit     | Mastercard |      USD |      100,00 | 000,01 | True    
// Unicredit     | Maestro    |      USD |      100,00 | 000,01 | True    
//
//
//                 ... 78 test cases ...
//
//
// Bank of China | Visa       |      EUR |      100,00 | 089,40 | False   
// Bank of China | Mastercard |      EUR |      100,00 | 089,40 | False   
// Bank of China | Maestro    |      EUR |      100,00 | 089,40 | False  
```

Refactoring with multiple contributors
--------------------------------------

... under construction ...

Coming features
---------------

- Support record/replay on class instances, including properties and methods
- `IPairwise` case set: allows you to generate only the pairwise combinations between multiple test dimensions
- `IPermutation` case set: allows you to generate all permutations within a set of actions
- `IPairwisePermutation` case set: allows you to generate permutations to ensure that any pair of action A1 and A2 is tested in both order (A1, A2) and (A2, A1)
- Improved tree syntax to support free tree structure

[demoTests]: https://github.com/jeromerg/NCase/blob/master/src/NCaseTest/DemoTests.cs