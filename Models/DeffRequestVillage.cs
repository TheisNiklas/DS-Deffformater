using System;
using System.Collections.Generic;
using System.Text;

namespace Tribalwars.UI.DeffRequester.Models
{
    public class DeffRequestVillage
    {
        public bool IsSelected { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int WallLevel { get; set; }
        public int Loyalty { get; set; }
        public DeffRequestUnits Units { get; set; }
        public List<DeffRequestAttack> Attacks { get; set; } = new List<DeffRequestAttack>();

        public DeffRequestVillage() { }

        public DeffRequestVillage(DeffRequestVillage village)
        {
            IsSelected = village.IsSelected;
            X = village.X;
            Y = village.Y;
            WallLevel = village.WallLevel;
            Loyalty = village.Loyalty;
            Units = new DeffRequestUnits(village.Units);
            foreach (var attack in village.Attacks)
            {
                Attacks.Add(new DeffRequestAttack(attack));
            }
        }

        public void AddCoord(string s)
        {
            string xString = s.Substring(7, 3);
            string yString = s.Substring(11, 3);
            X = int.Parse(xString);
            Y = int.Parse(yString);
        }

        public void AddWallLevel(string s)
        {
            WallLevel = int.Parse(s);
        }

        public void AddLoyalty(string s)
        {
            Loyalty = int.Parse(s);
        }
    }
}
