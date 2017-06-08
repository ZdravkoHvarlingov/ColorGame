using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace WpfApplication1
{
    class BackgroundToArrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            if (value==null)
            {
                return null;
            }

            var brush = (Brush)value;           

            switch (brush.ToString())
            {
                case "#FFFF0000":
                    return "Images/arrowDown.png";
                case "#FF0000FF":
                    return "Images/arrowUp.png";
                case "#FFFFFF00":
                    return "Images/arrowLeft.png";
                default:
                    return "Images/arrowRight.png";                    
            }
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
