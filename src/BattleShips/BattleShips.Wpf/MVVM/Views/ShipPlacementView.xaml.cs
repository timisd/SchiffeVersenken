using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Views;

public partial class ShipPlacementView : UserControl
{
    private const string Alphabet = "ABCDEFGHIJ";
    public ShipPlacementView()
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
        for (var row = 1; row < 11; row++)
        {
            for (var column = 1; column < 11; column++)
            {
                var btn = new Button()
                {
                    Background = Brushes.Transparent
                };

                var binding = new MultiBinding();
                binding.Bindings.Add(new Binding("BtnContentArray"));
                binding.Bindings.Add(new Binding {Source = row - 1});
                binding.Bindings.Add(new Binding {Source = column - 1});
                binding.Converter = new MultiDimensionalConverter();
                
                btn.SetBinding(Button.ContentProperty, binding);
                btn.Click += OceanButton_Click;
                
                Grid.SetRow(btn, row);
                Grid.SetColumn(btn, column);

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
    
    private void OceanButton_Click(object sender, RoutedEventArgs e)
    {
        (DataContext as ShipPlacementViewModel)?.OceanButton_Clicked(sender, e);
    }
}