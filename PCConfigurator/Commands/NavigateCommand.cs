using PCConfigurator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCConfigurator.Commands
{
    internal class NavigateCommand<T>(NavigationStorage navigationStorage) : ICommand
        where T : ViewModel.BaseViewModel, new()
    {
        private readonly NavigationStorage _navigationStorage = navigationStorage;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _navigationStorage.CurrentViewModel = new T();
        }
    }
}
