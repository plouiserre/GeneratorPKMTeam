using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
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

        [Fact]
        public void RecupererPlusieursListesPKMsOptimiser()
        {
            var trouverTypePKMEquipePKM = InitTrouverTypePKMEquipePKM();
            var genererMeilleuresEquipesPKM = new GeneratePKMTeamHandler(trouverTypePKMEquipePKM);

            var resultats = genererMeilleuresEquipesPKM.Generer();

            foreach (var resultat in resultats)
            {
                Assert.Equal(6, resultat.Value.Count);
            }
        }

        private TrouverTypePKMEquipePKM InitTrouverTypePKMEquipePKM()
        {
            var choisirMeilleuresCombinaisonsTypes = InitChoisirMeilleuresCombinaisonsTypes();
            var assemblerEquipePKM = InitAssemblerEquipePKM();
            var trouverTypePKMEquipePKM = new TrouverTypePKMEquipePKM(choisirMeilleuresCombinaisonsTypes, assemblerEquipePKM);
            return trouverTypePKMEquipePKM;
        }

        private ChoisirMeilleuresCombinaisonsTypes InitChoisirMeilleuresCombinaisonsTypes()
        {
            var tousTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);
            var PKMDonnees = new PKMDonnees() { PKMTypes = tousTypes };
            var PKMTypePersistence = Substitute.For<IPKMTypePersistence>();
            PKMTypePersistence.GetPKMDonnees().Returns(PKMDonnees);
            var chargementPKMTypes = new ChargerPKMTypes(PKMTypePersistence);
            var tousPKMs = DatasHelperTest.RetournersTousPKM();
            var pkmPersistence = Substitute.For<IPKMPersistence>();
            pkmPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var starterPKM = new GererStarterPKM(pkmPersistence);
            starterPKM.ChoisirStarter("Bulbizarre");
            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var gererResultatTiragePKMTypes = new GererResultatTiragePKMTypes();
            var meilleursCombinaisonsTypes = new ChoisirMeilleuresCombinaisonsTypes(chargementPKMTypes, choisirPKMTypes,
                            resultatCombatPKMTypes, gererResultatTiragePKMTypes);
            return meilleursCombinaisonsTypes;
        }

        private AssemblerEquipePKM InitAssemblerEquipePKM()
        {
            var tousPKMs = DatasHelperTest.RetournersTousPKM();
            var pkmPersistence = Substitute.For<IPKMPersistence>();
            pkmPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var starterPKM = new GererStarterPKM(pkmPersistence);
            starterPKM.ChoisirStarter("Bulbizarre");
            var determinerTousLesTypesExistant = new DeterminerTousLesTypesExistant(pkmPersistence, starterPKM);
            var gererRecuperationPKMType = new GererRecuperationPKMType();
            var definirOrdrePKMTypes = new DefinirOrdrePKMType(determinerTousLesTypesExistant, starterPKM, gererRecuperationPKMType, _generation);
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, starterPKM, _generation);
            var assemblerEquipePKM = new AssemblerEquipePKM(definirOrdrePKMTypes, recuperationPKMs);
            return assemblerEquipePKM;
        }
    }
}