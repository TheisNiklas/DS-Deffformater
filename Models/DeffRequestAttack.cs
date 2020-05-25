using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tribalwars.UI.DeffRequester.Models
{
    public class DeffRequestAttack
    {
        public string
            Type
        {
            get;
            set;
        } //attack = unkonwn,    attack_small = <1000,    attack_medium = 1000 < x < 5000,    attack_large = >5000

        public string Name { get; set; }

        public int
            NamedType { get; set; } //0 = unnamed, 1 = Spear, 2 = Sword, 3 = Spy, 4 = LKav, 5 = Skav, 6 = Ram, 7 = AG

        public int OriginX { get; set; }
        public int OriginY { get; set; }
        public string Attacker { get; set; }
        public DateTime Arrival { get; set; }

        public DeffRequestAttack() { }

        public DeffRequestAttack(DeffRequestAttack attack)
        {
            Type = attack.Type;
            Name = attack.Name;
            NamedType = attack.NamedType;
            OriginX = attack.OriginX;
            OriginY = attack.OriginY;
            Attacker = attack.Attacker;
            Arrival = attack.Arrival;
        }
        public DeffRequestAttack(List<string> parts)
        {
            bool commandDone = false;
            for (int i = 0; i < parts.Count; i++)
            {
                switch (parts[i].Substring(0, Math.Min(7, parts[i].Length)))
                {
                    case "[comman":
                        if (!commandDone)
                        {
                            string commandType = parts[i].Substring(9, parts[i].Length - 19);
                            Type = commandType;
                            commandDone = true;
                        }
                        break;
                    case "[coord]":
                        string xString = parts[i].Substring(7, 3);
                        string yString = parts[i].Substring(11, 3);
                        OriginX = int.Parse(xString);
                        OriginY = int.Parse(yString);
                        // + " " + parts[i + 4], "dd.MM.yy hh:mm:ss:fff"
                        Arrival = DateTime.ParseExact(parts[i + 3] + " " + parts[i + 4], "dd.MM.yy HH:mm:ss:fff", null);
                        i += 4;
                        break;
                    case "[player":
                        string attacker;
                        if(parts[i].Substring(parts[i].Length -9, 9) == "[/player]") attacker = parts[i].Substring(8, parts[i].Length - 17);
                        else
                        {
                            attacker = parts[i].Substring(8, parts[i].Length - 8);
                            i++;
                            while (parts[i].Substring(Math.Max(0, parts[i].Length - 9), Math.Min(9, parts[i].Length)) != "[/player]")
                            {
                                var deb = parts[i].Substring(Math.Max(0, parts[i].Length - 9), Math.Min(9, parts[i].Length));
                                attacker += " " + parts[i];
                                i++;
                            }
                            attacker += " " + parts[i].Substring(0, parts[i].Length - 9);
                        }
                        Attacker = attacker;
                        break;
                    default:
                        Name = parts[i];
                        i++;
                        while (parts[i].Substring(0, Math.Min(7, parts[i].Length-1)) != "[coord]")
                        {
                            Name += " " + parts[i];
                            i++;
                        }
                        i--;
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                if (Name.Contains("Axt", StringComparison.CurrentCultureIgnoreCase)) NamedType = 1;
                else if (Name.Contains("Schwert", StringComparison.CurrentCultureIgnoreCase)) NamedType = 2;
                else if (Name.Contains("Spy", StringComparison.CurrentCultureIgnoreCase)) NamedType = 3;
                else if (Name.Contains("Späh", StringComparison.CurrentCultureIgnoreCase)) NamedType = 3;
                else if (Name.Contains("Lkav", StringComparison.CurrentCultureIgnoreCase)) NamedType = 4;
                else if (Name.Contains("Skav", StringComparison.CurrentCultureIgnoreCase)) NamedType = 5;
                else if (Name.Contains("Ram", StringComparison.CurrentCultureIgnoreCase)) NamedType = 6;
                else if (Name.Contains("AG", StringComparison.CurrentCultureIgnoreCase)) NamedType = 7;
            }
            else
            {
                NamedType = 0;
            }
        }
    }
}
