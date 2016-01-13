using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Parse
{
    public class ParseVisitors 
        : IParseVisitor<NullToken>
    {
        public void Visit([NotNull] IParseDirector director, [NotNull] NullToken node)
        {
            director.AddChildToScope(new NullNode(node.CodeLocation));
        }
    }
}