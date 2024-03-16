using PCConfigurator.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Stores
{
    internal class NavigationStorage(BaseViewModel viewModel)
    {
        private BaseViewModel _currentViewModel = viewModel;

        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                ViewModelChanged();
            }
        }

        public event Action ViewModelChanged;

        protected void OnPropertyChanged()
        {
            ViewModelChanged?.Invoke();
        }
    }
}
