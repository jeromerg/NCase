NDocUtil
========

NDocUtil enables to automatically maintain C# and console snippets in your documentation and presentations. 

You declare snippets in unit tests. After that, you can automatically:

- Inject them into the related markdown document, and keep it up-to-date 
- Export the snippets as row file, colored html fragment, colored image, pixel-image  (PNG, BMP...) and lossless image (EMF/WMF).

Have you ever made a powerpoint presentation containing code snippets? The maintenance of code snippets is a nightmare! With *NDocUtil*, it is a dream :-) The code snippets are refreshed automatically at each build!
  
*NDocUtil* was primarly developed to write the documentation for the [NCase] project. 

Let's see how it works...

Installation
------------

In the Nuget Package Manager Console:

```
Install-Package NDocUtil
```

Use with markdown documentation
----------------------------------

Imagine, you write the following documentation in a file called [MyDocumentation.markdown][MyDocumentation_markdown]:

	MyDocumentation Example
	=======================
	
	Here is how you get the ISO 8601 date:

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

Edit the previous markdown document as follows:
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

We added over the code blocks the following html comments:

    <!--# CODE_SNIPPET_TO_INJECT -->

*NDocUtil* recognizes this syntax and injects the snippet named `CODE_SNIPPET_TO_INJECT` into the code block.

### Write the C# Unit Test generating the snippets

TODO HERE
Add a csharp file next to You need to add a file with the same name in the same folder: [MyDocumentation.cs][MyDocumentation_cs].

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
