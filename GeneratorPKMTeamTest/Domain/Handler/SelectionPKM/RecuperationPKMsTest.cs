using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeamTest.Utils.Helper;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;
using GeneratorPKMTeam.Infrastructure.Services;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;

namespace GeneratorPKMTeamTest.Domain.Handler.SelectionPKM
{

    //TODO factoriser les tests surtout la partie init
    public class RecuperationPKMsTest
    {
        private Dictionary<int, List<PKMType>> PKMTypesOrdonnees;

        // private PKM? _eauPKM { get; set; }
        // private PKM? _insectePKM { get; set; }
        // private PKM? _plantePoisonPKM { get; set; }
        // private PKM? _psyPKM { get; set; }
        // private PKM? _feuVolPKM { get; set; }
        // private PKM? _acierSolPkm { get; set; }
        private PKM? _starterPKM { get; set; }
        private Dictionary<int, PKMStatsLabel> _mockStats { get; set; }

        public RecuperationPKMsTest()
        {
            PKMTypesOrdonnees = new Dictionary<int, List<PKMType>>();
            InitPKMTypesOrdonnees();
            _mockStats = new Dictionary<int, PKMStatsLabel>(){
                {6,PKMStatsLabel.SPAttaque},
                {5,PKMStatsLabel.Attaque},
                {4,PKMStatsLabel.PV},
                {3,PKMStatsLabel.Vitesse},
                {2,PKMStatsLabel.SPDefense},
                {1,PKMStatsLabel.Defense}
            };

        }

