namespace WinCryptKeyExplorer.Models {
    public enum X509ProviderType2 {
        NONE          = 0,
        RSA_FULL      = 1,
        RSA_SIG       = 2,
        DSS           = 3,
        FORTEZZA      = 4,
        MS_EXCHANGE   = 5,
        SSL           = 6,
        RSA_SCHANNEL  = 12,
        DSS_DH        = 13,
        EC_ECDSA_SIG  = 14,
        EC_ECNRA_SIG  = 15,
        EC_ECDSA_FULL = 16,
        EC_ECNRA_FULL = 17,
        DH_SCHANNEL   = 18,
        SPYRUS_LYNKS  = 20,
        RNG           = 21,
        INTEL_SEC     = 22,
        REPLACE_OWF   = 23,
        RSA_AES       = 24,
    }
}