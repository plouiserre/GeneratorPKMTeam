using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using GeneratorPKMTeam.Infrastructure.Mapper;
using GeneratorPKMTeam.Infrastructure.Models.PKMDonnees;

namespace GeneratorPKMTeam.Infrastructure.Connector
{
    public class PKMTypeJson
    {
        public PKMDonneesInf GetPKMDatas()
        {
            string data = File.ReadAllText(@"PKMType.json");
            var json = JsonSerializer.Deserialize<PKMDonneesInf>(data);
            return json;
        }
    }
}