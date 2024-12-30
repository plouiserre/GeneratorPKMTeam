using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler.SelectionPKM
{
    public class CalculerScorePKM
    {
        private IPKMStatsPersistence _pkmStatsPersistence;
        private DeterminerClassementPKMParStat _classer;
        private Dictionary<PKMStatsLabel, int> _coefficientsSpe;
        private Dictionary<PKM, int> _classementStatsFinales;
        private Dictionary<PKM, int> _classementSPAttaque;
        private Dictionary<PKM, int> _classementAttaque;
        private Dictionary<PKM, int> _classementPV;
        private Dictionary<PKM, int> _classementVitesse;
        private Dictionary<PKM, int> _classementSPDefense;
        private Dictionary<PKM, int> _classementDefense;

        public CalculerScorePKM(IPKMStatsPersistence pKMStatsPersistence)
        {
            _pkmStatsPersistence = pKMStatsPersistence;
            _classer = new DeterminerClassementPKMParStat();
            _classementStatsFinales = new Dictionary<PKM, int>();
        }

        public Dictionary<PKM, int> CalculerScore(List<PKM> pkms)
        {
            ObtenirCoefficientsSpe();
            RecupererClassementPKM(pkms);
            CalculerTousLesStats();
            return _classementStatsFinales;
        }

        private void RecupererClassementPKM(List<PKM> pkms)
        {
            _classementSPAttaque = _classer.Classer(pkms, PKMStatsLabel.SPAttaque);
            _classementAttaque = _classer.Classer(pkms, PKMStatsLabel.Attaque);
            _classementPV = _classer.Classer(pkms, PKMStatsLabel.PV);
            _classementVitesse = _classer.Classer(pkms, PKMStatsLabel.Vitesse);
            _classementSPDefense = _classer.Classer(pkms, PKMStatsLabel.SPDefense);
            _classementDefense = _classer.Classer(pkms, PKMStatsLabel.Defense);
        }

        private Dictionary<PKMStatsLabel, int> ObtenirCoefficientsSpe()
        {
            _coefficientsSpe = new Dictionary<PKMStatsLabel, int>();
            var statsCoefficientsConf = _pkmStatsPersistence.AvoirConfigurationStats();
            foreach (var stat in statsCoefficientsConf.StatsParImportance)
            {
                _coefficientsSpe.Add(stat.Value, stat.Key);
            }
            return _coefficientsSpe;
        }

        private void CalculerTousLesStats()
        {
            CalculerPointSpeSpecifique(PKMStatsLabel.SPAttaque, _classementSPAttaque);
            CalculerPointSpeSpecifique(PKMStatsLabel.Attaque, _classementAttaque);
            CalculerPointSpeSpecifique(PKMStatsLabel.PV, _classementPV);
            CalculerPointSpeSpecifique(PKMStatsLabel.Vitesse, _classementVitesse);
            CalculerPointSpeSpecifique(PKMStatsLabel.SPDefense, _classementSPDefense);
            CalculerPointSpeSpecifique(PKMStatsLabel.Defense, _classementDefense);
        }

        private void CalculerPointSpeSpecifique(PKMStatsLabel statsLabel, Dictionary<PKM, int> classementSpecifiqueStat)
        {
            foreach (var pkmSpe in classementSpecifiqueStat)
            {
                var pkm = pkmSpe.Key;
                var place = pkmSpe.Value;
                int coefficient = _coefficientsSpe[statsLabel];
                int point = (place + 1) * coefficient;
                AjouterPointClassementFinale(pkm, point);
            }
        }

        private void AjouterPointClassementFinale(PKM pkm, int point)
        {
            if (_classementStatsFinales.ContainsKey(pkm))
                _classementStatsFinales[pkm] += point;
            else
                _classementStatsFinales[pkm] = point;
        }
    }
}