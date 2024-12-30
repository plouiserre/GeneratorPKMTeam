using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.SelectionPKM
{
    public class DeterminerClassementPKMParStat
    {
        private PKMStatsLabel _statAComparer;
        private bool _egalite = false;
        private List<PKM> _pkmsAComparer;
        private int _statPkmResultat;
        private int _statPKM;

        public Dictionary<PKM, int> Classer(List<PKM> pKMs, PKMStatsLabel statAComparer)
        {
            _statAComparer = statAComparer;
            _pkmsAComparer = new List<PKM>(pKMs);
            int nbrePkmAComparer = _pkmsAComparer.Count;
            Dictionary<PKM, int> pkmsClasses = new Dictionary<PKM, int>();
            int index = 0;
            int egaliteFois = 0;
            while (pkmsClasses.Count < nbrePkmAComparer)
            {
                var pkmResultat = DeterminerPlusPetiteStat();
                pkmsClasses.Add(pkmResultat, index);
                _pkmsAComparer.Remove(pkmResultat);
                GererPlaceClassementState(ref egaliteFois, ref index);
            }
            return pkmsClasses;
        }

        private void GererPlaceClassementState(ref int egaliteFois, ref int index)
        {
            if (!_egalite)
            {
                index = index + 1 + egaliteFois;
                egaliteFois = 0;
            }
            else
            {
                egaliteFois += 1;
            }
        }

        private PKM DeterminerPlusPetiteStat()
        {
            PKM pkmResultat = new PKM();
            foreach (var pkm in _pkmsAComparer)
            {
                _statPkmResultat = pkmResultat.Stats != null ? StatAComparer(pkmResultat) : 0;
                _statPKM = StatAComparer(pkm);
                pkmResultat = CompareStats(pkmResultat, pkm);
            }
            return pkmResultat;
        }

        private int StatAComparer(PKM pkm)
        {
            switch (_statAComparer)
            {
                case PKMStatsLabel.PV:
                    return pkm.Stats.PV;
                case PKMStatsLabel.Attaque:
                    return pkm.Stats.Attaque;
                case PKMStatsLabel.Defense:
                    return pkm.Stats.Defense;
                case PKMStatsLabel.SPAttaque:
                    return pkm.Stats.SpeAttaque;
                case PKMStatsLabel.SPDefense:
                    return pkm.Stats.SpeDefense;
                case PKMStatsLabel.Vitesse:
                default:
                    return pkm.Stats.Vitesse;
            }
        }

        private PKM CompareStats(PKM pkmResultat, PKM pkm)
        {
            if (string.IsNullOrEmpty(pkmResultat.Nom))
            {
                pkmResultat = pkm;
                _egalite = false;
            }
            else if (_statPKM < _statPkmResultat)
            {
                pkmResultat = pkm;
                _egalite = false;
            }
            else if (_statPKM == _statPkmResultat)
            {
                pkmResultat = pkm;
                _egalite = true;
            }
            return pkmResultat;
        }
    }
}