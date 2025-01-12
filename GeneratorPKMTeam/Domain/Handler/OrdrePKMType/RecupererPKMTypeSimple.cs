using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public class RecupererPKMTypeSimple : RecupererPKMTypeBase, IRecupererPKMType, IRecupererPKMTypeSimple
    {
        private Dictionary<string, List<PKMType>> _pKMTypesDejaRecuperes;
        private Dictionary<string, List<PKMType>> _pkmTypeRecupere;
        private List<PKMType> _starterType;

        public RecupererPKMTypeSimple()
        {
        }

        public void RecupererPKMTypeDoublesDejaCalcules(Dictionary<string, List<PKMType>> pKMTypesDejaRecuperes)
        {
            _pKMTypesDejaRecuperes = pKMTypesDejaRecuperes;
        }

        public override Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType, Dictionary<string, List<PKMType>> tousLesTypesPossibles)
        {
            _pkmTypeRecupere = new Dictionary<string, List<PKMType>>();
            _starterType = starterType;
            GererStarterType();
            RecupererDoubleType();
            RecupererSimpleType(tousLesTypesPossibles);
            GererQuandPasAssezPKMRecupere();
            return _pkmTypeRecupere;
        }

        private void GererStarterType()
        {
            if (_starterType.Count == 1)
                _pkmTypeRecupere.Add(_starterType[0].Nom, _starterType);
        }

        private void RecupererDoubleType()
        {
            if (_pKMTypesDejaRecuperes != null)
            {
                foreach (var doubleType in _pKMTypesDejaRecuperes)
                {
                    _pkmTypeRecupere.Add(doubleType.Key, doubleType.Value);
                }
            }
        }

        private void RecupererSimpleType(Dictionary<string, List<PKMType>> tousLesTypesPossibles)
        {
            var simpleTypes = tousLesTypesPossibles.Where(o => o.Value.Count == 1);
            foreach (var simpleType in simpleTypes)
            {
                if (_pkmTypeRecupere.Count == 6)
                    break;
                if (!PKMTypeDejaSelectionne(simpleType.Key))
                    _pkmTypeRecupere.Add(simpleType.Key, simpleType.Value);
            }
        }

        private void GererQuandPasAssezPKMRecupere()
        {
            if (_pkmTypeRecupere.Count < 6)
            {
                var simpleTypeDejaRecupere = new List<PKMType>();
                foreach (var pkmTypes in _pkmTypeRecupere.Values)
                {
                    if (pkmTypes.Count == 1)
                    {
                        simpleTypeDejaRecupere.AddRange(pkmTypes);
                    }
                }
                throw new PasAssezPKMTypeSimpleSelectionnableException(simpleTypeDejaRecupere);
            }
        }

        private bool PKMTypeDejaSelectionne(string nomPkmType)
        {
            bool resultat = false;
            foreach (var pkmType in _pkmTypeRecupere)
            {
                if (pkmType.Key.Contains(nomPkmType))
                {
                    resultat = true;
                    break;
                }
            }

            return resultat;
        }
    }
}