using System;

namespace WinCryptKeyExplorer.Models {
    [Flags]
    public enum AlgorithmOperationFlags2 {
        NoOperation          = 0,
        Cipher               = 1,
        Hash                 = 2,
        AsymmetricEncryption = 4,
        SecretAgreement      = 8,
        Signature            = 16,
        AnyAsymmetric        = 28,
        RNG                  = 32,
        KeyDerivation        = 64,
        PreferSignatureOnly  = 2097152,
        PreferNonSignature   = 4194304,
        ExactMatch           = 8388608,
        PreferenceMask       = 14680064,
    }
}
