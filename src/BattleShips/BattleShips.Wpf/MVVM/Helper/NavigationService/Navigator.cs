using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Helper.NavigationService;

public class Navigator : ObservableObject, INavigator
{
    private BaseViewModel? _currentViewModel;

    public BaseViewModel? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public void Navigate(BaseViewModel? viewModel)
    {
        CurrentViewModel = viewModel;
    }
}