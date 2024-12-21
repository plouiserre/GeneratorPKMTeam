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
        private int _generation;
        private Dictionary<string, List<PKMType>> _tousLesTypesPossibles;
        private bool _starterDoubleType;
        private Dictionary<int, List<PKMType>> _typesPKMSelectionnes;
        private int _nbrePKMMax;

        public DefinirOrdrePKMType(IDeterminerTousLesTypesExistant determinerTousLesTypesExistant, IGererStarterPKM gererStarterPKM, int generation)
        {
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
            AffectationPKMType(starterType);
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

        //TODO faire plus de sous méthode quand ca marchera pour rendre le code plus lisible
        private void AffectationPKMType(List<PKMType> starterType)
        {
            int index = 0;
            var doubleTypes = RecupererDoublesTypesAvecTypesDifferents(starterType);
            foreach (var doubleType in doubleTypes)
            {
                if (index > 5)
                    break;
                _typesPKMSelectionnes.Add(index, doubleType.Value);
                index += 1;
            }

            var pkmTypesRestantNecessaires = _nbrePKMMax - index;
            if (pkmTypesRestantNecessaires > 0)
            {
                var simpleTypes = RecupererSimpleTypeNecessaire(pkmTypesRestantNecessaires);
                foreach (var typesPKM in simpleTypes)
                {
                    _typesPKMSelectionnes.Add(index, typesPKM);
                    RetirerToutesLesOccurencesDuType(typesPKM[0]);
                }
            }
        }

        //TODO diviser cette méthode en sous méthodes
        private Dictionary<string, List<PKMType>> RecupererDoublesTypesAvecTypesDifferents(List<PKMType> starterType)
        {
            var doublesTypesDifferents = new Dictionary<string, List<PKMType>>();
            var doublesTypes = _tousLesTypesPossibles.Where(o => o.Value.Count == 2);
            var pkmTypesSauvegardes = new List<PKMType>();
            int index = 0;
            if (starterType.Count == 2)
            {
                string key = starterType[0].Nom + "-" + starterType[1].Nom;
                doublesTypesDifferents.Add(key, starterType);
                pkmTypesSauvegardes.AddRange(starterType);
            }
            foreach (var types in doublesTypes)
            {

                if (index == 0 && pkmTypesSauvegardes.Count == 0)
                {
                    doublesTypesDifferents.Add(types.Key, types.Value);
                    pkmTypesSauvegardes.AddRange(types.Value);
                    RetirerToutesLesOccurencesDuType(types.Value[0]);
                    RetirerToutesLesOccurencesDuType(types.Value[1]);
                }
                else
                {
                    bool doublesTypesVierge = true;
                    foreach (var pkmType in types.Value)
                    {
                        bool contientPKMType = pkmTypesSauvegardes.Any(o => o.Nom == pkmType.Nom);
                        if (contientPKMType)
                        {
                            doublesTypesVierge = false;
                            break;
                        }
                    }
                    if (doublesTypesVierge)
                    {
                        doublesTypesDifferents.Add(types.Key, types.Value);
                        pkmTypesSauvegardes.AddRange(types.Value);
                        RetirerToutesLesOccurencesDuType(types.Value[0]);
                        RetirerToutesLesOccurencesDuType(types.Value[1]);

                    }
                }

                index += 1;
            }
            return doublesTypesDifferents;
        }

        private List<List<PKMType>> RecupererSimpleTypeNecessaire(int besoin)
        {
            var resultat = new List<List<PKMType>>();
            var simpleTypesSelectionnalble = _tousLesTypesPossibles.Select(o => o.Value).Where(o => o.Count == 1).ToList();
            for (int i = 0; i < besoin; i++)
            {
                var random = new Random();
                int index = random.Next(simpleTypesSelectionnalble.Count);
                var simpleType = simpleTypesSelectionnalble[index];
                resultat.Add(simpleType);
                simpleTypesSelectionnalble.RemoveAt(index);
            }
            return resultat;
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