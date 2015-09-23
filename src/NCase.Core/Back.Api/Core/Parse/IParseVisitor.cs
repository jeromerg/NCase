using NDsl.Api.Dev.Core.Tok;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Core.Parse
{
    public interface IParseVisitor<TToken> : IActionVisitor<IToken, IParseDirector, TToken>
        where TToken : IToken
    {
    }
}