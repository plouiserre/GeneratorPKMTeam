using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public interface IRecupererPKMTypeDouble
    {
        Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType, Dictionary<string, List<PKMType>> tousLesTypesPossibles);
    }
}