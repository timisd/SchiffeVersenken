using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Views;

public partial class ShipPlacementView
{
    private const string Alphabet = "ABCDEFGHIJ";
    private readonly Button[,] _btnArray;
    public ShipPlacementView()
    {
        _btnArray = new Button[10, 10];
        
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
                Thickness thick;
                switch (row)
                {
                    case < 10 when column < 10:
                        thick = new Thickness(2, 2, 0, 0);
                        break;
                    case 10 when column == 10:
                        thick = new Thickness(2);
                        break;
                    default:
                    {
                        if (column == 10)
                            thick = new Thickness(2, 2, 2, 0);
                        else if (row == 10)
                            thick = new Thickness(2, 2, 0, 2);
                        break;
                    }
                }
                
                var btn = new Button
                {
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    BorderThickness = thick,
                    Style = (Style)Application.Current.Resources["OceanButton"]
                };

                var binding = new MultiBinding();
                binding.Bindings.Add(new Binding("BtnContentArray"));
                binding.Bindings.Add(new Binding {Source = row - 1});
                binding.Bindings.Add(new Binding {Source = column - 1});
                binding.Converter = new MultiDimensionalConverter();

                btn.SetBinding(Button.ContentProperty, binding);
                btn.Click += (sender, args) => 
                    (DataContext as ShipPlacementViewModel)?.OceanButton_Clicked(sender, args);
                btn.MouseEnter += (sender, args) =>
                    (DataContext as ShipPlacementViewModel)?.OceanButton_MouseEnter(sender, args);
                btn.MouseLeave += (sender, args) =>
                    (DataContext as ShipPlacementViewModel)?.OceanButton_MouseLeave(sender, args);
                
                _btnArray[row - 1, column - 1] = btn;
                
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
    
    private void ShipPlacementView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not ShipPlacementViewModel vm) return;
        
        vm.BtnArray = _btnArray;
    }
}