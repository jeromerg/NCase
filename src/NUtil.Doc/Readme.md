Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)](https://www.nuget.org/packages/NCase/)

NUtil
=====

Utility classes for C# used in the [NCase] project.

Installation
------------

In the Nuget Package Manager Console:

```
Install-Package NUtil
```

Content
-------

### Pairwise Generator

Considering a list of discrete and finite sets, the `PairwiseGenerator ` generates a list of tuples ensuring that all pairs between all sets pairwise appear at least once. 

<!--# PairwiseGenerator -->
```C#
var setCardinals = new int[] {3, 2, 2};
IEnumerable<int[]> tuples = new PairwiseGenerator().Generate(setCardinals);
foreach (int[] t in tuples)
    Console.WriteLine("Tuple #{0}:  {1}, {2}, {3}", i++, t[0], t[1], t[2]);
```

Console:

<!--# PairwiseGenerator_Console -->
```
Tuple #0:  0, 0, 0
Tuple #1:  0, 1, 1
Tuple #2:  1, 0, 0
Tuple #3:  1, 1, 1
Tuple #4:  2, 0, 0
Tuple #5:  2, 1, 1
Tuple #6:  0, 0, 1
Tuple #7:  0, 1, 0
```

This pairwise generator is used in [NCase] as an alternative to the default cartesian product, in order to reduce the amount of generated test cases. More about pairwise testing [here][pair].

### Linq ForEach

Every Util framework re-implement the following ForEach extension method:

<!--# LinqForEachExtensions1 -->
```C#
var set = Enumerable.Range(0, 10);
set.ForEach(v => Console.Write(v));
```
Console:
<!--# LinqForEachExtensions1_Console -->
```
0123456789
```

The following overload is a little bit more interesting, as it enables placing an action between the processing of two items:

<!--# LinqForEachExtensions2 -->
```C#
var set = Enumerable.Range(0, 10);
set.ForEach(v => Console.Write(v), () => Console.Write(", "));
```
Console:
<!--# LinqForEachExtensions2_Console -->
```
0, 1, 2, 3, 4, 5, 6, 7, 8, 9
```

If you want to perform the previous aggregation you already have the Linq `Aggregate(...)` function and the `string.Join(...)` static method. The following use has no counterpart in the C# framework:

<!--# LinqForEachExtensions3 -->
```C#
var set = Enumerable.Range(0, 10);
set.ForEach(v => SendToServer(v), () => Thread.Sleep(10));
```

### Linq Quadratic Processing

#### CartesianProduct

<!--# QuadraticExtensions -->
```C#
var set1 = new [] {"a", "b"};
var set2 = new [] {0, 1};

var product = set1.CartesianProduct(set2, (s1, s2) => new { s1, s2 });

foreach (var pair in product)
    Console.WriteLine("({0}, {1})", pair.s1, pair.s2);
```
Console:
<!--# QuadraticExtensions_Console -->
```
(a, 0)
(a, 1)
(b, 0)
(b, 1)
```

#### TriangularProductWithoutDiagonal

<!--# TriangularProductWithoutDiagonal -->
```C#
var set1 = new [] {0, 1, 2};

var product = set1.TriangularProductWithoutDiagonal((s1, s2) => new { s1, s2 });

foreach (var pair in product)
    Console.WriteLine("({0}, {1})", pair.s1, pair.s2);
```
Console:
<!--# TriangularProductWithoutDiagonal_Console -->
```
(0, 1)
(0, 2)
(1, 2)
```

### Chained Dictionaries Processing

### String Processing


[NCase]: https://github.com/jeromerg/NCase
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
