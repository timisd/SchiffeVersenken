using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class ShipPlacementViewModel : BaseViewModel
{
    public ICommand ChangeOrientationCommand { get; }
    public string Headline => $"{_currentPlayer.Name} du bist dran deine Schiffe zu platzieren";
    public bool CanContinue => _currentPlayer.AllShipsPlaced;
    public string SubmarinesRadioButtonContent => $"x{_currentPlayer.MissingSubmarinesCounter} U-Boot";
    public bool EnableSubmarineRadioButton => _currentPlayer.MissingSubmarinesCounter > 0;
    public string DestroyerRadioButtonContent => $"x{_currentPlayer.MissingDestroyerCounter} Zerstörer";
    public bool EnableDestroyerRadioButton => _currentPlayer.MissingSubmarinesCounter > 0;
    public string CruiserRadioButtonContent => $"x{_currentPlayer.MissingCruiserCounter} Kreuzer";
    public bool EnableCruiserRadioButton => _currentPlayer.MissingSubmarinesCounter > 0;
    public string BattleshipRadioButtonContent => $"x{_currentPlayer.MissingBattleshipCounter} Schlachtschiff";
    public bool EnableBattleshipRadioButton => _currentPlayer.MissingSubmarinesCounter > 0;
    public string CarrierRadioButtonContent => $"x{_currentPlayer.MissingCarrierCounter} Flugzeugträger";
    public bool EnableCarrierRadioButton => _currentPlayer.MissingSubmarinesCounter > 0;

    public string OrientationString
    {
        get => _orientationString;
        private set => SetProperty(ref _orientationString, value);
    }

    private readonly Player _currentPlayer;
    private OrientationEnum _orientation = OrientationEnum.Horizontal;
    private ShipTypeEnum _selectedShipType = ShipTypeEnum.Submarine;
    private string _orientationString = "🠖";

    public ShipPlacementViewModel(ShipPlacementDto dto)
    {
        _currentPlayer = dto.Player1.AllShipsPlaced ? dto.Player2 : dto.Player1;

        ChangeOrientationCommand = new RelayCommand(ChangeOrientation);
    }
    
    public void ShipSelectionChanged(RadioButton rb)
    {
        _selectedShipType = rb.Name switch
        {
            "RbSubmarine" => ShipTypeEnum.Submarine,
            "RbDestroyer" => ShipTypeEnum.Destroyer,
            "RbCruiser" => ShipTypeEnum.Cruiser,
            "RbBattleship" => ShipTypeEnum.Battleship,
            "RbCarrier" => ShipTypeEnum.Carrier,
            _ => _selectedShipType
        };
    }

    private void ChangeOrientation()
    {
        _orientation = _orientation == OrientationEnum.Horizontal ? 
            OrientationEnum.Vertical : OrientationEnum.Horizontal;
        OrientationString = _orientation == OrientationEnum.Horizontal ? "🠖" : "🠗";
    }
}