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

        public List<PKMType> TrouverPKMType(List<PKMType> PKMTypes)
        {
            foreach (var PKMType in PKMTypes)
            {
                var relPKMTypes = RetournerFaiblesPKMTypesPourUnType(PKMType);
                ConstruireResultatsRelPKMTypesSansDoublon(relPKMTypes);
            }
            return _PKMTypesRecherches;
        }

        private List<PKMType> RetournerFaiblesPKMTypesPourUnType(PKMType pkmType)
        {
            var pkmTypes = new List<PKMType>();
            foreach (var relPkmType in pkmType.RelPKMTypes)
            {
                if (relPkmType.ModeImpact > 1)
                {
                    var PKMType = new PKMType() { Nom = relPkmType.TypePKM };
                    pkmTypes.Add(PKMType);
                }
            }
            return pkmTypes;
        }
    }
}