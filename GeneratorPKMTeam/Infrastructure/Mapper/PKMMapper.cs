using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Infrastructure.Models;
using GeneratorPKMTeam.Infrastructure.Models.PKMs;
using GeneratorPKMTeam.Infrastructure.Models.PKMStats;

namespace GeneratorPKMTeam.Infrastructure.Mapper
{
    public class PKMMapper
    {
        public static PKMs ToDomain(PKMsInf json, PKMDataStatsInf pkmStatJson)
        {
            var pkms = new List<PKM>();
            foreach (var pkmJson in json.PKMs)
            {
                var stat = pkmStatJson.pkmStats.FirstOrDefault(o => o.id == pkmJson.pokedex_id);
                var pkm = ToDomain(pkmJson, stat);
                pkms.Add(pkm);
            }
            return new PKMs() { TousPKMs = pkms };
        }

        private static PKM ToDomain(PKMInf pkm, PKMStatInf statInf)
        {
            return new PKM()
            {
                Nom = pkm.fr,
                Generation = pkm.generation,
                PKMTypes = pkm.types.Select(o => o.name).ToList(),
                Stats = ToDomain(statInf)
            };
        }

        private static PKMStats ToDomain(PKMStatInf statInf)
        {
            if (statInf != null)
            {
                return new PKMStats()
                {
                    Attaque = statInf.stats.Attaque,
                    SpeAttaque = statInf.stats.SpAttaque,
                    Defense = statInf.stats.Defense,
                    SpeDefense = statInf.stats.SpDefense,
                    PV = statInf.stats.PV,
                    Vitesse = statInf.stats.Vitesse
                };
            }
            else
            {
                return new PKMStats();
            }
        }
    }
}