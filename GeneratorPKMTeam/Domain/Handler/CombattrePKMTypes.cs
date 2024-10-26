using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class CombattrePKMTypes : ICombattrePKMTypes
    {
        private List<RelPKMType> _tousRelPkmTypes;
        private List<PKMType> _tousPKMTypes;

        public CombattrePKMTypes()
        {
            _tousRelPkmTypes = new List<RelPKMType>();
        }

        public List<RelPKMType> RetournerTousFaiblesPKMTypes(List<PKMType> pkmTypes)
        {
            foreach (var PKMType in pkmTypes)
            {
                var relPKMTypes = RetournerFaiblesPKMTypesPourUnType(PKMType);
                ConstruireResultatsRelPKMTypesSansDoublon(relPKMTypes);
            }
            return _tousRelPkmTypes;
        }

        private List<RelPKMType> RetournerFaiblesPKMTypesPourUnType(PKMType pkmType)
        {
            var relPkmTypes = new List<RelPKMType>();
            foreach (var relPkmType in pkmType.RelPKMTypes)
            {
                if (relPkmType.ModeImpact > 1)
                {
                    relPkmTypes.Add(relPkmType);
                }
            }
            return relPkmTypes;
        }

        public List<RelPKMType> RetournerPKMTypesDangereux(List<PKMType> tousPKMTypes, List<PKMType> pkmTypes)
        {
            _tousPKMTypes = tousPKMTypes;
            var pkmTypesNames = pkmTypes.Select(o => o.Nom).ToList();
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

        private void ConstruireResultatsRelPKMTypesSansDoublon(List<RelPKMType> relPKMTypes)
        {
            foreach (var relPkmType in relPKMTypes)
            {
                if (!VerifierDoublonRelPKMTypes(relPkmType))
                {
                    _tousRelPkmTypes.Add(relPkmType);
                }
            }
        }

        private bool VerifierDoublonRelPKMTypes(RelPKMType relPKMType)
        {
            bool doublonPresent = false;
            foreach (var relPKMTypeAVerif in _tousRelPkmTypes)
            {
                if (relPKMTypeAVerif.TypePKM == relPKMType.TypePKM)
                {
                    doublonPresent = true;
                    break;
                }
            }
            return doublonPresent;
        }
    }
}