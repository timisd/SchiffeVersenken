using System.Windows;
using System.Windows.Input;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigator Navigator { get; private set; }
    public ICommand MinimizeCommand { get; }
    public ICommand CloseCommand { get; }

    public MainViewModel(Window window, INavigator navigator) : base("Schiffe Versenken")
    {
        Navigator = navigator;
        
        MinimizeCommand = new RelayCommand(() => window.WindowState = WindowState.Minimized);
        CloseCommand = new RelayCommand(() => Application.Current.Shutdown());
        
        var updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
        updateCurrentViewModelCommand.Execute(ViewsEnum.Menu);
    }
}