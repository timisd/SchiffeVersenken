using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.MVVM.Views;

/// <summary>
/// Interaction logic for GameBoardView.xaml
/// </summary>
public partial class GameBoardView
{
    private const string CaptionLetters = "ABCDEFGHIJ";

    public GameBoardView()
    {
        InitializeComponent();
        GenerateCaption();
        GenerateBorders();
    }

    private void GenerateCaption()
    {
        // Setting up the numbers as caption for the grid on the left side
        for (var i = 1; i < 11; i++)
        {
            var txtBlock = GenerateLabel($"{i}");
            Grid.SetRow(txtBlock, i);
            Grid.SetColumn(txtBlock, 0);
            GameBoardGrid.Children.Add(txtBlock);
        }

        // Setting up the letters as caption for the grid on the top side
        for (var i = 1; i < 11; i++)
        {
            var lbl = GenerateLabel(CaptionLetters[i - 1].ToString());
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, i);
            GameBoardGrid.Children.Add(lbl);
        }
    }

    private Label GenerateLabel(string text)
    {
        return new Label
        {
            Content = text,
            FontSize = 40,
            FontWeight = FontWeights.Bold,
            FontFamily = new FontFamily("Roboto"),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            // Check if the text is a number if the text is no number set no margin otherwise margin right to 5
            Margin = Regex.IsMatch(text, @"^\d+$") ? new Thickness(0, 0, 5, 0) : new Thickness(0, 0, 0, 0)
        };
    }

    private void GenerateBorders()
    {
        for (var row = 1; row < 11; row++)
        for (var col = 1; col < 11; col++)
        {
            Thickness thick;
            if (row < 10 && col < 10)
                thick = new Thickness(2, 2, 0, 0);
            else if (row == 10 && col == 10)
                thick = new Thickness(2);
            else if (col == 10)
                thick = new Thickness(2, 2, 2, 0);
            else if (row == 10)
                thick = new Thickness(2, 2, 0, 2);


            var border = new Border
            {
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = thick
            };
            Grid.SetRow(border, row);
            Grid.SetColumn(border, col);
            GameBoardGrid.Children.Add(border);
        }
    }
}