using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrodalomProjekt
{
    internal class Kerdes
    {

        public Kerdes(string kerdesSzoveg, string valaszA, string valaszB, string valaszC, string helyesValasz)
        {
            KerdesSzoveg = kerdesSzoveg;
            ValaszA = valaszA;
            ValaszB = valaszB;
            ValaszC = valaszC;
            HelyesValasz = helyesValasz;
        }

        public string KerdesSzoveg { get; set; }

        public string ValaszA { get; set; }

        public string ValaszB { get; set; }

        public string ValaszC { get; set; }

        public string HelyesValasz { get; set; }

        public string FelhasznaloValasza { get; set; }

        public bool ValaszEllenorzes()
        {
            return FelhasznaloValasza is null ? false : FelhasznaloValasza.ToLower() == HelyesValasz.ToLower();
        }
    }
}
