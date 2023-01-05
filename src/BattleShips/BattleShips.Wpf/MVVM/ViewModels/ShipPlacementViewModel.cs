using System.ComponentModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using BattleShips.Game.Enums;
using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;
using BattleShips.Wpf.MVVM.Models;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class ShipPlacementViewModel : BaseViewModel
{
    public ShipPlacementModel Model
    {
        get => _model;
        set
        {
            _model = value;
            _model.PropertyChanged += Model_PropertyChanged;
        }
    }
    public INavigator Navigator { get; set; }
    public ICommand UpdateCurrentViewModelCommand { get; }
    public ICommand ChangeOrientationCommand { get; }
    public bool CanContinue => _model.CanContinue;
    public int MissingSubmarinesCounter => _model.MissingSubmarinesCounter;
    public bool EnableSubmarineRadioButton => _model.EnableSubmarineRadioButton;
    public int MissingDestroyerCounter => _model.MissingDestroyerCounter;
    public bool EnableDestroyerRadioButton => _model.EnableDestroyerRadioButton;
    public int MissingCruiserCounter => _model.MissingCruiserCounter;
    public bool EnableCruiserRadioButton => _model.EnableCruiserRadioButton;
    public int MissingBattleshipCounter => _model.MissingBattleshipCounter;
    public bool EnableBattleshipRadioButton => _model.EnableBattleshipRadioButton;
    public int MissingCarrierCounter => _model.MissingCarrierCounter;
    public bool EnableCarrierRadioButton => _model.EnableCarrierRadioButton;
    public string OrientationString => _model.OrientationString;
    public Player CurrentPlayer { get; }
    
    private ShipPlacementModel _model;
    
    public ShipPlacementViewModel(ShipPlacementDto dto)
    {
        Navigator = dto.Navigator;
        CurrentPlayer = dto.Player1.AllShipsPlaced ? dto.Player2 : dto.Player1;
        _model = new ShipPlacementModel(CurrentPlayer);

        ChangeOrientationCommand = new RelayCommand(_model.ChangeOrientation);

        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(Navigator);
    }
    
    private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        MessageBox.Show(e.PropertyName);
        OnPropertyChanged(e.PropertyName);
    }
}