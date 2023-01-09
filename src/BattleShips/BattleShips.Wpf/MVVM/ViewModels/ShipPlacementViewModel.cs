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
    public ICommand StartGameCommand { get; }
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
    private string _orientationString = "🠖";
    private bool _canContinue = false;

    public ShipPlacementViewModel(ShipPlacementDto dto)
    {
        _navigator = dto.Navigator;
        _updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator);
        
        _currentPlayer = dto.Player1.AllShipsPlaced ? dto.Player2 : dto.Player1;
        _shipPlacementDto = dto;
        _btnContentArray = new string[10, 10];

        ChangeOrientationCommand = new RelayCommand(ChangeOrientation);
        StartGameCommand = new RelayCommand(StartGame);
        RadioButtonDataCollection = new ObservableCollection<RadioButtonData>();
        
        SetDefaultButtonContent();
        AddRadioButtonsToCollection();
    }

    public void OceanButton_Clicked(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn) return;
        
        var row = Grid.GetRow(btn) - 1;
        var column = Grid.GetColumn(btn) - 1;
        var position = new Position(column, row);

        _currentPlayer.PlaceShip(_selectedShipType, position, _orientation);

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

    private void StartGame()
    {
        var dto = new GameDto(_navigator, ViewsEnum.Game, _shipPlacementDto.Player1, _shipPlacementDto.Player2);
        _updateCurrentViewModelCommand.Execute(dto);
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
        switch (ship)
        {
            case ShipTypeEnum.Submarine:
                RadioButtonDataCollection[0].RadioButtonContent = $"x{_currentPlayer.MissingSubmarinesCounter} U-Boot";
                break;
            case ShipTypeEnum.Destroyer:
                RadioButtonDataCollection[1].RadioButtonContent = $"x{_currentPlayer.MissingDestroyerCounter} Zerstörer";
                break;
            case ShipTypeEnum.Cruiser:
                RadioButtonDataCollection[2].RadioButtonContent = $"x{_currentPlayer.MissingCruiserCounter} Kreuzer";
                break;
            case ShipTypeEnum.Battleship:
                RadioButtonDataCollection[3].RadioButtonContent = $"x{_currentPlayer.MissingBattleshipCounter} Schlachtschiff";
                break;
            case ShipTypeEnum.Carrier:
                RadioButtonDataCollection[4].RadioButtonContent = $"x{_currentPlayer.MissingCarrierCounter} Flugzeugträger";
                break;
            default:
                throw new Exception("Schiffstyp nicht vorhanden!");
        }

        if (_currentPlayer.MissingSubmarinesCounter == 0)
        {
            RadioButtonDataCollection[1].RadioButtonIsChecked = true;
            _selectedShipType = ShipTypeEnum.Destroyer;
        } 
        if (_currentPlayer.MissingDestroyerCounter == 0)
        {
            RadioButtonDataCollection[2].RadioButtonIsChecked = true;
            _selectedShipType = ShipTypeEnum.Cruiser;
        }
        if (_currentPlayer.MissingCruiserCounter == 0)
        {
            RadioButtonDataCollection[3].RadioButtonIsChecked = true;
            _selectedShipType = ShipTypeEnum.Battleship;
        } 
        if (_currentPlayer.MissingBattleshipCounter == 0)
        {
            RadioButtonDataCollection[4].RadioButtonIsChecked = true;
            _selectedShipType = ShipTypeEnum.Carrier;
        }

        if (_currentPlayer.MissingCarrierCounter == 0)
            CanContinue = true;
    }
}

public class RadioButtonData : ObservableObject
{
    public string RadioButtonContent
    {
        get => _radioButtonContent;
        set => SetProperty(ref _radioButtonContent, value);
    }

    public bool RadioButtonIsChecked
    {
        get => _radioButtonIsChecked;
        set => SetProperty(ref _radioButtonIsChecked, value);
    }

    private string _radioButtonContent;
    private bool _radioButtonIsChecked;

    public RadioButtonData(string radioButtonContent, bool radioButtonIsChecked)
    {
        RadioButtonContent = radioButtonContent;
        RadioButtonIsChecked = radioButtonIsChecked;
    }
}