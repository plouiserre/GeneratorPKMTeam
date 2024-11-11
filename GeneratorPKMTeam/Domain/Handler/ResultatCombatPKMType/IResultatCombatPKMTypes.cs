using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType
{
    public interface IResultatCombatPKMTypes
    {
        ResultatTirage NoterResultatTirage(List<PKMType> PKMTypesFaibles, List<PKMType> PKMTypesDangereux, List<PKMType> PKMTypesContrables);
    }
}