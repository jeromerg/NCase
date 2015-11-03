NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase begins where other framework end:

- With mocking frameworks, like [Moq][Moq], you mock properties and methods one by one
==> NCase also allows to mock, but in such a way, that you can generate all test cases of multiple assignments at once!

- With testing frameworks, like [Nunit][NUnit], you define parametrized tests with combinatorial operators.
==> But you have a limited set of operators. NCase supports a growing number of combinatorial operators (currently 3, soon 6) and allows to combine them together! So you can build your test cases aspect by aspect: for example your can define all hardware configurations, all software configurations, and user profiles separately, and combine them together to generate all your test cases.

Example
-------

When you implement tests, you always start implementing a first test.

With [Moq][Moq]: 

<!--# MoqExample1 -->
```C#
// ARRANGE
var mock = new Mock<ITodo>();
mock.SetupProperty(todo => todo.Task, "Don't forget to forget");
mock.SetupProperty(todo => todo.DueDate, now);
mock.SetupProperty(todo => todo.IsDone, false);

// ACT
var taskManager = new TaskManager();
taskManager.CreateTask(mock.Object.Task, mock.Object.DueDate, mock.Object.IsDone);

// ASSERT
//...
```

With NCase

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

foreach (var cas in set.Cases().Replay())
{
    // ACT
    var taskManager = new TaskManager();
    taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

    // ASSERT
    //...
}
```

So far, they are almost equivalent. NCase is a little bit more verbose. We see that the code is already prepared to receive multiple test cases: It is the purpose of NCase: to seamless test one to many test cases!

Now, you need to implement additional test cases. No surprise, it is always the same. With `Moq`, you typically perform a copy&paste, a write second test, by keeping all mocked properties unchanged except one:

<!--# MoqExample2 -->
```C#
[Test]
public void MoqTest1()
{
    // ARRANGE
    var mock = new Mock<ITodo>();
    mock.SetupProperty(todo => todo.Task, "Don't forget to forget");
    mock.SetupProperty(todo => todo.DueDate, now);
    mock.SetupProperty(todo => todo.IsDone, false);

    //...
}

[Test]
public void MoqTest2()
{
    // ARRANGE
    var mock = new Mock<ITodo>();
    mock.SetupProperty(todo => todo.Task, "Another task to forget");
    mock.SetupProperty(todo => todo.DueDate, now);
    mock.SetupProperty(todo => todo.IsDone, false);

    // ACT
    //...
}
```

With NCase, you only need to add one line:

<!--# NCaseExample2_AddedLine -->
```C#
todo.Task = "Another task to forget";
```

The previous test becomes:

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

foreach (var cas in set.Cases().Replay())
{
    // ACT
    var taskManager = new TaskManager();
    taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

    // ASSERT
    //...
}
```

NCase is stupid systematic: You can add as many assignments to `Task`, `DueDate` and `IsDone` as you want, NCase will generate and test all possible combinations. 

For example...

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

foreach (var cas in set.Cases().Replay())
{
    // ACT
    var taskManager = new TaskManager();
    taskManager.CreateTask(todo.Task, todo.DueDate, todo.IsDone);

    // ASSERT
    //...
}
``` 

**...will generate and test 6 x 7 x 2 = 84 test cases**!!



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