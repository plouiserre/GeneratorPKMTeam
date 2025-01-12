using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType
{
    public class ResultatCombatPKMTypeDEF : IResultatCombatPKMTypeDEF
    {

        public ResultatCombatPKMTypeDEF()
        {
        }

        public ResultatTirage NoterResultatTirage(List<PKMType> pkmTypesDangereux, List<PKMType> pkmTypesContrables)
        {
            double pourcentagePKMTypesContres = CalculerPourcentPKMTypesContres(pkmTypesDangereux, pkmTypesContrables);
            if (pourcentagePKMTypesContres < 30)
            {
                return new ResultatTirage() { ResultatStatus = ResultatTirageStatus.Faible, NoteResultatTirage = pourcentagePKMTypesContres };
            }
            else if (pourcentagePKMTypesContres < 100)
            {
                return new ResultatTirage() { ResultatStatus = ResultatTirageStatus.Acceptable, NoteResultatTirage = pourcentagePKMTypesContres };
            }
            else
                return new ResultatTirage() { ResultatStatus = ResultatTirageStatus.Parfait, NoteResultatTirage = pourcentagePKMTypesContres };
        }


        private double CalculerPourcentPKMTypesContres(List<PKMType> PKMTypesDangereux, List<PKMType> PKMTypesContres)
        {
            double nombrePKMTypesContres = PKMTypesContres.Count;
            double nombrePKMTypesDangereux = PKMTypesDangereux.Count;
            double resultat = nombrePKMTypesContres / nombrePKMTypesDangereux * 100;
            return Math.Round(resultat, 2);
        }
    }
}