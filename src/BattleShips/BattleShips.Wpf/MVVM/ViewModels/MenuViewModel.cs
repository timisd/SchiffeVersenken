using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class MenuViewModel : BaseViewModel
{
    public MenuViewModel(INavigator navigator) : base("Schiffe Versenken - Menü")
    {
        var updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
    }
}