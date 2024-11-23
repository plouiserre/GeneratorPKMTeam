using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeamTest.Utils.Helper;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;
using GeneratorPKMTeam.Infrastructure.Services;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class RecuperationPKMsTest
    {
        private Dictionary<int, List<PKMType>> PKMTypesOrdonnees;

        private PKM _eauPKM { get; set; }
        private PKM _insectePKM { get; set; }
        private PKM _plantePoisonPKM { get; set; }
        private PKM _psyPKM { get; set; }
        private PKM _feuVolPKM { get; set; }
        private PKM _acierSolPkm { get; set; }

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

        [Fact]
        public void OnRecupereSixPokemonsAvecLesBonsTypesOuIlYaUnBonOuPlusieursChoixAChaqueFoisPourInferieurOuEgalSecondeGeneration()
        {
            int generation = 2;

            var pkmsStore = DatasHelperTest.RetournersPKMsOuChaqueTypeEstPresentUneOuPlusieursFois(2);
            RecupererPKMs(pkmsStore);

            var pkmPersistence = MockPKMPersistence(pkmsStore.ToList());

            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, generation);

            var pkms = recuperationPKMs.Recuperer(PKMTypesOrdonnees);

            Assert.Equal(6, pkms.Count);

            Assert.Equal(_eauPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Eau", null, 2));
            Assert.Equal(_insectePKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Insecte", null, 2));
            Assert.Equal(_plantePoisonPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Plante", "Poison", 2));
            Assert.Equal(_psyPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Psy", null, 2));
            Assert.Equal(_feuVolPKM.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Feu", "Vol", 2));
            Assert.Equal(_acierSolPkm.Nom, RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(pkms, "Acier", "Sol", 2));

            foreach (var pkm in pkms)
            {
                Assert.True(generation >= pkm.Generation);
            }
        }


        private string RetournerPKMNomAPartirListesPKMsAvecPlusieursChoix(List<PKM> pkms, string premierType, string secondType, int generation)
        {
            if (secondType == null)
                return pkms.Where(o => o.PKMTypes[0] == premierType).OrderBy(o => o.Nom).First().Nom;
            else
                return pkms.Where(o => o.PKMTypes.Count > 1 && o.PKMTypes[0] == premierType &&
                    o.PKMTypes[1] == secondType).OrderBy(o => o.Nom).First().Nom;
        }

        private void RecupererPKMs(IEnumerable<PKM> pkmsMock)
        {
            _eauPKM = pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 1 && o.PKMTypes[0] == "Eau");
            _insectePKM = pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 1 && o.PKMTypes[0] == "Insecte");
            _plantePoisonPKM = pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 2 && o.PKMTypes[0] == "Plante" && o.PKMTypes[1] == "Poison");
            _psyPKM = pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 1 && o.PKMTypes[0] == "Psy");
            _feuVolPKM = pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 2 && o.PKMTypes[0] == "Feu" && o.PKMTypes[1] == "Vol");
            _acierSolPkm = pkmsMock.FirstOrDefault(o => o.PKMTypes.Count == 2 && o.PKMTypes[0] == "Acier" && o.PKMTypes[1] == "Sol");
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
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, generation);

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
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, generation);

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