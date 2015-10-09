using JetBrains.Annotations;
using NDsl.Back.Api.Def;

namespace NCase.Back.Api.SetDef
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