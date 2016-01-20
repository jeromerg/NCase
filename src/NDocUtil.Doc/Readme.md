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

Instead of writing the code blocks yourself in the documentation, NDocUtil can inject code from a real scenario. It ensures that you don't write mistakes, and the documentation is refreshed during the build process, whatever the refactoring you performed.

You need to proceed as follows:

### Add placeholders in the markdown file

First, you need to define placeholders in the markdown document, where the snippets will be injected. A placeholder consists of a code block preceded by a html comment looking like `<!--# SNIPPET_NAME -->`, where `SNIPPET_NAME` is the name of the snippet to inject. 

The previous document looks like:

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

#### Remark
if you introduce a new code block for the first time, you need to add at least one character inside the code block:

	<!--# MY_CONSOLE_SNIPPET -->
	```
	at least one character here!
	```

...elsewhere the code snippet will not be injected.

### Generate snippets and update the markdown file

Now, you can write the real code snippets as unit test. The file must have the same name without extension as the markdown file and must be located in the same folder as the documentation file. Therefore, in the example, we name the file [MyDocumentation.cs][MyDocumentation_cs]. 

You need to add this file to a C# project. If the file is not in a subfolder of the project, you must add it as a link ([see here the msdn documentation][addaslink]).

In the example the file looks like: 

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
	- The argument `docu` in `new NDocUtil("docu")` is a (regex) tag that can be used to hide rows in code snippets
- The call to `docu.UpdateDocAssociatedToThisFile()` performs the documentation update. It is located in the fixture tear down method. It ensures that the console records have been recorded before.
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

That's it! Now every time that you execute the unit tests of the test fixture, then the markdown document is refreshed automatically.

Exporting snippets
------------------

Instead of updating a markdown documentation, you can export the snippets.



Maintaining snippets in Powerpoint presentation
-----------------------------------


[MyDocumentation_markdown]: MyDocumentation.markdown 
[MyDocumentation_cs]: MyDocumentation.cs 
[NCase]: https://github.com/jeromerg/NCase
[addaslink]: https://msdn.microsoft.com/de-de/library/windows/apps/jj714082%28v=vs.105%29.aspx?f=255&MSPPError=-2147217396