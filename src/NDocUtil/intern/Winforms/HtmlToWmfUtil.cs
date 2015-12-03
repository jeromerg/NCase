using System;
using System.Drawing;
using System.Drawing.Imaging;
using NDocUtil.intern.WinApi;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace NDocUtil.intern.Winforms
{
    public class HtmlToWmfUtil
    {
        public static Metafile Convert(string htmlSnippet)
        {
            Metafile image;
            IntPtr dib;
            var memoryHdc = Win32Utils.CreateMemoryHdc(IntPtr.Zero, 1, 1, out dib);
            try
            {
                image = new Metafile(memoryHdc, EmfType.EmfPlusDual, "..");

                using (var g = Graphics.FromImage(image))
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
