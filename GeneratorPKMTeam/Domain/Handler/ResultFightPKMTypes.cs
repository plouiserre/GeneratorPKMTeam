using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class ResultFightPKMTypes : IResultFightPKMTypes
    {
        private const double NombrePKMTypes = 18;
        public ResultatTirageStatus AccepterResultatTirage(List<RelPKMType> listPKMTypesFaibles)
        {
            double pourcentPKMTypesFaiblesTrouves = listPKMTypesFaibles.Count / NombrePKMTypes * 100;
            if (pourcentPKMTypesFaiblesTrouves <= 30)
                return ResultatTirageStatus.Faible;
            else if (pourcentPKMTypesFaiblesTrouves <= 80)
                return ResultatTirageStatus.Acceptable;
            else
                return ResultatTirageStatus.Parfait;
        }
    }
}