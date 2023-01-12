using System.Windows.Controls;
using BattleShips.Wpf.MVVM.Views;

namespace BattleShips.Wpf.MVVM.Helper.NavigationService;

public class Navigator : ObservableObject, INavigator
{
    public UserControl? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }
    
    private UserControl? _currentView = new MenuView();
    
    public void Navigate(UserControl? view)
    {
        CurrentView = view;
    }
}