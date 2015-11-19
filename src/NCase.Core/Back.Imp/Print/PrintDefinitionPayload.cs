using System;
using System.Text;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintDefinitionPayload : IPrintDefinitionPayload
    {
        #region inner types

        private interface IStragegy
        {
            void AppendLine([NotNull] CodeLocation codeLocation, [NotNull] string txt);

            [NotNull]
            string GetString();
        }

        private class StrategyWithoutFileInfo : IStragegy
        {
            [NotNull] private readonly StringBuilder mStringBuilder = new StringBuilder();

            public void AppendLine([NotNull] CodeLocation codeLocation, [NotNull] string txt)
            {
                if (codeLocation == null) throw new ArgumentNullException("codeLocation");
                if (txt == null) throw new ArgumentNullException("txt");

                mStringBuilder.AppendLine(txt);
            }

            [NotNull]
            public string GetString()
            {
                return mStringBuilder.ToString();
            }
        }

        private class StrategyWithFileInfo : IStragegy
        {
            [NotNull] private static readonly ITableColumn sDefinitionColumn = new SimpleTableColumn("Definition");
            [NotNull] private static readonly ITableColumn sFileInfoColumn = new SimpleTableColumn("Location");

            [NotNull] private readonly ITableBuilder mStringBuilder;

            public StrategyWithFileInfo([NotNull] ITableBuilder stringBuilder)
            {
                mStringBuilder = stringBuilder;
            }

            public void AppendLine([NotNull] CodeLocation codeLocation, [NotNull] string txt)
            {
                mStringBuilder.NewRow();
                mStringBuilder.Print(sDefinitionColumn, txt);
                mStringBuilder.Print(sFileInfoColumn, codeLocation.GetFullInfoWithSameSyntaxAsStackTrace());
            }

            [NotNull]
            public string GetString()
            {
                var sb = new StringBuilder();
                mStringBuilder.GenerateTable(sb);
                return sb.ToString();
            }
        }

        #endregion

        private const string INDENTATION_STRING = "    ";

        private readonly bool mIsRecursive;
        [NotNull] private readonly IStragegy mStragegy;

        private int mIndentation;

        public class Factory : IPrintDefinitionPayloadFactory
        {
            [NotNull] private readonly ITableBuilderFactory mTableBuilderFactory;

            public Factory([NotNull] ITableBuilderFactory tableBuilderFactory)
            {
                mTableBuilderFactory = tableBuilderFactory;
            }

            public IPrintDefinitionPayload Create(bool isFileInfo, bool isRecursive)
            {
                return new PrintDefinitionPayload(mTableBuilderFactory.Create(), isFileInfo, isRecursive);
            }
        }

        public PrintDefinitionPayload([NotNull] ITableBuilder tableBuilder, bool isFileInfo, bool isRecursive)
        {
            if (tableBuilder == null) throw new ArgumentNullException("tableBuilder");
            mIsRecursive = isRecursive;

            if (isFileInfo)
                mStragegy = new StrategyWithFileInfo(tableBuilder);
            else
                mStragegy = new StrategyWithoutFileInfo();
        }

        public bool IsRecursive
        {
            get { return mIsRecursive; }
        }

        public void Indent()
        {
            mIndentation++;
        }

        public void Dedent()
        {
            mIndentation--;
        }

        public void PrintLine([NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (format == null) throw new ArgumentNullException("format");
            if (args == null) throw new ArgumentNullException("args");

            string indentedTxt = IndentTxt(format, args);
            mStragegy.AppendLine(codeLocation, indentedTxt);
        }

        [NotNull]
        public string GetString()
        {
            return mStragegy.GetString();
        }

        [NotNull]
        private string IndentTxt([NotNull] string format, [NotNull] object[] args)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < mIndentation; i++)
                sb.Append(INDENTATION_STRING);

            sb.AppendFormat(format, args);

            string txt = sb.ToString();
            return txt;
        }
    }
}