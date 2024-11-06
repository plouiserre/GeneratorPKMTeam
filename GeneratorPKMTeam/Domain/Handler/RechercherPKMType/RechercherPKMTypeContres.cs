using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.RechercherPKMType
{
    public class RechercherPKMTypeContres : RechercherPKMType, IRechercherPKMType
    {
        private List<PKMType> _PKMADefendre;
        private RechercherPKMTypeFaibles _rechercherPKMTypesFaibles;

        public RechercherPKMTypeContres(List<PKMType> PKMADefendre, RechercherPKMTypeFaibles rechercherPKMTypeFaibles)
        {
            _PKMADefendre = PKMADefendre;
            _rechercherPKMTypesFaibles = rechercherPKMTypeFaibles;
        }

        public List<RelPKMType> TrouverPKMType(List<PKMType> PKMTypes)
        {
            var relPKMTypes = _rechercherPKMTypesFaibles.TrouverPKMType(PKMTypes);
            foreach (var relPKMType in relPKMTypes)
            {
                foreach (var PKMTypeDEF in _PKMADefendre)
                {
                    if (relPKMType.TypePKM == PKMTypeDEF.Nom && !VerifierDoublonPKMTypes(PKMTypeDEF.Nom))
                    {
                        _tousRelPkmTypes.Add(relPKMType);
                    }
                }
            }
            return _tousRelPkmTypes;
        }
    }
}