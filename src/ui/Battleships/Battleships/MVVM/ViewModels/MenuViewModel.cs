using System;
using Battleships.Helper;
using Battleships.Services.Navigation;
using System.Windows.Input;

namespace Battleships.MVVM.ViewModels;

internal class MenuViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }
    public ICommand CloseCommand { get; }

    public ICommand UpdateCurrentViewModelCommand { get; }

    public MenuViewModel(INavigator navigator)
    {
        CloseCommand = new RelayCommand(() => Environment.Exit(0));
        Navigator = navigator;
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
    }
}