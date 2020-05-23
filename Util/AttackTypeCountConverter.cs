using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Tribalwars.UI.DeffRequester.Models;

namespace Tribalwars.UI.DeffRequester.Util
{
    public class AttackTypeCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var paramParts = (parameter as string).Split();
            if (paramParts[0] == "NameDetected")
            {
                int unitType = int.Parse(paramParts[1]);
                if (value is DeffRequestVillage drv) return drv.Attacks.Count(s => s.NamedType == unitType);
                return value;
            }
            else if (paramParts[0] == "AttackSize")
            {
                int sizeType = int.Parse(paramParts[1]);
                if (!(value is DeffRequestVillage drv)) return value;
                switch (sizeType)
                {
                    case 0:
                        return drv.Attacks.Count(s => s.Type == "attack");
                    case 1:
                        return drv.Attacks.Count(s => s.Type == "attack_large");
                    case 2:
                        return drv.Attacks.Count(s => s.Type == "attack_medium");
                    case 3:
                        return drv.Attacks.Count(s => s.Type == "attack_small");
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
