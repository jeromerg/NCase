using NCase.Front.Ui;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface IFactFactory
    {
        IFact Create(INode fact);
    }
}