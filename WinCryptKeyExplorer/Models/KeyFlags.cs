using System;

namespace WinCryptKeyExplorer.Models {
    [Flags]
    public enum KeyFlags : UInt32 {
        None                  = 0,
        MachineKeySet         = 0x20,
        Silent                = 0x40,
        OverwriteKey          = 0x80,
        WriteKeyToLegacyStore = 0x200,
        DoNotFinalize         = 0x400,
        PersistOnly           = 0x40000000,
        Persist               = 0x80000000
    }
}