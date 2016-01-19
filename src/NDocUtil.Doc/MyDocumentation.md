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
2011-11-11T11:11:11.0000000
```
