using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class ResultFightPKMTypes : IResultFightPKMTypes
    {
        private const double NombrePKMTypes = 18;

        public ResultatTirage NoterResultatTirage(List<RelPKMType> listPKMTypesFaibles)
        {
            double pourcentPKMTypesFaiblesTrouves = listPKMTypesFaibles.Count / NombrePKMTypes * 100;
            if (pourcentPKMTypesFaiblesTrouves < 60)
                return BuildResultatTirage(pourcentPKMTypesFaiblesTrouves, ResultatTirageStatus.Faible);
            else if (pourcentPKMTypesFaiblesTrouves < 100)
                return BuildResultatTirage(pourcentPKMTypesFaiblesTrouves, ResultatTirageStatus.Acceptable);
            else
                return BuildResultatTirage(pourcentPKMTypesFaiblesTrouves, ResultatTirageStatus.Parfait);
        }

        private ResultatTirage BuildResultatTirage(double pourcent, ResultatTirageStatus status)
        {
            var resultatTirage = new ResultatTirage() { NoteResultatTirage = pourcent, ResultatStatus = status };
            return resultatTirage;
        }
    }
}