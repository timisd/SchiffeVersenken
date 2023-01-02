using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Helper.NavigationService;

public class Navigator : ObservableObject, INavigator
{
    public string Title { get; private set; } = "";
    
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
        if (viewModel != null)
            Title = viewModel.Title;
    }
}