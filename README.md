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

todo.Title = "Don't forget to forget";
todo.DueDate = now;
todo.IsDone = false;

// ACT
var todoManager = new TodoManager();
todoManager.CreateTodo(todo);

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
var set = builder.NewDefinition<AllCombinations>("set");

using (set.Define())
{
    todo.Title = "Don't forget to forget";
    todo.DueDate = now;
    todo.IsDone = false;
}

set.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var todoManager = new TodoManager();
    todoManager.CreateTodo(todo);

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

    todo.Title = "Don't forget to forget";
    todo.DueDate = now;
    todo.IsDone = false;

    // ACT ... ASSERT ...
}

[Test]
public void MoqTest2()                      // DUPLICATED TEST
{
    // ARRANGE
    var mock = new Mock<ITodo>();
    mock.SetupAllProperties();
    ITodo todo = mock.Object;

    todo.Title = "Another todo to forget"; // CHANGE
    todo.DueDate = now;
    todo.IsDone = false;

    // ACT ... ASSERT ...
}
```

But check this out! With NCase, you only need to add the single following line:

<!--# NCaseExample2_AddedLine -->
```C#
todo.Title = "Another todo to forget";
```

Thus, the previous NCase test becomes:

<!--# NCaseExample2 -->
```C#
// ARRANGE
var builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var set = builder.NewDefinition<AllCombinations>("set");
using (set.Define())
{
    todo.Title = "Don't forget to forget";
    todo.Title = "Another todo to forget";  // ADDITION

    todo.DueDate = now;
    todo.IsDone = false;
}

set.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var todoManager = new TodoManager();
    todoManager.CreateTodo(todo);

    // ASSERT
    //...
});
```

Like a sorcerer, NCase calls the *Act and Assert* statements twice exactly in the same way as `MoqTest1` and `MoqTest2` do!

Why? Because the Arrange statements are located inside a definition of type `AllCombinations`: the `AllCombinations` definition groups together subsequent assignments of the same property and performs a so called cartesian product between all groups. 

Finally the chain `set.Cases().Replay().ActAndAssert(...)` replays each test case and calls the *Act and Assert* statements.

Many test cases
---------------

NCase is stupidly systematic: You may add as many assignments to `Task`, `DueDate` and `IsDone` as you wish, and NCase will generate and test all possible combinations. For example, the following lines will generate and test 6 x 7 x 2 = **84 test cases!!**


<!--# NCaseExample3 -->
```C#
// ARRANGE
var builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var set = builder.NewDefinition<AllCombinations>("set");
using (set.Define())
{
    todo.Title = "Don't forget to forget";
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

set.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var todoManager = new TodoManager();
    todoManager.CreateTodo(todo);

    // ASSERT
    //...
});
``` 

Combining Contributors
----------------------

In NCase, you can mix any contributors in any definition.

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
using (set.Define())
{
    todo.Title = "Don't forget to forget";
    //... alternatives

    todo.DueDate = yesterday;
    //... alternatives

    todo.IsDone = false;
    //... alternatives

    user.IsActive = true;
    //... alternatives

    user.NotificationEmail = null;
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
var todoSet = builder.NewDefinition<AllCombinations>("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget";
    //... alternatives

    todo.DueDate = yesterday;
    //... alternatives

    todo.IsDone = false;
    //... alternatives
}
```

Then you define the set of cases related to the `user` contributor:

<!--# NCaseCombiningSets_USER_SET -->
```C#
var userSet = builder.NewDefinition<AllCombinations>("userSet");
using (userSet.Define())
{
    user.IsActive = true;
    //... alternatives

    user.NotificationEmail = null;
    //... alternatives

}
```

Finally, you **combine both sets together** as follows:

<!--# NCaseCombiningSets_ALL_SET -->
```C#
var allSet = builder.NewDefinition<AllCombinations>("allSet");
using (allSet.Define())
{
    todoSet.Ref();
    userSet.Ref();
}
```

The result is the same set of test cases as in the previous example, but the definition of test cases has been split into two sub-sets. 

