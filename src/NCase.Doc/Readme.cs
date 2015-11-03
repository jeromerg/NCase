using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using NUtil.Doc;

namespace NCaseFramework.doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Readme
    {
        // ReSharper disable once InconsistentNaming
        private readonly ConsoleRecorder Console = new ConsoleRecorder();

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            new DocUtil().UpdateDocAssociatedToThisFile(Console);
        }

        public enum Architecture { x86, x64, arm }
        public struct Size
        {
            int Horizontal {get;set;}
            int Vertical {get;set;}
        }
        public interface IHardware
        {
            Architecture Architecture { get; set; }
            int RamInGb { get; set; }
            int HardDriveInGb { get; set; }
            Size ScreenResolution { get; set; }
        }

        public enum Os
        {
            Ios6,
            Ios7,
            Ios8,
            Android4,
            Android5,
            Android6,
            WindowsMobile7,
            WindowsMobile8,
            WindowsMobile10,
            Windows7,
            OsX
        }

        public enum Browser
        {
            Chrome,
            Safari,
            Firefox,
            Dolphin
        }

        public interface ISoftware
        {
            Os Os { get; set; }
            Browser Browser { get; set; }
            bool IsFacebookInstalled { get; set; }
            bool IsTwitterInstalled { get; set; }
        }

        public interface IUser
        {
            string Culture { get; set; }
        }
    }
}