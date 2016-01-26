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

Here is the list of utilities:

- [Pairwise Generator](#pairwise)
- [ForEach](#foreach)
- [Quadratic Processing](#quadratic)
    - [CartesianProduct](#cartesianproduct)
	- [TriangularProductWithoutDiagonal](#triangularproductwithoutdiagonal)
- [Processing of Chained Dictionaries](#chaineddictionary)
	- [CascadeAdd](#cascadeadd)
	- [CascadeRemove](#cascaderemove)
	- [Unsafe Indexer](#unsafeindexer)
	- [CascadeGetOrDefault](#cascadegetordefault)
	- [CascadeTryFirst](#cascadetryfirst)
- [String Processing](#stringprocessing)
	- [CascadeAdd](#cascadeadd)
	- [Lines and JoinLines](#lines)
	- [Desindent](#desindent)


### Pairwise Generator 
<a name="pairwise"></a>

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

#### About the algorithm
- The implementation is very compact: the main algorithm contains only 51 lines of code 
- Simple tests show good results
	- The algorithm tries to distribute the re-use of pairs among the whole set of pairs, by introducing a concept of generations
	- Further quantitative and qualitative analysis are required to evaluate precisely the properties of the algorithm
- Simple tests show good performance: The generation of tuple is lazy, resulting in low initial pre-processing time. Only the available set of pairs have to be generated at startup time.

### ForEach (Linq)
<a name="foreach"></a>

Every utility framework re-implements the following ForEach extension method:

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

set.ForEach( v => Console.Write(v), 
            () => Console.Write(", "));                
```
Output:
<!--# LinqForEachExtensions2_Console -->
```
0, 1, 2, 3, 4, 5, 6, 7, 8, 9
```

It is an alternative to the Linq `Aggregate(...)` function and the `string.Join(...)` static method, in order to perform aggregations. But the following usage has no counterpart in the C# framework:

<!--# LinqForEachExtensions3 -->
```C#
var set = Enumerable.Range(0, 10);

set.ForEach( v => SendToServer(v), 
            () => Thread.Sleep(10));                
```

### Quadratic Processing (Linq)
<a name="quadratic"></a>

#### CartesianProduct
<a name="cartesianproduct"></a>

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
<a name="triangularproductwithoutdiagonal"></a>

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

### Processing of Chained Dictionaries
<a name="chaineddictionary"></a>

<!--# CascadeExtensions_Def -->

Let's say, you need a model for the triplet (country, city, street). You can use the following model:
```C#
var stats = new Dictionary<string,               // Country
                    Dictionary<string,           // City
                            HashSet<string>>>(); // Street
```

The purpose of the `CascadeExtensions` class is to provide extension methods that reduces as much as possible the need of `if-else` statements to handle this kind of model composed of chained dictionaries.

#### CascadeAdd
<a name="cascadeadd"></a>

<!--# CascadeExtensions_CascadeAdd -->
```C#
model.CascadeAdd("FR").CascadeAdd("Paris").Add("Rue de la paix");
model.CascadeAdd("FR").CascadeAdd("Paris").Add("Rue de Paradis");
model.CascadeAdd("DE").CascadeAdd("Mainz").Add("Gutenbergplatz");
```

#### CascadeRemove
<a name="cascaderemove"></a>

<!--# CascadeExtensions_CascadeRemove -->
```C#
bool isRemoved1 = model
    .CascadeRemove("FR")
    .CascadeRemove("Paris")
    .CascadeRemove("Rue de la paix");

Console.WriteLine("isRemoved1= {0}", isRemoved1);

bool isRemoved2 = model
    .CascadeRemove("FR")
    .CascadeRemove("Paris")
    .CascadeRemove("Trafalgar Square");

Console.WriteLine("isRemoved2= {0}", isRemoved2);
```

Output:
<!--# CascadeExtensions_CascadeRemove_Console -->
```C#
isRemoved1= True
isRemoved2= False
```

#### Unsafe Indexer 
<a name="unsafeindexer"></a>

You can get items from the model by using the indexer defined in the `Dictionary` class. But this indexer is unsafe: it throws an exception if the key does not exist.

<!--# CascadeExtensions_Indexer -->
```C#
var streets1 = model["FR"]["Paris"];

Console.WriteLine("Street in Paris: {0}", string.Join(", ", streets1));

try
{
    var streets2 = model["Switzerland"]["Lausanne"]; // UNREGISTERED COUNTRY!
}
catch (KeyNotFoundException e)
{
    Console.WriteLine("Streets in Lausanne: KeyNotFoundException has been thrown");
}
```

Output:

<!--# CascadeExtensions_Indexer_Console -->
```
Street in Paris: Rue de la paix, Rue de Paradis
Streets in Lausanne: KeyNotFoundException has been thrown
```

#### CascadeGetOrDefault
<a name="cascadegetordefault"></a>

So if you need a safe get implementation, you can use the `.CascadeGetOrDefault()` extension method:

<!--# CascadeExtensions_CascadeGetOrDefault -->
```C#
var safeStreets1  = model.CascadeGetOrDefault("FR")
                         .CascadeGetOrDefault("Paris");

Console.WriteLine("Streets in Paris : {0}", string.Join(", ", safeStreets1));

var safeStreets2 = model.CascadeGetOrDefault("Switzerland") // UNREGISTERED COUNTRY!
                        .CascadeGetOrDefault("Lausanne"); 

if(safeStreets2 == null)
    Console.WriteLine("No street found in Lausanne");
```

Output: 
<!--# CascadeExtensions_CascadeGetOrDefault_Console -->
```
Streets in Paris : Rue de la paix, Rue de Paradis
No street found in Lausanne
```

#### CascadeTryFirst
<a name="cascadetryfirst"></a>

<!--# CascadeExtensions_CascadeTryFirst -->
```C#
string country,city, street;
bool ok = model.CascadeTryFirst(out country)
               .CascadeTryFirst(out city)
               .CascadeTryFirst(out street);

Console.WriteLine("CascadeTryFirst: ok={0}, country={1}, city={2}, street={3}", 
                  ok, country, city, street);
```

<!--# CascadeExtensions_CascadeTryFirst_Console -->
```
CascadeTryFirst: ok=True, country=FR, city=Paris, street=Rue de la paix
```

### String Processing
<a name="stringprocessing"></a>

#### Lines and JoinLines
<a name="lines"></a>

The `.Lines()` extension method enables splitting a string into an enumeration of lines, whereas `.JoinLines()` enables to join a enumeration of lines into a single string:
<!--# TextExtensions_Lines_JoinLines -->
```C#
string txt = "one line\nand a second line";
IEnumerable<string> lines = txt.Lines();
string rejoinedLines = lines.JoinLines();
```

#### Desindent
<a name="desindent"></a>

The `.Desindent()` allows to removed the indentation of a string:
<!--# TextExtensions_Desindent -->
```C#
string txt = "    I was originally indented!";

string desindentedTxt = txt.Desindent(tabIndentation:4);

Console.WriteLine("Before: {0}\nAfter :{1}", txt, desindentedTxt);
```

Output: 

<!--# TextExtensions_Desindent_Console -->
```C#
Before:     I was originally indented!
After :I was originally indented!
```


[NCase]: https://github.com/jeromerg/NCase
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
