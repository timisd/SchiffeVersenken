using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using BattleShips.Wpf.MVVM.Helper;

namespace BattleShips.Wpf.MVVM.Views;

public partial class GameView
{
    private const string Alphabet = "ABCDEFGHIJ";
    public GameView()
    {
        InitializeComponent();
        AddLabelsToGrid(OceanLeft);
        AddLabelsToGrid(OceanRight);
        AddButtonsToGrid(OceanLeft, "Left", "LeftGridButtonArray");
        AddButtonsToGrid(OceanRight, "Right", "RightGridButtonArray");
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
    
    private void AddButtonsToGrid(Panel grid, string btnNamePrefix, string nameofBinding)
    {
        for (var row = 1; row < 11; row++)
        {
            for (var column = 1; column < 11; column++)
            {
                var btn = new Button()
                {
                    Name = $"{btnNamePrefix}GridButton{row}{column}",
                    Background = Brushes.Transparent
                };

                var binding = new MultiBinding();
                binding.Bindings.Add(new Binding(nameofBinding));
                binding.Bindings.Add(new Binding {Source = row - 1});
                binding.Bindings.Add(new Binding {Source = column - 1});
                binding.Converter = new MultiDimensionalConverter();
                
                btn.SetBinding(Button.ContentProperty, binding);
                btn.Click += OceanButton_Click;
                
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
    
    private void OceanButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Btn clicked");
    }
}