﻿using System;
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
        public static Metafile Convert(
            string htmlSnippet, 
            float leftBorder = 0, 
            float topBorder = 0, 
            float rightBorder = 0, 
            float bottomBorder = 0)
        {
            Metafile image;
            IntPtr dib;
            IntPtr memoryHdc = Win32Utils.CreateMemoryHdc(IntPtr.Zero, 1, 1, out dib);
            try
            {
                image = new Metafile(memoryHdc, EmfType.EmfPlusDual, "..");

                using (Graphics g = Graphics.FromImage(image))
                {
                    SizeF size = HtmlRender.Measure(g, htmlSnippet);
                    
                    g.DrawRectangle(
                        new Pen(Color.FromArgb(0, 0, 0, 0)), 
                        leftBorder * -1, 
                        topBorder * -1, 
                        size.Width + leftBorder + rightBorder, 
                        size.Height + topBorder + bottomBorder);

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