using System;
using System.Collections.ObjectModel;
using System.Windows;
using BattleShips.Wpf.MVVM.Helper;

namespace BattleShips.Wpf.MVVM.ViewModels;

public class OceanViewModel : BaseViewModel
{
    public ObservableCollection<ButtonData> ButtonDataCollection { get; }

    public OceanViewModel()
    {
        ButtonDataCollection = new ObservableCollection<ButtonData>();
        AddButtonData();
    }

    private void AddButtonData()
    {
        for (var y = 1; y < 11; y++)
        {
            for (var x = 1; x < 11; x++)
            {
                var data = new ButtonData()
                {
                    ButtonContent = $"{x} | {y}", 
                    Row = y - 1,
                    Column = x - 1
                };

                Console.WriteLine($"{data.ButtonContent}\n{data.Row}\n{data.Column}");
                
                ButtonDataCollection.Add(data);
            }
        }
    }
}