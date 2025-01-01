using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GeneratorPKMTeam.Infrastructure.Models.PKMStats;

namespace GeneratorPKMTeam.Infrastructure.Connector
{
    public class PKMStatsJson
    {
        public PKMDataStatsInf GetPKMStatsDatas()
        {
            string data = File.ReadAllText(@"PKMStats.json");
            var json = JsonSerializer.Deserialize<PKMDataStatsInf>(data);
            return json;
        }
    }
}