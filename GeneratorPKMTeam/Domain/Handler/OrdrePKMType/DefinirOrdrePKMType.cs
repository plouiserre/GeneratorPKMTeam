using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public class DefinirOrdrePKMType : IDefinirOrdrePKMType
    {
        private IDeterminerTousLesTypesExistant _determinerTousLesTypesExistant;
        private IGererStarterPKM _gererStarterPKM;
        private IGererRecuperationPKMType _gererRecuperationPKMType;
        private int _generation;
        private Dictionary<string, List<PKMType>> _tousLesTypesPossibles;
        private bool _starterDoubleType;
        private Dictionary<int, List<PKMType>> _typesPKMSelectionnes;
        private int _nbrePKMMax;

        public DefinirOrdrePKMType(IDeterminerTousLesTypesExistant determinerTousLesTypesExistant, IGererStarterPKM gererStarterPKM, IGererRecuperationPKMType gererRecuperationPKMType, int generation)
        {
            _gererRecuperationPKMType = gererRecuperationPKMType;
            _determinerTousLesTypesExistant = determinerTousLesTypesExistant;
            _generation = generation;
            _gererStarterPKM = gererStarterPKM;
            _nbrePKMMax = 6;

        }

        public Dictionary<int, List<PKMType>> Generer(List<PKMType> TypesAOrdonnerParPKM)
        {
            _typesPKMSelectionnes = new Dictionary<int, List<PKMType>>();
            _tousLesTypesPossibles = _determinerTousLesTypesExistant.Calculer(_generation, TypesAOrdonnerParPKM);
            List<PKMType> starterType = RecupererTypesStarter();
            Dictionary<string, List<PKMType>> typesRecuperes = _gererRecuperationPKMType.RecupererPKMTypes(starterType, _tousLesTypesPossibles);
            int index = 0;
            foreach (var typeRecupere in typesRecuperes)
            {
                _typesPKMSelectionnes.Add(index, typeRecupere.Value);
                index += 1;
            }
            return _typesPKMSelectionnes;
        }

        private List<PKMType> RecupererTypesStarter()
        {
            var starterPKM = _gererStarterPKM.RecupererStarter();
            var pkmTypes = starterPKM.PKMTypes.Select(o => new PKMType() { Nom = o }).ToList();
            RetirerToutesLesOccurencesDuType(pkmTypes[0]);
            if (pkmTypes.Count == 2)
            {
                _starterDoubleType = true;
                RetirerToutesLesOccurencesDuType(pkmTypes[1]);
            }
            return pkmTypes;
        }


        private void RetirerToutesLesOccurencesDuType(PKMType pKMType)
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
    }
}