NUtil
=====

Various C# Utility classes, that were initially developed for the [NCase] project.

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

Outout:

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

Every Util framework re-implements the following ForEach extension method:

<!--# LinqForEachExtensions1 -->
```C#
var set = Enumerable.Range(0, 10);

set.ForEach(v => Console.Write(v));
```
Output:
<!--# LinqForEachExtensions1_Console -->
```
0123456789
```

The following overload is a little bit more interesting, as it enables placing an action between the processing of the items:

<!--# LinqForEachExtensions2 -->
```C#
var set = Enumerable.Range(0, 10);

set.ForEach(v => Console.Write(v), () => Console.Write(", "));
```
Output:
<!--# LinqForEachExtensions2_Console -->
```
0, 1, 2, 3, 4, 5, 6, 7, 8, 9
```

It is an alternative to the Linq `Aggregate(...)` function and the `string.Join(...)` static method, in order to perform aggregations. But the following use has no counterpart in the C# framework:

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
Output:
<!--# QuadraticExtensions_Console -->
```
(a, 0)
(a, 1)
(b, 0)
(b, 1)
```

Remark: it is nothing else than a wrapper around:

```C#
return from in1 in first
       from in2 in second
       select selector(in1, in2);
```

But the wrapper enables to elegantly chain the cartesian product with other transformations. 

#### TriangularProductWithoutDiagonal

<!--# TriangularProductWithoutDiagonal -->
```C#
var set1 = new [] {0, 1, 2};

var product = set1.TriangularProductWithoutDiagonal((s1, s2) => new { s1, s2 });

foreach (var pair in product)
    Console.WriteLine("({0}, {1})", pair.s1, pair.s2);
```
Output:
<!--# TriangularProductWithoutDiagonal_Console -->
```
(0, 1)
(0, 2)
(1, 2)
```

### Chained Dictionaries Processing

<!--# CascadeExtensions_Def -->

Let's say, you want to list year by year the city that you visited. You need to aggregate the values by city and by country. You can use the following model:
```C#
var stats = new Dictionary<string,                // Country
                    Dictionary<string,            // City
                            HashSet<int>>>();     // Year of Visit
```

The first thing to to is to add

<!--# CascadeExtensions_CascadeAdd -->
```C#
stats.CascadeAdd("FR").CascadeAdd("Paris").Add(2010);
stats.CascadeAdd("FR").CascadeAdd("Paris").Add(2011);
stats.CascadeAdd("DE").CascadeAdd("Mainz").Add(2013);
```

<!--# CascadeExtensions_Indexer -->
```C#
var years1 = stats["FR"]["Paris"];

Console.WriteLine("years1 = {0}", string.Join(", ", years1));
```

<!--# CascadeExtensions_Indexer_Console -->
```
years1 = 2010, 2011
```

<!--# CascadeExtensions_CascadeGetOrDefault -->
```C#
var yearsSafe1  = stats.CascadeGetOrDefault("FR")
                       .CascadeGetOrDefault("Paris");

Console.WriteLine("yearsSafe1 = {0}", string.Join(", ", yearsSafe1));

var yearsSafe2 = stats.CascadeGetOrDefault("FR")
                      .CascadeGetOrDefault("Lyon");

Console.WriteLine("yearsSafe2 is null? {0} (no exception)", yearsSafe2 == null ? "true" : "false");
```

<!--# CascadeExtensions_CascadeGetOrDefault_Console -->
```
yearsSafe1 = 2010, 2011
yearsSafe2 is null? true (no exception)
```

<!--# CascadeExtensions_CascadeTryFirst -->
```C#
string country,city;
int year;
bool ok = stats.CascadeTryFirst(out country)
               .CascadeTryFirst(out city)
               .CascadeTryFirst(out year);

Console.WriteLine("CascadeTryFirst: ok={0}, country={1}, city={2}, year={3}", 
                   ok, country, city, year);
```

<!--# CascadeExtensions_CascadeTryFirst_Console -->
```
CascadeTryFirst: ok=True, country=FR, city=Paris, year=2010
```

<!--# CascadeExtensions_CascadeRemove -->
```C#
bool isRemoved1 = stats
    .CascadeRemove("FR")
    .CascadeRemove("Paris")
    .CascadeRemove(2011);

Console.WriteLine("isRemoved1= {0}", isRemoved1);

bool isRemoved2 = stats
    .CascadeRemove("FR")
    .CascadeRemove("Paris")
    .CascadeRemove(2052);

Console.WriteLine("isRemoved2= {0}", isRemoved2);
```

<!--# CascadeExtensions_CascadeRemove_Console -->
```C#
isRemoved1= True
isRemoved2= False
```

### String Processing


[NCase]: https://github.com/jeromerg/NCase
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