Why do you need to split a definition? In order to acquire greater flexibility! Indeed, now, you can:

- Re-use each sub-set individually
- Use alternative definitions for each sets. Because `AllCombinations` is only one definition among others... as you will see now...

Tackle complexity with Pairwise testing
---------------------------------------

Testing all combinations is nice, but it is expensive. We generated before 84 test cases with only three properties and a few values for each one. Instead of generating all combinations with the `AllCombinations` definition, you can use the alternative definition called `PairwiseCombinations`. It generates a set of test cases, that contains all possible pairs between all groups of assignments (more about [pairwise testing here][pair]). 

Both definitions, `AllCombinations` and `PairwiseCombinations`, have exactly the same syntax, so you just need to change the name of the definition when you call `builder.NewDefinition<...>(...)`: 

<!--# NCasePairwiseCombinations -->
```C#
var todoSet = builder.NewDefinition<PairwiseCombinations>("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget";
    //... alternatives

    todo.DueDate = yesterday;
    //... alternatives

    todo.IsDone = false;
    //... alternatives

}
```

As with `AllCombinations`, you can combine this definition with others: 

<!--# NCaseCombiningSets_ALL_SET -->
```C#
var allSet = builder.NewDefinition<AllCombinations>("allSet");
using (allSet.Define())
{
    todoSet.Ref();
    userSet.Ref();
}
```

The result is a fine granular level of testing: By keeping the `userSet` as it is, you exhaustively test of the `user` input. And by switching the `todoSet` to a `PairwiseCombinations` definition, you attain a more efficient, albeit superficial, level of testing of the `todo` input.

Tackle dedicated asserts: Tree Definition
-----------------------------------------

If you need to perform asserts that depend on the input values, you have two alternatives. You can:

- Rewrite in a simplified form the logic of the system under test in your test, in order to calculate the expectations as a function of the input values
- Or you can provide the expected values along with the input values and pass both to the *Act and Assert* statements.

With the latter solution, you cannot automatically generate test cases with combinatorial operators, like `AllCombinations` or `PairwiseCombinations` because the expected values are bound to the input values. To solve this issue, NCase contains another definition called `Tree`. The `Tree` definition allows you to define a set of test cases by the mean of a tree.

The following lines of code illustrate how it works:

<!--# NCaseTree -->
```C#
var todo = builder.NewContributor<ITodo>("todo");
var isValid = builder.NewContributor<IHolder<bool>>("isValid");

var todoSet = builder.NewDefinition<Tree>("todoSet");
using (todoSet.Define())
{
    todo.Title = "forget";
        isValid.Value = true;
            todo.IsDone = false;
                todo.DueDate = yesterday;
                todo.DueDate = tomorrow;
        isValid.Value = false;
            todo.DueDate = yesterday;
                todo.IsDone = false;
    todo.Title = "*++**+*";
        isValid.Value = false;
            todo.IsDone = false;
                todo.DueDate = yesterday;
            todo.IsDone = true;
                todo.DueDate = tomorrow;
}
```

The `Tree` definition performs an implicit fork every times it encounters an assignment of an already assigned property, at the level where the property was assigned the last time. Every path from a leaf back to the root builds a test case. 

In the example, we mix the input values (`todo` instance) along with the expected values (`isValid` instance), illustrating how you simply define expected value along with input values.

### `IHolder<T>` Wrapper 
By the way, note how you can create contributors of simple types, like `bool`, by using the interface `IHolder<T>`. This interface contains a single property `Value` allowing to record/replay any value of any type.

## Visualize

NCase provides methods to help visualize what is going on, while you develop and execute tests.

#### Visualize Definition

If at some point, you get lost and don't understand what is going on, then first take a break, drink a coffee! And then print the definitions that you are trying to write with the help of the `PrintDefinition()` extension method:

<!--# Visualize_Def -->
```C#
string def = todoSet.PrintDefinition(isFileInfo: true);

Console.WriteLine(def);
```

Result:

