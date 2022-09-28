using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CERTENROLLLib;
using SysadminsLV.WPF.OfficeTheme.Toolkit.Commands;
using WinCryptKeyExplorer.Models;

namespace WinCryptKeyExplorer.ViewModels {
    class MainWindowVM : ViewModelBase {
        INotifyPropertyChanged mainPanelVM;
        CspProviderVM selectedProv;
        Boolean isBusy;

        public MainWindowVM() {
            EnumProviderKeys = new RelayCommand(enumProvKeys, canEnumKeys);
            LoadProvidersCommand = new AsyncCommand(enumProviders);
            MainPanelVM = new ProvListVM(Providers);
            LoadProvidersCommand.ExecuteAsync(null);
        }

        public IAsyncCommand LoadProvidersCommand { get; }
        public ICommand EnumProviderKeys { get; }
        public ObservableCollection<CspProviderVM> Providers { get; } = new ObservableCollection<CspProviderVM>();

        public INotifyPropertyChanged MainPanelVM {
            get => mainPanelVM;
            set {
                mainPanelVM = value;
                OnPropertyChanged(nameof(MainPanelVM));
            }
        }
        public CspProviderVM SelectedProvider {
            get => selectedProv;
            set {
                selectedProv = value;
                OnPropertyChanged(nameof(SelectedProvider));
                selectedProv?.EnumKeys();
            }
        }
        public Boolean IsBusy {
            get => isBusy;
            set {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        async Task enumProviders(Object o, CancellationToken token) {
            IsBusy = true;
            Providers.Clear();
            foreach (CspProviderVM csp in await enumProvsAsync()) {
                Providers.Add(csp);
            }

            IsBusy = false;
        }
        Task<IEnumerable<CspProviderVM>> enumProvsAsync() {
            return Task<IEnumerable<CspProviderVM>>.Factory.StartNew(() => {
                var retValue = new List<CspProviderVM>();
                var providers = new CCspInformations();
                providers.AddAvailableCsps();
                foreach (ICspInformation csp in providers) {
                    retValue.Add(new CspProviderVM(csp));
                }

                return retValue;
            });
        }

        void enumProvKeys(Object o) {
            SelectedProvider.EnumKeys();
        }
        Boolean canEnumKeys(Object o) {
            return SelectedProvider != null;
        }
    }
}
