TEST
----


```C#
<!--# AllCombinations -->
IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");

var all = builder.NewDefinition<AllCombinations>("all");
using (all.Define())
{
    todo.Task = "Don't forget to forget";
    todo.Task = "";
    todo.Task = "@()/&%$§ ß üäö ÖÄÜ éè";
    todo.Task = "@(SELECT * FROM USERS)";

    todo.DueDate = yesterday;
    todo.DueDate = now;
    todo.DueDate = tomorrow;

    todo.IsDone = false;
    todo.IsDone = true;
}

Console.WriteLine(all.PrintCasesAsTable());
<!--#-->
```

Console:
```
<!--# AllCombinationsConsole -->
  # |              todo.Task |        todo.DueDate | todo.IsDone 
 -- | ---------------------- | ------------------- | ----------- 
  1 | Don't forget to forget | 27.10.2015 23:17:29 |       False 
  2 | Don't forget to forget | 27.10.2015 23:17:29 |        True 
  3 | Don't forget to forget | 28.10.2015 23:17:29 |       False 
  4 | Don't forget to forget | 28.10.2015 23:17:29 |        True 
  5 | Don't forget to forget | 29.10.2015 23:17:29 |       False 
  6 | Don't forget to forget | 29.10.2015 23:17:29 |        True 
  7 |                        | 27.10.2015 23:17:29 |       False 
  8 |                        | 27.10.2015 23:17:29 |        True 
  9 |                        | 28.10.2015 23:17:29 |       False 
 10 |                        | 28.10.2015 23:17:29 |        True 
 11 |                        | 29.10.2015 23:17:29 |       False 
 12 |                        | 29.10.2015 23:17:29 |        True 
 13 |  @()/&%$§ ß üäö ÖÄÜ éè | 27.10.2015 23:17:29 |       False 
 14 |  @()/&%$§ ß üäö ÖÄÜ éè | 27.10.2015 23:17:29 |        True 
 15 |  @()/&%$§ ß üäö ÖÄÜ éè | 28.10.2015 23:17:29 |       False 
 16 |  @()/&%$§ ß üäö ÖÄÜ éè | 28.10.2015 23:17:29 |        True 
 17 |  @()/&%$§ ß üäö ÖÄÜ éè | 29.10.2015 23:17:29 |       False 
 18 |  @()/&%$§ ß üäö ÖÄÜ éè | 29.10.2015 23:17:29 |        True 
 19 | @(SELECT * FROM USERS) | 27.10.2015 23:17:29 |       False 
 20 | @(SELECT * FROM USERS) | 27.10.2015 23:17:29 |        True 
 21 | @(SELECT * FROM USERS) | 28.10.2015 23:17:29 |       False 
 22 | @(SELECT * FROM USERS) | 28.10.2015 23:17:29 |        True 
 23 | @(SELECT * FROM USERS) | 29.10.2015 23:17:29 |       False 
 24 | @(SELECT * FROM USERS) | 29.10.2015 23:17:29 |        True 

TOTAL: 24 TEST CASES
<!--#-->
```