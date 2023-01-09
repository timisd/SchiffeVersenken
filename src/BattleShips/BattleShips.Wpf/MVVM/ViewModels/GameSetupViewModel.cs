using System.Windows;
using System.Windows.Input;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class GameSetupViewModel : BaseViewModel
{
    public ICommand NextViewButtonCommand { get; }
    public string PlayerOneName { get; set; } = "";
    public string PlayerTwoName { get; set; } = "";
    public Visibility IsMultiplayer => _isMultiplayer ? Visibility.Visible : Visibility.Hidden;
    
    private readonly INavigator _navigator;
    private readonly ICommand _updateCurrentViewModelCommand;
    private readonly bool _isMultiplayer;

    public GameSetupViewModel(GameSetupDto dto)
    {
        _navigator = dto.Navigator;
        _isMultiplayer = dto.IsMultiplayer;

        NextViewButtonCommand = new RelayCommand(NextViewButton_Clicked);

        _updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator);
    }

    private void NextViewButton_Clicked()
    {
        if (_isMultiplayer)
        {
            var dto = new ShipPlacementDto(_navigator, ViewsEnum.ShipPlacement, 
                new HumanPlayer(PlayerOneName), new HumanPlayer(PlayerTwoName), false);
            _updateCurrentViewModelCommand.Execute(dto);
        }
        else
        {
            var dto = new ShipPlacementDto(_navigator, ViewsEnum.ShipPlacement, 
                new HumanPlayer(PlayerOneName), new ComputerPlayer(), true);
            _updateCurrentViewModelCommand.Execute(dto);
        }
    }
}