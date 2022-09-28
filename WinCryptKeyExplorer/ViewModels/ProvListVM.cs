using System.Collections.ObjectModel;
using WinCryptKeyExplorer.Models;

namespace WinCryptKeyExplorer.ViewModels {
    class ProvListVM : ViewModelBase {

        public ProvListVM(ObservableCollection<CspProviderVM> providers) {
            Providers = providers;
        }

        public ObservableCollection<CspProviderVM> Providers { get; }
    }
}
