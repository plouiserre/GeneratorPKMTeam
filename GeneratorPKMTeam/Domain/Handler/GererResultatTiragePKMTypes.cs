using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GererResultatTiragePKMTypes : IGererResultatTiragePKMTypes
    {
        private List<TiragePKMTypes> resultats;
        private double notePireTirage;

        public GererResultatTiragePKMTypes()
        {
            resultats = new List<TiragePKMTypes>();
        }

        public List<TiragePKMTypes> TirerPKMTypes(List<TiragePKMTypes> tiragesPKMTypesSauvegarde, TiragePKMTypes tirageATraiter)
        {
            resultats = tiragesPKMTypesSauvegarde;

            notePireTirage = resultats != null && resultats.Count > 0 ? resultats.Min(o => o.NoteTirage) : 0;

            if (tirageATraiter.ResultatTirageStatus == ResultatTirageStatus.Faible)
                GererTirageFaible(tirageATraiter);
            else if (tirageATraiter.ResultatTirageStatus == ResultatTirageStatus.Acceptable)
                GererTirageAcceptable(tirageATraiter);
            else
                GererTirageParfait(tirageATraiter);

            return resultats;
        }

        private void GererTirageFaible(TiragePKMTypes tirageATraiter)
        {
            if (resultats.Count < 10)
                resultats.Add(tirageATraiter);
        }

        private void GererTirageAcceptable(TiragePKMTypes tirageATraiter)
        {
            if (resultats.Count < 10)
                resultats.Add(tirageATraiter);
            else if (tirageATraiter.NoteTirage > notePireTirage)
            {
                SupprimerPireTirage();
                resultats.Add(tirageATraiter);
            }
        }

        private void GererTirageParfait(TiragePKMTypes tirageATraiter)
        {
            if (resultats.Count < 10)
                resultats.Add(tirageATraiter);
            else if (tirageATraiter.NoteTirage > notePireTirage)
            {
                SupprimerPireTirage();
                resultats.Add(tirageATraiter);
            }
        }

        private void SupprimerPireTirage()
        {
            var pireTirage = resultats.First(o => o.NoteTirage == notePireTirage);
            resultats.Remove(pireTirage);
        }
    }
}