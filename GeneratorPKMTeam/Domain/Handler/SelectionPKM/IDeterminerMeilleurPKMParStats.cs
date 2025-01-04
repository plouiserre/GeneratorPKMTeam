using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.SelectionPKM
{
    public interface IDeterminerMeilleurPKMParStats
    {
        public List<PKM> Calculer(List<PKM> pkmsAComparer);
    }
}