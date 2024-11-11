using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType
{
    public class ResultatCombatPKMTypeATK : IResultatCombatPKMTypeATK
    {
        private const double NombrePKMTypes = 18;

        private List<PKMType> _PKMTypesFaibles;

        public ResultatTirage NoterResultatTirage(List<PKMType> PKMTypesFaibles)
        {
            _PKMTypesFaibles = PKMTypesFaibles;
            double note = CalculNoteTirage();
            var resultatTirageStatus = ResultatTirageStatus.Parfait;
            if (note < 60)
                resultatTirageStatus = ResultatTirageStatus.Faible;
            else if (note < 100)
                resultatTirageStatus = ResultatTirageStatus.Acceptable;
            return BuildResultatTirage(note, resultatTirageStatus);
        }

        private double CalculNoteTirage()
        {
            double resultat = _PKMTypesFaibles.Count / NombrePKMTypes * 100;
            double resultatArrondi = Math.Round(resultat, 2);
            return resultatArrondi;
        }

        private ResultatTirage BuildResultatTirage(double note, ResultatTirageStatus resultatTirage)
        {
            return new ResultatTirage() { NoteResultatTirage = note, ResultatStatus = resultatTirage };
        }
    }
}