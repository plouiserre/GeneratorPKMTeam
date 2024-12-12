using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class ChoisirMeilleuresCombinaisonsTypesTest
    {
        private PKMDonnees _fakeDatas;

        public ChoisirMeilleuresCombinaisonsTypesTest()
        {
            _fakeDatas = PKMDonneesPersonas.GetPersonas();
        }

        [Fact]
        public void ChoixMeilleuresCombinaisonsTypesEstOK()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            var PMKPersistence = Substitute.For<IPKMTypePersistence>();
            PMKPersistence.GetPKMDonnees().Returns(_fakeDatas);

            var loadPKMTypes = new ChargerPKMTypes(PMKPersistence);
            var selectPKMTypes = new ChoisirPKMTypes(starterPKM);
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var gererResultatTiragePKMTypes = new GererResultatTiragePKMTypes();
            var choisir = new ChoisirMeilleuresCombinaisonsTypes(loadPKMTypes, selectPKMTypes, resultatCombatPKMTypes,
                            gererResultatTiragePKMTypes);

            var tirageParfaitTrouve = choisir.GenererTirageParfait();


            Assert.True(tirageParfaitTrouve.NoteTirage >= 90);

        }

        [Fact]
        public void ChoixMeilleuresCombinaisonsTypesSeTermineJamais()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });
            var PMKPersistence = Substitute.For<IPKMTypePersistence>();
            PMKPersistence.GetPKMDonnees().Returns(_fakeDatas);
            var gererResultatTiragePKMTypes = Substitute.For<IGererResultatTiragePKMTypes>();
            gererResultatTiragePKMTypes.GarderTirage(Arg.Any<TiragePKMTypes>()).Returns(false);

            var loadPKMTypes = new ChargerPKMTypes(PMKPersistence);
            var selectPKMTypes = new ChoisirPKMTypes(starterPKM);
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var handler = new ChoisirMeilleuresCombinaisonsTypes(loadPKMTypes, selectPKMTypes, resultatCombatPKMTypes,
                            gererResultatTiragePKMTypes);

            var result = Assert.Throws<CombinaisonParfaitesIntrouvablesException>(() => handler.GenererTirageParfait());


            Assert.Equal("Aucun tirage de PKM parfait n'a été trouvé", result.CustomMessage);
            Assert.Equal(TypeErreur.NoCombinaisonsParfaitesTrouvees, result.TypeErreur);
        }
    }
}