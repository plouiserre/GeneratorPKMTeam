using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler.SelectionPKM
{
    public class DeterminerMeilleurPKMParStats : IDeterminerMeilleurPKMParStats
    {
        private IPKMStatsPersistence _pKMStatsPersistence;

        public DeterminerMeilleurPKMParStats(IPKMStatsPersistence pKMStatsPersistence)
        {
            _pKMStatsPersistence = pKMStatsPersistence;
        }

        public List<PKM> Calculer(List<PKM> pkmsAComparer)
        {
            var pkmsPlusPuissantWithStats = new List<PKM>();
            var calculerScore = new CalculerScorePKM(_pKMStatsPersistence);
            var statsPKms = calculerScore.CalculerScore(pkmsAComparer);
            int scorePlusHaut = int.MinValue;
            foreach (var statsPkm in statsPKms)
            {
                var pkm = statsPkm.Key;
                int point = statsPkm.Value;
                if (point == scorePlusHaut)
                {
                    pkmsPlusPuissantWithStats.Add(pkm);
                }
                else if (point > scorePlusHaut)
                {
                    pkmsPlusPuissantWithStats.Clear();
                    pkmsPlusPuissantWithStats.Add(pkm);
                    scorePlusHaut = point;
                }
            }
            return pkmsPlusPuissantWithStats;
        }
    }
}