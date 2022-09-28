# WinCryptKeyExplorer

WinCryptKeyExplorer is a WPF-based application to explore Cryptographic Service Providers (CSP) and Key Storage Providers (KSP), their algorithms and persistent keys backed by provider.

# Usage
Simply start executable and wait until providers are loaded.
![image](https://user-images.githubusercontent.com/6384119/192849475-42a72ad8-6d8d-4447-98ad-51ae829cbd0b.png)

The following properties are shown for the provider:
- Name -- provider name
- Provider type -- [provider type](https://learn.microsoft.com/en-us/windows/win32/seccrypto/cryptographic-provider-types)
- Is Legacy -- when checked, means that the provider is legacy CSP. When unchecked, it is modern CNG KSP.
- Is Hardware -- when checked, key material storage is backed by secure and tamper-evident hardware chip (smart card, HSM)
- Hardware RNG -- when checked, the provider supports a hardware random number generator that can be used to create random bytes for. Not all hardware-based providers may support this.
- Software -- when checked, provider is implemented in software. Because a provider can be implemented in both hardware and software, you cannot assume that a checked "Software" property indicates that there is no hardware component.
- Is Removable -- 
