using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Infrastructure.Models.PKMDonnees
{
    public class PKMTypeInf
    {
        public string Nom { get; set; }

        public List<RelPKMTypeInf> RelPKMTypes { get; set; }
    }
}