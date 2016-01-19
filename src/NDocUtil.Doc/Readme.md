NDocUtil
========

NDocUtil enables to automatically maintain C# and console snippets in your documentation and presentations. 

You declare snippets in unit tests. After that, you can automatically:

- Update the snippets contained in your markdown documentation 
- Export the snippets as row file, colored html fragment or colored image, either pixel-image like PNG, BMP... or lossless EMF/WMF image.

Have you ever made a powerpoint presentation containing code snippets? The maintenance of code snippets is a nightmare! With *NDocUtil*, it is a dream :-) The code snippets are refreshed automatically at each build!
  
*NDocUtil* was primarly developed to write the documentation for the [NCase] project. 

let's see how it works...

Installation
------------

In the Nuget Package Manager Console:

```
Install-Package NDocUtil
```

Maintaining Markdown documentation
----------------------------------

In a C# project, you need two files in the same folder, with the same name. One must have the `*.markdown` extension, the other the `*.cs` extension. For example [MyDocumentation.markdown][MyDocumentation_markdown] and [MyDocumentation.cs][MyDocumentation_cs].

The csharp file is used to generate the snippets and to update the code blocks located in the markdown file. Here is an example: 

```C#
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
        //# MY_CODE_SNIPPET
        var now = DateTime.Now;
        var this_Row_Will_Be_Hidden = "as it contains the tag 'docu'";
        //#

        docu.BeginRecordConsole("MY_CONSOLE_SNIPPET");
        Console.WriteLine("This line will be exported into the console snippet");
        docu.StopRecordConsole();
    }
}
```

- Here we use NUnit, but you can use any other test framework
- The instance of `NDocUtil` is used to record the snippets and update the documentation
	- The argument `docu` in `new NDocUtil("docu")` is a (regex) tag that can be used to hide rows of code snippets
- The code snippets are declared with the following syntax:

      //# NAME_OF_THE_SNIPPET
      ... code here
	  //#
    - Nesting is not supported 
    - There is no escape character, so you can't write `//#` at the beginning of a line inside the snippet
    - Lines containing the exclusion tag are excluded from the snippet (here "docu")
- The console snippets are recorded with the following calls:

       docu.BeginRecordConsole("NAME_OF_THE_SNIPPET");
       ... calls echoing to the console
       docu.StopRecordConsole();
	- Nesting is not supported
	- The exclusion tag doesn't apply to console snippets
- The call to `docu.UpdateDocAssociatedToThisFile()` updates the documentation at the end of the unit test run, as it is located in the test fixture tear down method. 

The markdown file `MyDocumentation.markdown` looks like as follows: 

	MyDocumentation Example
	=======================
	
	This code snippet is refreshed on every unit test run:
	
	<!--# MY_CODE_SNIPPET -->
	```C#
	var now = DateTime.Now; // this line will be included
	```
	
	As well as the following console snippet:
	
	<!--# MY_CONSOLE_SNIPPET -->
	```
	This line will be exported into the console snippet
	```

    Bingo!

- The syntax for the snippet placeholders is as follows:

      <!--# NAME_OF_SNIPPET -->
      ```
          ... code snippet is inserted here
      ``` 
    - When you introduce a new code block for the first time, you must put at least one character inside the two  ```

Maintaining Powerpoint presentation
-----------------------------------



[MyDocumentation_markdown]: MyDocumentation.markdown 
[MyDocumentation_cs]: MyDocumentation.cs 
[NCase]: https://github.com/jeromerg/NCase
