using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BattleShips.Wpf.MVVM.Helper;

public class RectangleConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return new Rect(0, 0, (double)values[0], (double)values[1]);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}