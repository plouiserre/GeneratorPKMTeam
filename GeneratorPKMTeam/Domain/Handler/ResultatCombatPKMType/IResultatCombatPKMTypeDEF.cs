using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType
{
    public interface IResultatCombatPKMTypeDEF
    {
        ResultatTirage NoterResultatTirage(List<RelPKMType> listesPKMTypesDangereux, List<PKMType> PKMTypes);
    }
}