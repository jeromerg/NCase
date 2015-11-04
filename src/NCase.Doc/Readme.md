NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase is a mix between a Mocking Framework like [Moq][Moq] and a parametrized test framework, having advanced combinatorial capabilities. 

Use it in every test!
-------------------------------

The following example shows how to use NCase to test the method ``


Define
------

### All Combinations

<!--# AllCombinations -->
```C#
var swSet = builder.NewDefinition<AllCombinations>("swSet");
using (swSet.Define())
{
    sw.Os = Os.Ios7;
    sw.Os = Os.Ios8;
    sw.Os = Os.Android5;
    sw.Os = Os.Android6;
    sw.Os = Os.WindowsMobile8;
    sw.Os = Os.WindowsMobile10;
    sw.Os = Os.OsX;
    sw.Os = Os.Windows7;

    sw.Browser = Browser.Chrome;
    sw.Browser = Browser.Firefox;
    sw.Browser = Browser.Safari;
    sw.Browser = Browser.Dolphin;

    sw.IsFacebookInstalled = false;
    sw.IsFacebookInstalled = true;
}
```

... generates 8 x 4 x 2 = 64 software configurations: the cartesian product between all groups of property assignments.

### Pairwise combinations

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

... generates 24 hardware configurations, containing any pair of two property values. The `AllCombinations` would produce 162 configurations (3 x 3 x 3 x 6)!!

### Tree

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

Combine
-------

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

... generates 6144 test cases (64 x 24 x 4): the combination of all software, hardware and user configurations.

Visualize
---------

### Visualize Definition

<!--# Visualize_Def -->
```C#
Console.WriteLine(userSet.PrintDefinition(isFileInfo: true));
```

Result:

<!--# Visualize_Def_Console -->
```
 Definition                         | Location                                            
 ---------------------------------- | --------------------------------------------------- 
 Tree userSet                       | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 171 
     user.UserName=Richard          | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 173 
         user.Password=SomePass678; | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 174 
             user.Age=24            | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 175 
             user.Age=36            | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 176 
     user.UserName=*+#&%$!$         | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 177 
         user.Password=tooeasy      | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 178 
             user.Age=-1            | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 179 
             user.Age=0             | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 180 












































```

### Visualize Test Cases as a Table

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


Replay
------

<!--# Replay -->
```C#
allSet.Cases().Replay().ActAndAssert(ctx =>
{
    Environment env = GetHardwareAndSoftwareEnvironment(hw, sw);
    SignInPage signInPage = env.GetSignInPage();
    signInPage.FillInForm(user);
});
```

... replays all 6144 test cases one by one and call the body of `ActAndAssert` for each one!

Here is the beginning of the console output:

<!--# Replay_Console -->
```
Test Case #0
================

Definition
----------

 Fact                           | Location                                            
 ------------------------------ | --------------------------------------------------- 
 hw.Architecture=arm            | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 146 
 hw.HardDriveInGb=10            | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 150 
 hw.RamInGb=1                   | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 154 
 hw.ScreenResolution=(480, 320) | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 158 
 sw.Os=Ios7                     | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 121 
 sw.Browser=Chrome              | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 130 
 sw.IsFacebookInstalled=False   | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 135 
 user.UserName=Richard          | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 173 
 user.Password=SomePass678;     | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 174 
 user.Age=24                    | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 175 


Act and Assert
--------------

TEST CASE RESULT: SUCCESSFUL!


Test Case #1
================

Definition
----------

 Fact                           | Location                                            
 ------------------------------ | --------------------------------------------------- 
 hw.Architecture=arm            | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 146 
 hw.HardDriveInGb=10            | d:\src\test\NCase\src\NCase.Doc\Readme.cs: line 150 
(...)
```


[Moq]: https://github.com/Moq/moq4 
[NUnit]: http://www.nunit.org/