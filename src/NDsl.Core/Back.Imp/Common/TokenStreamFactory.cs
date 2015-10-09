using NDsl.Back.Api.Book;

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