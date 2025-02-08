using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.RechercherPKMType
{
    public class RechercherPKMTypeContres : RechercherPKMType, IRechercherPKMType
    {
        private List<PKMType> _pKMTypeEquipe;
        private RechercherPKMTypeDangereux _rechercherPKMTypesDangereux;

        public RechercherPKMTypeContres(List<PKMType> pKMTypeEquipe, RechercherPKMTypeDangereux rechercherPKMTypeDangereux)
        {
            _pKMTypeEquipe = pKMTypeEquipe;
            _rechercherPKMTypesDangereux = rechercherPKMTypeDangereux;
        }

        public List<PKMType> TrouverPKMType(List<PKMType> pKMTypesDangereux)
        {
            var pKMTypesDangereuxNeutralises = new List<PKMType>();
            var pkmTypesDangereuxEnDifficultes = _rechercherPKMTypesDangereux.TrouverPKMTypeDangereuxPourChaqueType(pKMTypesDangereux);
            foreach (var pkmTypesAllies in pkmTypesDangereuxEnDifficultes)
            {
                foreach (var pkmType in _pKMTypeEquipe)
                {
                    bool dangereuxNeutralise = pkmTypesAllies.Value.Any(o => o.Nom == pkmType.Nom);
                    if (dangereuxNeutralise)
                    {
                        pKMTypesDangereuxNeutralises.Add(pkmTypesAllies.Key);
                        break;
                    }
                }
            }
            return pKMTypesDangereuxNeutralises;
        }
    }
}