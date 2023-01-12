using System;
using System.Windows;
using System.Windows.Input;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;
using BattleShips.Wpf.MVVM.ViewModels;
using BattleShips.Wpf.MVVM.Views;

namespace BattleShips.Wpf.MVVM.Helper;

public class UpdateCurrentViewModelCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly INavigator _navigator;

    public UpdateCurrentViewModelCommand(INavigator navigator)
    {
        _navigator = navigator;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not ViewSwitchDto dto) return;

        var gameSetupDto = dto as GameSetupDto;
        var shipPlacementDto = dto as ShipPlacementDto;
        var gameDto = dto as GameDto;
        
        switch (dto.View)
        {
            case ViewsEnum.Menu:
                _navigator.Navigate(new MenuView {DataContext = new MenuViewModel(dto.Navigator)});
                break;
            case ViewsEnum.GameSetup:
                if (gameSetupDto != null)
                    _navigator.Navigate(new GameSetupView {DataContext = new GameSetupViewModel(gameSetupDto)});
                break;
            case ViewsEnum.ShipPlacement:
                if (shipPlacementDto != null)
                    _navigator.Navigate(new ShipPlacementView {DataContext = new ShipPlacementViewModel(shipPlacementDto)});
                break;
            case ViewsEnum.Game:
                if (gameDto != null)
                    _navigator.Navigate(new GameView {DataContext = new GameViewModel(gameDto)});
                break;
            default:
                break;
        }
    }
}