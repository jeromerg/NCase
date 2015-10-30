NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase begins where other frameworks end:

- With mocking frameworks, like [Moq][Moq], you mock properties and methods one by one. NCase allows to mock as well, but in such a way, that it can generate multiple combinations of mock records and replay them to generate more test cases!

- With testing frameworks, like [Nunit][NUnit], you define parametrized tests with combinatorial operators. But you have a limited set of these operators and you cannot combine them together. NCase in contrary supports a growing number of combinatorial operators (currently 3, soon 6) and allows to combine them together! So you can build your test cases aspect by aspect: for example your can define all hardware configurations, all software configurations, and multiple user profiles separately, and combine them together to generate all possible test cases.

Example
-------

Let's say, you need to implement a first test for some `TestManager.CreateTask(...)` method.

With [Moq][Moq], it looks like: 

<!--# MoqExample1 -->
```C#
// ARRANGE
var mock = new Mock<ITodo>();
mock.SetupAllProperties();
ITodo todo = mock.Object;

todo.Task = "Don't forget to forget";
todo.DueDate = now;
todo.IsDone = false;

// ACT
var taskManager = new TaskManager();
taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

// ASSERT
//...
```

With NCase, it looks like:

<!--# NCaseExample1 -->
```C#
// ARRANGE
IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var set = builder.NewDefinition<AllCombinations>("set");

using (set.Define())
{
    todo.Task = "Don't forget to forget";
    todo.DueDate = now;
    todo.IsDone = false;
}

set.Cases().Replay().ActAndAssert(ea =>
{
    // ACT
    var taskManager = new TaskManager();
    taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

    // ASSERT
    //...
});
```

So far, both tests look like almost the same. NCase is a little bit more verbose. We see that the code is already prepared to handle multiple test cases: Act and Assert are located in a statement lambda, allowing multiple calls.

Let's say, now you need to implement additional test cases. No surprise, it is always the same. With `Moq`, you typically perform a copy&paste, keeping all mocked properties unchanged except one:

<!--# MoqExample2 -->
```C#
[Test]
public void MoqTest1()
{
    // ARRANGE
    var mock = new Mock<ITodo>();
    mock.SetupAllProperties();
    ITodo todo = mock.Object;

    todo.Task = "Don't forget to forget";
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

    todo.Task = "Another task to forget";
    todo.DueDate = now;
    todo.IsDone = false;

    // ACT ... ASSERT ...
}
```

With NCase, you only need to add a single line...

<!--# NCaseExample2_AddedLine -->
```C#
todo.Task = "Another task to forget";
```

... as following:

<!--# NCaseExample2 -->
```C#
// ARRANGE
IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var set = builder.NewDefinition<AllCombinations>("set");
using (set.Define())
{
    todo.Task = "Don't forget to forget";
    todo.Task = "Another task to forget";

    todo.DueDate = now;
    todo.IsDone = false;
}

set.Cases().Replay().ActAndAssert( ea =>
{
    // ACT
    var taskManager = new TaskManager();
    taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

    // ASSERT
    //...
});
```

The statement lambda containing the Act and Asserts will be called twice exactly in the same conditions as `MoqTest1` and `MoqTest2` are called in the previous example!

Why? Because the Arrange statements are located inside a definition of type `AllCombinations`: the `AllCombinations` group together subsequent assignments of the same property, peform the so call cartesian product between all groups, replay them and pass them to the statement lambda in order to execute the Act and Assert statements.

NCase is stupid systematic: You can add as many assignments to `Task`, `DueDate` and `IsDone` as you wish, NCase will generate and test all possible combinations. For example, the following lines will generate and test 6 x 7 x 2 = **84 test cases!!**


<!--# NCaseExample3 -->
```C#
// ARRANGE
IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");
var set = builder.NewDefinition<AllCombinations>("set");
using (set.Define())
{
    todo.Task = "Don't forget to forget";
    todo.Task = "";
    todo.Task = null;
    todo.Task = "ß üöä ÜÖÄ !§$%&/()=?";
    todo.Task = "SELECT USER_ID, PASSWORD FROM USER";
    todo.Task = "Another Task";

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
    var taskManager = new TaskManager();
    taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

    // ASSERT
    //...
});
``` 


Combining test cases
--------------------

TODO: Ref

Tackle complexity with Pairwise testing
---------------------------------------

TODO: Pairwise

Tackle dedicated asserts
------------------------

TODO: Tree




[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/