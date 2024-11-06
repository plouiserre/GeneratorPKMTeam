using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.RechercherPKMType
{
    public abstract class RechercherPKMType
    {

        protected List<RelPKMType> _tousRelPkmTypes { get; set; }

        public RechercherPKMType()
        {
            _tousRelPkmTypes = new List<RelPKMType>();
        }

        protected void ConstruireResultatsRelPKMTypesSansDoublon(List<RelPKMType> relPKMTypes)
        {
            foreach (var relPkmType in relPKMTypes)
            {
                if (!VerifierDoublonPKMTypes(relPkmType.TypePKM))
                {
                    _tousRelPkmTypes.Add(relPkmType);
                }
            }
        }
        protected bool VerifierDoublonPKMTypes(string NomPKMType)
        {
            bool doublonPresent = false;
            foreach (var relPKMTypeAVerif in _tousRelPkmTypes)
            {
                if (relPKMTypeAVerif.TypePKM == NomPKMType)
                {
                    doublonPresent = true;
                    break;
                }
            }
            return doublonPresent;
        }
    }
}