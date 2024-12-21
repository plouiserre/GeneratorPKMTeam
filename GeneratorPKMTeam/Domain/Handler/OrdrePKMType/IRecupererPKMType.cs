using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public interface IRecupererPKMType
    {
        Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType);
    }
}