using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Tribalwars.UI.DeffRequester.Models;

namespace Tribalwars.UI.DeffRequester.Util
{
    public static class ParseInputCode
    {
        public static List<DeffRequestVillage> Parse(string input)
        {
            List<DeffRequestVillage> villages = new List<DeffRequestVillage>();
            DeffRequestVillage currentVillage = null;
            List<string> lines = Regex.Split(input, "\r\n|\r|\n").ToList();
            lines = lines.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            foreach (string line in lines)
            {
                List<string> parts = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).ToList();
                switch (parts[0].Substring(0, Math.Min(12, parts[0].Length)))
                {
                    case "[b]Dorf:[/b]":
                        if (currentVillage != null)
                        {
                            villages.Add(currentVillage);
                            currentVillage = new DeffRequestVillage();
                            currentVillage.AddCoord(parts[1]);
                            break;
                        }
                        else
                        {
                            currentVillage = new DeffRequestVillage();
                            currentVillage.AddCoord(parts[1]);
                            break;
                        }
                    case "[b]Wallstufe":
                        currentVillage.AddWallLevel(parts[1]);
                        break;
                    case "[b]Zustimmun":
                        currentVillage.AddLoyalty(parts[1]);
                        break;
                    case "[b]Verteidig":
                        parts.RemoveAt(0);
                        currentVillage.Units = new DeffRequestUnits(parts);
                        break;
                    case "[command]att":
                        currentVillage.Attacks.Add(new DeffRequestAttack(parts));
                        break;
                }
            }
            villages.Add(currentVillage);
            return villages;
        }
    }
}
