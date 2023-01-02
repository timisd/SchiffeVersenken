using System;
using System.ComponentModel;

namespace BattleShips.Wpf.MVVM.Helper;

public class ObservableObject
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}