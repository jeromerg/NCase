Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)](https://www.nuget.org/packages/NCase/)

NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase is a mix between a Mocking Framework like [Moq][Moq] and a parametrized test framework, having advanced combinatorial capabilities. 

It can also be used alone to generate a set of data based on combinatorial rules.

(Related projects: [NVisitor], [NDocUtil], [NUtil], [NGitVersion])

Features at Glance
------------------

![loading slides](http://jeromerg.github.io/NCase/slides.gif)

Read the slides on: [http://slides.com/jeromerg/ncase](http://slides.com/jeromerg/ncase) 

Installation
------------

The easiest way is to install NCase with Nuget.

In combination with NUnit:

```
Install-Package NCase, NCase.NunitAdapter
```

In combination with XUnit:

```
Install-Package NCase, NCase.XunitAdapter
```

First Steps
-----------

Create a new console application containing the following lines:

<!--# FIRST_UNIT_TEST -->
```C#
public interface IVar
{
    int X { get; set; }
    int Y { get; set; }
}

public void Main()
{
    var builder = NCase.NewBuilder();
    var v = builder.NewContributor<IVar>("v");
    var mySet = builder.NewCombinationSet("c");

    using (mySet.Define())
    {
        v.X = 0;
        v.X = 1;

        v.Y = 0;
        v.Y = 1;
    }

    string table = mySet.PrintCasesAsTable();

    Console.WriteLine(table);
}
```

Ensure that the configuration is set to `DEBUG`. Execute the program. The result in the console should look like:

<!--# FIRST_UNIT_TEST_CONSOLE -->
```
 # | v.X | v.Y 
 - | --- | --- 
 1 |   0 |   0 
 2 |   0 |   1 
 3 |   1 |   0 
 4 |   1 |   1 

TOTAL: 4 TEST CASES
```

Bingo! You generated your first set of test cases. 

Now, add the following lines to use each combination:

<!--# FIRST_UNIT_TEST_2 -->
```C#
foreach (Case c in mySet.Cases().Replay())
    Console.WriteLine("{0} ^ {1} = {2}", v.X, v.Y, v.X ^ v.Y);
```

<!--# FIRST_UNIT_TEST_2_CONSOLE -->
```
0 ^ 0 = 0
0 ^ 1 = 1
1 ^ 0 = 1
1 ^ 1 = 0
```

It generates and replays all test cases one by one! 

Discovering NCase
-----------------

See the **[long introduction](./Introduction.md)**


Next Steps
==========

First, have fun with NCase! 

Then, please provide feedbacks, critiques, and suggestions! 

[Moq]: http://github.com/Moq/moq4
[NVisitor]: http://github.com/jeromerg/NVisitor
[NDocUtil]: http://github.com/jeromerg/NDocUtil
[NUtil]: http://github.com/jeromerg/NUtil
[NGitVersion]: http://github.com/jeromerg/NGitVersion
