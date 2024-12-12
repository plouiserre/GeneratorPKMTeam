using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class DefinirOrdrePKMType : IDefinirOrdrePKMType
    {
        private IDeterminerTousLesTypesExistant _determinerTousLesTypesExistant;
        private IGererStarterPKM _gererStarterPKM;
        private int _generation;
        private Dictionary<string, List<PKMType>> _tousLesTypesPossibles;
        private Dictionary<string, int> _occurencesTypes;
        private bool _starterDoubleType;
        private Dictionary<int, List<PKMType>> _typesPKMSelectionnes;

        public DefinirOrdrePKMType(IDeterminerTousLesTypesExistant determinerTousLesTypesExistant, IGererStarterPKM gererStarterPKM, int generation)
        {
            _determinerTousLesTypesExistant = determinerTousLesTypesExistant;
            _generation = generation;
            _gererStarterPKM = gererStarterPKM;
        }

        public Dictionary<int, List<PKMType>> Generer(List<PKMType> TypesAOrdonnerParPKM)
        {
            _typesPKMSelectionnes = new Dictionary<int, List<PKMType>>();
            _tousLesTypesPossibles = _determinerTousLesTypesExistant.Calculer(_generation, TypesAOrdonnerParPKM);

            List<PKMType> starterType = RecupererTypesStarter();
            if (!_starterDoubleType)
                AffectionPKMTypeAvecStarterSimpleType(starterType);
            else
                AffectionPKMTypeAvecStarterDoubleType(starterType);
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

        private void AffectionPKMTypeAvecStarterSimpleType(List<PKMType> starterType)
        {
            List<PKMType> premierDoubleType = RecupererDoubleTypeEnModeRandom();
            List<PKMType> secondDoubleType = RecupererDoubleTypeEnModeRandom();
            List<PKMType> premierTypeSimple = RecupererSimplePKMType();
            List<PKMType> secondTypeSimple = RecupererSimplePKMType();
            List<PKMType> troisiemeTypeSimple = RecupererSimplePKMType();
            _typesPKMSelectionnes.Add(1, starterType);
            _typesPKMSelectionnes.Add(2, premierTypeSimple);
            _typesPKMSelectionnes.Add(3, premierDoubleType);
            _typesPKMSelectionnes.Add(4, secondTypeSimple);
            _typesPKMSelectionnes.Add(5, troisiemeTypeSimple);
            _typesPKMSelectionnes.Add(6, secondDoubleType);
        }

        private void AffectionPKMTypeAvecStarterDoubleType(List<PKMType> starterType)
        {
            List<PKMType> premierDoubleType = RecupererDoubleTypeEnModeRandom();
            List<PKMType> premierTypeSimple = RecupererSimplePKMType();
            List<PKMType> secondTypeSimple = RecupererSimplePKMType();
            List<PKMType> troisiemeTypeSimple = RecupererSimplePKMType();
            List<PKMType> quatriemeTypeSimple = RecupererSimplePKMType();
            _typesPKMSelectionnes.Add(1, starterType);
            _typesPKMSelectionnes.Add(2, premierTypeSimple);
            _typesPKMSelectionnes.Add(3, secondTypeSimple);
            _typesPKMSelectionnes.Add(4, troisiemeTypeSimple);
            _typesPKMSelectionnes.Add(5, premierDoubleType);
            _typesPKMSelectionnes.Add(6, quatriemeTypeSimple);
        }

        private List<PKMType> RecupererDoubleTypeEnModeRandom()
        {
            List<PKMType> doublesTypes = new List<PKMType>();
            var tousLesDoublesTypesPossibles = _tousLesTypesPossibles.Select(o => o.Value).Where(o => o.Count == 2).ToList();

            Random random = new Random();
            int index = tousLesDoublesTypesPossibles.Count() > 1 ? random.Next(tousLesDoublesTypesPossibles.Count() - 1) : 0;
            doublesTypes = tousLesDoublesTypesPossibles[index];

            RetirerToutesLesOccurencesDuType(doublesTypes[0]);
            RetirerToutesLesOccurencesDuType(doublesTypes[1]);
            return doublesTypes;
        }

        private List<PKMType> RecupererSimplePKMType()
        {
            Random random = new Random();
            var tousLesSimpleTypesPossibles = _tousLesTypesPossibles.Select(o => o.Value).Where(o => o.Count == 1).ToList();
            int index = random.Next(tousLesSimpleTypesPossibles.Count - 1);
            var PKMTypeChoisi = tousLesSimpleTypesPossibles[index];
            var pkmTypes = new List<PKMType>();
            pkmTypes.AddRange(PKMTypeChoisi);
            RetirerToutesLesOccurencesDuType(PKMTypeChoisi[0]);
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