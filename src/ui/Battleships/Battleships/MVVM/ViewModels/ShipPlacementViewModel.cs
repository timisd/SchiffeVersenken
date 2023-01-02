using Battleships.Helper;
using Battleships.Services.Navigation;
using System.Windows.Input;

namespace Battleships.MVVM.ViewModels;

internal class ShipPlacementViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }
    public ICommand UpdateCurrentViewModelCommand { get; }

    public ShipPlacementViewModel(INavigator navigator)
    {
        Navigator = navigator;
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
    }
}