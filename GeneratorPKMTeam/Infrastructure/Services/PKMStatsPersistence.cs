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
        private ConfigurationStats? _configurationStats;
        public ConfigurationStats AvoirConfigurationStats()
        {
            if (_configurationStats == null)
            {
                var pkmStatConnector = new PKMStatsJson();
                var pkmStatJson = pkmStatConnector.GetPKMStatsDatas();
                var configurationJson = pkmStatJson.ordreImportanceStats;
                _configurationStats = ConfigurationStatsMapper.ToDomain(configurationJson);
            }
            return _configurationStats;
        }
    }
}