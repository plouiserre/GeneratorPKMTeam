using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GeneratorPKMTeam.Infrastructure.Models.PKMStats
{
    public class PKMDataStatsInf
    {
        public Dictionary<string, string> ordreImportanceStats { get; set; }
        public List<PKMStatInf> pkmStats { get; set; }
    }
}