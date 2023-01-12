using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class ShipPlacementViewModel : BaseViewModel
{
    public string Headline => $"{_currentPlayer.Name} du bist dran deine Schiffe zu platzieren";
    public ICommand ChangeOrientationCommand { get; }
    public ICommand ContinueCommand { get; }
    public ObservableCollection<RadioButtonData> RadioButtonDataCollection { get; }

    public bool CanContinue
    {
        get => _canContinue;
        set => SetProperty(ref _canContinue, value);
    }
    public string[,] BtnContentArray
    {
        get => _btnContentArray; 
        set => SetProperty(ref _btnContentArray, value);
    }
    public Button[,] BtnArray
    {
        get => _btnArray;
        set => SetProperty(ref _btnArray, value);
    }
    public string OrientationString
    {
        get => _orientationString;
        private set => SetProperty(ref _orientationString, value);
    }
    
    private readonly INavigator _navigator;
    private readonly ICommand _updateCurrentViewModelCommand;
    private readonly Player _currentPlayer;
    private readonly ShipPlacementDto _shipPlacementDto;
    private OrientationEnum _orientation = OrientationEnum.Horizontal;
    private ShipTypeEnum _selectedShipType = ShipTypeEnum.Submarine;
    private string[,] _btnContentArray;
    private Button[,] _btnArray;
    private string _orientationString = "🠖";
    private bool _canContinue = false;

    public ShipPlacementViewModel(ShipPlacementDto dto)
    {
        _navigator = dto.Navigator;
        _updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator);

        if (dto.Player2 is ComputerPlayer computer)
        {
            computer.PlaceShip();
        }
        
        _currentPlayer = dto.Player1.AllShipsPlaced ? dto.Player2 : dto.Player1;
        _shipPlacementDto = dto;
        _btnContentArray = new string[10, 10];
        _btnArray = new Button[10, 10];
        
        ChangeOrientationCommand = new RelayCommand(ChangeOrientation);
        ContinueCommand = new RelayCommand(ContinueButtonClicked);
        RadioButtonDataCollection = new ObservableCollection<RadioButtonData>();
        
        SetDefaultButtonContent();
        AddRadioButtonsToCollection();
    }

    public void OceanButton_Clicked(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn) return;
        if (_currentPlayer.AllShipsPlaced) return;
        
        var row = Grid.GetRow(btn) - 1;
        var column = Grid.GetColumn(btn) - 1;
        var position = new Position(column, row);

        if (_currentPlayer.PlaceShip(_selectedShipType, position, _orientation))
        {
            DeactivateSurroundingButtons(new Position(column, row));
        }

        btn.IsEnabled = false;
        UpdateOcean();
        UpdateRadioButtons(_selectedShipType);
    }
    
    private void ChangeOrientation()
    {
        _orientation = _orientation == OrientationEnum.Horizontal ? 
            OrientationEnum.Vertical : OrientationEnum.Horizontal;
        OrientationString = _orientation == OrientationEnum.Horizontal ? "🠖" : "🠗";
    }

    private void ContinueButtonClicked()
    {
        if (_shipPlacementDto.Player1.AllShipsPlaced && _shipPlacementDto.Player2.AllShipsPlaced)
        {
            var dto = new GameDto(_navigator, ViewsEnum.Game, _shipPlacementDto.Player1, _shipPlacementDto.Player2);
                _updateCurrentViewModelCommand.Execute(dto);
        }
        else
        {
            _updateCurrentViewModelCommand.Execute(_shipPlacementDto);
        }
    }

    private void SetDefaultButtonContent()
    {
        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                _btnContentArray[row, column] = $"{row + 1} | {column + 1}";
            }
        }
    }
    
    private void AddRadioButtonsToCollection()
    {
        RadioButtonDataCollection.Add(new RadioButtonData(
            $"x{_currentPlayer.MissingSubmarinesCounter} U-Boot",
            true));
        RadioButtonDataCollection.Add(new RadioButtonData(
            $"x{_currentPlayer.MissingDestroyerCounter} Zerstörer",
            false));
        RadioButtonDataCollection.Add(new RadioButtonData(
            $"x{_currentPlayer.MissingCruiserCounter} Kreuzer",
            false));
        RadioButtonDataCollection.Add(new RadioButtonData(
            $"x{_currentPlayer.MissingBattleshipCounter} Schlachtschiff",
            false));
        RadioButtonDataCollection.Add(new RadioButtonData(
            $"x{_currentPlayer.MissingCarrierCounter} Flugzeugträger",
            false));
    }

    private void UpdateOcean()
    {
        for (var row = 0; row < 10; row++)
        {
            for (var column = 0; column < 10; column++)
            {
                BtnContentArray[row, column] = _currentPlayer.Board.Ocean[row, column].Status.ToString();
            }
        }
        
        OnPropertyChanged(nameof(BtnContentArray));
    }

    private void UpdateRadioButtons(ShipTypeEnum ship)
    {
        var shipTypes = new[] { ShipTypeEnum.Submarine, ShipTypeEnum.Destroyer, ShipTypeEnum.Cruiser, ShipTypeEnum.Battleship, ShipTypeEnum.Carrier };

        for (var i = 0; i < shipTypes.Length; i++)
        {
            if (shipTypes[i] != ship) continue;
            
            RadioButtonDataCollection[i].RadioButtonContent = $"x{_currentPlayer.GetMissingShipCounter(shipTypes[i])} {GetShipName(shipTypes[i])}";
            
            if (_currentPlayer.GetMissingShipCounter(shipTypes[i]) != 0 || i >= shipTypes.Length - 1) continue;
            
            _selectedShipType = shipTypes[i+1];
            RadioButtonDataCollection[i+1].RadioButtonIsChecked = true;
        }
        CanContinue = _currentPlayer.GetMissingShipCounter(ShipTypeEnum.Carrier) == 0;
    }

    private string GetShipName(ShipTypeEnum shipType)
    {
        return shipType switch
        {
            ShipTypeEnum.Submarine => "U-Boot",
            ShipTypeEnum.Destroyer => "Zerstörer",
            ShipTypeEnum.Cruiser => "Kreuzer",
            ShipTypeEnum.Battleship => "Schlachtschiff",
            ShipTypeEnum.Carrier => "Flugzeugträger",
            _ => throw new Exception("Invalid ship type")
        };
    }
    
    private void DeactivateSurroundingButtons(Position position)
    {
        switch (_selectedShipType)
        {
            case ShipTypeEnum.Submarine:
                if (_orientation == OrientationEnum.Horizontal)
                {
                    HorizontalButtonDeactivate(position, 1);
                }
                else
                {
                    VerticalButtonDeactivate(position, 1);
                }
                break;
            case ShipTypeEnum.Destroyer:
                if (_orientation == OrientationEnum.Horizontal)
                {
                    HorizontalButtonDeactivate(position, 2);
                }
                else
                {
                    VerticalButtonDeactivate(position, 2);
                }
                break;
            case ShipTypeEnum.Cruiser:
                if (_orientation == OrientationEnum.Horizontal)
                {
                    HorizontalButtonDeactivate(position, 3);
                }
                else
                {
                    VerticalButtonDeactivate(position, 3);
                }
                break;
            case ShipTypeEnum.Battleship:
                if (_orientation == OrientationEnum.Horizontal)
                {
                    HorizontalButtonDeactivate(position, 4);
                }
                else
                {
                    VerticalButtonDeactivate(position, 4);
                }
                break;
            case ShipTypeEnum.Carrier:
                if (_orientation == OrientationEnum.Horizontal)
                {
                    HorizontalButtonDeactivate(position, 5);
                }
                else
                {
                    VerticalButtonDeactivate(position, 5);
                }
                break;
            default:
                throw new Exception("Diesen Schiff Typ gibt es gar nicht!");
        }
    }
    
    private void HorizontalButtonDeactivate(Position start, int length)
    {
        for (var rowOffset = -1; rowOffset < 2; rowOffset++)
        {
            for (var colOffset = -1; colOffset < length + 1; colOffset++)
            {
                if (start.X + colOffset < 0 || start.X + colOffset > 9 || start.Y + rowOffset < 0 || start.Y + rowOffset > 9)
                {
                    continue;
                }
                
                BtnArray[start.Y + rowOffset, start.X + colOffset].IsEnabled = false;
            }
        }
    }
    
    private void VerticalButtonDeactivate(Position start, int length)
    {
        for (var rowOffset = -1; rowOffset < length + 1; rowOffset++)
        {
            for (var colOffset = -1; colOffset < 2; colOffset++)
            {
                if (start.X + colOffset < 0 || start.X + colOffset > 9 || start.Y + rowOffset < 0 || start.Y + rowOffset > 9)
                {
                    continue;
                }
                
                BtnArray[start.Y + rowOffset, start.X + colOffset].IsEnabled = false;
            }
        }
    }
}