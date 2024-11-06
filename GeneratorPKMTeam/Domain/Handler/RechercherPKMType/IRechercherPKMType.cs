using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.RechercherPKMType
{
    public interface IRechercherPKMType
    {
        List<RelPKMType> TrouverPKMType(List<PKMType> PKMTypes);
    }
}