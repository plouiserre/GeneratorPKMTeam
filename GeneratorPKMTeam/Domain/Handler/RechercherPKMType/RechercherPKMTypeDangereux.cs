using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.RechercherPKMType
{
    public class RechercherPKMTypeDangereux : RechercherPKMType, IRechercherPKMType
    {
        private List<PKMType> _tousPKMTypes { get; set; }

        public RechercherPKMTypeDangereux(List<PKMType> tousPKMTypes) : base()
        {
            _tousPKMTypes = tousPKMTypes;
        }

        public List<PKMType> TrouverPKMType(List<PKMType> PKMTypes)
        {
            var pkmTypesNames = PKMTypes.Select(o => o.Nom).ToList();
            foreach (var pkmTypePeutEtreDangereux in _tousPKMTypes)
            {
                var pKMTypes = RetournerDangereuxPKMTypesPourPlusieursTypes(pkmTypePeutEtreDangereux, pkmTypesNames);
                ConstruireResultatsRelPKMTypesSansDoublon(pKMTypes);
            }
            return _PKMTypesRecherches;
        }

        private List<PKMType> RetournerDangereuxPKMTypesPourPlusieursTypes(PKMType pkmTypePeutEtreDangereux, List<string> pkmTypesNomsEnDanger)
        {
            var pkmTypes = new List<PKMType>();
            foreach (var relPKMType in pkmTypePeutEtreDangereux.RelPKMTypes)
            {
                if (relPKMType.ModeImpact > 1 && pkmTypesNomsEnDanger.Any(o => o == relPKMType.TypePKM))
                {
                    var pKMType = new PKMType()
                    {
                        Nom = pkmTypePeutEtreDangereux.Nom
                    };
                    pkmTypes.Add(pKMType);
                }
            }
            return pkmTypes;
        }

        public Dictionary<PKMType, List<PKMType>> TrouverPKMTypeDangereuxPourChaqueType(List<PKMType> PKMTypes)
        {
            var pkmDangereuxParType = new Dictionary<PKMType, List<PKMType>>();
            var pkmTypesNames = PKMTypes.Select(o => o.Nom).ToList();
            foreach (var pkmType in PKMTypes)
            {
                pkmDangereuxParType.Add(pkmType, new List<PKMType>());
                foreach (var pkmTypePeutEtreDangereux in _tousPKMTypes)
                {
                    foreach (var relPKMType in pkmTypePeutEtreDangereux.RelPKMTypes)
                    {
                        if (relPKMType.ModeImpact > 1 && pkmType.Nom == relPKMType.TypePKM)
                        {
                            var pKMTypeDangereux = new PKMType()
                            {
                                Nom = pkmTypePeutEtreDangereux.Nom
                            };
                            pkmDangereuxParType[pkmType].Add(pKMTypeDangereux);
                        }
                    }
                }
            }
            return pkmDangereuxParType;
        }
    }
}