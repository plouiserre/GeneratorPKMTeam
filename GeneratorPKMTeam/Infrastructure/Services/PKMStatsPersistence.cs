using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Mapper;

namespace GeneratorPKMTeam.Infrastructure.Services
{
    public class PKMStatsPersistence : IPKMStatsPersistence
    {
        public ConfigurationStats AvoirConfigurationStats()
        {
            var pkmStatConnector = new PKMStatsJson();
            var pkmStatJson = pkmStatConnector.GetPKMStatsDatas();
            var configurationJson = pkmStatJson.ordreImportanceStats;
            return ConfigurationStatsMapper.ToDomain(configurationJson);
        }
    }
}