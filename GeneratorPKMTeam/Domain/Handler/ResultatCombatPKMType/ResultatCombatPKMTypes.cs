using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType
{
    public class ResultatCombatPKMTypes : IResultatCombatPKMTypes
    {
        private const double NombrePKMTypes = 18;
        private double _pourcentPKMTypesFaiblesTrouves;
        private double _pourcentPKMTypesDangereuxTrouves;
        private double _pourcentPKMTypesFinales;
        private ResultatTirageStatus _statusTirage;
        private List<PKMType> _pkmTypes;
        private IResultatCombatPKMTypeATK _resultatCombatPKMTypeATK;
        private IResultatCombatPKMTypeDEF _resultatCombatPKMTypeDEF;

        public ResultatCombatPKMTypes(IResultatCombatPKMTypeATK resultatCombatPKMTypeATK, IResultatCombatPKMTypeDEF resultatCombatPKMTypeDEF)
        {
            _resultatCombatPKMTypeATK = resultatCombatPKMTypeATK;
            _resultatCombatPKMTypeDEF = resultatCombatPKMTypeDEF;
        }

        public ResultatTirage NoterResultatTirage(List<RelPKMType> listPKMTypesFaibles, List<RelPKMType> listesPKMTypesDangereux, List<PKMType> PKMTypes)
        {
            _pkmTypes = PKMTypes;
            CalculerPourcentFinal(listPKMTypesFaibles, listesPKMTypesDangereux);
            DeterminerStatusTirage();
            return BuildResultatTirage(_pourcentPKMTypesFinales, _statusTirage);
        }

        private void CalculerPourcentFinal(List<RelPKMType> listPKMTypesFaibles, List<RelPKMType> listesPKMTypesDangereux)
        {
            CalculerPourcentPKMTypesFaibles(listPKMTypesFaibles);
            CalculerPourcentPKMTypesDangereux(listesPKMTypesDangereux);
            _pourcentPKMTypesFinales = (_pourcentPKMTypesFaiblesTrouves + _pourcentPKMTypesDangereuxTrouves) / 2;
        }

        private void CalculerPourcentPKMTypesFaibles(List<RelPKMType> listPKMTypesFaibles)
        {
            var resultatTirage = _resultatCombatPKMTypeATK.NoterResultatTirage(listPKMTypesFaibles);
            _pourcentPKMTypesFaiblesTrouves = resultatTirage.NoteResultatTirage;
        }

        private void CalculerPourcentPKMTypesDangereux(List<RelPKMType> listPKMTypesDangereux)
        {
            var resultatTirage = _resultatCombatPKMTypeDEF.NoterResultatTirage(listPKMTypesDangereux, _pkmTypes);
            _pourcentPKMTypesDangereuxTrouves = resultatTirage.NoteResultatTirage;
        }

        private void DeterminerStatusTirage()
        {
            if (_pourcentPKMTypesFinales < 20)
                _statusTirage = ResultatTirageStatus.Faible;
            else if (_pourcentPKMTypesFinales < 40)
                _statusTirage = ResultatTirageStatus.Passables;
            else if (_pourcentPKMTypesFinales < 60)
                _statusTirage = ResultatTirageStatus.Acceptable;
            else if (_pourcentPKMTypesFinales < 80)
                _statusTirage = ResultatTirageStatus.Bonnes;
            else if (_pourcentPKMTypesFinales < 100)
                _statusTirage = ResultatTirageStatus.Excellent;
            else
                _statusTirage = ResultatTirageStatus.Parfait;
        }

        private ResultatTirage BuildResultatTirage(double pourcent, ResultatTirageStatus status)
        {
            var resultatTirage = new ResultatTirage() { NoteResultatTirage = pourcent, ResultatStatus = status };
            return resultatTirage;
        }
    }
}