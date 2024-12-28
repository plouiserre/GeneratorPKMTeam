using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Models
{
    public class PKMStats
    {
        public int PV { get; set; }
        public int Attaque { get; set; }
        public int Defense { get; set; }
        public int SpeAttaque { get; set; }
        public int SpeDefense { get; set; }
        public int Vitesse { get; set; }
    }
}