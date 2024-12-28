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
    public class RecuperationPKMsTest
    {
        private Dictionary<int, List<PKMType>> PKMTypesOrdonnees;

        private PKM? _eauPKM { get; set; }
        private PKM? _insectePKM { get; set; }
        private PKM? _plantePoisonPKM { get; set; }
        private PKM? _psyPKM { get; set; }
        private PKM? _feuVolPKM { get; set; }
        private PKM? _acierSolPkm { get; set; }
        private PKM? _starterPKM { get; set; }

        public RecuperationPKMsTest()
        {
            PKMTypesOrdonnees = new Dictionary<int, List<PKMType>>();
            InitPKMTypesOrdonnees();
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
            RecupererPKMs(pkmsStore);

            var pkmPersistence = MockPKMPersistence(pkmsStore.ToList());

            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(_starterPKM);
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, generation);

            var pkms = recuperationPKMs.Recuperer(PKMTypesOrdonnees);

            Assert.Equal(6, pkms.Count);

            Assert.Equal(_eauPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Eau", null));
            Assert.Equal(_insectePKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Insecte", null));
            Assert.Equal(_plantePoisonPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Plante", "Poison"));
            Assert.Equal(_psyPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Psy", null));
            Assert.Equal(_feuVolPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Feu", "Vol"));
            Assert.Equal(_acierSolPkm.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Acier", "Sol"));

            foreach (var pkm in pkms)
            {
                Assert.True(generation >= pkm.Generation);
            }
        }


        private string RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(List<PKM> pkms, string premierType, string? secondType)
        {
            bool isStarter = premierType == _starterPKM.PKMTypes[0] && (secondType == null || secondType == _starterPKM.PKMTypes[1]);
            if (isStarter)
                return _starterPKM.Nom;
            else if (secondType == null)
                return pkms.Where(o => o.PKMTypes[0] == premierType).OrderBy(o => o.Nom).First().Nom;
            else
                return pkms.Where(o => o.PKMTypes.Count > 1 && o.PKMTypes[0] == premierType &&
                    o.PKMTypes[1] == secondType).OrderBy(o => o.Nom).First().Nom;
        }

        private void RecupererPKMs(IEnumerable<PKM> pkmsMock)
        {
            _eauPKM = CestUnStarter(new List<string> { "Eau" }) ? _starterPKM : pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 1 && o.PKMTypes[0] == "Eau");
            _insectePKM = CestUnStarter(new List<string> { "Insecte" }) ? _starterPKM : pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 1 && o.PKMTypes[0] == "Insecte");
            _plantePoisonPKM = CestUnStarter(new List<string> { "Plante", "Poison" }) ? _starterPKM : pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 2 && o.PKMTypes[0] == "Plante" && o.PKMTypes[1] == "Poison");
            _psyPKM = CestUnStarter(new List<string> { "Psy" }) ? _starterPKM : pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 1 && o.PKMTypes[0] == "Psy");
            _feuVolPKM = CestUnStarter(new List<string> { "Feu", "Vol" }) ? _starterPKM : pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 2 && o.PKMTypes[0] == "Feu" && o.PKMTypes[1] == "Vol");
            _acierSolPkm = CestUnStarter(new List<string> { "Acier", "Sol" }) ? _starterPKM : pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 2 && o.PKMTypes[0] == "Acier" && o.PKMTypes[1] == "Sol");
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
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, generation);

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
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, generation);

            var result = Assert.Throws<PKMAvecTypeInexistantException>(() => recuperationPKMs.Recuperer(PKMTypesOrdonneesErreur));

            Assert.Equal("Aucun PKM trouvé avec le type Eau-Feu", result.CustomMessage);
            Assert.Equal(TypeErreur.PKMAvecPKMTypeInexistant, result.TypeErreur);
        }


        private IPKMPersistence MockPKMPersistence(List<PKM> pKMs)
        {
            var pkmPersistence = Substitute.For<IPKMPersistence>();
            pkmPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = pKMs });
            return pkmPersistence;
        }
    }
}