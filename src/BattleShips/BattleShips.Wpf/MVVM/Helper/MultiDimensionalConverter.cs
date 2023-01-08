using System;
using System.Globalization;
using System.Windows.Data;

namespace BattleShips.Wpf.MVVM.Helper;

public class MultiDimensionalConverter :IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return (values[0] as string[,])[(int) values[1], (int) values[2]];  
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}