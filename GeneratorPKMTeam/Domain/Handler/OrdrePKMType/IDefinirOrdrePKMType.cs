using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public interface IDefinirOrdrePKMType
    {
        Dictionary<int, List<PKMType>> Generer(List<PKMType> TypesAOrdonnerParPKM);
    }
}