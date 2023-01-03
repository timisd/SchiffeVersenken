using System.Windows.Input;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class MenuViewModel : BaseViewModel
{
    public ICommand SinglePlayerButtonCommand { get; }
    public ICommand MultiPlayerButtonCommand { get; }

    private INavigator _navigator;
    private ICommand _updateCurrentViewModelCommand;

    public MenuViewModel(INavigator navigator)
    {
        _navigator = navigator;

        _updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);

        SinglePlayerButtonCommand = new RelayCommand(SinglePlayerButton_Clicked);
        MultiPlayerButtonCommand = new RelayCommand(MultiPlayerButton_Clicked);
    }

    private void SinglePlayerButton_Clicked()
    {
        var dto = new GameSetupDto(_navigator, ViewsEnum.GameSetup, false);
        _updateCurrentViewModelCommand.Execute(dto);
    }

    private void MultiPlayerButton_Clicked()
    {
        var dto = new GameSetupDto(_navigator, ViewsEnum.GameSetup, true);
        _updateCurrentViewModelCommand.Execute(dto);
    }
}