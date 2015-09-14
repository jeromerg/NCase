using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IParseVisitor<TToken> : IVisitor<IToken, IParseDirector, TToken>
        where TToken : IToken
    {
    }
}
