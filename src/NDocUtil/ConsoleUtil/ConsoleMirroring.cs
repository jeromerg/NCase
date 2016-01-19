using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace NDocUtilLibrary.ConsoleUtil
{
    /// <summary>inspired by: http://stackoverflow.com/questions/420429/mirroring-console-output-to-a-file </summary>
    public class ConsoleMirroring : TextWriter
    {
        [NotNull] private readonly TextWriter mConsoleOutput;
        [NotNull] private readonly TextWriter mConsoleError;

        [NotNull] private readonly StringWriter mTextWriter;

        public ConsoleMirroring()
        {
            if (Console.Out == null) throw new ArgumentException("Console.Out is null");
            if (Console.Error == null) throw new ArgumentException("Console.Error is null");

            mTextWriter = new StringWriter();
            mConsoleOutput = Console.Out;
            mConsoleError = Console.Error;

            Console.SetOut(this);
            Console.SetError(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Console.SetOut(mConsoleOutput);
                Console.SetError(mConsoleError);
            }
        }

        public override Encoding Encoding
        {
            get { return mConsoleOutput.Encoding; }
        }

        public override IFormatProvider FormatProvider
        {
            get { return mConsoleOutput.FormatProvider; }
        }

        public override string NewLine
        {
            get { return mConsoleOutput.NewLine; }
            set { mConsoleOutput.NewLine = value; }
        }

        public override void Close()
        {
            mConsoleOutput.Close();
            mTextWriter.Close();
        }

        public override void Flush()
        {
            mConsoleOutput.Flush();
            mTextWriter.Flush();
        }

        public override void Write(double value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(string value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(object value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(decimal value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(float value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(bool value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(int value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(uint value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(ulong value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(long value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(char[] buffer)
        {
            mConsoleOutput.Write(buffer);
            mTextWriter.Write(buffer);
        }

        public override void Write(char value)
        {
            mConsoleOutput.Write(value);
            mTextWriter.Write(value);
        }

        public override void Write(string format, params object[] arg)
        {
            mConsoleOutput.Write(format, arg);
            mTextWriter.Write(format, arg);
        }

        public override void Write(string format, object arg0)
        {
            mConsoleOutput.Write(format, arg0);
            mTextWriter.Write(format, arg0);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            mConsoleOutput.Write(format, arg0, arg1);
            mTextWriter.Write(format, arg0, arg1);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            mConsoleOutput.Write(buffer, index, count);
            mTextWriter.Write(buffer, index, count);
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            mConsoleOutput.Write(format, arg0, arg1, arg2);
            mTextWriter.Write(format, arg0, arg1, arg2);
        }

        public override void WriteLine()
        {
            mConsoleOutput.WriteLine();
            mTextWriter.WriteLine();
        }

        public override void WriteLine(double value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(decimal value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(string value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(object value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(float value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(bool value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(uint value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(long value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(ulong value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(int value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(char[] buffer)
        {
            mConsoleOutput.WriteLine(buffer);
            mTextWriter.WriteLine(buffer);
        }

        public override void WriteLine(char value)
        {
            mConsoleOutput.WriteLine(value);
            mTextWriter.WriteLine(value);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            mConsoleOutput.WriteLine(format, arg);
            mTextWriter.WriteLine(format, arg);
        }

        public override void WriteLine(string format, object arg0)
        {
            mConsoleOutput.WriteLine(format, arg0);
            mTextWriter.WriteLine(format, arg0);
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            mConsoleOutput.WriteLine(format, arg0, arg1);
            mTextWriter.WriteLine(format, arg0, arg1);
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            mConsoleOutput.WriteLine(buffer, index, count);
            mTextWriter.WriteLine(buffer, index, count);
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            mConsoleOutput.WriteLine(format, arg0, arg1, arg2);
            mTextWriter.WriteLine(format, arg0, arg1, arg2);
        }

        public override string ToString()
        {
            return mTextWriter.ToString();
        }
    }
}