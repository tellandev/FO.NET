//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Fonet.Pdf.Gdi
{
    internal sealed class LibWrapperLinux
    {
        private static Lazy<ConcurrentDictionary<long, Graphics>> graphicsList = new Lazy<ConcurrentDictionary<long, Graphics>>(() => new ConcurrentDictionary<long, Graphics>());

        internal static IntPtr GetDC(
            IntPtr hWnd // handle to window
            )
        {
            var graphics = Graphics.FromHwnd(hWnd);
            var hdc = graphics.GetHdc();
            graphicsList.Value.TryAdd(hdc.ToInt64(), graphics);

            return hdc;
        }

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern uint GetFontData(
            IntPtr hdc, // handle to DC
            uint dwTable, // metric table name
            uint dwOffset, // offset into table
            [MarshalAs(UnmanagedType.LPArray)]
                byte[] lpvBuffer, // buffer for returned data
            uint cbData // length of data
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern int AddFontResourceEx(
            [In, MarshalAs(UnmanagedType.LPTStr)]
                string lpszFilename, // font file name
            uint fl, // font characteristics
            int pdv // reserved
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern bool RemoveFontResourceEx(
            [In, MarshalAs(UnmanagedType.LPTStr)]
                string lpFileName, // name of font file
            uint fl, // font characteristics
            int pdv // Reserved.
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateFontIndirect(
            [MarshalAs(UnmanagedType.LPStruct)]
                LogFont lplf // characteristics
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern uint GetGlyphIndices(
            IntPtr hdc, // handle to DC
            string lpstr, // string to convert
            int c, // number of characters in string
            [MarshalAs(UnmanagedType.LPArray)]
                ushort[] pgi, // array of glyph indices
            uint fl // glyph options
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern uint GetFontUnicodeRanges(
            IntPtr hdc, // handle to DC
            [Out, MarshalAs(UnmanagedType.LPStruct)]
                GlyphSet lpgs // glyph set
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern IntPtr SelectObject(
            IntPtr hdc, // handle to DC
            IntPtr hgdiobj // handle to object
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern IntPtr DeleteObject(
            IntPtr hgdiobj // handle to object
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetCurrentObject(
            IntPtr hdc, // handle to DC
            GdiDcObject uObjectType // object type
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern int GetTextFace(
            IntPtr hdc, // handle to DC
            int nCount, // length of typeface name buffer
            [MarshalAs(UnmanagedType.LPTStr)]
                StringBuilder lpFaceName // typeface name buffer
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        private static extern bool DeleteDC(
            IntPtr hdc // handle to DC
            );

        internal static bool DeleteDCWithGraphics(IntPtr hdc)
        {
            if (graphicsList.Value.TryRemove(hdc.ToInt64(), out Graphics graphics))
            {
                graphics?.ReleaseHdc();
                graphics?.Dispose();
                return true;
            }
            else
                return DeleteDC(hdc);
        }

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern int EnumFontFamilies(
            IntPtr hdc, // handle to DC
            [MarshalAs(UnmanagedType.LPTStr)]
                string lpszFamily, // font family
            FontEnumDelegate lpEnumFontFamProc, // callback function
            int lParam // additional data
            );

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        internal static extern int EnumFontFamiliesEx(
            IntPtr hdc, // handle to DC
            [MarshalAs(UnmanagedType.LPStruct)]
                LogFont lplf, // characteristics
            FontEnumDelegate lpEnumFontFamProc, // callback function
            int lParam, // additional data
            int dwFlags // font family
            );
    }
}