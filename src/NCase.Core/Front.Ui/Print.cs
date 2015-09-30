using System.Collections.Generic;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public class PrintSetDef : IOp<ISetDef, IEnumerable<ICase>>
    {
        public int Indentation { get; set; }
        public bool RecurseIntoReferences { get; set; }

        public PrintSetDef()
        {
            Indentation = 4;
            RecurseIntoReferences = false;
        }
    }
}