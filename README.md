Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)](https://www.nuget.org/packages/NCase/)


NCase *(Pre-Alpha !!)*
======================

NCase allows you to define compact and readable test cases in C#.

When you write tests, you quickly reach a few dozen of combinations to test. 3 parameters having 4 remarkable states and you already have 64 test cases to test. That's why test frameworks like NUnit support parametrized tests. But even parametrized tests are hard to maintain. The tabular syntax used to define test parameters lacks of readability. That's where NCase comes to rescue: it provides several compact and readable syntaxes to write test cases. NCase use a record/replay mechanism of C# statements, so that you can directly use NCase in your C# code or unit tests.

Currently NCase supports following features:
- `ITree` set: allows you to define test cases with a simplified tree syntax
- `IProd` set: allows you to perform the cartesian product between multiple test dimensions
- Reference: allows you to inject test cases into other test cases, allowing to mix many `ITree` and `IProd` sets together, as well as to re-use test combinations
- Record/replay is supported on interface properties, including indexed properties

Let's see with an example how it works...

Example 1: Generating test cases from a tree
--------------------------------------------

All examples are from the [demo tests][demoTests].

Let's say, you want to test a routine performing money transfer, having for input: account balance in USD, currency and amount to transfer. You declare an interface `ITransfer`, which contain the parameters for a single test case:

```C#
public enum Curr {  EUR, USD, YEN }
public interface ITransfer
{
    double BalanceUsd { get; set; }   // the balance of the customer account in USD
    Curr Currency { get; set; }       // currency of the transfer
    double Amount { get; set; }       // amount of the transfer
    bool Accepted { get; set; }       // ASSERT: Expected result of the routine under test
}
```

Now, let's write a few tests with NCase:

```C#
ICaseBuilder builder = Case.GetBuilder();
ITransfer t = builder.GetContributor<ITransfer>("t");

ITree transfers = builder.CreateSet<ITree>("transfers");
using (transfers.Define())
{
    t.Currency = Curr.USD;
        t.BalanceUsd = 100.00;
            t.Amount =   0.01;  t.Accepted = true;
            t.Amount = 100.00;  t.Accepted = true;
            t.Amount = 100.01;  t.Accepted = false;
        t.BalanceUsd =   0.00;  
            t.Amount =   0.01;  t.Accepted = false;
    t.Currency = Curr.YEN;      
        t.BalanceUsd =   0.00;  
            t.Amount =   0.01;  t.Accepted = false;
    t.Currency = Curr.EUR;      
        t.BalanceUsd = 100.00;  
            t.Amount =   0.01;  t.Accepted = true;
            t.Amount =  89.39;  t.Accepted = true;
            t.Amount =  89.40;  t.Accepted = false;
}

```

First you call `Case.GetBuilder()` to get a builder, the swiss-knife of NCase.

Then you call `builder.GetContributor<ITransfer>()` in order to get a contributor of type `ITransfer`. A contributor is a variable that will contribute to the construction of the test cases. Under the hood, you get a dynamic proxy of type `ITransfer`, which will have the ability to record/replay assignments in the same way as mocks do in mocking frameworks.

Then you call `builder.CreateSet<ITree>("anything")` in order to create a set of test cases of type `ITree`. `ITree` enables you to define the test cases with a tree structure. Each single test case is represented by the path between a leaf and the root. `"anything"` is simply a display name for the tree.

Once you have created the `ITree` instance, you call `using (tree.Define())` and define the tree within the using block. The `ITree` set expects a well-defined syntax: Every time that a contributor's property is assigned, it extends the current case with a new fact (the assignment itself). If the property was already defined on the way up to the root, then if performs a branching of the tree at the level where the last assignment was performed. As a result the assignment of a property are all siblings in the tree. In the example, we use indentation to highlight the tree structure.

You can now generate the cases one by one, by calling `builder.GetAllCases(tree)` and iterate the returned enumerable: 
```C#
foreach (Pause pause in builder.GetAllCases(allTransfers))
    Console.WriteLine("{0} | {1} | {2} | {3}", t.Currency, t.BalanceUsd, t.Amount, t.Accepted);
```

At each iteration, the builder replays a test case, by settings the contributor's properties back to the expected values... So that you can use them for whatever you need... 

The console lists all test cases (with additional formatting): 
```
CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED
---------|-------------|--------|---------
     USD |      100,00 | 000,01 | True    
     USD |      100,00 | 100,00 | True    
     USD |      100,00 | 100,01 | False   
     USD |      000,00 | 000,01 | False   
     EUR |      000,00 | 000,01 | False   
     EUR |      100,00 | 000,01 | True    
     EUR |      100,00 | 089,39 | True    
     EUR |      100,00 | 089,40 | False   
```
In practice, you would call the system under test (SUT) as following:

```C#
foreach (Pause pause in builder.GetAllCases(allTransfers))
{
    bool accepted = _sut.PerformTransfer(t.BalanceUsd, t.Currency, t.Amount);
	Assert.AreEqual(t.Accepted, accepted);
}
```

Or equivalently inject the values into a parametrized test.


Example 2: Generating all combinations (cartesian product)
--------------------------------------

If you want to get all combinations between different parameters, then you need `IProd`. `IProd` performs the cartesian product of all dimensions it finds in its definition. 

For the purpose of this test, we add two properties to `ITransfer` as following:
```C#
public enum Card { Visa, Mastercard, Maestro } // new
public interface ITransfer
{
    (...)
    string DestBank { get; set; }  // new: The name of the destination bank
    Card Card { get; set; }        // new: the card type used to perform the payment
}
```

Now, you can generate all combinations between a set of bank and the set of cards, as following:

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
```

`IProd` expects a well-defined syntax. All contiguous assignments to a contributor's property are grouped together. `IProd` performs the cartesian product between all these these groups.

Again, you can generate all test cases and print them out:

```C#
foreach (Pause pause in builder.GetAllCases(cardsAndBanks))
    Console.WriteLine("{0} | {1}", t.DestBank, t.Card);
```

The console lists all combinations (with additional formatting): 
```
DEST_BANK     | CARD       
--------------|------------
HSBC          | Visa      
HSBC          | Mastercard
HSBC          | Maestro   
Unicredit     | Visa      
Unicredit     | Mastercard
Unicredit     | Maestro   
Bank of China | Visa      
Bank of China | Mastercard
Bank of China | Maestro   
```

Example 3: References to set of cases
--------------------------

NCase allows you to inject existing test cases by reference. You can for example produce the cartesian product between the test cases of *Example 1*, and those of *Example 2*, as following:

```C# 
IProd transferForAllcardsAndBanks = builder.CreateSet<IProd>("transferForAllcardsAndBanks");
using (transferForAllcardsAndBanks.Define())
{
    transfers.Ref();
    cardsAndBanks.Ref();
}
``` 

You get now 72 test cases, which you can print out into the console:

```C#
foreach (Pause pause in builder.GetAllCases(transferForAllcardsAndBanks))
{
    Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", 
        t.DestBank, t.Card, t.Currency, t.BalanceUsd, t.Amount, t.Accepted);
}
```

The console lists all test cases (with additional formatting): 
```
DEST_BANK     | CARD       | CURRENCY | BALANCE_USD | AMOUNT | ACCEPTED
--------------|------------|----------|-------------|--------|---------
HSBC          | Visa       |      USD |      100,00 | 000,01 | True    
HSBC          | Mastercard |      USD |      100,00 | 000,01 | True    
HSBC          | Maestro    |      USD |      100,00 | 000,01 | True    
Unicredit     | Visa       |      USD |      100,00 | 000,01 | True    
Unicredit     | Mastercard |      USD |      100,00 | 000,01 | True    
Unicredit     | Maestro    |      USD |      100,00 | 000,01 | True    


                ... 78 test cases ...


Bank of China | Visa       |      EUR |      100,00 | 089,40 | False   
Bank of China | Mastercard |      EUR |      100,00 | 089,40 | False   
Bank of China | Maestro    |      EUR |      100,00 | 089,40 | False  
```

Assuming that the destination bank and the card don't impact the `Accepted` asserts, you can keep the same code to test the system under test:

```C#
foreach (Pause pause in builder.GetAllCases(transferForAllcardsAndBanks))
{
    bool accepted = _sut.PerformTransfer(t.BalanceUsd, t.Currency, t.Amount, t.Card, t.DestBank);
	Assert.AreEqual(t.Accepted, accepted);
}
```

Refactoring with multiple contributors
--------------------------------------

... example coming soon ...

Coming features
---------------

- Support record/replay on class instances, including properties and methods
- `IPairwise` case set: allows you to generate only the pairwise combinations between multiple test dimensions
- `IPermutation` case set: allows you to generate all permutations within a set of actions
- `IPairwisePermutation` case set: allows you to generate permutations to ensure that any pair of action A1 and A2 is tested in both order (A1, A2) and (A2, A1)
- Improved tree syntax to support free tree structure

[demoTests]: https://github.com/jeromerg/NCase/blob/master/src/NCaseTest/DemoTests.cs
