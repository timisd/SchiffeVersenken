using System.Windows;
using System.Windows.Input;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.DTOs;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }
    public ICommand MinimizeCommand { get; set; }
    public ICommand CloseCommand { get; set; }
    public ICommand UpdateCurrentViewModelCommand { get; }

    public MainViewModel(Window window, INavigator navigator)
    {
        Navigator = navigator;

        MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
        CloseCommand = new RelayCommand(window.Close);
        
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        UpdateCurrentViewModelCommand.Execute(new ViewSwitchDto(navigator, ViewsEnum.Menu));
    }
}