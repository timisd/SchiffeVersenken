namespace BattleShips.Wpf.MVVM.ViewModels;

public class BaseViewModel
{
    public string Title { get; }

    protected BaseViewModel(string title)
    {
        Title = title;
    }
}