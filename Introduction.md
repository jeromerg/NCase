Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)](https://www.nuget.org/packages/NCase/)

NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase is a mix between a Mocking Framework like [Moq][Moq] and a parametrized test framework, having advanced combinatorial capabilities. 

Installation
------------

In the Nuget Package Manager Console:

In combination with NUnit:

```
Install-Package NCase, NCase.NunitAdapter
```

In combination with XUnit:

```
Install-Package NCase, NCase.XunitAdapter
```

NCase is not officially released yet: Further commits may introduce breaking changes.  

Comparison to Moq: The first test case 
----------------------------------------

Let's say, you need to test a method called `TodoManager.AddTodo(ITodo todo)`.

A simple test would typically look like this:  

<!--# MoqExample1 -->
```C#
// ARRANGE
var mock = new Mock<ITodo>();
mock.SetupAllProperties();
ITodo todo = mock.Object;

todo.Title = "Don't forget to forget NCase";
todo.DueDate = now;
todo.IsDone = false;

// ACT
var todoManager = new TodoManager();
todoManager.AddTodo(todo);

// ASSERT
//...
```

You recognize the three blocks *Arrange, Act, Assert* as well as the use of [Moq][Moq] to provide an instance of `ITodo`. Until now, nothing new hopefully.


Now look how you write the same test with NCase:

<!--# NCaseExample1 -->
```C#
// ARRANGE
var builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var todoSet = builder.NewCombinationSet("todoSet");

using (todoSet.Define())
{
    todo.Title = "Don't forget to forget NCase";

    todo.DueDate = now;

    todo.IsDone = false;
}

todoSet.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var todoManager = new TodoManager();
    todoManager.AddTodo(todo);

    // ASSERT
    //...
});
```

Both tests look like very similar. You recognize the same blocks *Arrange, Act, Assert*. But NCase is a little bit more verbose: 

- The mock has been replaced by something called a *contributor* 
- The contributor's properties are set inside a block, which amazingly looks like a definition
- And finally the *Act and Asserts* are located inside a statement lambda passed to a method called `ActAndAssert`

Have I got your attention? Now, let's see the power of the few new lines... 

The second test case
--------------------

Let's say, you now need to implement additional test cases. No surprise: it is always the same! Usually, you typically perform a copy&paste, keep all mocked properties unchanged except one:

<!--# MoqExample2 -->
```C#
[Test]
public void MoqTest1()
{
    // ARRANGE
    var mock = new Mock<ITodo>();
    mock.SetupAllProperties();
    ITodo todo = mock.Object;

    todo.Title = "Don't forget to forget NCase";
    todo.DueDate = now;            
    todo.IsDone = false;

    // ACT ... ASSERT ...
}

[Test]
public void MoqTest2()                                 // DUPLICATED TEST
{
    // ARRANGE
    var mock = new Mock<ITodo>();
    mock.SetupAllProperties();
    ITodo todo = mock.Object;

    todo.Title = "Another todo to never forget NCase!"; // CHANGE
    todo.DueDate = now;
    todo.IsDone = false;

    // ACT ... ASSERT ...
}
```

But check this out! With NCase, you only need to add the single following line:

<!--# NCaseExample2_AddedLine -->
```C#
todo.Title = "Another todo to never forget NCase";
```

Thus, the previous NCase test becomes:

<!--# NCaseExample2 -->
```C#
// ARRANGE
var builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var todoSet = builder.NewCombinationSet("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget NCase";
    todo.Title = "Another todo to never forget NCase";  // SINGLE ADDITION!!!

    todo.DueDate = now;

    todo.IsDone = false;
}

todoSet.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var todoManager = new TodoManager();
    todoManager.AddTodo(todo);

    // ASSERT
    //...
});
```

Like a sorcerer, NCase calls the *Act and Assert* statements twice exactly in the same way as `MoqTest1` and `MoqTest2` do!

Why? Because the Arrange statements are not executed directly, but recorded first inside a definition, building a set of test cases. Following rules apply:
- Consecutive lines build a *union set* of test cases
- An empty line build a cartesian product between two union sets
- An indentation build a branch

Once the definition has been recorded, the call to `set.Cases().Replay().ActAndAssert(...)` generates and replays each test case and calls the *Act and Assert* statements.


*Remark*

- To be aware of empty lines and line indentation, NCase parses again the C# file at runtime. For that purpose it needs the PDB file of the assembly and the related source files. Most of the time, Unit Tests are executed in environments that comply with both restrictions. If you need an alternative solution, you could provide your own `IFileCache` or `IFileAnalyzer` implementation at NCase initialization. NCase is 100% DI-Container based, so you can override most behaviors of it. 

Many test cases
---------------

NCase is stupidly systematic: You may add as many assignments to `Task`, `DueDate` and `IsDone` as you wish, and NCase will generate and test all possible combinations. For example, the following lines will generate and test 6 x 7 x 2 = **84 test cases!!**


<!--# NCaseExample3 -->
```C#
// ARRANGE
var builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var todoSet = builder.NewCombinationSet("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget NCase";
    todo.Title = "";
    todo.Title = null;
    todo.Title = "ß üöä ÜÖÄ !§$%&/()=?";
    todo.Title = "SELECT USER_ID, PASSWORD FROM USER";
    todo.Title = "Another Title";

    todo.DueDate = yesterday;
    todo.DueDate = now;
    todo.DueDate = tomorrow;
    todo.DueDate = DateTime.MaxValue;
    todo.DueDate = DateTime.MinValue;
    todo.DueDate = daylightSavingTimeMissingTime;
    todo.DueDate = daylightSavingTimeAmbiguousTime;

    todo.IsDone = false;
    todo.IsDone = true;
}

todoSet.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var todoManager = new TodoManager();
    todoManager.AddTodo(todo);

    // ASSERT
    //...
});
``` 

Combining Contributors
----------------------

In a definition, you can mix any contributors.

Imagine `TodoManager.AddTodo(...)` requires an additional argument, for example the user to assign the todo to:

```C#
TodoManager.AddTodo(ITodo todo, IUser assignee)
```

So, you need a new contributor of type `IUser`:

<!--# NCaseCombiningContributors_VAR -->

```C#
var user = builder.NewContributor<IUser>("user");
```

So that you can extend the existing the definition, as follows:

<!--# NCaseCombiningContributors_DEF -->
```C#
using (wholeSet.Define())
{
    todo.Title = "Don't forget to forget NCase";
    //... alternatives

    todo.DueDate = yesterday;
    //... alternatives

    todo.IsDone = false;
    //... alternatives

    user.IsActive = true;
    //... alternatives

    user.Email = null;
    //... alternatives
}
```

This definition generates the cartesian product of all groups of property assignments for both contributors. It simply works!

Combining Sets
--------------

NCase has a powerful combinatorial engine. You can define multiple combinatorial sets, and, you can combine them together! 

For example, you can split the previous arrange statements into two subsets. 

You first define the set of cases related to the `todo` contributor:

<!--# NCaseCombiningSets_TODO_SET -->
```C#
var todoSet = builder.NewCombinationSet("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget NCase";
    //... and alternatives

    todo.DueDate = yesterday;
    //... and alternatives

    todo.IsDone = false;
    //... and alternatives
}
```

Then you define the set of cases related to the `user` contributor:

<!--# NCaseCombiningSets_USER_SET -->
```C#
var userSet = builder.NewCombinationSet("userSet");
using (userSet.Define())
{
    user.IsActive = true;
    //... and alternatives

    user.Email = null;
    //... and alternatives

}
```

Finally, you **combine both sets together** as follows:

<!--# NCaseCombiningSets_ALL_SET -->
```C#
var allSet = builder.NewCombinationSet("allSet");
using (allSet.Define())
{
    todoSet.Ref();

    userSet.Ref();
}
```

The result is the same set of test cases as in the previous example, but the definition of test cases has been split into two sub-sets. 

Why do you need to split a definition? In order to acquire greater flexibility! Indeed, now, you can:

- Re-use each sub-set individually
- Use alternative definitions for each sets. as you will see now...

Tackle complexity with Pairwise testing
---------------------------------------

Testing all combinations is nice, but it is expensive. We generated before 84 test cases with only three properties and a few values for each one. You can tell to the `CombinationSet` to substitute the pairwise product to the cartesian product. It generates a set of test cases, that contains all possible pairs between all union set (more about [pairwise testing here][pair]). 

<!--# NCasePairwiseCombinations -->
```C#
var todoSet  = builder.NewCombinationSet("todoSet", onlyPairwise: true);
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget NCase";
    //... alternatives

    todo.DueDate = yesterday;
    //... alternatives

    todo.IsDone = false;
    //... alternatives

}
```

You can mix test case sub-sets defined with both cartesian and pairwise product: 

<!--# NCaseCombiningSets_ALL_SET_Pairwise -->
```C#
var allSet = builder.NewCombinationSet("allSet");
using (allSet.Define())
{
    todoSet.Ref();    // Pairwise product!

    userSet.Ref();    // Default cartesian product
}
```

The result is a fine granular level of testing: By keeping the `userSet` as it is, you exhaustively test the `user` input. And by switching the `todoSet` to pairwise testing, you attain a more efficient, albeit superficial, level of testing of the `todo` input.

Tackle dedicated asserts: Tree Definition
-----------------------------------------

Cartesian and pairwise products are fantastic if you want to perform *n* times the same (the same!) test in *n* different input environments, like "this function must behave the same on all three x86, x64 and ARM CPUs". But you get a problem if you need to write tests which perform asserts depending on the input values, like `o = i1 * i2`. One solution is to rewrite a simplified logic of the system under test in your unit test to calculate the expectations. But you know, unit tests must have as few logic as possible...

NCase provides the ability to define test cases as a tree. Tree is a good alternative to the systematic automatic generation of test cases: you can factorize aspects and re-use statement all the way up to the root of the tree:    

The following lines of code illustrate how it works:

<!--# NCaseTree -->
```C#
var todo = builder.NewContributor<ITodo>("todo");
var isValid = builder.NewContributor<IHolder<bool>>("isValid");

var todoSet  = builder.NewCombinationSet("todoSet");
using (todoSet.Define())
{
    todo.Title = "forget me";
        isValid.Value = true;
            todo.DueDate = tomorrow;
                todo.IsDone = false;
            todo.DueDate = yesterday;
                todo.IsDone = false;
                todo.IsDone = true;
        isValid.Value = false;
            todo.DueDate = tomorrow;
                todo.IsDone = true;
}
```

On every indentation, NCase attaches the indented set of sub-cases as children of the previous statement. Here, at the root level,  we factorize the `Title` value. At the first tree level, we divide the sub-sets into two categories: the valid and invalid ones. And so on!

*Remark*
- By the way, note how you can create contributors of simple types, like `bool`, by using the interface `IHolder<T>`. This interface contains a single property `Value` allowing to record/replay any single value of any type.

### Explicit branching

Sometimes you need to introduce a branch, but cannot use the indentation as in the previous example. NCase solves this problem by providing the ability to declare explicit branches:

<!--# NCaseTree2 -->
```C#
var todo = builder.NewContributor<ITodo>("todo");
var isValid = builder.NewContributor<IHolder<bool>>("isValid");

var todoSet  = builder.NewCombinationSet("todoSet");
using (var d = todoSet.Define())
{
    todo.Title = "forget me";
        isValid.Value = true;
            todo.DueDate = tomorrow;
                todo.IsDone = false;
            todo.DueDate = yesterday;
                todo.IsDone = false;
                todo.IsDone = true;
    d.Branch();
        todo.Title = "remember me";
        todo.Title = "forgive me";

        isValid.Value = false;
            todo.DueDate = tomorrow;
                todo.IsDone = true;
}
```

We introduce an explicit branch by calling the `d.Branch()` on the instance returned by the `Define()` method. It forces NCase to build a new branch. In the example, we use to define a union set of two titles, with a common sub-tree, and the whole sub-set is joined by union to the previous sub-set.

## Visualize

NCase provides methods to help visualize what is going on, while you develop and execute tests.

#### Visualize Definition

If at some point, you get lost and don't understand what is going on, then first take a break, drink a coffee! And then print the definitions that you are trying to write with the help of the `PrintDefinition()` extension method:

<!--# Visualize_Def -->
```C#
string def = todoSet.PrintDefinition(isFileInfo: true);

Console.Write(def);
```

Result:

<!--# Visualize_Def_Console -->
```
 Definition                                   | Location                               
 -------------------------------------------- | -------------------------------------- 
 Combination Set 'todoSet'                    | c:\dev\NCase\Introduction.cs: line 394 
     todo.Title=forget me                     | c:\dev\NCase\Introduction.cs: line 396 
         isValid.Value=True                   | c:\dev\NCase\Introduction.cs: line 397 
             todo.DueDate=12.11.2011 00:00:00 | c:\dev\NCase\Introduction.cs: line 398 
                 todo.IsDone=False            | c:\dev\NCase\Introduction.cs: line 399 
             todo.DueDate=10.11.2011 00:00:00 | c:\dev\NCase\Introduction.cs: line 400 
                 todo.IsDone=False            | c:\dev\NCase\Introduction.cs: line 401 
                 todo.IsDone=True             | c:\dev\NCase\Introduction.cs: line 402 
         isValid.Value=False                  | c:\dev\NCase\Introduction.cs: line 403 
             todo.DueDate=12.11.2011 00:00:00 | c:\dev\NCase\Introduction.cs: line 404 
                 todo.IsDone=True             | c:\dev\NCase\Introduction.cs: line 405 
```

#### Visualize Test Cases as a Table

You can get a systematic overview of the generated test cases, by calling the `PrintCasesAsTable()`. This extension method displays a table containing all the test cases, line by line.

<!--# Visualize_Table -->
```C#
string table = todoSet.PrintCasesAsTable();

Console.Write(table);
```

Result:

<!--# Visualize_Table_Console -->
```
 # | todo.Title | isValid.Value |        todo.DueDate | todo.IsDone 
 - | ---------- | ------------- | ------------------- | ----------- 
 1 |  forget me |          True | 12.11.2011 00:00:00 |       False 
 2 |  forget me |          True | 10.11.2011 00:00:00 |       False 
 3 |  forget me |          True | 10.11.2011 00:00:00 |        True 
 4 |  forget me |         False | 12.11.2011 00:00:00 |        True 

TOTAL: 4 TEST CASES
```

#### Visualize Single Case Definition

You can print information about a single test case, by calling the `Print()` extension method on it. It prints the facts row by row. 

<!--# Visualize_Case -->
```C#
string cas = todoSet.Cases().First().Print();

Console.Write(cas);
```

Result:

<!--# Visualize_Case_Console -->
```
 Fact                             | Location                               
 -------------------------------- | -------------------------------------- 
 todo.Title=forget me             | c:\dev\NCase\Introduction.cs: line 396 
 isValid.Value=True               | c:\dev\NCase\Introduction.cs: line 397 
 todo.DueDate=12.11.2011 00:00:00 | c:\dev\NCase\Introduction.cs: line 398 
 todo.IsDone=False                | c:\dev\NCase\Introduction.cs: line 399 
```

Next Steps
==========

First, have fun with NCase! 

Then, please provide feedbacks, critiques, and suggestions! 

Finally, be aware that NCase is under continuous development. Some upcoming features are:

- Full mocking functionalities 
	- mocking of classes
	- mocking of methods
	- "[moq][moq] like" `Setup(...)` and `Verify(...)`
- Autonomous parametrized test framework (including Assert compatible with NCase record/replay mechanism, CLI, Visual Studio and Resharper adapter) 



[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
