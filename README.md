Status:

[![Build status](https://ci.appveyor.com/api/projects/status/5t819acpeymgqdoh/branch/master?svg=true)](https://ci.appveyor.com/project/jeromerg/ncase/branch/master)  [![NuGet](https://img.shields.io/nuget/dt/NCase.svg)](https://www.nuget.org/packages/NCase/)

NCase
=====

Define, Combine, Visualize and Replay hundreds of test cases with a few lines of code.

NCase is a mix between a Mocking Framework like [Moq][Moq] and a parametrized test framework, having advanced combinatorial capabilities. 

It can also be used alone to perform Batch Processing of combinatorial set of data.

Features at Glance
------------------

![loading slides](http://jeromerg.github.io/NCase/slides.gif)

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

First Use
---------

Write the following unit test:

<!--# FIRST_UNIT_TEST -->
```
...coming soon...
```


More
------------

Read the [long introduction](./Introduction.md).

Next Steps
==========

First, have fun with NCase! 

Then, please provide feedbacks, critiques, and suggestions! 

Finally, be aware that NCase is under continuous development. Some upcoming features are:

- Full mocking functionalities 
	- mocking of classes
	- mocking of methods
	- "[moq][moq] like" `Setup(...)` and `Verify(...)`
- Autonomous parametrized test framework (including Assert compatible with NCase record/replay mechanism, CLI, Visual Studio and Resharper adapter) 
