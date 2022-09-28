using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using CERTENROLLLib;
using Microsoft.Win32.SafeHandles;
using SysadminsLV.WPF.OfficeTheme.Toolkit;
using WinCryptKeyExplorer.ViewModels;
using static WinCryptKeyExplorer.Utils.Win32;

namespace WinCryptKeyExplorer.Models {
    class CspProviderVM : ViewModelBase {
        Boolean isBusy;
        public CspProviderVM(ICspInformation csp) {
            Name = csp.Name;
            Type = (X509ProviderType2)csp.Type;
            IsHardware = csp.IsHardwareDevice;
            IsSoftware = csp.IsSoftwareDevice;
            IsRemovable = csp.IsRemovable;
            IsSmartCard = csp.IsSmartCard;
            IsLegacy = csp.LegacyCsp;
            HardwareRNG = csp.HasHardwareRandomNumberGenerator;
            KeyContainerLength = csp.MaxKeyContainerNameLength;
            KeySpec = (X509KeySpec2)csp.KeySpec;
            Version = csp.Version;
            IsValid = csp.Valid;
            foreach (ICspAlgorithm alg in csp.CspAlgorithms) {
                Algorithms.Add(new CspProvAlg(alg));
            }
            Marshal.ReleaseComObject(csp);
        }
        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Gets the type of the provider.
        /// </summary>
        public X509ProviderType2 Type { get; set; }
        /// <summary>
        /// Gets a collection of <see cref="CspProviderAlgorithmInfo"/> objects that contains information about the algorithms
        /// supported by the provider.
        /// </summary>
        public List<CspProvAlg> Algorithms { get; } = new List<CspProvAlg>();
        /// <summary>
        /// Gets a Boolean value that determines whether the provider is implemented in a hardware device.
        /// </summary>
        public Boolean IsHardware { get; set; }
        /// <summary>
        /// Gets a Boolean value that specifies whether the provider is implemented in software.
        /// </summary>
        public Boolean IsSoftware { get; set; }
        /// <summary>
        /// Gets a Boolean value that specifies whether the token that contains the key can be removed.
        /// </summary>
        public Boolean IsRemovable { get; set; }
        /// <summary>
        /// Gets a Boolean value that specifies whether the provider is a smart card provider.
        /// </summary>
        public Boolean IsSmartCard { get; set; }
        /// <summary>
        /// Gets a Boolean value that specifies whether the provider is a Cryptography API: Next Generation (CNG)
        /// provider or a CryptoAPI (legacy) CSP.
        /// </summary>
        public Boolean IsLegacy { get; set; }
        /// <summary>
        /// Gets a Boolean value that specifies whether the provider supports a hardware random number generator
        /// that can be used to create random bytes for cryptographic operations.
        /// </summary>
        public Boolean HardwareRNG { get; set; }
        /// <summary>
        /// Gets the maximum supported length for the name of the private key container associated with the provider.
        /// </summary>
        public Int32 KeyContainerLength { get; set; }
        /// <summary>
        /// Gets a value that specifies the intended use of the algorithms supported by the provider.
        /// </summary>
        public X509KeySpec2 KeySpec { get; set; }
        /// <summary>
        /// Gets the version number of the provider.
        /// </summary>
        public Int32 Version { get; set; }
        /// <summary>
        /// Gets a Boolean value that specifies whether the provider is installed on the client computer.
        /// </summary>
        public Boolean IsValid { get; set; }

        public ObservableCollection<KeyContainerVM> Keys { get; } = new ObservableCollection<KeyContainerVM>();
        public Boolean IsBusy {
            get => isBusy;
            set {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public void EnumKeys() {
            IsBusy = true;
            Keys.Clear();
            foreach (UInt32 flags in new[] { 0x40, 0x60 }) {
                try {
                    Int32 hresult = NCryptOpenStorageProvider(out SafeNCryptProviderHandle phProvider, Name, 0);
                    if (hresult > 0) {
                        MsgBox.Show("Error", "Failed to open provider:\n" + new Win32Exception(hresult).Message);

                        return;
                    }

                    IntPtr ppKeyName = IntPtr.Zero;
                    IntPtr ppEnumState = IntPtr.Zero;
                    do {
                        hresult = NCryptEnumKeys(phProvider, null, ref ppKeyName, ref ppEnumState, flags);
                        if (hresult == 0) {
                            NCryptKeyName keyStruct = Marshal.PtrToStructure<NCryptKeyName>(ppKeyName);
                            Keys.Add(new KeyContainerVM {
                                KeyName = keyStruct.pszName,
                                Algorithm = keyStruct.pszAlgid,
                                KeySpec = (X509KeySpec2)keyStruct.dwLegacyKeySpec,
                                Flags = keyStruct.dwFlags
                            });

                            NCryptFreeObject(ppKeyName);
                        } else {
                            ppKeyName = IntPtr.Zero;
                        }
                    } while (!IntPtr.Zero.Equals(ppKeyName));
                    NCryptFreeObject(phProvider.DangerousGetHandle());
                    if (!IntPtr.Zero.Equals(ppEnumState)) {
                        NCryptFreeObject(ppEnumState);
                    }
                } catch (Exception ex) {
                    MsgBox.Show("Error", "Failed to enumerate keys:\n" + ex.Message);
                }
            }


            IsBusy = false;
        }
    }
}
