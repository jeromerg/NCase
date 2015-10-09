using System;
using System.Text;
using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Print
{
    public class PrintDefinitionDirector : ActionDirector<INode, IPrintDefinitionDirector>, IPrintDefinitionDirector
    {
        #region inner types

        private interface IStragegy
        {
            void AppendLine(CodeLocation codeLocation, string txt);
            string GetString();
        }

        private class StrategyWithoutFileInfo : IStragegy
        {
            private readonly StringBuilder mStringBuilder = new StringBuilder();

            public void AppendLine(CodeLocation codeLocation, string txt)
            {
                mStringBuilder.AppendLine(txt);
            }

            public string GetString()
            {
                return mStringBuilder.ToString();
            }
        }

        private class StrategyWithFileInfo : IStragegy
        {
            private static readonly ITableColumn sDefinitionColumn = new SimpleTableColumn("Definition");
            private static readonly ITableColumn sFileInfoColumn = new SimpleTableColumn("Location");

            private readonly ITableBuilder mStringBuilder;

            public StrategyWithFileInfo(ITableBuilder stringBuilder)
            {
                mStringBuilder = stringBuilder;
            }

            public void AppendLine(CodeLocation codeLocation, string txt)
            {
                mStringBuilder.NewRow();
                mStringBuilder.Print(sDefinitionColumn, txt);
                mStringBuilder.Print(sFileInfoColumn, codeLocation.GetFullInfoWithSameSyntaxAsStackTrace());
            }

            public string GetString()
            {
                var sb = new StringBuilder();
                mStringBuilder.GenerateTable(sb);
                return sb.ToString();
            }
        }

        #endregion

        [NotNull] private readonly ITableBuilder mStringBuilder;

        private int mIndentation;
        private IStragegy mStragegy = new StrategyWithoutFileInfo();

        public PrintDefinitionDirector([NotNull] IActionVisitMapper<INode, IPrintDefinitionDirector> visitMapper,
                                       [NotNull] ITableBuilder stringBuilder)
            : base(visitMapper)
        {
            if (stringBuilder == null) throw new ArgumentNullException("stringBuilder");
            mStringBuilder = stringBuilder;
        }

        public string IndentationString { get; set; }
        public bool IsRecursive { get; set; }

        public bool IsFileInfo
        {
            set
            {
                if (value)
                    mStragegy = new StrategyWithFileInfo(mStringBuilder);
                else
                    mStragegy = new StrategyWithoutFileInfo();
            }
        }

        public void Indent()
        {
            mIndentation++;
        }

        public void Dedent()
        {
            mIndentation--;
        }

        public void PrintLine(CodeLocation codeLocation, string format, params object[] args)
        {
            string indentedTxt = IndentTxt(format, args);
            mStragegy.AppendLine(codeLocation, indentedTxt);
        }

        public string GetString()
        {
            return mStragegy.GetString();
        }

        private string IndentTxt(string format, object[] args)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < mIndentation; i++)
                sb.Append(IndentationString);

            sb.AppendFormat(format, args);

            string txt = sb.ToString();
            return txt;
        }
    }
}