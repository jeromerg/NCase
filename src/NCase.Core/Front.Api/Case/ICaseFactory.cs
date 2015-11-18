using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;

namespace NCaseFramework.Front.Api.Case
{
    public interface ICaseFactory
    {
        [NotNull] 
        Ui.Case Create([NotNull] List<INode> factNodes, [NotNull] IRecorder recorder);
    }
}