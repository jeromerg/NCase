IBuilder builder = NCase.NewBuilder();
var todo = builder.NewContributor<ITodo>("todo");

var tree = builder.NewDefinition<Tree>("tree");
using (tree.Define())
{
    todo.Task = "Don't forget to forget";
    todo.DueDate = yesterday;
    todo.IsDone = true;
    todo.IsDone = false;
    todo.DueDate = now;
    todo.IsDone = true;
    todo.DueDate = tomorrow;
    todo.IsDone = false;
}
