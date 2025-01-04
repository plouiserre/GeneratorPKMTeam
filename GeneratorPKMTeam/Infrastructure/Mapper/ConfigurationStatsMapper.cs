using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Infrastructure.Mapper
{
    public static class ConfigurationStatsMapper
    {
        public static ConfigurationStats ToDomain(Dictionary<string, string> ordreImportanceStats)
        {
            var configurationStats = new ConfigurationStats();
            configurationStats.StatsParImportance = new Dictionary<int, PKMStatsLabel>();
            foreach (var stat in ordreImportanceStats)
            {
                var statLabel = ToDomain(stat.Value);
                configurationStats.StatsParImportance.Add(int.Parse(stat.Key), statLabel);
            }
            return configurationStats;
        }

        private static PKMStatsLabel ToDomain(string label)
        {
            switch (label)
            {
                case "Attaque":
                    return PKMStatsLabel.Attaque;
                case "SpAttaque":
                    return PKMStatsLabel.SPAttaque;
                case "Defense":
                    return PKMStatsLabel.Defense;
                case "SpDefense":
                    return PKMStatsLabel.SPDefense;
                case "Vitesse":
                    return PKMStatsLabel.Vitesse;
                case "PV":
                default:
                    return PKMStatsLabel.PV;
            }
        }
    }
}