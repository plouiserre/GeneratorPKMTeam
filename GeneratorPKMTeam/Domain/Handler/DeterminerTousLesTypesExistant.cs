using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class DeterminerTousLesTypesExistant : IDeterminerTousLesTypesExistant
    {
        private IPKMPersistence _pKMPersistence;

        public DeterminerTousLesTypesExistant(IPKMPersistence pKMPersistence)
        {
            _pKMPersistence = pKMPersistence;
        }

        public Dictionary<string, List<PKMType>> Calculer(int generation, List<PKMType> PKMTypes)
        {
            var resultatCalcul = new Dictionary<string, List<PKMType>>();
            var pkms = _pKMPersistence.GetPKMs();
            var pkmsBonneGeneration = pkms.TousPKMs.Where(o => o.Generation <= generation);
            var tousSimpleType = TrouverPKMTypeSimpleCompatible(PKMTypes, pkmsBonneGeneration);
            var tousDoubleTypes = TrouverPKMTypeDoubleCompatible(PKMTypes, pkmsBonneGeneration);
            foreach (var data in tousSimpleType)
            {
                resultatCalcul.Add(data.Key, data.Value);
            }
            foreach (var data in tousDoubleTypes)
            {
                resultatCalcul.Add(data.Key, data.Value);
            }
            return resultatCalcul;
        }

        private Dictionary<string, List<PKMType>> TrouverPKMTypeSimpleCompatible(List<PKMType> PKMTypes, IEnumerable<PKM> pkms)
        {
            var resultatCalcul = new Dictionary<string, List<PKMType>>();
            foreach (var PKMType in PKMTypes)
            {
                bool pkmACeType = VerifierTypeSimplePKM(pkms, PKMType);
                if (pkmACeType)
                {
                    resultatCalcul.Add(PKMType.Nom, new List<PKMType>() { PKMType });
                }
            }
            return resultatCalcul;
        }

        private bool VerifierTypeSimplePKM(IEnumerable<PKM> pkms, PKMType pKMType)
        {
            bool aCeTypeEnPremierType = pkms.Any(o => o.PKMTypes[0] == pKMType.Nom);
            return aCeTypeEnPremierType;
        }

        private Dictionary<string, List<PKMType>> TrouverPKMTypeDoubleCompatible(List<PKMType> PKMTypes, IEnumerable<PKM> pkms)
        {
            var resultatCalcul = new Dictionary<string, List<PKMType>>();
            foreach (var premierPKMType in PKMTypes)
            {
                foreach (var secondPKMType in PKMTypes)
                {
                    if (premierPKMType.Nom == secondPKMType.Nom)
                        continue;
                    bool pkmACeType = VerifierTypeDoublePKM(pkms, premierPKMType, secondPKMType);
                    if (pkmACeType)
                    {
                        string cleDoubleType = premierPKMType.Nom + "-" + secondPKMType.Nom;
                        resultatCalcul.Add(cleDoubleType, new List<PKMType>() { premierPKMType, secondPKMType });
                    }
                }
            }
            return resultatCalcul;
        }

        private bool VerifierTypeDoublePKM(IEnumerable<PKM> pkms, PKMType premierPKMType, PKMType secondPKMType)
        {
            bool aCesDeuxTypes = pkms.Any(o => o.PKMTypes.Count == 2 && o.PKMTypes[0] == premierPKMType.Nom
                                        && o.PKMTypes[1] == secondPKMType.Nom);
            return aCesDeuxTypes;
        }

    }
}