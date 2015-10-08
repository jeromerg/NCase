using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Tree
{
    public abstract class SetDefId : DefId, ISetDefId
    {
        protected SetDefId()
        {
        }

        protected SetDefId([NotNull] string name)
            : base(name)
        {
        }
    }
}