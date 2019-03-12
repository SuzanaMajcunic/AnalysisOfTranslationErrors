using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AnalysisOfTranslationErrors.Services
{
    public class StringToBrushConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string myColor = (string)value;

            if (myColor == "RED")
                return new SolidColorBrush(Windows.UI.Colors.Red);
            else if (myColor == "GREEN")
                return new SolidColorBrush(Windows.UI.Colors.Green);
            else if (myColor == "PURPLE")
                return new SolidColorBrush(Windows.UI.Colors.Purple);
            else if (myColor == "OLIVE")
                return new SolidColorBrush(Windows.UI.Colors.Olive);
            else if (myColor == "SADDLEBROWN")
                return new SolidColorBrush(Windows.UI.Colors.SaddleBrown);
            else if (myColor == "DEEPPINK")
                return new SolidColorBrush(Windows.UI.Colors.DeepPink);
            else if (myColor == "BLUE")
                return new SolidColorBrush(Windows.UI.Colors.Blue);
            else
                return new SolidColorBrush(Windows.UI.Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((Visibility)value == Visibility.Visible);
        }
    }
}
