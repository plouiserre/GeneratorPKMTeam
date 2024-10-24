using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GeneratorPKMTeam.Infrastructure.Connector.JsonModels;
using GeneratorPKMTeam.Infrastructure.Mapper;

namespace GeneratorPKMTeam.Infrastructure.Connector
{
    public class PKMTypeJson
    {
        public PKMDonnees GetPKMDatas()
        {
            string data = File.ReadAllText(@"PKMType.json");
            var json = JsonSerializer.Deserialize<PKMDonneesInf>(data);
            var result = PKMDonneesMapper.ToDomain(json);
            return result;
        }
    }
}