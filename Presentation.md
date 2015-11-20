Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)](https://www.nuget.org/packages/NCase/)

NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase is a mix between a Mocking Framework like [Moq][Moq] and a parametrized test framework, having advanced combinatorial capabilities. 

NCase is not released yet! The API is now quite stable, but further commits may introduce breaking changes. Please give as much feedback as possible, positive, negative, critics!  

Installation
------------

To install NCase, run the following command in the Nuget Package Manager Console:

```
Install-Package NCase
```

Comparison to Moq: The first test case 
----------------------------------------

Let's say, you need to test a method called `TodoManager.AddTodo(ITodo todo)`.

Here is how you typically write a conventional test:  

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

So far, both tests look like very similar. You recognize the same blocks *Arrange, Act, Assert*. NCase is a little bit more verbose: 

- The mock has been replaced by something called a *contributor* 
- The contributor's properties are set inside a block, that amazingly looks like a definition
- And finally the *Act and Asserts* are located inside a statement lambda passed to a method called `ActAndAssert`

You wonder? Now, let's see the power of these few additional lines... 

The second test case
--------------------

Let's say, you need to implement additional test cases. No surprise: it is always the same! Usually, you typically perform a copy&paste, keep all mocked properties unchanged except one:

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

Now, open your eyes, with NCase, you only need to add a single line:

<!--# NCaseExample2_AddedLine -->
```C#
todo.Title = "Another todo to forget";
```

The initial test becomes:

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

Like a sorcerer, NCase calls twice the *Act and Asserts* exactly in the same way as `MoqTest1` and `MoqTest2` do!

Why? Because the Arrange statements are located inside a definition of type `AllCombinations`: the `AllCombinations` definition groups together subsequent assignments of the same property, peform the so called cartesian product between all groups. 

Afterward, the chain `set.Cases().Replay().ActAndAssert(...)` generates the test cases one by one, replay them and call the *Act and Assert* statements.

Many test cases
---------------

NCase is stupid systematic: You can add as many assignments to `Task`, `DueDate` and `IsDone` as you wish, NCase will generate and test all possible combinations. For example, the following lines will generate and test 6 x 7 x 2 = **84 test cases!!**


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

Imagine you test `TodoManager.AddTodo(...)` requires an additional argument: the user to assign the todo to:

```C#
TodoManager.AddTodo(ITodo todo, IUser assignee)
```

You can introduce an additional contributor as following:

<!--# NCaseCombiningContributors_VAR -->

```C#
var user = builder.NewContributor<IUser>("user");
```

And then you can use the contributor in the existing the definition:

<!--# NCaseCombiningContributors_DEF -->
```C#
using (set.Define())
{
    todo.Title = "Don't forget to forget";
    //... alternative assignments

    todo.DueDate = yesterday;
    //... alternative assignments

    todo.IsDone = false;
    //... alternative assignments

    user.IsActive = true;
    //... alternative assignments

    user.NotificationEmail = null;
    //... alternative assignments
}
```

This definition generates the cartesian product between all groups of property assignments for both contributors. It simply works!

Combining Sets
--------------

NCase has a very powerful combinatorial engine. You can define multiple combinatorial sets and, the best, you can combine them together! 

For example, you may want to split the previous arrange into two subsets. 

You first define the set of cases related to the `todo` contributor:

<!--# NCaseCombiningSets_TODO_SET -->
```C#
var todoSet = builder.NewDefinition<AllCombinations>("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget";
    //... alternative assignments

    todo.DueDate = yesterday;
    //... alternative assignments

    todo.IsDone = false;
    //... alternative assignments
}
```

Then you define the set of cases related to the `user` contributor:

<!--# NCaseCombiningSets_USER_SET -->
```C#
var userSet = builder.NewDefinition<AllCombinations>("userSet");
using (userSet.Define())
{
    user.IsActive = true;
    //... alternative assignments

    user.NotificationEmail = null;
    //... alternative assignments

}
```

Finally, you **combine both sets together** as following:

<!--# NCaseCombiningSets_ALL_SET -->
```C#
var allSet = builder.NewDefinition<AllCombinations>("allSet");
using (allSet.Define())
{
    todoSet.Ref();
    userSet.Ref();
}
```

The result is the same set of cases as in the previous example, but you acquired more flexibility! Now, you can:

-  Re-use each set at multiple places 
-  Apply alternative definitions to each set individually... (as you will see in the paragraph).

Tackle complexity with Pairwise testing
---------------------------------------

Testing all combinations is nice, but it is expensive. We generated before 84 test cases with only three properties and a few values for each one. Instead of generating all combinations with the `AllCombinations` definition, you can use the alternative definition called `PairwiseCombinations`. It generates a set of test cases, that contains all possible pairs between all groups of assignments (more about [pairwise testing here][pair]). 

Both definitions `AllCombinations` and `PairwiseCombinations` have exactly the same syntax, so you just need to change the name of the definition as you call `builder.NewDefinition<...>(...)`: 

<!--# NCasePairwiseCombinations -->
```C#
var todoSet = builder.NewDefinition<PairwiseCombinations>("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget";
    //... alternative assignments

    todo.DueDate = yesterday;
    //... alternative assignments

    todo.IsDone = false;
    //... alternative assignments

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

The result is a fine granular testing: you keep an exhaustive testing of the `user` sub-cases and an efficient, but more superficial, testing of the `todo` sub-cases.

Tackle dedicated asserts: Tree Definition
-----------------------------------------

If you need to perform asserts that depend on the input values, you have two alternatives. You can:

- Rewrite a simplified logic of the system under test in your test, in order to calculate the expectations, given the input values
- Or you can provide the expected values (asserts) along with the input values and pass both to the *Act and Assert* statements.

With the latter solution, you cannot automatically generate test cases with combinatorial operators, like `AllCombinations` or `PairwiseCombinations` because the expected values are bound to the input values. To solve this issue, NCase contains another definition called `Tree`. The `Tree` definition allows to define a set of test cases by the mean of trees.

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

The `Tree` definition performs an implicit fork every times it encounters an assignment of an already assigned property, at the level where the property was assigned for the last time. Every path from a leaf back to the root builds a test case. 

In the example, you see how you can mix the input values (`todo` instance) along with the expected values (`isValid` instance). You see how you can write dedicated asserts to specific set of input values.

### `IHolder<T>` Wrapper 
By the way, note how you can create contributors of simple types, like `bool`, by using the interface `IHolder<T>`. This interface contains a single property `Value` used to store any value of any type.

## Visualize

NCase provides methods to help to visualize what is going on, while you develop and execute tests.

#### Visualize Definition

If at some time, you get lost and doesn't understand anything more about what you are writing, then first have a break, drink a coffee, and then print the definitions you are trying to write with the `PrintDefinition()` extension method:

<!--# Visualize_Def -->
```C#
string def = todoSet.PrintDefinition(isFileInfo: true);

Console.WriteLine(def);
```

Result:

<!--# Visualize_Def_Console -->
```
 Definition                                       | Location                                                
 ------------------------------------------------ | ------------------------------------------------------- 
 Tree todoSet                                     | c:\dev\NCase\Presentation.cs: line 371 
     todo.Title=forget                            | c:\dev\NCase\Presentation.cs: line 373 
         isValid.Value=True                       | c:\dev\NCase\Presentation.cs: line 374 
             todo.IsDone=False                    | c:\dev\NCase\Presentation.cs: line 375 
                 todo.DueDate=10.11.2011 00:00:00 | c:\dev\NCase\Presentation.cs: line 376 
                 todo.DueDate=12.11.2011 00:00:00 | c:\dev\NCase\Presentation.cs: line 377 
         isValid.Value=False                      | c:\dev\NCase\Presentation.cs: line 378 
             todo.DueDate=10.11.2011 00:00:00     | c:\dev\NCase\Presentation.cs: line 379 
                 todo.IsDone=False                | c:\dev\NCase\Presentation.cs: line 380 
     todo.Title=*++**+*                           | c:\dev\NCase\Presentation.cs: line 381 
         isValid.Value=False                      | c:\dev\NCase\Presentation.cs: line 382 
             todo.IsDone=False                    | c:\dev\NCase\Presentation.cs: line 383 
                 todo.DueDate=10.11.2011 00:00:00 | c:\dev\NCase\Presentation.cs: line 384 
             todo.IsDone=True                     | c:\dev\NCase\Presentation.cs: line 385 
                 todo.DueDate=12.11.2011 00:00:00 | c:\dev\NCase\Presentation.cs: line 386
```

#### Visualize Test Cases as a Table

If you want to compete against your computer, or need, for some reason, to enter the Matrix, then you must print the table of test cases with the `PrintCasesAsTable()` extension method! It lists all the test cases, as you did yourself at the university during the lecture of Logic, hopelessly trying to verify the professor's assertions about the the NOR, the NAND, the implication and all other weird logical things.

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

If you want to visualize the facts building a test case, then you can use the `Print()` extension method on it. It prints the facts row by row and provides the line where the statement has been recorded. 

<!--# Visualize_Case -->
```C#
string cas = todoSet.Cases().First().Print();

Console.WriteLine(cas);
```

Result:

<!--# Visualize_Case_Console -->
```
 Fact                             | Location                                                
 -------------------------------- | ------------------------------------------------------- 
 todo.Title=forget                | c:\dev\NCase\Presentation.cs: line 373 
 isValid.Value=True               | c:\dev\NCase\Presentation.cs: line 374 
 todo.IsDone=False                | c:\dev\NCase\Presentation.cs: line 375 
 todo.DueDate=10.11.2011 00:00:00 | c:\dev\NCase\Presentation.cs: line 376
```


[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
