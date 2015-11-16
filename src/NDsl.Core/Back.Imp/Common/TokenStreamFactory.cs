using NDsl.Back.Api.TokenStream;

namespace NDsl.Back.Imp.Common
{
    public class TokenStreamFactory : ITokenStreamFactory
    {
        public ITokenStream Create()
        {
            return new TokenStream();
        }
    }
}