using NDsl.Back.Api.Common;
using NVisitor.Api.Action;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IParseVisitor<TToken> : IActionVisitor<IToken, IParseDirector, TToken>
        where TToken : IToken
    {
    }
}