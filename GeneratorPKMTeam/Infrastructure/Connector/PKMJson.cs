using System.Text.Json;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Infrastructure.Mapper;
using GeneratorPKMTeam.Infrastructure.Models;
using GeneratorPKMTeam.Infrastructure.Models.PKMs;

namespace GeneratorPKMTeam.Infrastructure.Connector
{
    public class PKMJson
    {
        public PKMs RecupererListePKMs()
        {
            string data = File.ReadAllText(@"PKMs.json");
            var json = JsonSerializer.Deserialize<PKMsInf>(data);
            var result = PKMMapper.ToDomain(json);
            return result;
        }
    }
}