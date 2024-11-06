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

namespace PCConfigurator.ViewModel.ComponentsViewModels;

/// <summary>
/// Base class for all ComponentViewModels
/// </summary>
public abstract class ComponentViewModel : BaseViewModel
{
    /// <summary>
    /// Database context to use for operations
    /// </summary>
    protected ApplicationContext dbContext = new ApplicationContext();

    /// <summary>
    /// CollectionViewSource of components
    /// </summary>
    protected readonly CollectionViewSource _viewSource = new CollectionViewSource();

    /// <summary>
    /// View of CollectionViewSource of components
    /// </summary>
    public ICollectionView ViewSource { get => _viewSource.View; }

    /// <summary>
    /// Resets database context
    /// </summary>
    protected abstract void ResetContext();

    protected RelayCommand? _add;
    /// <summary>
    /// Command to add a component
    /// </summary>
    public ICommand Add => _add ??= new RelayCommand(PerformAdd);
    /// <summary>
    /// Adds a components
    /// </summary>
    /// <param name="commandParameter">Component to add</param>
    protected abstract void PerformAdd(object? commandParameter);

    protected RelayCommand? _remove;
    /// <summary>
    /// Command ro remove a component
    /// </summary>
    public ICommand Remove => _remove ??= new RelayCommand(PerformRemove);
    /// <summary>
    /// Removes a component
    /// </summary>
    /// <param name="commandParameter">Component to remove</param>
    protected abstract void PerformRemove(object? commandParameter);

    protected RelayCommand? _edit;
    /// <summary>
    /// Command to edit a component
    /// /// </summary>
    public ICommand Edit => _edit ??= new RelayCommand(PerformEdit);
    /// <summary>
    /// Edits a component
    /// </summary>
    /// <param name="commandParameter">Component to edit</param>
    protected abstract void PerformEdit(object? commandParameter);
}

