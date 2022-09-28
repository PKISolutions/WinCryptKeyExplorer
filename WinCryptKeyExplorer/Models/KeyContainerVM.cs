using System;

namespace WinCryptKeyExplorer.Models {
    public class KeyContainerVM {
        public String KeyName { get; set; }
        public String Algorithm { get; set; }
        public X509KeySpec2 KeySpec { get; set; }
        public UInt32 Flags { get; set; }
    }
}