using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Models
{
    public class ConfigurationStats
    {
        public Dictionary<int, PKMStatsLabel> StatsParImportance { get; set; }
    }
}