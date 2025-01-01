using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Mapper;

namespace GeneratorPKMTeam.Infrastructure.Services
{
    public class PKMPersistence : IPKMPersistence
    {
        public PKMs GetPKMs()
        {
            var pkmConnector = new PKMJson();
            var pkmsJson = pkmConnector.RecupererListePKMs();
            var pkmStatConnector = new PKMStatsJson();
            var pkmStatJson = pkmStatConnector.GetPKMStatsDatas();
            var pkms = PKMMapper.ToDomain(pkmsJson, pkmStatJson);
            return pkms;
        }
    }
}