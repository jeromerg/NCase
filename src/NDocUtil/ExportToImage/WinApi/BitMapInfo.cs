using System.Runtime.InteropServices;

namespace NDocUtilLibrary.ExportToImage.WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct BitMapInfo
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
        public byte bmiColors_rgbBlue;
        public byte bmiColors_rgbGreen;
        public byte bmiColors_rgbRed;
        public byte bmiColors_rgbReserved;
    }
}