using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Tribalwars.UI.DeffRequester.Models;

namespace Tribalwars.UI.DeffRequester.Util
{
    public static class GenerateOutputCode
    {
        public static string Generate(string template, List<DeffRequestVillage> villages, List<int> deffPerAttackCount)
        {
            if (string.IsNullOrWhiteSpace(template)) return "Bitte ein Template eingeben!";
            if (villages == null) return "Es wurden keine eingelesenen Dörfer gefunden!";
            
            string output = "";
            int i = 1;
            foreach (var village in villages.Where(s => s.IsSelected == true))
            {
                if (i != 1 && i % 5 == 1) output += "\n";
                string temp = template;
                temp = temp.Replace("{Counter}", i.ToString().PadLeft(2, '0'));
                temp = temp.Replace("{Coords}", village.X + "|" + village.Y);
                temp = temp.Replace("{WallLevel}", village.WallLevel.ToString());
                temp = temp.Replace("{Loyalty}", village.Loyalty.ToString());
                temp = temp.Replace("{Units}", village.Units.ToString());
                temp = temp.Replace("{ArrivalFirstInc}", village.Attacks.Aggregate((curMin, x) => 
                    (curMin == null || x.Arrival < curMin.Arrival) ? x : curMin).Arrival.ToString("dd.MM.yy HH:mm:ss:fff"));
                temp = temp.Replace("{IncCount}", village.Attacks.Count.ToString());
                temp = temp.Replace("{RequestedDeff}", GenerateRequestedDeff(village.Attacks.Count, deffPerAttackCount));
                output += temp + "\n";

                i++;
            }

            return output;
        }

        private static string GenerateRequestedDeff(int attackCount, List<int> deffPerAttackCount)
        {
            return "0/" + deffPerAttackCount[attackCount - 1];
        }
    }
}
