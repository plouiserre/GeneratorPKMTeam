using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class DefinirOrdrePKMType : IDefinirOrdrePKMType
    {
        private IDeterminerTousLesTypesExistant _determinerTousLesTypesExistant;
        private int _generation;
        private Dictionary<string, List<PKMType>> _tousLesTypesPossibles;
        private List<PKMType> _typesAOrdonnerParPKM;

        public DefinirOrdrePKMType(IDeterminerTousLesTypesExistant determinerTousLesTypesExistant, int generation)
        {
            _determinerTousLesTypesExistant = determinerTousLesTypesExistant;
            _generation = generation;
        }

        public Dictionary<int, List<PKMType>> Generer(List<PKMType> TypesAOrdonnerParPKM)
        {
            Dictionary<int, List<PKMType>> typesPKMSelectionnes = new Dictionary<int, List<PKMType>>();
            _tousLesTypesPossibles = _determinerTousLesTypesExistant.Calculer(_generation, TypesAOrdonnerParPKM);
            _typesAOrdonnerParPKM = TypesAOrdonnerParPKM;

            List<PKMType> premierDoubleType = RecupererDoubleTypeEnModeRandom();
            List<PKMType> secondDoubleType = RecupererDoubleTypeEnModeRandom();
            List<PKMType> troisiemeDoubleType = RecupererDoubleTypeEnModeRandom();
            List<PKMType> premierTypeSimple = RecupererSimplePKMType();
            List<PKMType> secondTypeSimple = RecupererSimplePKMType();
            List<PKMType> troisiemeTypeSimple = RecupererSimplePKMType();

            typesPKMSelectionnes.Add(1, premierTypeSimple);
            typesPKMSelectionnes.Add(2, secondTypeSimple);
            typesPKMSelectionnes.Add(3, premierDoubleType);
            typesPKMSelectionnes.Add(4, troisiemeTypeSimple);
            typesPKMSelectionnes.Add(5, secondDoubleType);
            typesPKMSelectionnes.Add(6, troisiemeDoubleType);
            return typesPKMSelectionnes;
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