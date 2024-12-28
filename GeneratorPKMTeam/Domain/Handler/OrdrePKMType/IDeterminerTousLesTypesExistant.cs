using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public interface IDeterminerTousLesTypesExistant
    {
        Dictionary<string, List<PKMType>> Calculer(int generation, List<PKMType> PKMTypes);
    }
}