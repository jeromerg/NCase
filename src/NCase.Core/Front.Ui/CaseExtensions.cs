using NCaseFramework.Front.Api.SetDef;

namespace NCaseFramework.Front.Ui
{
    public static class CaseExtensions
    {
        public static string Print(this Case cas)
        {
            IPrintCase printCase = cas.Zapi.Services.GetService<IPrintCase>();
            return printCase.Perform(cas.Zapi.Model);
        }
    }
}