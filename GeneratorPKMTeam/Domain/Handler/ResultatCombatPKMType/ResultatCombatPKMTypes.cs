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
        private List<PKMType> _pkmTypesFaibles;
        private List<PKMType> _pkmTypesDangereux;
        private List<PKMType> _pkmTypesContrables;
        private IResultatCombatPKMTypeATK _resultatCombatPKMTypeATK;
        private IResultatCombatPKMTypeDEF _resultatCombatPKMTypeDEF;

        public ResultatCombatPKMTypes(IResultatCombatPKMTypeATK resultatCombatPKMTypeATK, IResultatCombatPKMTypeDEF resultatCombatPKMTypeDEF)
        {
            _resultatCombatPKMTypeATK = resultatCombatPKMTypeATK;
            _resultatCombatPKMTypeDEF = resultatCombatPKMTypeDEF;
        }

        public ResultatTirage NoterResultatTirage(List<PKMType> PKMTypesFaibles, List<PKMType> PKMTypesDangereux, List<PKMType> PKMTypesContrables)
        {
            _pkmTypesFaibles = PKMTypesFaibles;
            _pkmTypesDangereux = PKMTypesDangereux;
            _pkmTypesContrables = PKMTypesContrables;
            CalculerPourcentFinal();
            DeterminerStatusTirage();
            return BuildResultatTirage(_pourcentPKMTypesFinales, _statusTirage);
        }

        private void CalculerPourcentFinal()
        {
            CalculerPourcentPKMTypesFaibles();
            CalculerPourcentPKMTypesDangereux();
            double resultat = (_pourcentPKMTypesFaiblesTrouves + _pourcentPKMTypesDangereuxTrouves) / 2;
            _pourcentPKMTypesFinales = Math.Round(resultat, 2);
        }

        private void CalculerPourcentPKMTypesFaibles()
        {
            var resultatTirage = _resultatCombatPKMTypeATK.NoterResultatTirage(_pkmTypesFaibles);
            _pourcentPKMTypesFaiblesTrouves = resultatTirage.NoteResultatTirage;
        }

        private void CalculerPourcentPKMTypesDangereux()
        {
            var resultatTirage = _resultatCombatPKMTypeDEF.NoterResultatTirage(_pkmTypesDangereux, _pkmTypesContrables);
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