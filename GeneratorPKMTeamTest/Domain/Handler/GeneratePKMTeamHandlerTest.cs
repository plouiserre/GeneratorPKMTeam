using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Helper;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler.OrdrePKMTypeTest
{
    public class GeneratePKMTeamHandlerTest
    {
        private int _generation;

        public GeneratePKMTeamHandlerTest()
        {
            _generation = 2;
        }

        [Theory]
        [InlineData("Bulbizarre")]
        [InlineData("Arcanin")]

        public void RecupererPlusieursListesPKMsOptimiser(string nomStarter)
        {
            var trouverTypePKMEquipePKM = InitTrouverTypePKMEquipePKM(nomStarter);
            var genererMeilleuresEquipesPKM = new GeneratePKMTeamHandler(trouverTypePKMEquipePKM);

            var resultats = genererMeilleuresEquipesPKM.Generer();

            foreach (var resultat in resultats)
            {
                Assert.Equal(6, resultat.Value.Count);
            }
        }

        private TrouverTypePKMEquipePKM InitTrouverTypePKMEquipePKM(string nomStarter)
        {
            var choisirMeilleuresCombinaisonsTypes = InitChoisirMeilleuresCombinaisonsTypes(nomStarter);
            var assemblerEquipePKM = InitAssemblerEquipePKM(nomStarter);
            var trouverTypePKMEquipePKM = new TrouverTypePKMEquipePKM(choisirMeilleuresCombinaisonsTypes, assemblerEquipePKM);
            return trouverTypePKMEquipePKM;
        }

        private NoterEquipePKM InitChoisirMeilleuresCombinaisonsTypes(string nomStarter)
        {
            var tousTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);
            var PKMDonnees = new PKMDonnees() { PKMTypes = tousTypes };
            var PKMTypePersistence = Substitute.For<IPKMTypePersistence>();
            PKMTypePersistence.GetPKMDonnees().Returns(PKMDonnees);
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var meilleursCombinaisonsTypes = new NoterEquipePKM(resultatCombatPKMTypes, PKMTypePersistence);
            return meilleursCombinaisonsTypes;
        }

        private AssemblerEquipePKM InitAssemblerEquipePKM(string nomStarter)
        {
            var chargementPKMTypes = ChargementPKMTypes();
            var choisirPKMTypes = ChoisirPKMTypes(nomStarter);
            var mockStats = new Dictionary<int, PKMStatsLabel>(){
                {6,PKMStatsLabel.SPAttaque},
                {5,PKMStatsLabel.Attaque},
                {4,PKMStatsLabel.PV},
                {3,PKMStatsLabel.Vitesse},
                {2,PKMStatsLabel.SPDefense},
                {1,PKMStatsLabel.Defense}
            };
            var tousPKMs = DatasHelperTest.RetournersTousPKM();
            var pkmPersistence = Substitute.For<IPKMPersistence>();
            pkmPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var PKMStatsPersistence = Substitute.For<IPKMStatsPersistence>();
            PKMStatsPersistence.AvoirConfigurationStats().Returns(new ConfigurationStats() { StatsParImportance = mockStats });
            var starterPKM = new GererStarterPKM(pkmPersistence);
            starterPKM.ChoisirStarter(nomStarter);
            var determinerTousLesTypesExistant = new DeterminerTousLesTypesExistant(pkmPersistence, starterPKM);
            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();
            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
            var gererRecuperationPKMType = new GererRecuperationPKMType(recupererPKMTypeDouble, recupererPKMTypeSimple);
            var definirOrdrePKMTypes = new DefinirOrdrePKMType(determinerTousLesTypesExistant, starterPKM, gererRecuperationPKMType, _generation);
            var determinerMeilleurPKMParStats = new DeterminerMeilleurPKMParStats(PKMStatsPersistence);
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, determinerMeilleurPKMParStats, _generation);
            var assemblerEquipePKM = new AssemblerEquipePKM(chargementPKMTypes, choisirPKMTypes, definirOrdrePKMTypes, recuperationPKMs);
            return assemblerEquipePKM;
        }

        private ChargerPKMTypes ChargementPKMTypes()
        {
            var tousTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);
            var PKMDonnees = new PKMDonnees() { PKMTypes = tousTypes };
            var PKMTypePersistence = Substitute.For<IPKMTypePersistence>();
            PKMTypePersistence.GetPKMDonnees().Returns(PKMDonnees);
            var chargementPKMTypes = new ChargerPKMTypes(PKMTypePersistence);
            return chargementPKMTypes;
        }

        private ChoisirPKMTypes ChoisirPKMTypes(string nomStarter)
        {
            var tousPKMs = DatasHelperTest.RetournersTousPKM();
            var pkmPersistence = Substitute.For<IPKMPersistence>();
            pkmPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var starterPKM = new GererStarterPKM(pkmPersistence);
            starterPKM.ChoisirStarter(nomStarter);
            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);
            return choisirPKMTypes;
        }
    }
}