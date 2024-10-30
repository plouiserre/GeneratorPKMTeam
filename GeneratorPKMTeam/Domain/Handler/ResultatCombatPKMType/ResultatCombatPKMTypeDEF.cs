using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType
{
    public class ResultatCombatPKMTypeDEF : IResultatCombatPKMTypeDEF
    {
        private const double NombrePKMTypes = 18;

        private List<RelPKMType> _listesPKMTypesFaibles;
        private ICombattrePKMTypes _combattrePKMTypes;

        public ResultatCombatPKMTypeDEF(ICombattrePKMTypes combattrePKMTypes)
        {
            _combattrePKMTypes = combattrePKMTypes;
        }

        public ResultatTirage NoterResultatTirage(List<RelPKMType> listesPKMTypesDangereux, List<PKMType> PKMTypes)
        {
            var PKMTypesContres = _combattrePKMTypes.RetournerPKMTypesContres(listesPKMTypesDangereux, PKMTypes);
            double pourcentagePKMTypesContres = CalculerPourcentPKMTypesContres(PKMTypesContres, listesPKMTypesDangereux);
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


        private double CalculerPourcentPKMTypesContres(List<RelPKMType> PKMTypesContres, List<RelPKMType> PKMTypesDangereux)
        {
            double nombrePKMTypesContres = PKMTypesContres.Count;
            double nombrePKMTypesDangereux = PKMTypesDangereux.Count;
            return nombrePKMTypesContres / nombrePKMTypesDangereux * 100;
        }
    }
}