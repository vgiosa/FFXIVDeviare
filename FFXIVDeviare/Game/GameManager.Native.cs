using System;
using System.Runtime.InteropServices;

namespace FFXIVDeviare.Game
{
    partial class GameManager
    {
        private static class Native
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct FILETIME
            {
                public UInt32 dwDateTimeLow;
                public UInt32 dwDateTimeHigh;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct SYSTEMTIME
            {
                public UInt16 wYear;
                public UInt16 wMonth;
                public UInt16 wDayOfWeek;
                public UInt16 wDay;
                public UInt16 wHour;
                public UInt16 wMinute;
                public UInt16 wSecond;
                public UInt16 wMilliseconds;
            }

            [DllImport("kernel32.dll")]
            public static extern void GetSystemTime(out SYSTEMTIME lpSystemTime);

            [DllImport("kernel32.dll")]
            public static extern bool SystemTimeToFileTime(ref SYSTEMTIME pSystemTime, out FILETIME lpFileTime);
        }
    }
}
