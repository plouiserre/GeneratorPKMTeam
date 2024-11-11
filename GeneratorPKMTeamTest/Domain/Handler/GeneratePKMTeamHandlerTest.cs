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
    public class GeneratePKMTeamHandlerTest
    {
        private PKMDonnees _fakeDatas;

        public GeneratePKMTeamHandlerTest()
        {
            _fakeDatas = PKMDonneesPersonas.GetPersonas();
        }

        [Fact]
        public void GeneratePKMTeamEstOK()
        {
            var PMKPersistence = Substitute.For<IPKMTypePersistence>();
            PMKPersistence.GetPKMDonnees().Returns(_fakeDatas);

            var loadPKMTypes = new ChargerPKMTypes(PMKPersistence);
            var selectPKMTypes = new ChoisirPKMTypes();
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var gererResultatTiragePKMTypes = new GererResultatTiragePKMTypes();
            var handler = new GeneratePKMTeamHandler(loadPKMTypes, selectPKMTypes, resultatCombatPKMTypes,
                            gererResultatTiragePKMTypes);

            handler.Generer();

            Assert.Equal(10, handler.TiragePKMTypes.Count);
            foreach (var tirage in handler.TiragePKMTypes)
            {
                Assert.True(tirage.NoteTirage >= 90);
            }
        }

        [Fact]
        public void GeneratePKMTeamSeTermineJamais()
        {
            var PMKPersistence = Substitute.For<IPKMTypePersistence>();
            PMKPersistence.GetPKMDonnees().Returns(_fakeDatas);
            var gererResultatTiragePKMTypes = Substitute.For<IGererResultatTiragePKMTypes>();
            gererResultatTiragePKMTypes.TirerPKMTypes(Arg.Any<List<TiragePKMTypes>>(), Arg.Any<TiragePKMTypes>()).Returns(new List<TiragePKMTypes>());

            var loadPKMTypes = new ChargerPKMTypes(PMKPersistence);
            var selectPKMTypes = new ChoisirPKMTypes();
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var handler = new GeneratePKMTeamHandler(loadPKMTypes, selectPKMTypes, resultatCombatPKMTypes,
                            gererResultatTiragePKMTypes);

            var result = Assert.Throws<CombinaisonParfaitesIntrouvablesException>(() => handler.Generer());


            Assert.Equal("Les 10 combinaisons parfaites n'ont pas été trouvé", result.CustomMessage);
            Assert.Equal(TypeErreur.NoCombinaisonsParfaitesTrouvees, result.TypeErreur);
        }
    }
}