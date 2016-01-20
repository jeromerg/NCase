NDocUtil
========

NDocUtil enables to automatically maintain C# and console snippets in your documentation and presentations. 

You declare snippets in unit tests. After that, you can automatically:

- Inject them into the related markdown documentation, and keep them up-to-date 
- Export the snippets as row file, colored html fragment, colored image, pixel-image  (PNG, BMP...) and lossless image (EMF/WMF).

Have you ever made a powerpoint presentation containing code snippets? The maintenance of code snippets is a nightmare! With *NDocUtil*, the code snippets are refreshed automatically every build!

*NDocUtil* was primarly developed to write the documentation for the [NCase] project. 

Let's see how it works...

Installation
------------

In the Nuget Package Manager Console:

```
Install-Package NDocUtil
```

Use with markdown documentation
-------------------------------

Imagine, you write the following documentation in a file called [MyDocumentation.markdown][MyDocumentation_markdown]:

	MyDocumentation Example
	=======================
	
	Here is how you print out the ISO 8601 date:

	```C#
    var someDate = new DateTime(2011, 11, 11, 11, 11, 11);
	Console.WriteLine(someDate.ToString("o"));
	```
	
    Output:

	```
	2011-11-11T11:11:11.0000000Z
	```

Now, you want to inject the code blocks directly from real lines of code. In this way, you avoid writing mistakes and you can automatically upgrade the documentation.

You will have to proceed as follows:

### Add placeholders in the markdown file

Add placeholders into the the previous markdown document:
	MyDocumentation Example
	=======================
	
	Here is how you get the ISO 8601 date:

	<!--# MY_CODE_SNIPPET -->
	```C#
    var someDate = new DateTime(2011, 11, 11, 11, 11, 11);
	Console.WriteLine(someDate.ToString("o"));
	```
	
    Output:

	<!--# MY_CONSOLE_SNIPPET -->
	```
	2011-11-11T11:11:11.0000000Z
	```

The addition consists of the following html comments over the code blocks:

    <!--# CODE_SNIPPET_TO_INJECT -->

*NDocUtil* recognizes this syntax and injects the snippet with the same name, here `CODE_SNIPPET_TO_INJECT`, into the code block.

#### Remark
if you introduce a new code block for the first time, you need to add at least one character in side the code block:

	<!--# MY_CONSOLE_SNIPPET -->
	```
	at least one character here!
	```

Elsewhere the code snippet is not inserted properly.


### Generate snippets and update the markdown file

Write the code snippets as unit test in a file with the same name in the same folder as the documentation. Thus, in the example, the file must be named [MyDocumentation.cs][MyDocumentation_cs]. Add this file to any C# project. If the file is not in a subfolder of the project, you must add it as a link ([see here][addaslink]).

The csharp file is used to generate the snippets and to update the code blocks located in the markdown file. Here is the example corresponding to the previous markdown document: 

    [TestFixture]
    public class MyDocumentation
    {
        private readonly NDocUtil docu = new NDocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void PairwiseGenerator()
        {
            docu.BeginRecordConsole("MY_CONSOLE_SNIPPET");

            //# MY_CODE_SNIPPET
            var someDate = new DateTime(2011, 11, 11, 11, 11, 11);
            Console.WriteLine(someDate.ToString("o"));
            //#

            docu.StopRecordConsole();
        }
    }

- Here we use NUnit, but you can use any other test framework
- The instance of `NDocUtil` is used to record the snippets and update the documentation
	- The argument `docu` in `new NDocUtil("docu")` is a (regex) tag that can be used to hide rows of code snippets
- The call to `docu.UpdateDocAssociatedToThisFile()` performs the documentation update at the end of the unit test run (fixture tear down method), so that the console snippets are correctly recorded
- The code snippets are declared with the following syntax:

      //# NAME_OF_THE_SNIPPET
      ... code here
	  //#
    - Nesting is not supported 
    - There is no escape character, so you can't write `//#` at the beginning of a line inside the snippet
    - Lines containing the exclusion tag are excluded from the snippet
- The console snippets are recorded with the following syntax:

       docu.BeginRecordConsole("NAME_OF_THE_SNIPPET");
       ... C# statements echoing to the console
       docu.StopRecordConsole();
	- Nesting is not supported
	- The exclusion tag doesn't apply to console snippets

Now every time that you execute the unit tests of the test fixture, then the markdown document is refreshed automatically.

Maintaining Powerpoint presentation
-----------------------------------



[MyDocumentation_markdown]: MyDocumentation.markdown 
[MyDocumentation_cs]: MyDocumentation.cs 
[NCase]: https://github.com/jeromerg/NCase
[addaslink]: https://msdn.microsoft.com/de-de/library/windows/apps/jj714082%28v=vs.105%29.aspx?f=255&MSPPError=-2147217396