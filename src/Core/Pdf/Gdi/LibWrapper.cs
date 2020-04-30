using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Fonet.Pdf.Gdi
{
    internal static class LibWrapper
    {
        private static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        internal static IntPtr GetDC(IntPtr hWnd)
        {
            return IsWindows ? LibWrapperWindows.GetDC(hWnd) : LibWrapperLinux.GetDC(hWnd);
        }

        internal static uint GetFontData(
            IntPtr hdc, // handle to DC
            uint dwTable, // metric table name
            uint dwOffset, // offset into table
                byte[] lpvBuffer, // buffer for returned data
            uint cbData // length of data
            )
        {
            return IsWindows ? LibWrapperWindows.GetFontData(hdc, dwTable, dwOffset, lpvBuffer, cbData) : LibWrapperLinux.GetFontData(hdc, dwTable, dwOffset, lpvBuffer, cbData);
        }

        internal static int AddFontResourceEx(
                string lpszFilename, // font file name
            uint fl, // font characteristics
            int pdv // reserved
            )
        {
            return IsWindows ? LibWrapperWindows.AddFontResourceEx(lpszFilename, fl, pdv) : LibWrapperLinux.AddFontResourceEx(lpszFilename, fl, pdv);
        }

        internal static bool RemoveFontResourceEx(
                string lpFileName, // name of font file
            uint fl, // font characteristics
            int pdv // Reserved.
            )
        {
            return IsWindows ? LibWrapperWindows.RemoveFontResourceEx(lpFileName, fl, pdv) : LibWrapperLinux.RemoveFontResourceEx(lpFileName, fl, pdv);
        }

        internal static IntPtr CreateFontIndirect(

                LogFont lplf // characteristics
            )
        {
            return IsWindows ? LibWrapperWindows.CreateFontIndirect(lplf) : LibWrapperLinux.CreateFontIndirect(lplf);
        }

        internal static uint GetGlyphIndices(
            IntPtr hdc, // handle to DC
            string lpstr, // string to convert
            int c, // number of characters in string
                ushort[] pgi, // array of glyph indices
            uint fl // glyph options
            )
        {
            return IsWindows ? LibWrapperWindows.GetGlyphIndices(hdc, lpstr, c, pgi, fl) : LibWrapperLinux.GetGlyphIndices(hdc, lpstr, c, pgi, fl);
        }

        internal static uint GetFontUnicodeRanges(
            IntPtr hdc, // handle to DC
                GlyphSet lpgs // glyph set
            )
        {
            return IsWindows ? LibWrapperWindows.GetFontUnicodeRanges(hdc, lpgs) : LibWrapperLinux.GetFontUnicodeRanges(hdc, lpgs);
        }

        internal static IntPtr SelectObject(
            IntPtr hdc, // handle to DC
            IntPtr hgdiobj // handle to object
            )
        {
            return IsWindows ? LibWrapperWindows.SelectObject(hdc, hgdiobj) : LibWrapperLinux.SelectObject(hdc, hgdiobj);
        }

        internal static IntPtr DeleteObject(
            IntPtr hgdiobj // handle to object
            )
        {
            return IsWindows ? LibWrapperWindows.DeleteObject(hgdiobj) : LibWrapperLinux.DeleteObject(hgdiobj);
        }

        internal static IntPtr GetCurrentObject(
            IntPtr hdc, // handle to DC
            GdiDcObject uObjectType // object type
            )
        {
            return IsWindows ? LibWrapperWindows.GetCurrentObject(hdc, uObjectType) : LibWrapperLinux.GetCurrentObject(hdc, uObjectType);
        }

        internal static int GetTextFace(
            IntPtr hdc, // handle to DC
            int nCount, // length of typeface name buffer
                StringBuilder lpFaceName // typeface name buffer
            )
        {
            return IsWindows ? LibWrapperWindows.GetTextFace(hdc, nCount, lpFaceName) : LibWrapperLinux.GetTextFace(hdc, nCount, lpFaceName);
        }

        internal static bool DeleteDC(
            IntPtr hdc // handle to DC
            )
        {
            return IsWindows ? LibWrapperWindows.DeleteDC(hdc) : LibWrapperLinux.DeleteDC(hdc);
        }

        internal static int EnumFontFamilies(
            IntPtr hdc, // handle to DC
                string lpszFamily, // font family
            FontEnumDelegate lpEnumFontFamProc, // callback function
            int lParam // additional data
            )
        {
            return IsWindows ? LibWrapperWindows.EnumFontFamilies(hdc, lpszFamily, lpEnumFontFamProc, lParam) : LibWrapperLinux.EnumFontFamilies(hdc, lpszFamily, lpEnumFontFamProc, lParam);
        }

        internal static int EnumFontFamiliesEx(
            IntPtr hdc, // handle to DC
                LogFont lplf, // characteristics
            FontEnumDelegate lpEnumFontFamProc, // callback function
            int lParam, // additional data
            int dwFlags // font family
            )
        {
            return IsWindows ? LibWrapperWindows.EnumFontFamiliesEx(hdc, lplf, lpEnumFontFamProc, lParam, dwFlags) : LibWrapperLinux.EnumFontFamiliesEx(hdc, lplf, lpEnumFontFamProc, lParam, dwFlags);
        }
    }

    internal delegate int FontEnumDelegate(
        [MarshalAs(UnmanagedType.Struct)]
            ref EnumLogFont lpelf,
        [MarshalAs(UnmanagedType.Struct)]
            ref NewTextMetric lpntm,
        uint fontType,
        int lParam
        );
}