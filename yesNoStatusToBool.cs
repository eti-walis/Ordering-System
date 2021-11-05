using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BE;
using BL;
namespace UI
{

    class yesNoStatusToBool : IValueConverter
    {
        // convert from yes/no to true/false
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            YesONo yesNoValue = (YesONo)value;
            if (yesNoValue == YesONo.No)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // convert from true/false to yes/no
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue == true)
            {
                return YesONo.Yes;
            }
            else
            {
                return YesONo.No;
            }
        }
    }
}