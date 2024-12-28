using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public abstract class RecupererPKMTypeBase : IRecupererPKMType
    {
        protected Dictionary<string, List<PKMType>> _tousLesTypesPossibles;

        protected void RetirerToutesLesOccurencesDuType(PKMType pKMType)
        {
            var tousTypesPossiblesEncoreIllegibles = new Dictionary<string, List<PKMType>>();
            foreach (var typesPossible in _tousLesTypesPossibles)
            {
                if (PKMTypePresent(typesPossible.Value, pKMType))
                {
                    continue;
                }
                else
                {
                    tousTypesPossiblesEncoreIllegibles.Add(typesPossible.Key, typesPossible.Value);
                }
            }
            _tousLesTypesPossibles = tousTypesPossiblesEncoreIllegibles;
        }

        private bool PKMTypePresent(List<PKMType> types, PKMType pKMType)
        {
            bool present = false;
            foreach (var type in types)
            {
                if (type.Nom == pKMType.Nom)
                {
                    present = true;
                    break;
                }
            }
            return present;
        }

        public abstract Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType, Dictionary<string, List<PKMType>> tousLesTypesPossibles);
    }
}