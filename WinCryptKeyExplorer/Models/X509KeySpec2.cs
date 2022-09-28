using System;

namespace WinCryptKeyExplorer.Models {
    [Flags]
    public enum X509KeySpec2 {
        None        = 0,
        KeyExchange = 1,
        Signature   = 2
    }
}
