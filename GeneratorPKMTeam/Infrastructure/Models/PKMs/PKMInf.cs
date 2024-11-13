using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Infrastructure.Models.PKMs
{
    public class PKMInf
    {
        public int generation { get; set; }
        public string fr { get; set; }
        public List<PKMPKMTypeInf> types { get; set; }
    }
}