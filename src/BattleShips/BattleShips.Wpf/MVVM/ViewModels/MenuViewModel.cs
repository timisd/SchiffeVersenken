﻿using System.Windows.Input;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class MenuViewModel : BaseViewModel
{
    public INavigator Navigator { get; }
    public ICommand UpdateCurrentViewModelCommand { get; }
    
    public MenuViewModel(INavigator navigator) : base("Schiffe Versenken - Menü")
    {
        Navigator = navigator;
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
    }
}