<!--# Visualize_Def_Console -->
```
 Definition                                       | Location                         
 ------------------------------------------------ | -------------------------------- 
 Tree todoSet                                     | c:\dev\NCase\Readme.cs: line 373 
     todo.Title=forget                            | c:\dev\NCase\Readme.cs: line 375 
         isValid.Value=True                       | c:\dev\NCase\Readme.cs: line 376 
             todo.IsDone=False                    | c:\dev\NCase\Readme.cs: line 377 
                 todo.DueDate=10.11.2011 00:00:00 | c:\dev\NCase\Readme.cs: line 378 
                 todo.DueDate=12.11.2011 00:00:00 | c:\dev\NCase\Readme.cs: line 379 
         isValid.Value=False                      | c:\dev\NCase\Readme.cs: line 380 
             todo.DueDate=10.11.2011 00:00:00     | c:\dev\NCase\Readme.cs: line 381 
                 todo.IsDone=False                | c:\dev\NCase\Readme.cs: line 382 
     todo.Title=*++**+*                           | c:\dev\NCase\Readme.cs: line 383 
         isValid.Value=False                      | c:\dev\NCase\Readme.cs: line 384 
             todo.IsDone=False                    | c:\dev\NCase\Readme.cs: line 385 
                 todo.DueDate=10.11.2011 00:00:00 | c:\dev\NCase\Readme.cs: line 386 
             todo.IsDone=True                     | c:\dev\NCase\Readme.cs: line 387 
                 todo.DueDate=12.11.2011 00:00:00 | c:\dev\NCase\Readme.cs: line 388
```

#### Visualize Test Cases as a Table

You can get a systematic overview of the generated test cases, by calling the `PrintCasesAsTable()`. This extension method displays a table containing all the test cases, line by line.

<!--# Visualize_Table -->
```C#
string table = todoSet.PrintCasesAsTable();

Console.WriteLine(table);
```

Result:

<!--# Visualize_Table_Console -->
```
 # | todo.Title | isValid.Value | todo.IsDone |        todo.DueDate 
 - | ---------- | ------------- | ----------- | ------------------- 
 1 |     forget |          True |       False | 10.11.2011 00:00:00 
 2 |     forget |          True |       False | 12.11.2011 00:00:00 
 3 |     forget |         False |       False | 10.11.2011 00:00:00 
 4 |    *++**+* |         False |       False | 10.11.2011 00:00:00 
 5 |    *++**+* |         False |        True | 12.11.2011 00:00:00 

TOTAL: 5 TEST CASES
```

#### Visualize Single Case Definition

You can print information about a single test case, by calling the `Print()` extension method on it. It prints the facts row by row. 

<!--# Visualize_Case -->
```C#
string cas = todoSet.Cases().First().Print();

Console.WriteLine(cas);
```

Result:

<!--# Visualize_Case_Console -->
```
 Fact                             | Location                         
 -------------------------------- | -------------------------------- 
 todo.Title=forget                | c:\dev\NCase\Readme.cs: line 375 
 isValid.Value=True               | c:\dev\NCase\Readme.cs: line 376 
 todo.IsDone=False                | c:\dev\NCase\Readme.cs: line 377 
 todo.DueDate=10.11.2011 00:00:00 | c:\dev\NCase\Readme.cs: line 378
```

Next Steps
==========

First, have fun with NCase! 

Then, please provide feedbacks, critiques, and suggestions! 

Finally, be aware that NCase is under continuous development. Some upcoming features are:

- Improved syntax
	- Inline definition
	- Inline assignment of multiple values
- Full mocking functionalities 
	- mocking of classes
	- mocking of methods
	- "[moq][moq] like" `Setup(...)` and `Verify(...)`
- New use case: Record & replay of test steps
	- new definitions: `AllPermutations`, `PairwisePermutations`
- Improved testing of borderline cases
	- new definition `DrawDimensions`
- Autonomous parametrized test framework (including Assert compatible with NCase record/replay mechanism, CLI, Visual Studio and Resharper adapter) 





















[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
