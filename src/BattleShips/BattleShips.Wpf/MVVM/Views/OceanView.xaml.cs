using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace BattleShips.Wpf.MVVM.Views;

public partial class OceanView : UserControl
{
    private const string Alphabet = "ABCDEFGHIJ";
    public OceanView()
    {
        InitializeComponent();
        AddLabelsToGrid();
        AddButtonsToGrid();
    }

    private void AddLabelsToGrid()
    {
        for (var i = 1; i < 11; i++)
        {
            var txtBlock = GenerateLabel($"{i}");
            Grid.SetRow(txtBlock, i);
            Grid.SetColumn(txtBlock, 0);
            Ocean.Children.Add(txtBlock);
        }
        
        for (var i = 1; i < 11; i++)
        {
            var lbl = GenerateLabel(Alphabet[i - 1].ToString());
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, i);
            Ocean.Children.Add(lbl);
        }
    }

    private void AddButtonsToGrid()
    {
        for (var y = 1; y < 11; y++)
        {
            for (var x = 1; x < 11; x++)
            {
                var btn = new Button()
                {
                    Content = $"{x} | {y}",
                    Background = Brushes.Transparent
                };
                
                Grid.SetRow(btn, x);
                Grid.SetColumn(btn, y);

                Ocean.Children.Add(btn);
            }
        }
    }
    
    private Label GenerateLabel(string text)
    {
        return new Label
        {
            Content = text,
            FontSize = 40,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
    }
}