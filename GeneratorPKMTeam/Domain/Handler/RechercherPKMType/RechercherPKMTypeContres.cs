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

        public List<PKMType> TrouverPKMType(List<PKMType> PKMTypesChoisis)
        {
            var PKMTypesFaibles = _rechercherPKMTypesFaibles.TrouverPKMType(PKMTypesChoisis);
            foreach (var PKMType in PKMTypesFaibles)
            {
                foreach (var PKMTypeDEF in _PKMADefendre)
                {
                    if (PKMType.Nom == PKMTypeDEF.Nom && !VerifierDoublonPKMTypes(PKMTypeDEF.Nom))
                    {
                        _PKMTypesRecherches.Add(PKMType);
                    }
                }
            }
            return _PKMTypesRecherches;
        }
    }
}