using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public interface IRecupererPKMTypeSimple
    {
        void RecupererPKMTypeDoublesDejaCalcules(Dictionary<string, List<PKMType>> pKMTypesDejaRecuperes);
        Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType, Dictionary<string, List<PKMType>> tousLesTypesPossibles);
    }
}