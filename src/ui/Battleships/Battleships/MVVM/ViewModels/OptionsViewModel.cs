using Battleships.Helper;
using Battleships.Services.Navigation;
using System.Windows.Input;

namespace Battleships.MVVM.ViewModels;

internal class OptionsViewModel : BaseViewModel
{
    public INavigator Navigator { get; set; }

    public ICommand UpdateCurrentViewModelCommand { get; }

    public OptionsViewModel(INavigator navigator)
    {
        Navigator = navigator;
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator);
    }
}