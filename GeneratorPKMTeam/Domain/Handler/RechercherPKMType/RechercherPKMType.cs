using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.RechercherPKMType
{
    public abstract class RechercherPKMType
    {

        protected List<PKMType> _PKMTypesRecherches { get; set; }

        public RechercherPKMType()
        {
            _PKMTypesRecherches = new List<PKMType>();
        }

        protected void ConstruireResultatsRelPKMTypesSansDoublon(List<PKMType> PKMTypes)
        {
            foreach (var pKMType in PKMTypes)
            {
                if (!VerifierDoublonPKMTypes(pKMType.Nom))
                {
                    _PKMTypesRecherches.Add(pKMType);
                }
            }
        }
        protected bool VerifierDoublonPKMTypes(string NomPKMType)
        {
            bool doublonPresent = false;
            foreach (var pKMTypeAVerif in _PKMTypesRecherches)
            {
                if (pKMTypeAVerif.Nom == NomPKMType)
                {
                    doublonPresent = true;
                    break;
                }
            }
            return doublonPresent;
        }
    }
}