using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType
{
    public interface IResultatCombatPKMTypes
    {
        ResultatTirage NoterResultatTirage(List<RelPKMType> listPKMTypesFaibles, List<RelPKMType> listesPKMTypesDangereux, List<PKMType> PKMTypes);
    }
}