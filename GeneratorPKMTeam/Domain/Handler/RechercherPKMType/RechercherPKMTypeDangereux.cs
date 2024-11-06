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


        public List<RelPKMType> TrouverPKMType(List<PKMType> PKMTypes)
        {
            var pkmTypesNames = PKMTypes.Select(o => o.Nom).ToList();
            foreach (var pkmTypePeutEtreDangereux in _tousPKMTypes)
            {
                var relPKMTypes = RetournerDangereuxPKMTypesPourUnType(pkmTypePeutEtreDangereux, pkmTypesNames);
                ConstruireResultatsRelPKMTypesSansDoublon(relPKMTypes);
            }
            return _tousRelPkmTypes;
        }

        private List<RelPKMType> RetournerDangereuxPKMTypesPourUnType(PKMType pkmTypePeutEtreDangereux, List<string> pkmTypesNomsEnDanger)
        {
            var relPkmTypes = new List<RelPKMType>();
            foreach (var relPKMType in pkmTypePeutEtreDangereux.RelPKMTypes)
            {
                if (relPKMType.ModeImpact > 1 && pkmTypesNomsEnDanger.Any(o => o == relPKMType.TypePKM))
                {
                    var relPKMTypeDef = new RelPKMType()
                    {
                        TypePKM = pkmTypePeutEtreDangereux.Nom,
                        ModeImpact = relPKMType.ModeImpact,
                    };
                    relPkmTypes.Add(relPKMTypeDef);
                }
            }
            return relPkmTypes;
        }
    }
}