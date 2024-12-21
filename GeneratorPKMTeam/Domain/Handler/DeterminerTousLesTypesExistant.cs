using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class DeterminerTousLesTypesExistant : IDeterminerTousLesTypesExistant
    {
        private IPKMPersistence _pKMPersistence;
        private IGererStarterPKM _gererStarterPKM;
        private List<PKMType> _tousPKMTypes;
        private List<PKMType> _PKMTypesStarter;


        public DeterminerTousLesTypesExistant(IPKMPersistence pKMPersistence, IGererStarterPKM gererStarterPKM)
        {
            _pKMPersistence = pKMPersistence;
            _gererStarterPKM = gererStarterPKM;
        }

        public Dictionary<string, List<PKMType>> Calculer(int generation, List<PKMType> PKMTypes)
        {
            _PKMTypesStarter = new List<PKMType>();
            var resultatCalcul = new Dictionary<string, List<PKMType>>();
            _tousPKMTypes = PKMTypes;
            var pkms = _pKMPersistence.GetPKMs();
            RecupererDonneesStarterPKM();
            var pkmsBonneGeneration = pkms.TousPKMs.Where(o => o.Generation <= generation);
            var tousSimpleType = TrouverPKMTypeSimpleCompatible(pkmsBonneGeneration);
            var tousDoubleTypes = TrouverPKMTypeDoubleCompatible(pkmsBonneGeneration);
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

        private void RecupererDonneesStarterPKM()
        {
            var _starterPKM = _gererStarterPKM.RecupererStarter();
            foreach (var typePKM in _tousPKMTypes)
            {
                foreach (var starterTypePKM in _starterPKM.PKMTypes)
                {
                    if (typePKM.Nom == starterTypePKM)
                        _PKMTypesStarter.Add(typePKM);
                }
            }
        }

        private Dictionary<string, List<PKMType>> TrouverPKMTypeSimpleCompatible(IEnumerable<PKM> pkms)
        {
            var resultatCalcul = new Dictionary<string, List<PKMType>>();
            foreach (var PKMType in _tousPKMTypes)
            {
                bool pkmACeType = VerifierTypeSimplePKM(pkms, PKMType);
                bool pasDoubleTypeStarter = VerifierPasDoubleTypeStarter(PKMType);
                if (pkmACeType && pasDoubleTypeStarter)
                {
                    resultatCalcul.Add(PKMType.Nom, new List<PKMType>() { PKMType });
                }
            }
            return resultatCalcul;
        }

        private bool VerifierTypeSimplePKM(IEnumerable<PKM> pkms, PKMType pKMType)
        {
            bool aCeTypeEnPremierType = pkms.Any(o => o.PKMTypes[0] == pKMType.Nom && o.PKMTypes.Count == 1);
            return aCeTypeEnPremierType;
        }

        private bool VerifierPasDoubleTypeStarter(PKMType PKMType)
        {
            return _PKMTypesStarter.Count == 1 || !_PKMTypesStarter.Contains(PKMType);
        }

        private Dictionary<string, List<PKMType>> TrouverPKMTypeDoubleCompatible(IEnumerable<PKM> pkms)
        {
            var resultatCalcul = new Dictionary<string, List<PKMType>>();
            foreach (var premierPKMType in _tousPKMTypes)
            {
                foreach (var secondPKMType in _tousPKMTypes)
                {
                    if (premierPKMType.Nom == secondPKMType.Nom)
                        continue;
                    bool pkmACeType = VerifierTypeDoublePKM(pkms, premierPKMType, secondPKMType);
                    bool pasUnPKMTypeStarter = NeContientPasUnTypeStarter(new List<PKMType>() { premierPKMType, secondPKMType });
                    if (pkmACeType && pasUnPKMTypeStarter)
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

        private bool NeContientPasUnTypeStarter(List<PKMType> typesAComparer)
        {
            bool pasEgalPremierTypeStarterPremierTypesAComparer = _PKMTypesStarter[0] != typesAComparer[0];
            bool pasEgalPremierTypeStarterDeuxiemeTypesAComparer = _PKMTypesStarter[0] != typesAComparer[1];
            if (_PKMTypesStarter.Count == 1)
                return pasEgalPremierTypeStarterPremierTypesAComparer && pasEgalPremierTypeStarterDeuxiemeTypesAComparer;
            else if (typesAComparer[0] == _PKMTypesStarter[0] && typesAComparer[1] == _PKMTypesStarter[1])
            {
                return true;
            }
            else if (typesAComparer[0] == _PKMTypesStarter[1] && typesAComparer[1] == _PKMTypesStarter[0])
            {
                return true;
            }
            else
            {
                bool pasEgalDeuxiemeTypeStarterPremierTypesAComparer = _PKMTypesStarter[1] != typesAComparer[0];
                bool pasEgalDeuxiemeTypeStarterDeuxiemeTypesAComparer = _PKMTypesStarter[1] != typesAComparer[1];
                return pasEgalPremierTypeStarterPremierTypesAComparer && pasEgalPremierTypeStarterDeuxiemeTypesAComparer
                && pasEgalDeuxiemeTypeStarterPremierTypesAComparer && pasEgalDeuxiemeTypeStarterDeuxiemeTypesAComparer;
            }
        }

    }
}