using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.RechercherPKMType
{
    public class RechercherPKMTypeFaibles : RechercherPKMType, IRechercherPKMType
    {
        public RechercherPKMTypeFaibles() : base()
        {
        }

        public List<RelPKMType> TrouverPKMType(List<PKMType> PKMTypes)
        {
            foreach (var PKMType in PKMTypes)
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
    }
}