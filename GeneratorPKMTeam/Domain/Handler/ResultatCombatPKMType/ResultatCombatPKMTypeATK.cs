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

        private List<RelPKMType> _listesPKMTypesFaibles;

        public ResultatTirage NoterResultatTirage(List<RelPKMType> listesPKMTypesFaibles)
        {
            _listesPKMTypesFaibles = listesPKMTypesFaibles;
            double note = CalculNoteTirage();
            if (note < 60)
                return BuildResultatTirage(30, ResultatTirageStatus.Faible);
            else if (note < 100)
                return BuildResultatTirage(100, ResultatTirageStatus.Acceptable);
            else
                return BuildResultatTirage(100, ResultatTirageStatus.Parfait);
        }

        private double CalculNoteTirage()
        {
            return _listesPKMTypesFaibles.Count / NombrePKMTypes * 100;
        }

        private ResultatTirage BuildResultatTirage(double note, ResultatTirageStatus resultatTirage)
        {
            return new ResultatTirage() { NoteResultatTirage = note, ResultatStatus = resultatTirage };
        }
    }
}