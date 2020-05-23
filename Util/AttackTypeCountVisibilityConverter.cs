using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Tribalwars.UI.DeffRequester.Models;

namespace Tribalwars.UI.DeffRequester.Util
{
    public class AttackTypeCountVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var paramParts = (parameter as string).Split();
            if (paramParts[0] == "NameDetected")
            {
                int unitType = int.Parse(paramParts[1]);
                if (value is DeffRequestVillage drv)
                {
                    if (drv.Attacks.Count(s => s.NamedType == unitType) == 0)
                        return Visibility.Collapsed;
                    return Visibility.Visible;
                }
            }
            else if (paramParts[0] == "AttackSize")
            {
                int sizeType = int.Parse(paramParts[1]);
                if (!(value is DeffRequestVillage drv)) return value;
                switch (sizeType)
                {
                    case 0:
                        if (drv.Attacks.Count(s => s.Type == "attack") == 0)
                            return Visibility.Collapsed;
                        return Visibility.Visible;
                    case 1:
                        if (drv.Attacks.Count(s => s.Type == "attack_large") == 0)
                            return Visibility.Collapsed;
                        return Visibility.Visible;
                    case 2:
                        if (drv.Attacks.Count(s => s.Type == "attack_medium") == 0)
                            return Visibility.Collapsed;
                        return Visibility.Visible;
                    case 3:
                        if (drv.Attacks.Count(s => s.Type == "attack_small") == 0)
                            return Visibility.Collapsed;
                        return Visibility.Visible;
                }
            }

           
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("AttackTypeCountConverter can only be used for one way conversion.");
        }
    }
}