        private void InitPKMTypesOrdonnees()
        {
            PKMTypesOrdonnees.Add(1, new List<PKMType> { new PKMType() { Nom = "Eau" } });
            PKMTypesOrdonnees.Add(2, new List<PKMType> { new PKMType() { Nom = "Insecte" } });
            PKMTypesOrdonnees.Add(3, new List<PKMType> { new PKMType() { Nom = "Plante" }, new PKMType() { Nom = "Poison" } });
            PKMTypesOrdonnees.Add(4, new List<PKMType> { new PKMType() { Nom = "Psy" } });
            PKMTypesOrdonnees.Add(5, new List<PKMType> { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Vol" } });
            PKMTypesOrdonnees.Add(6, new List<PKMType> { new PKMType() { Nom = "Acier" }, new PKMType() { Nom = "Sol" } });
        }

        [Theory]
        [InlineData("Carapuce", new string[] { "Eau" })]
        [InlineData("Bulbizarre", new string[] { "Plante", "Poison" })]
        [InlineData("Dracaufeu", new string[] { "Feu", "Vol" })]
        public void OnRecupereSixPokemonsAvecLesBonsTypesOuIlYaUnBonOuPlusieursChoixAChaqueFoisPourInferieurOuEgalSecondeGeneration(string starterName, string[] pkmTypes)
        {
            int generation = 2;
            _starterPKM = new PKM() { Nom = starterName, PKMTypes = pkmTypes.ToList() };

            var pkmsStore = DatasHelperTest.RetournersPKMsOuChaqueTypeEstPresentUneOuPlusieursFois(2);

            var pkmPersistence = MockPKMPersistence(pkmsStore.ToList());

            var PKMStatsPersistence = Substitute.For<IPKMStatsPersistence>();
            PKMStatsPersistence.AvoirConfigurationStats().Returns(new ConfigurationStats() { StatsParImportance = _mockStats });

            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(_starterPKM);

            var determinerMeilleurPKMParStats = new DeterminerMeilleurPKMParStats(PKMStatsPersistence);

            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, determinerMeilleurPKMParStats, generation);

            var pkms = recuperationPKMs.Recuperer(PKMTypesOrdonnees);

            Assert.Equal(6, pkms.Count);

            Assert.True(RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Eau", null));
            Assert.True(RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Insecte", null));
            Assert.True(RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Plante", "Poison"));
            Assert.True(RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Psy", null));
            Assert.True(RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Feu", "Vol"));
            Assert.True(RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Acier", "Sol"));

            foreach (var pkm in pkms)
            {
                Assert.True(generation >= pkm.Generation);
            }
        }


        private bool RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(List<PKM> pkms, string premierType, string? secondType)
        {
            bool isStarter = premierType == _starterPKM.PKMTypes[0] && (secondType == null || secondType == _starterPKM.PKMTypes[1]);
            if (isStarter)
                return true;
            else if (secondType == null)
                return pkms.Any(o => o.PKMTypes[0] == premierType);
            else
                return pkms.Any(o => o.PKMTypes.Count > 1 && o.PKMTypes[0] == premierType &&
                    o.PKMTypes[1] == secondType);
        }

        private bool CestUnStarter(List<string> PKMTypes)
        {
            if (PKMTypes.Count != _starterPKM.PKMTypes.Count)
            {
                return false;
            }
            else
            {
                if (_starterPKM.PKMTypes[0] != PKMTypes[0])
                    return false;
                else if (_starterPKM.PKMTypes.Count == 2 && _starterPKM.PKMTypes[1] != PKMTypes[1])
                    return false;
                else
                    return true;
            }
        }

        [Fact]
        public void ProvoqueUnePKMAvecTypeInexistantExceptionSiPKMAUnTypeNonTrouve()
        {
            int generation = 2;
            var PKMTypesOrdonneesErreur = new Dictionary<int, List<PKMType>>(){
                {1, new List<PKMType>(){new PKMType(){Nom = "Dinosaure"}}}
            };
            var pkmsStore = DatasHelperTest.RetournersPKMsOuChaqueTypeEstPresentUneOuPlusieursFois(2);
            var pkmPersistence = MockPKMPersistence(pkmsStore.ToList());
            var PKMStatsPersistence = Substitute.For<IPKMStatsPersistence>();
            PKMStatsPersistence.AvoirConfigurationStats().Returns(new ConfigurationStats() { StatsParImportance = _mockStats });
            var determinerMeilleurPKMParStats = Substitute.For<IDeterminerMeilleurPKMParStats>();
            determinerMeilleurPKMParStats.Calculer(Arg.Any<List<PKM>>()).Returns(RandomPkm(pkmsStore.ToList()));
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, determinerMeilleurPKMParStats, generation);

            var result = Assert.Throws<PKMAvecTypeInexistantException>(() => recuperationPKMs.Recuperer(PKMTypesOrdonneesErreur));

            Assert.Equal("Aucun PKM trouvé avec le type Dinosaure", result.CustomMessage);
            Assert.Equal(TypeErreur.PKMAvecPKMTypeInexistant, result.TypeErreur);
        }

        [Fact]
        public void ProvoqueUnePKMAvecTypeInexistantExceptionSiPKMAUnDoubleTypeNonTrouve()
        {
            int generation = 2;
            var pkmsStore = DatasHelperTest.RetournersPKMsOuChaqueTypeEstPresentUneOuPlusieursFois(2);
            var PKMTypesOrdonneesErreur = new Dictionary<int, List<PKMType>>(){
                {1, new List<PKMType>(){new PKMType(){Nom = "Eau"}, new PKMType(){Nom="Feu"}}}
            };
            var pkmPersistence = MockPKMPersistence(pkmsStore.ToList());
            var PKMStatsPersistence = Substitute.For<IPKMStatsPersistence>();
            PKMStatsPersistence.AvoirConfigurationStats().Returns(new ConfigurationStats() { StatsParImportance = _mockStats });
            var determinerMeilleurPKMParStats = new DeterminerMeilleurPKMParStats(PKMStatsPersistence);
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, determinerMeilleurPKMParStats, generation);

            var result = Assert.Throws<PKMAvecTypeInexistantException>(() => recuperationPKMs.Recuperer(PKMTypesOrdonneesErreur));

            Assert.Equal("Aucun PKM trouvé avec le type Eau-Feu", result.CustomMessage);
            Assert.Equal(TypeErreur.PKMAvecPKMTypeInexistant, result.TypeErreur);
        }

        private List<PKM> RandomPkm(List<PKM> pKMs)
        {
            var nombreResultat = new Random();
            int fois = nombreResultat.Next(22);
            var pkms = new List<PKM>(pKMs);
            var mocksPKM = new List<PKM>();
            for (int i = 0; i < fois; i++)
            {
                var random = new Random();
                int index = random.Next(pkms.Count);
                var pkm = pKMs[index];
                mocksPKM.Add(pkm);
                pkms.Remove(pkm);
            }
            return mocksPKM;
        }


        private IPKMPersistence MockPKMPersistence(List<PKM> pKMs)
        {
            var pkmPersistence = Substitute.For<IPKMPersistence>();
            pkmPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = pKMs });
            return pkmPersistence;
        }
    }
}