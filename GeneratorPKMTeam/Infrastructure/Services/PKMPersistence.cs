using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Connector;

namespace GeneratorPKMTeam.Infrastructure.Services
{
    public class PKMPersistence : IPKMPersistence
    {
        public PKMs GetPKMs()
        {
            var connector = new PKMJson();
            var pkms = connector.RecupererListePKMs();
            return pkms;
        }
    }
}