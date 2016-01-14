IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");

var allPairs = builder.NewDefinition<PairwiseCombinations>("allPairs");
using (allPairs.Define())
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

Console.WriteLine(allPairs.PrintCasesAsTable());
