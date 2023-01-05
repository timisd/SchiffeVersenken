using System.ComponentModel;
using System.Windows;
using BattleShips.Game.Enums;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;

namespace BattleShips.Wpf.MVVM.Models;

public class ShipPlacementModel : ObservableObject
{
    public bool CanContinue => _player.AllShipsPlaced;
    public int MissingSubmarinesCounter => _player.MissingSubmarinesCounter;
    public bool EnableSubmarineRadioButton => MissingSubmarinesCounter > 0;
    public int MissingDestroyerCounter => _player.MissingDestroyerCounter;
    public bool EnableDestroyerRadioButton => MissingDestroyerCounter > 0;
    public int MissingCruiserCounter => _player.MissingCruiserCounter;
    public bool EnableCruiserRadioButton => MissingCruiserCounter > 0;
    public int MissingBattleshipCounter => _player.MissingBattleshipCounter;
    public bool EnableBattleshipRadioButton => MissingBattleshipCounter > 0;
    public int MissingCarrierCounter => _player.MissingCarrierCounter;
    public bool EnableCarrierRadioButton => MissingCarrierCounter > 0;
    public string OrientationString
    {
        get => _orientationString;
        private set => SetProperty(ref _orientationString, value);
    }
    
    private OrientationEnum _orientation = OrientationEnum.Horizontal;
    private string _orientationString = "🠖";
    private Player _player;
    
    public ShipPlacementModel(Player player)
    {
        _player = player;
    }
    
    public void ChangeOrientation()
    {
        _orientation = _orientation == OrientationEnum.Horizontal ? 
            OrientationEnum.Vertical : OrientationEnum.Horizontal;
        OrientationString = _orientation == OrientationEnum.Horizontal ? "🠖" : "🠗";
    }
}