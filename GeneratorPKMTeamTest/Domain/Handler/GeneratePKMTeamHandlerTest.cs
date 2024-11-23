using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Services;
using GeneratorPKMTeamTest.Utils.Helper;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
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

            var choisirMeilleuresCombinaisonsTypes = InitChoisirMeilleuresCombinaisonsTypes();
            var assemblerEquipePKM = InitAssemblerEquipePKM();
            var genererMeilleuresEquipesPKM = new GeneratePKMTeamHandler(choisirMeilleuresCombinaisonsTypes, assemblerEquipePKM);

            var resultats = genererMeilleuresEquipesPKM.Generer();

            foreach (var resultat in resultats)
            {
                Assert.Equal(6, resultat.Value.Count);
            }
        }

        private ChoisirMeilleuresCombinaisonsTypes InitChoisirMeilleuresCombinaisonsTypes()
        {
            var tousTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);
            var PKMDonnees = new PKMDonnees() { PKMTypes = tousTypes };
            var PKMTypePersistence = Substitute.For<IPKMTypePersistence>();
            PKMTypePersistence.GetPKMDonnees().Returns(PKMDonnees);
            var chargementPKMTypes = new ChargerPKMTypes(PKMTypePersistence);
            var choisirPKMTypes = new ChoisirPKMTypes();
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
            var definirOrdrePKMTypes = new DefinirOrdrePKMType();
            var pkmPersistence = Substitute.For<IPKMPersistence>();
            pkmPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var recuperationPKMs = new RecuperationPKMs(pkmPersistence, _generation);
            var assemblerEquipePKM = new AssemblerEquipePKM(definirOrdrePKMTypes, recuperationPKMs);
            return assemblerEquipePKM;
        }
    }
}