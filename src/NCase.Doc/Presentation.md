NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase begins where other frameworks end:

- With mocking frameworks, like [Moq][Moq], you mock properties and methods one by one. NCase allows to mock as well, but in such a way, that you can generate multiple combinations of mock records and replay them to generate more test cases!

- With testing frameworks, like [Nunit][NUnit], you can define parametrized tests. But you have limited tools to generate the test cases. NCase in contrary supports a growing number of combinatorial operators (currently 3, soon 6) and allows the combination of these operators together! So you can build your test cases aspect by aspect: for example your can define all hardware configurations, all software configurations, and multiple user profiles separately, and combine them together to generate all possible test cases.

Example
-------

Let's say, you test some `TodoManager.AddTodo(ITodo todo)` method.

With [Moq][Moq], a test typically looks like: 

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

With NCase, you write the same test as following:

<!--# NCaseExample1 -->
```C#
// ARRANGE
IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var set = builder.NewDefinition<AllCombinations>("set");

using (set.Define())
{
    todo.Title = "Don't forget to forget";
    todo.DueDate = now;
    todo.IsDone = false;
}

// REPLAY TEST CASES
set.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var todoManager = new TodoManager();
    todoManager.CreateTodo(todo);

    // ASSERT
    //...
});
```

So far, both tests look like very similar. NCase is a little bit more verbose. We see that the code is already prepared to handle multiple test cases: The Act and Asserts are located in a statement lambda, allowing multiple calls.

Now, let's say, you need to implement additional test cases. (No surprise: it is always the same). With `Moq`, you typically perform a copy&paste, keeping all mocked properties unchanged except one:

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
public void MoqTest2()
{
    // ARRANGE
    var mock = new Mock<ITodo>();
    mock.SetupAllProperties();
    ITodo todo = mock.Object;

    todo.Title = "Another todo to forget";
    todo.DueDate = now;
    todo.IsDone = false;

    // ACT ... ASSERT ...
}
```

With NCase, you only need to add a single line:

<!--# NCaseExample2_AddedLine -->
```C#
todo.Title = "Another todo to forget";
```

So that the previous test becomes:

<!--# NCaseExample2 -->
```C#
// ARRANGE
IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var set = builder.NewDefinition<AllCombinations>("set");
using (set.Define())
{
    todo.Title = "Don't forget to forget";
    todo.Title = "Another todo to forget";

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

The statement lambda containing the Act and Asserts will be called twice exactly in the same conditions as `MoqTest1` and `MoqTest2` are called in the previous example!

Why? Because the Arrange statements are located inside a definition of type `AllCombinations`: the `AllCombinations` definition groups together subsequent assignments of the same property, peform the so called cartesian product between all groups, and pass them to the statement lambda in order to execute the Act and Assert statements.

NCase is stupid systematic: You can add as many assignments to `Task`, `DueDate` and `IsDone` as you wish, NCase will generate and test all possible combinations. For example, the following lines will generate and test 6 x 7 x 2 = **84 test cases!!**


<!--# NCaseExample3 -->
```C#
// ARRANGE
IBuilder builder = NCase.NewBuilder();
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
Now, imagine you test `TodoManager.AddTodo(ITodo todo, IUser assignee)` containing an additional argument: the user to assign the todo to. 

You can extend the previous `AllCombinations` set with the assignee properties to test. For that purpose, you need to create a new contributor of type `IUser`, as following:

<!--# NCaseCombiningContributors_VAR -->
```C#
var user = builder.NewContributor<IUser>("user");
```

And then you can extend the definition as following:

<!--# NCaseCombiningContributors_DEF -->
```C#
using (set.Define())
{
    todo.Title = "Don't forget to forget";
    //...
    todo.DueDate = yesterday;
    //...
    todo.IsDone = false;
    //...
    user.IsActive = true;
    //...
    user.NotificationEmail = null;
    //...
}
```

It will generate the cartesian product between all groups of property assignments of both contributors. It works, you can mix any contributors together in any definition.

Combining Sets
--------------

You can also define multiple combinatorial sets and combine them together. For example, you may want to separate the set of `ITodo` cases from the set of	`IUser` cases. 

First you define the both sets for `ITodo` and `IUser` separately as following:

<!--# NCaseCombiningSets_TODO_SET -->
```C#
var todoSet = builder.NewDefinition<AllCombinations>("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget";
    //...
    todo.DueDate = yesterday;
    //...
    todo.IsDone = false;
    //...
}
```

<!--# NCaseCombiningSets_USER_SET -->
```C#
var userSet = builder.NewDefinition<AllCombinations>("userSet");
using (userSet.Define())
{
    user.IsActive = true;
    //...
    user.NotificationEmail = null;
    //...
}
```

And them you combine both set of test cases together as following:

<!--# NCaseCombiningSets_ALL_SET -->
```C#
var allSet = builder.NewDefinition<AllCombinations>("allSet");
using (allSet.Define())
{
    todoSet.Ref();
    userSet.Ref();
}
```

Subdividing the set of test cases into different parts has two imprtant advantages:

-  You can re-use each set individually
-  You can apply different combinatorial rules for each definition, as you will see below.

Tackle complexity with Pairwise testing
---------------------------------------

Testing all combinations is nice, but it is expensive. We generated before 84 test cases with only three properties and a few values for each one. Instead of generating all combinations with the `AllCombinations` definition, you can use the alternative definition called `PairwiseCombinations`. It generates a set of test cases, that contain all possible pairs between all groups of assignments (about [pairwise testing][pair]). Both definitions have exactly the same syntax, so you just need to change the name of the definition as you call `builder.NewDefinition<...>(...)`: 

<!--# NCasePairwiseCombinations -->
```C#
var todoSet = builder.NewDefinition<PairwiseCombinations>("todoSet");
using (todoSet.Define())
{
    todo.Title = "Don't forget to forget";
    //...
    todo.DueDate = yesterday;
    //...
    todo.IsDone = false;
    //...
}
```

When you combine multiple sets together as previously described, you can use the `PairwiseCombinations` definition for only a few sets, and keep using the `AllCombinations` definition for the others. So, you can improve the execution speed but keep testing all combinations of some important dimensions.


Tackle dedicated asserts: use Trees
-----------------------------------

If you need to perform Asserts that depend on the input values, you have two alternatives. You can:

- (Re-)Write a simplified logic of the system under test in your test, in order to calculate the expectations, given the input values
- Or pass the expected values next to the input values

With the latter solution, you cannot automatically generate test cases with combinatorial operators. For that purpose NCase contains an `Tree` definition, allowing to define a set of test cases by the mean of a tree.






























[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing