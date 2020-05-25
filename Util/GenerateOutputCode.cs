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
        public static string Generate(string template, List<DeffRequestVillage> villages, List<int> deffPerAttackCount, MainWindow contextWindow)
        {
            if (string.IsNullOrWhiteSpace(template)) return "Bitte ein Template eingeben!";
            if (villages == null) return "Es wurden keine eingelesenen Dörfer gefunden!";

            string output = "";
            int i = 1;
            foreach (var village in villages.Where(s => s.IsSelected == true))
            {
                if (contextWindow.CheckBoxIgnoreGreenAttacks.IsChecked == true &&
                    village.Attacks.Where(s => s.Type != "attack_small").ToList().Count == 0)
                {
                }
                else
                {
                    if (i != 1 && i % 5 == 1) output += "\n";
                    string temp = template;
                    temp = temp.Replace("{Counter}", i.ToString().PadLeft(2, '0'));
                    temp = temp.Replace("{Coords}", village.X + "|" + village.Y);
                    temp = temp.Replace("{WallLevel}", village.WallLevel.ToString());
                    temp = temp.Replace("{Loyalty}", village.Loyalty.ToString());
                    temp = temp.Replace("{Units}", village.Units.ToString());
                    temp = temp.Replace("{ArrivalFirstInc}", village.Attacks.Aggregate((curMin, x) =>
                            (curMin == null || x.Arrival < curMin.Arrival) ? x : curMin).Arrival
                        .ToString("dd.MM.yy HH:mm:ss:fff"));
                    temp = temp.Replace("{IncCount}", village.Attacks.Count.ToString());
                    temp = temp.Replace("{RequestedDeff}", GenerateRequestedDeff(village, deffPerAttackCount, contextWindow));
                    temp = temp.Replace("{AttackSizeAll}", GenerateAttackSizeCode("all", village, contextWindow));
                    temp = temp.Replace("{AttackSizeLarge}", GenerateAttackSizeCode("large", village, contextWindow));
                    temp = temp.Replace("{AttackSizeMedium}", GenerateAttackSizeCode("medium", village, contextWindow));
                    temp = temp.Replace("{AttackSizeSmall}", GenerateAttackSizeCode("small", village, contextWindow));
                    temp = temp.Replace("{AttackSizeUnknown}", GenerateAttackSizeCode("unknown", village, contextWindow));
                    output += temp + "\n";

                    i++;
                }
            }

            return output;
        }

        private static string GenerateAttackSizeCode(string size, DeffRequestVillage village, MainWindow contextWindow)
        {
            string attackTypeString = "";
            if (size == "all" || size == "large")
            {
                if (village.Attacks.Count(a => a.Type == "attack_large") > 0)
                {
                    attackTypeString += "[command]attack_large[/command]" +
                                        village.Attacks.Count(a => a.Type == "attack_large");
                }
            }
            if (size == "all" || size == "medium")
            {
                if (village.Attacks.Count(a => a.Type == "attack_medium") > 0)
                {
                    if (size == "all") attackTypeString += " ";
                    attackTypeString += "[command]attack_medium[/command]" +
                                        village.Attacks.Count(a => a.Type == "attack_medium");
                }
            }
            if ((size == "all" || size == "small") && contextWindow.CheckBoxIgnoreGreenAttacks.IsChecked != true )
            {
                if (village.Attacks.Count(a => a.Type == "attack_small") > 0)
                {
                    if (size == "all") attackTypeString += " ";
                    attackTypeString += "[command]attack_small[/command]" +
                                        village.Attacks.Count(a => a.Type == "attack_small");
                }
            }
            if (size == "all" || size == "unknown")
            {
                if (village.Attacks.Count(a => a.Type == "attack") > 0)
                {
                    if (size == "all") attackTypeString += " ";
                    attackTypeString += "[command]attack[/command]" +
                                        village.Attacks.Count(a => a.Type == "attack");
                }
            }

            return attackTypeString;
        }

        private static string GenerateRequestedDeff(DeffRequestVillage village, List<int> deffPerAttackCount, MainWindow contextWindow)
        {
            string deffString;
            if (contextWindow.CheckBoxCalculateAvailableDeff.IsChecked == true)
            {
                int usedFarmPlaces = village.Units.Spear + village.Units.Sword + village.Units.Arrow + 6 * village.Units.HeavyCavalry;
                int availableDeff = usedFarmPlaces / int.Parse(contextWindow.TextBoxAvailableDeffBhPlaces.Text);
                deffString = availableDeff + "/";
            }
            else
            {
                deffString = "0/";
            }
            if (contextWindow.CheckBoxIgnoreGreenAttacks.IsChecked == true)
            {
                int relevantAttackCount = village.Attacks.Where(s => s.Type != "attack_small").ToList().Count;
                deffString += deffPerAttackCount[Math.Max(relevantAttackCount - 1, 0)];
            }
            else
            {
                deffString += deffPerAttackCount[village.Attacks.Count - 1];
            }

            return deffString;
        }
    }
}
