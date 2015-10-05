using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Seq
{
    public class SeqId : DefId
    {
        private string mDefTypeName;

        public SeqId(string name)
            : base(name)
        {
        }

        public override string DefTypeName
        {
            get { return "Seq"; }
        }
    }
}