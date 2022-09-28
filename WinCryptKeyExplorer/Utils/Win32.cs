using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using WinCryptKeyExplorer.Models;

namespace WinCryptKeyExplorer.Utils {
    static class Win32 {
        static Int64 CRYPT_MACHINE_KEYSET = 0x20;
        static Int64 CRYPT_VERIFYCONTEXT = 0xF0000000;
        static UInt32 CRYPT_FIRST = 1;
        static UInt32 CRYPT_NEXT = 2;
        static UInt32 PP_ENUMCONTAINERS = 2;

        [DllImport("ncrypt.dll", SetLastError = true)]
        public static extern Int32 NCryptOpenStorageProvider(
            out SafeNCryptProviderHandle phProvider,
            [MarshalAs(UnmanagedType.LPWStr)]
            String pszProviderName,
            UInt32 dwFlags
        );
        [DllImport("ncrypt.dll", SetLastError = true)]
        public static extern Int32 NCryptEnumKeys(
            SafeNCryptProviderHandle hProvider,
            [MarshalAs(UnmanagedType.LPWStr)]
            String pszScope,
            ref IntPtr ppKeyName,
            ref IntPtr ppEnumState,
            UInt32 dwFlags
        );
        [DllImport("ncrypt.dll", SetLastError = true)]
        public static extern Int32 NCryptFreeObject(
            [In] IntPtr phProvider
        );
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean CryptAcquireContext(
            ref IntPtr phProv,
            String pszContainer,
            String pszProvider,
            UInt32 dwProvType,
            Int64 dwFlags
        );
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean CryptGetProvParam(
            IntPtr hProv,
            UInt32 dwParam,
            [MarshalAs(UnmanagedType.LPStr)]
            StringBuilder pbData,
            ref Int32 pdwDataLen,
            UInt32 dwFlags
        );
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean CryptReleaseContext(
            IntPtr hProv,
            UInt32 dwFlags
        );
        [StructLayout(LayoutKind.Sequential)]
        public struct NCryptKeyName {
            [MarshalAs(UnmanagedType.LPWStr)]
            public String pszName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public String pszAlgid;
            public Int32 dwLegacyKeySpec;
            public KeyFlags dwFlags;
        }
    }
}
