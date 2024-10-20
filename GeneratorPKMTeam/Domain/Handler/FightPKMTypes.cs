using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class FightPKMTypes : IFightPKMTypes
    {
        private List<RelPKMType> tousRelPkmTypes;

        public FightPKMTypes()
        {
            tousRelPkmTypes = new List<RelPKMType>();
        }

        public List<RelPKMType> RetournerTousFaiblesPKMTypes(List<PKMType> pkmTypes)
        {
            foreach (var PKMType in pkmTypes)
            {
                var relPKMTypes = RetournerFaiblesPKMTypesPourUnType(PKMType);
                ConstruireResultatsRelPKMTypesSansDoublon(relPKMTypes);
            }
            return tousRelPkmTypes;
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

        private void ConstruireResultatsRelPKMTypesSansDoublon(List<RelPKMType> relPKMTypes)
        {
            foreach (var relPkmType in relPKMTypes)
            {
                if (!VerifierDoublonRelPKMTypes(relPkmType))
                {
                    tousRelPkmTypes.Add(relPkmType);
                }
            }
        }

        private bool VerifierDoublonRelPKMTypes(RelPKMType relPKMType)
        {
            bool doublonPresent = false;
            foreach (var relPKMTypeAVerif in tousRelPkmTypes)
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