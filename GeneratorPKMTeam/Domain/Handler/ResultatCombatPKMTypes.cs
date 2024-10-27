using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class ResultatCombatPKMTypes : IResultatCombatPKMTypes
    {
        private const double NombrePKMTypes = 18;
        private double _pourcentPKMTypesFaiblesTrouves;
        private double _pourcentPKMTypesDangereuxTrouves;
        private double _pourcentPKMTypesFinales;
        private ResultatTirageStatus _statusTirage;

        public ResultatTirage NoterResultatTirage(List<RelPKMType> listPKMTypesFaibles, List<RelPKMType> listesPKMTypesDangereux)
        {
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
            _pourcentPKMTypesFaiblesTrouves = listPKMTypesFaibles.Count / NombrePKMTypes * 100;
        }

        private void CalculerPourcentPKMTypesDangereux(List<RelPKMType> listPKMTypesDangereux)
        {
            double nbrePKMTypesNonDangereux = NombrePKMTypes - listPKMTypesDangereux.Count;
            _pourcentPKMTypesDangereuxTrouves = nbrePKMTypesNonDangereux / NombrePKMTypes * 100;
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
            else
                _statusTirage = ResultatTirageStatus.Excellent;
        }

        private ResultatTirage BuildResultatTirage(double pourcent, ResultatTirageStatus status)
        {
            var resultatTirage = new ResultatTirage() { NoteResultatTirage = pourcent, ResultatStatus = status };
            return resultatTirage;
        }
    }
}