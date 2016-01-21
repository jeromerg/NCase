using System;
using System.Drawing;
using System.Drawing.Imaging;
using JetBrains.Annotations;
using NDocUtilLibrary.ExportToImage.WinApi;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace NDocUtilLibrary.ExportToImage
{
    public class HtmlToMetafileUtil
    {
        [NotNull]
        public static Metafile Convert(string htmlSnippet)
        {
            Metafile image;
            IntPtr dib;
            IntPtr memoryHdc = Win32Utils.CreateMemoryHdc(IntPtr.Zero, 1, 1, out dib);
            try
            {
                image = new Metafile(memoryHdc, EmfType.EmfPlusDual, "..");

                using (Graphics g = Graphics.FromImage(image))
                {
                    HtmlRender.Render(g, htmlSnippet);
                }
            }
            finally
            {
                Win32Utils.ReleaseMemoryHdc(memoryHdc, dib);
            }
            return image;
        }
    }
}