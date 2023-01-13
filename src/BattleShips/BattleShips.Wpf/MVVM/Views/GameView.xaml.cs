using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using BattleShips.Wpf.MVVM.Helper;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Views;

public partial class GameView
{
    private const string Alphabet = "ABCDEFGHIJ";
    private readonly Button[,] _leftButtons;
    private readonly Button[,] _rightButtons;
    
    public GameView()
    {
        InitializeComponent();
        
        _leftButtons = new Button[10, 10];
        _rightButtons = new Button[10, 10];
            
        AddLabelsToGrid(OceanLeft);
        AddLabelsToGrid(OceanRight);
        AddButtonsToGrid(OceanLeft, true);
        AddButtonsToGrid(OceanRight, false);
    }
    
    private void AddLabelsToGrid(Panel grid)
    {
        for (var i = 1; i < 11; i++)
        {
            var lbl = GenerateLabel($"{i}");
            Grid.SetRow(lbl, i);
            Grid.SetColumn(lbl, 0);
            grid.Children.Add(lbl);
        }
        
        for (var i = 1; i < 11; i++)
        {
            var lbl = GenerateLabel(Alphabet[i - 1].ToString());
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, i);
            grid.Children.Add(lbl);
        }
    }
    
    private void AddButtonsToGrid(Grid grid, bool left)
    {
        var prefix = left ? "Left" : "Right";
        
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
                    Name = $"{prefix}GridButton{row}{column}",
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    BorderThickness = thick,
                    Style = (Style)Application.Current.Resources["OceanGameButton"]
                };

                var binding = new MultiBinding();
                binding.Bindings.Add(new Binding(prefix + "BtnContentArray"));
                binding.Bindings.Add(new Binding {Source = row - 1});
                binding.Bindings.Add(new Binding {Source = column - 1});
                binding.Converter = new MultiDimensionalConverter();
                
                btn.SetBinding(Button.ContentProperty, binding);
                btn.Click += (sender, args) => (DataContext as GameViewModel)?.OceanButton_Clicked(sender, args);;
                btn.MouseEnter += (sender, args) =>
                    (DataContext as GameViewModel)?.OceanButton_MouseEnter(sender, args);
                btn.MouseLeave += (sender, args) =>
                    (DataContext as GameViewModel)?.OceanButton_MouseLeave(sender, args);
                
                if (left)
                {
                    _leftButtons[row - 1, column - 1] = btn;
                }
                else
                {
                    _rightButtons[row - 1, column - 1] = btn;
                }
                
                Grid.SetRow(btn, row);
                Grid.SetColumn(btn, column);

                grid.Children.Add(btn);
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
    
    private void GameView_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is not GameViewModel vm) return;
        
        vm.LeftButtons = _leftButtons;
        vm.RightButtons = _rightButtons;
    }
}