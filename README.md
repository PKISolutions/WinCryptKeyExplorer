# WinCryptKeyExplorer

WinCryptKeyExplorer is a WPF-based application to explore Cryptographic Service Providers (CSP) and Key Storage Providers (KSP), their algorithms and persistent keys backed by provider. This project is a visualization of some of my former PowerShell works: [Get registered CSPs on the system](https://www.sysadmins.lv/blog-en/get-registered-csps-on-the-system.aspx), [Get registered CNG CSPs on the system](https://www.sysadmins.lv/blog-en/get-registered-cng-csps-on-the-system.aspx) and [Certutil tips and tricks: query cryptographic service providers (CSP and KSP)](https://www.sysadmins.lv/blog-en/certutil-tips-and-tricks-query-cryptographic-service-providers-csp-and-ksp.aspx)

# Usage
Simply start executable and wait until providers are loaded.
![image](https://user-images.githubusercontent.com/6384119/192849475-42a72ad8-6d8d-4447-98ad-51ae829cbd0b.png)

The following properties are shown for the provider:
- **Name** -- provider name
- **Provider type** -- [provider type](https://learn.microsoft.com/en-us/windows/win32/seccrypto/cryptographic-provider-types)
- **Is Legacy** -- when checked, means that the provider is legacy CSP. When unchecked, it is modern CNG KSP.
- **Is Hardware** -- when checked, key material storage is backed by secure and tamper-evident hardware chip (smart card, HSM)
- **Hardware RNG** -- when checked, the provider supports a hardware random number generator that can be used to create random bytes for. Not all hardware-based providers may support this.
- **Software** -- when checked, provider is implemented in software. Because a provider can be implemented in both hardware and software, you cannot assume that a checked "Software" property indicates that there is no hardware component.
- **Is Removable** -- when checked, the hardware token can be removed from the system. Operator cards and smart cards are examples of removable tokens that can contain keys.
- **Is SmartCard** -- when checked, the provider is smart card provider.
- **Max. Container Length** -- specifies the maximum string length for key container name.
- **KeySpec** -- specifies the general key operations supported by the provider. This property is used only by legacy CSPs and always set to `None` for CNG/KSP.
- **Is Valid** -- when checked, indicates that the provider is completely registered on a system and can be used to generate and store keys.

The following properties are shown for provider algorithms:
- **Display Name** -- display name of the algorithm.
- **Algorithm Type** -- specifies the primary key operation. It is often the first value of the **Operations** column.
- **Operations** -- specifies a set of cryptographic operations supported by the algorithm (Asymmetric Encryption, Signature, Secret Agreement, Cipher, Hash, etc.).
- **Default Length** -- specifies the default key length when algorithm is initialized with no parameters. This property is not applicable to hash algorithms.
- **Minimum Key Length** -- specifies the minimum key length supported by the algorithm. This property is not applicable to hash algorithms.
- **Maximum Key Length** -- specifies the maximum key length supported by the algorithm. This property is not applicable to hash algorithms.
- **Increment Length** -- specifies the minimum key length increment. For fixed length algorithms (such as ECDSA, ECDH, AES, etc.) this property is set to zero. And Default, Minimum and Maximum key lengths are identical. This property is not applicable to hash algorithms.
- **Is Valid** -- when checked, the algorithm is usable.

The following properties are shown for keys:
- **Key Name** -- specifies the key container name used to uniquely identify the key container.
- **Algorithm** -- specifies the key algorithm.
- **KeySpec** -- same as for providers.
- **Flags** -- either `None` or `MachineKeySet`. If set to `None`, the key is stored in current user profile, if set to `MachineKeySet`, the key is stored in local system profile.

**Note:** same key may appear under multiple providers. Software-based providers are not necessary isolated and same key can be accessed using different providers. Moreover, keys from legacy CSPs can be shown under CNG providers. This is because of KSP->CSP bridge provided by a [NCryptTranslateHandle](https://docs.microsoft.com/en-us/windows/win32/api/ncrypt/nf-ncrypt-ncrypttranslatehandle) function. That is, KSP can access keys stored in legacy CSPs (software-based only) in unified way. Opposite is not true, CSPs cannot access keys generated and stored by KSP.
