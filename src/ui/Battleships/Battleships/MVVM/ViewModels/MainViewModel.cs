using Battleships.Services.Navigation;
using System.Windows.Input;
using System.Windows;
using Battleships.Helper;

namespace Battleships.MVVM.ViewModels;

internal class MainViewModel : BaseViewModel
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
        UpdateCurrentViewModelCommand.Execute(ViewsEnum.Menu);
    }
}