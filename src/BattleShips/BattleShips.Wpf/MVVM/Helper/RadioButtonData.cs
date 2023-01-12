namespace BattleShips.Wpf.MVVM.Helper;

public class RadioButtonData : ObservableObject
{
    public string RadioButtonContent
    {
        get => _radioButtonContent;
        set => SetProperty(ref _radioButtonContent, value);
    }

    public bool RadioButtonIsChecked
    {
        get => _radioButtonIsChecked;
        set => SetProperty(ref _radioButtonIsChecked, value);
    }

    private string _radioButtonContent;
    private bool _radioButtonIsChecked;

    public RadioButtonData(string radioButtonContent, bool radioButtonIsChecked)
    {
        RadioButtonContent = radioButtonContent;
        RadioButtonIsChecked = radioButtonIsChecked;
    }
}