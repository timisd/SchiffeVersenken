using System.Windows.Input;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class ShipPlacementViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }
    public ICommand UpdateCurrentViewModelCommand { get; }
    public bool CanContinue => CurrentPlayer.AllShipsPlaced;
    public int MissingSubmarinesCounter => CurrentPlayer.MissingSubmarinesCounter;
    public bool EnableSubmarineRadioButton => MissingSubmarinesCounter > 0;
    public int MissingDestroyerCounter => CurrentPlayer.MissingDestroyerCounter;
    public bool EnableDestroyerRadioButton => MissingDestroyerCounter > 0;
    public int MissingCruiserCounter => CurrentPlayer.MissingCruiserCounter;
    public bool EnableCruiserRadioButton => MissingCruiserCounter > 0;
    public int MissingBattleshipCounter => CurrentPlayer.MissingBattleshipCounter;
    public bool EnableBattleshipRadioButton => MissingBattleshipCounter > 0;
    public int MissingCarrierCounter => CurrentPlayer.MissingCarrierCounter;
    public bool EnableCarrierRadioButton => MissingCarrierCounter > 0;
    public Player CurrentPlayer { get; }

    public ShipPlacementViewModel(ShipPlacementDto dto)
    {
        Navigator = dto.Navigator;
        CurrentPlayer = dto.Player1.AllShipsPlaced ? dto.Player2 : dto.Player1;

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(Navigator);
    }
}