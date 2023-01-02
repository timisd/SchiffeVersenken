using System.Windows.Input;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class ShipPlacementViewModel : BaseViewModel
{
    public ICommand UpdateCurrentViewModelCommand { get; }
    
    public ShipPlacementViewModel(INavigator navigator) : base("Schiffe Versenken - Schiffe Platzieren")
    {
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
    }
}