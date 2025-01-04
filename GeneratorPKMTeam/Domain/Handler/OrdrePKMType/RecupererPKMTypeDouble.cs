using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public class RecupererPKMTypeDouble : RecupererPKMTypeBase, IRecupererPKMType, IRecupererPKMTypeDouble
    {
        private List<PKMType> _starterType;
        private List<PKMType> _pkmTypesSauvegardes;
        private Dictionary<string, List<PKMType>> _doublesTypesDifferentsRecuperes;
        private bool _starterDoubleTypes;

        public override Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType, Dictionary<string, List<PKMType>> tousLesTypesPossibles)
        {
            _starterType = starterType;
            _tousLesTypesPossibles = tousLesTypesPossibles;
            _pkmTypesSauvegardes = new List<PKMType>();
            _doublesTypesDifferentsRecuperes = new Dictionary<string, List<PKMType>>();
            if (starterType.Count == 2)
            {
                GererRecuperationAvecStarterDoubleType();
            }
            AffecterDoubleType();
            return _doublesTypesDifferentsRecuperes;
        }

        private void GererRecuperationAvecStarterDoubleType()
        {
            _starterDoubleTypes = true;
            string key = _starterType[0].Nom + "-" + _starterType[1].Nom;
            _doublesTypesDifferentsRecuperes.Add(key, _starterType);
            _pkmTypesSauvegardes.AddRange(_starterType);
        }

        private void AffecterDoubleType()
        {
            var doublesTypes = _tousLesTypesPossibles.Where(o => o.Value.Count == 2);
            int index = 0;
            foreach (var types in doublesTypes)
            {
                if (_doublesTypesDifferentsRecuperes.Count == 6)
                {
                    break;
                }
                else if (index == 0 && _pkmTypesSauvegardes.Count == 0)
                {
                    RecupererPremierDoubleTypeAvecStarterSimple(types.Key, types.Value);
                }
                else if (_starterDoubleTypes || (!_starterDoubleTypes && _doublesTypesDifferentsRecuperes.Count < 5))
                {
                    RecupererAutresDoublesTypes(types.Key, types.Value);
                }
                index += 1;
            }
        }

        private void RecupererPremierDoubleTypeAvecStarterSimple(string nomPKMTypes, List<PKMType> pKMTypes)
        {
            RecupererDoubleType(nomPKMTypes, pKMTypes);
        }

        private void RecupererAutresDoublesTypes(string nomPKMTypes, List<PKMType> pKMTypes)
        {
            bool doublesTypesVierge = true;
            foreach (var pkmType in pKMTypes)
            {
                bool contientPKMType = _pkmTypesSauvegardes.Any(o => o.Nom == pkmType.Nom);
                if (contientPKMType)
                {
                    doublesTypesVierge = false;
                    break;
                }
            }
            if (doublesTypesVierge)
            {
                RecupererDoubleType(nomPKMTypes, pKMTypes);
            }
        }

        private void RecupererDoubleType(string nomPKMTypes, List<PKMType> pKMTypes)
        {
            if (_starterDoubleTypes == true || (pKMTypes[0].Nom != _starterType[0].Nom && pKMTypes[1].Nom != _starterType[0].Nom))
            {
                _doublesTypesDifferentsRecuperes.Add(nomPKMTypes, pKMTypes);
                _pkmTypesSauvegardes.AddRange(pKMTypes);
                RetirerToutesLesOccurencesDuType(pKMTypes[0]);
                RetirerToutesLesOccurencesDuType(pKMTypes[1]);
            }
        }
    }
}