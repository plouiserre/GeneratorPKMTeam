using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Mapper;

namespace GeneratorPKMTeam.Infrastructure.Services
{
    public class PKMPersistence : IPKMPersistence
    {
        private PKMs? _pkms { get; set; }

        public PKMs GetPKMs()
        {
            if (_pkms == null)
            {
                var pkmConnector = new PKMJson();
                var pkmsJson = pkmConnector.RecupererListePKMs();
                var pkmStatConnector = new PKMStatsJson();
                var pkmStatJson = pkmStatConnector.GetPKMStatsDatas();
                _pkms = PKMMapper.ToDomain(pkmsJson, pkmStatJson);
            }
            return _pkms;
        }
    }
}