using Battleships.Helper;
using Battleships.MVVM.ViewModels;

namespace Battleships.Services.Navigation;

internal class Navigator : ObservableObject, INavigator
{
    private BaseViewModel? _currentViewModel;

    public BaseViewModel? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public void Navigate(BaseViewModel? viewModel)
    {
        CurrentViewModel = viewModel;
    }
}