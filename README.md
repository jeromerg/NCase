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
var builder = NCase.NewBuilder();            
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

NCase replays each test case one by one and calls the act and asserts declared in `ActAndAssert(...)`. Finally, `ActAndAssert` records all individual results and notify the test framework, here `Nunit`, that all tests were successful or some failed.

Installation
------------

To install NCase, run the following command in the Nuget Package Manager Console:

```
Install-Package NCase
```


Further Examples
----------------

### Define

NCase offers various ways to define sets of test cases, and combine them together.

#### `AllCombinations`

As already shown above, `AllCombinations` generates all possbible property assignments. For example:

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

`PairwiseCombinations` has exactly the same syntax as `AllCombinations`. But instead of generating all combinations, it only generates test cases, to cover all existing pair of value assignments. [Pairwise testing][pair] is a usual way to reduce the amount of test cases to run.

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

This example generates 24 hardware configurations, containing all values pairwise. This is to compare to the *162 configurations* that the `AllCombinations`  would produce (3 x 3 x 3 x 6)!!

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

With NCase, you can combine definitions together in order to build a full test scenario. For example, you can combine the previous sets of software, hardware and user configurations together as following: 

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

NCase has a helpers, in order to visualize the test cases.

#### Visualize Definition

<!--# Visualize_Def -->
```C#
string def = userSet.PrintDefinition(isFileInfo: true);

Console.WriteLine(def);
```

Result:

<!--# Visualize_Def_Console -->
```
Definition                         | Location                                          
 ---------------------------------- | ------------------------------------------------- 
 Tree userSet                       | c:\dev\NCase\Readme.cs: line 216 
     user.UserName=Richard          | c:\dev\NCase\Readme.cs: line 218 
         user.Password=SomePass678; | c:\dev\NCase\Readme.cs: line 219 
             user.Age=24            | c:\dev\NCase\Readme.cs: line 220 
             user.Age=36            | c:\dev\NCase\Readme.cs: line 221 
     user.UserName=*+#&%$!$         | c:\dev\NCase\Readme.cs: line 222 
         user.Password=tooeasy      | c:\dev\NCase\Readme.cs: line 223 
             user.Age=-1            | c:\dev\NCase\Readme.cs: line 224 
             user.Age=0             | c:\dev\NCase\Readme.cs: line 225
```

#### Visualize Test Cases as a Table

<!--# Visualize_Table -->
```C#
string table = userSet.PrintCasesAsTable();

Console.WriteLine(table);
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

#### Visualize Single Case Definition

<!--# Visualize_Case -->
```C#
string cas = userSet.Cases().First().Print();

Console.WriteLine(cas);
```

Result:

<!--# Visualize_Case_Console -->
```
Fact                       | Location                                          
 -------------------------- | ------------------------------------------------- 
 user.UserName=Richard      | c:\dev\NCase\Readme.cs: line 218 
 user.Password=SomePass678; | c:\dev\NCase\Readme.cs: line 219 
 user.Age=24                | c:\dev\NCase\Readme.cs: line 220
```

### Iterate test cases

You can iterate the test cases as following:

<!--# Iterate -->
```C#
foreach (Case userCase in userSet.Cases())
    Console.WriteLine(userCase.Print());
```

### Replay test cases 

You may additionally want to replay the test cases one by one: 

<!--# Replay -->
```C#
foreach (Case userCase in userSet.Cases().Replay())
    Console.WriteLine(user.UserName);
```

### Perform Act and Asserts

But most of the time, you want to perform act and asserts on each test case individually. For that purpose you can use the ActAndAssert() extension method, as following:

<!--# ActAndAssert -->
```C#
allSet.Cases().Replay().ActAndAssert(ctx =>
{
    Environment env = GetHardwareAndSoftwareEnvironment(hw, sw);
    SignInPage signInPage = env.GetSignInPage();
    signInPage.FillInForm(user);
});
```

This example replays all 6144 test cases one by one and calls the statement lambda of `ActAndAssert` for each test case!

Here is the beginning of the console output:

<!--# ActAndAssert_Console -->
```
Test Case #0
================

Definition
----------

 Fact                           | Location                                          
 ------------------------------ | ------------------------------------------------- 
 hw.Architecture=arm            | c:\dev\NCase\Readme.cs: line 191 
 hw.HardDriveInGb=10            | c:\dev\NCase\Readme.cs: line 195 
 hw.RamInGb=1                   | c:\dev\NCase\Readme.cs: line 199 
 hw.ScreenResolution=(480, 320) | c:\dev\NCase\Readme.cs: line 203 
 sw.Os=Ios8                     | c:\dev\NCase\Readme.cs: line 172 
 sw.Browser=Chrome              | c:\dev\NCase\Readme.cs: line 176 
 sw.IsFacebookInstalled=False   | c:\dev\NCase\Readme.cs: line 180 
 user.UserName=Richard          | c:\dev\NCase\Readme.cs: line 218 
 user.Password=SomePass678;     | c:\dev\NCase\Readme.cs: line 219 
 user.Age=24                    | c:\dev\NCase\Readme.cs: line 220 


Act and Assert
--------------

TEST CASE RESULT: SUCCESSFUL!


Test Case #1
================

Definition
----------

 Fact                           | Location                                          
 ------------------------------ | ------------------------------------------------- 
 hw.Architecture=arm            | c:\dev\NCase\Readme.cs: line 191 
 hw.HardDriveInGb=10            | c:\dev\NCase\Readme.cs: line 195 
(...)
```

#### Assert exception

You can use the context provided to `ActAndAssert` in order to assert exceptions: 

<!--# ActAndAssert_ExpectException -->
```
allSet.Cases().Replay().ActAndAssert(ctx =>
{
    ctx.ExceptionAssert = ExceptionAssert.IsOfType<ApplicationException>();

    throw new ApplicationException("this exception is expected");
});
```

[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/
[presentation]: ./Presentation.md
[pair]: http://en.wikipedia.org/wiki/All-pairs_testing
