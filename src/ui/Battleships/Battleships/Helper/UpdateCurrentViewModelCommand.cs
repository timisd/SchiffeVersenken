using Battleships.Services.Navigation;
using System;
using System.Windows.Input;
using Battleships.MVVM.ViewModels;

namespace Battleships.Helper;

internal class UpdateCurrentViewModelCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly INavigator _navigator;

    public UpdateCurrentViewModelCommand(INavigator navigator)
    {
        _navigator = navigator;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not ViewsEnum viewType) return;

        switch (viewType)
        {
            case ViewsEnum.Menu:
                _navigator.Navigate(new MenuViewModel(_navigator));
                break;
            case ViewsEnum.Options:
                _navigator.Navigate(new OptionsViewModel(_navigator));
                break;
            case ViewsEnum.ShipPlacement:
                _navigator.Navigate(new ShipPlacementViewModel(_navigator));
                break;
            default:
                break;
        }
    }
}