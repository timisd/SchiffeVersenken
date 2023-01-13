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

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, TitlePrefix + value);
    }
    
    private const string TitlePrefix = "Schiffe Versenken - ";
    private UserControl? _currentView = new MenuView();
    private string _title = TitlePrefix;

    public void Navigate(UserControl? view)
    {
        CurrentView = view;
    }
}