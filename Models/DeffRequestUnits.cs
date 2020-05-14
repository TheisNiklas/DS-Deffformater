using System;
using System.Collections.Generic;
using System.Printing;
using System.Reflection;
using System.Text;

namespace Tribalwars.UI.DeffRequester.Models
{
    public class DeffRequestUnits
    {
        public int Spear { get; set; }
        public int Sword { get; set; }
        public int Axe { get; set; }
        public int Arrow { get; set; }
        public int Spy { get; set; }
        public int LightCavalry { get; set; }
        public int MountedArcher { get; set; }
        public int HeavyCavalry { get; set; }
        public int Ram { get; set; }
        public int Catapult { get; set; }
        public int Paladin { get; set; }
        public int Snob { get; set; }
        public int Militia { get; set; }

        public DeffRequestUnits(DeffRequestUnits units)
        {
            Spear = units.Spear;
            Sword = units.Sword;
            Axe = units.Axe;
            Arrow = units.Arrow;
            Spy = units.Spy;
            LightCavalry = units.LightCavalry;
            MountedArcher = units.MountedArcher;
            HeavyCavalry = units.HeavyCavalry;
            Ram = units.Ram;
            Catapult = units.Catapult;
            Paladin = units.Paladin;
            Snob = units.Snob;
            Militia = units.Militia;
        }
        public DeffRequestUnits(List<string> strings)
        {
            Spear = int.Parse(strings[0]);
            Sword = int.Parse(strings[1]);
            Axe = int.Parse(strings[2]);
            Arrow = int.Parse(strings[3]);
            Spy = int.Parse(strings[4]);
            LightCavalry = int.Parse(strings[5]);
            MountedArcher = int.Parse(strings[6]);
            HeavyCavalry = int.Parse(strings[7]);
            Ram = int.Parse(strings[8]);
            Catapult = int.Parse(strings[9]);
            Paladin = int.Parse(strings[10]);
            Snob = int.Parse(strings[11]);
            Militia = int.Parse(strings[12]);
        }

        public override string ToString()
        {
            string output = Spear + " " + Sword + " " + Axe + " " + Arrow + " " + Spy + " " + LightCavalry + " " +
                            MountedArcher + " " + HeavyCavalry + " " + Ram + " " + Catapult + " " + Paladin + " " +
                            Snob + " " + Militia;
            return output;
        }
    }
}
