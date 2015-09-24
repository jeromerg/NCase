using NDsl.Api.Core;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Parse
{
    public interface IParseVisitor<TToken> : IActionVisitor<IToken, IParseDirector, TToken>
        where TToken : IToken
    {
    }
}