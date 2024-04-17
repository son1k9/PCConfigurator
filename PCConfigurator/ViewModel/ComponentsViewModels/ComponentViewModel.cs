using PCConfigurator.Commands;
using PCConfigurator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace PCConfigurator.ViewModel.ComponentsViewModels
{
    internal abstract class ComponentViewModel : BaseViewModel
    {
        protected ApplicationContext dbContext = new ApplicationContext();

        protected readonly CollectionViewSource _viewSource = new CollectionViewSource();

        public ICollectionView ViewSource { get => _viewSource.View; }

        protected abstract void ResetContext();

        protected RelayCommand? _add;
        public ICommand Add => _add ??= new RelayCommand(PerformAdd);
        protected abstract void PerformAdd(object? commandParameter);

        protected RelayCommand? _remove;
        public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
        protected abstract void PerformRemove(object? commandParameter);

        protected RelayCommand? _edit;
        public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
        protected abstract void PerformEdit(object? commandParameter);
    }
}
