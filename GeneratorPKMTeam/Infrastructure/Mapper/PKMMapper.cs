using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Infrastructure.Models;
using GeneratorPKMTeam.Infrastructure.Models.PKMs;

namespace GeneratorPKMTeam.Infrastructure.Mapper
{
    public class PKMMapper
    {
        public static PKMs ToDomain(PKMsInf json)
        {
            return new PKMs() { TousPKMs = json.PKMs.Select(o => ToDomain(o)).ToList() };
        }

        private static PKM ToDomain(PKMInf pkm)
        {
            return new PKM()
            {
                Nom = pkm.fr,
                Generation = pkm.generation,
                PKMTypes = pkm.types.Select(o => o.name).ToList()
            };
        }
    }
}