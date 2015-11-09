Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)](https://www.nuget.org/packages/NCase/)

NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase is a mix between a Mocking Framework like [Moq][Moq] and a parametrized test framework, having advanced combinatorial capabilities. 

NCase is not released yet! The API is now quite stable, but further commits may introduce breaking changes. Please give as much feedback as possible, positive, negative, critics!  

Use NCase in all your tests!
----------------------------

If you want to read the long story, look at this alternative [presentation][presentation].

The following example shows how to use NCase to test a method named `CanPrescribePenicillin`:

<!--# ShortExample -->
```C#
// ARRANGE
IBuilder builder = NCase.NewBuilder();            
var p = builder.NewContributor<IPatient>("patient");
var allPatients = builder.NewDefinition<AllCombinations>("swSet");

using (allPatients.Define())
{
    p.Age = 10;
    p.Age = 30;
    p.Age = 60;

    p.Sex = Sex.Female;
    p.Sex = Sex.Male;

    p.HasPenicillinAllergy = true;
}

allPatients.Cases().Replay().ActAndAssert(ctx =>
{
    // ACT
    var pharmacy = new Pharmacy();
    bool canPrescribe = pharmacy.CanPrescribePenicillin(p);

    // ASSERT
    Assert.IsTrue(canPrescribe);
});
```

It looks very similar to usual unit tests. You recognize the three groups of statements *Arrange, Act and Assert*. But contrary to conventional tests, this one actually covers 6 test cases instead of 1! Why? Because the properties `Age`, `Sex` and `HasPenicillinAllergy` are assigned multiple times contiguously inside a definition of type `AllCombinations`. In that case, Ncase performs the cartesian product of all assignments grouped by property. Here for example, it generates 6 test cases: 3 x 2 x 1 = 6. 

NCase replays each test case one by one and calls the act and asserts declared in `ActAndAssert(...)`. `ActAndAssert` records all result for all test cases and finally notify the test framework, here `Nunit`, that all tests were successful or some failed.

Installation
------------

To install NCase, run the following command in the Nuget Package Manager Console:

```
Install-Package NCase
```


Further Examples
----------------

### Define

NCase offers various ways of defining sets of test cases, and allows to combine them together.

#### `AllCombinations`

This is the definition used in the previous example. It generates all combinations between all assignments grouped by property. For example:

<!--# AllCombinations -->
```C#
var swSet = builder.NewDefinition<AllCombinations>("swSet");
using (swSet.Define())
{
    sw.Os = Os.Ios8;
    sw.Os = Os.Android6;
    sw.Os = Os.WindowsMobile10;

    sw.Browser = Browser.Chrome;
    sw.Browser = Browser.Firefox;
    sw.Browser = Browser.Safari;

    sw.IsFacebookInstalled = false;
    sw.IsFacebookInstalled = true;
}
```

... generates 3 x 3 x 2 = 18 software configurations.

#### `PairwiseCombinations`

`PairwiseCombinations` has exactly the same syntax as `AllCombinations`. But instead of generating all combinations, it only generates test cases, to cover all existing pair of values, no more. [Pairwise testing][pair] is a usual way to reduce the amount of test cases to run.

<!--# PairwiseCombinations -->
```C#
var hwSet = builder.NewDefinition<PairwiseCombinations>("hwSet");
using (hwSet.Define())
{
    hw.Architecture = Architecture.arm;
    hw.Architecture = Architecture.x64;
    hw.Architecture = Architecture.x86;

    hw.HardDriveInGb = 10;
    hw.HardDriveInGb = 20;
    hw.HardDriveInGb = 50;

    hw.RamInGb = 1;
    hw.RamInGb = 2;
    hw.RamInGb = 5;

    hw.ScreenResolution = new Size(480, 320);
    hw.ScreenResolution = new Size(320, 480);
    hw.ScreenResolution = new Size(960, 640);
    hw.ScreenResolution = new Size(640, 960);
    hw.ScreenResolution = new Size(1136, 640);
    hw.ScreenResolution = new Size(640, 1136);
}
```

... generates 24 hardware configurations, containing all property values pairwise. Remark: With the `AllCombinations` definition, you *would produce 162 configurations* (3 x 3 x 3 x 6)!!

#### `Tree`

In some circumstances, you may want to define all test cases one by one. A good compact way for that, is to use the `Tree` definition. For example:

<!--# Tree -->
```C#
var userSet = builder.NewDefinition<Tree>("userSet");
using (userSet.Define())
{
    user.UserName = "Richard";
        user.Password = "SomePass678;";
            user.Age = 24;
            user.Age = 36;
    user.UserName = "*+#&%$!$";
        user.Password = "tooeasy";
            user.Age = -1;
            user.Age = 00;
}
```

... generates 4 user configurations.

### Combine

With NCase, you can combine definitions together in order to build a full test scenario. For example, you can combine the previously defined sets of software, hardware and user configurations together: 

<!--# Combine -->
```C#
var allSet = builder.NewDefinition<AllCombinations>("allSet");
using (allSet.Define())
{
    hwSet.Ref();
    swSet.Ref();
    userSet.Ref();
}
```

Alltogether, it generates 6144 test cases (64 x 24 x 4)!

### Visualize

NCase has a few helpers, in order to help you visualizing the test cases you are implementing.

#### Visualize Definition

<!--# Visualize_Def -->
```C#
Console.WriteLine(userSet.PrintDefinition(isFileInfo: true));
```

Result:

<!--# Visualize_Def_Console -->
```
 Definition                         | Location                                          
 ---------------------------------- | ------------------------------------------------- 
 Tree userSet                       | e:\Data\itschwabing\dev\NCase\Readme.cs: line 212 
     user.UserName=Richard          | e:\Data\itschwabing\dev\NCase\Readme.cs: line 214 
         user.Password=SomePass678; | e:\Data\itschwabing\dev\NCase\Readme.cs: line 215 
             user.Age=24            | e:\Data\itschwabing\dev\NCase\Readme.cs: line 216 
             user.Age=36            | e:\Data\itschwabing\dev\NCase\Readme.cs: line 217 
     user.UserName=*+#&%$!$         | e:\Data\itschwabing\dev\NCase\Readme.cs: line 218 
         user.Password=tooeasy      | e:\Data\itschwabing\dev\NCase\Readme.cs: line 219 
             user.Age=-1            | e:\Data\itschwabing\dev\NCase\Readme.cs: line 220 
             user.Age=0             | e:\Data\itschwabing\dev\NCase\Readme.cs: line 221 








```

#### Visualize Test Cases as a Table

<!--# Visualize_Table -->
```C#
Console.WriteLine(userSet.PrintCasesAsTable());
```

Result:

<!--# Visualize_Table_Console -->
```
 # | user.UserName | user.Password | user.Age 
 - | ------------- | ------------- | -------- 
 1 |       Richard |  SomePass678; |       24 
 2 |       Richard |  SomePass678; |       36 
 3 |      *+#&%$!$ |       tooeasy |       -1 
 4 |      *+#&%$!$ |       tooeasy |        0 

TOTAL: 4 TEST CASES

```

#### Visualize Case

<!--# Visualize_Case -->
```C#
Console.WriteLine(userSet.Cases().First().Print());
```

Result:

<!--# Visualize_Case_Console -->
```
 Fact                       | Location                                          
 -------------------------- | ------------------------------------------------- 
 user.UserName=Richard      | e:\Data\itschwabing\dev\NCase\Readme.cs: line 214 
 user.Password=SomePass678; | e:\Data\itschwabing\dev\NCase\Readme.cs: line 215 
 user.Age=24                | e:\Data\itschwabing\dev\NCase\Readme.cs: line 216 


```

### Iterate test cases

You may simply want to iterate the test cases as following:

<!--# Iterate -->
```C#
foreach (Case userCase in userSet.Cases())
    Console.WriteLine(userCase.Print());
```

### Replay test cases 
Or, you may additionally want to replay the test cases one by one: 

<!--# Replay -->
```C#
foreach (Case userCase in userSet.Cases().Replay())
    Console.WriteLine(user.UserName);
```

### Perform act and asserts

But most of the time, you want to perform act and asserts on each test case. For example, the following test:

<!--# ActAndAssert -->
```C#
allSet.Cases().Replay().ActAndAssert(ctx =>
{
    Environment env = GetHardwareAndSoftwareEnvironment(hw, sw);
    SignInPage signInPage = env.GetSignInPage();
    signInPage.FillInForm(user);
});
```

... replays all 6144 test cases one by one and calls the statement lambda of `ActAndAssert` for each test case!

Here is the beginning of the console output:

<!--# ActAndAssert_Console -->
```
Test Case #0
================

Definition
----------

 Fact                           | Location                                          
 ------------------------------ | ------------------------------------------------- 
 hw.Architecture=arm            | e:\Data\itschwabing\dev\NCase\Readme.cs: line 187 
 hw.HardDriveInGb=10            | e:\Data\itschwabing\dev\NCase\Readme.cs: line 191 
 hw.RamInGb=1                   | e:\Data\itschwabing\dev\NCase\Readme.cs: line 195 
 hw.ScreenResolution=(480, 320) | e:\Data\itschwabing\dev\NCase\Readme.cs: line 199 
 sw.Os=Ios8                     | e:\Data\itschwabing\dev\NCase\Readme.cs: line 168 
 sw.Browser=Chrome              | e:\Data\itschwabing\dev\NCase\Readme.cs: line 172 
 sw.IsFacebookInstalled=False   | e:\Data\itschwabing\dev\NCase\Readme.cs: line 176 
 user.UserName=Richard          | e:\Data\itschwabing\dev\NCase\Readme.cs: line 214 
 user.Password=SomePass678;     | e:\Data\itschwabing\dev\NCase\Readme.cs: line 215 
 user.Age=24                    | e:\Data\itschwabing\dev\NCase\Readme.cs: line 216 


Act and Assert
--------------

TEST CASE RESULT: SUCCESSFUL!


Test Case #1
================

Definition
----------

 Fact                           | Location                                          
 ------------------------------ | ------------------------------------------------- 
 hw.Architecture=arm            | e:\Data\itschwabing\dev\NCase\Readme.cs: line 187 
 hw.HardDriveInGb=10            | e:\Data\itschwabing\dev\NCase\Readme.cs: line 191 
(...)
```

#### Assert exception

`ActAndAssert` provides the lambda with a context. If you need to assert that the system under test throws an exception, then you may set the TODO XXXXXXXXXXXXXXX 


[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/
[presentation]: ./Presentation.md
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
