TEST
----

<!--# AllCombinations -->
```C#
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

Console.WriteLine("//# AllCombinationsConsole");
Console.WriteLine(all.PrintCasesAsTable());
```

Console:
<!--# AllCombinationsConsole -->
```
  # |              todo.Task |        todo.DueDate | todo.IsDone 
 -- | ---------------------- | ------------------- | ----------- 
  1 | Don't forget to forget | 28.10.2015 14:01:05 |       False 
  2 | Don't forget to forget | 28.10.2015 14:01:05 |        True 
  3 | Don't forget to forget | 29.10.2015 14:01:05 |       False 
  4 | Don't forget to forget | 29.10.2015 14:01:05 |        True 
  5 | Don't forget to forget | 30.10.2015 14:01:05 |       False 
  6 | Don't forget to forget | 30.10.2015 14:01:05 |        True 
  7 |                        | 28.10.2015 14:01:05 |       False 
  8 |                        | 28.10.2015 14:01:05 |        True 
  9 |                        | 29.10.2015 14:01:05 |       False 
 10 |                        | 29.10.2015 14:01:05 |        True 
 11 |                        | 30.10.2015 14:01:05 |       False 
 12 |                        | 30.10.2015 14:01:05 |        True 
 13 |  @()/&%$§ ß üäö ÖÄÜ éè | 28.10.2015 14:01:05 |       False 
 14 |  @()/&%$§ ß üäö ÖÄÜ éè | 28.10.2015 14:01:05 |        True 
 15 |  @()/&%$§ ß üäö ÖÄÜ éè | 29.10.2015 14:01:05 |       False 
 16 |  @()/&%$§ ß üäö ÖÄÜ éè | 29.10.2015 14:01:05 |        True 
 17 |  @()/&%$§ ß üäö ÖÄÜ éè | 30.10.2015 14:01:05 |       False 
 18 |  @()/&%$§ ß üäö ÖÄÜ éè | 30.10.2015 14:01:05 |        True 
 19 | @(SELECT * FROM USERS) | 28.10.2015 14:01:05 |       False 
 20 | @(SELECT * FROM USERS) | 28.10.2015 14:01:05 |        True 
 21 | @(SELECT * FROM USERS) | 29.10.2015 14:01:05 |       False 
 22 | @(SELECT * FROM USERS) | 29.10.2015 14:01:05 |        True 
 23 | @(SELECT * FROM USERS) | 30.10.2015 14:01:05 |       False 
 24 | @(SELECT * FROM USERS) | 30.10.2015 14:01:05 |        True 

TOTAL: 24 TEST CASES
```