using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NDsl.All;

namespace NCase.Front.Api
{
    public interface IProdApi : ISetDefApi<IProdApi, ProdId>
    {
    }
}