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
            var PMKPersistence = Substitute.For<IPKMTypePersistence>();
            PMKPersistence.GetPKMDonnees().Returns(_fakeDatas);

            var loadPKMTypes = new ChargerPKMTypes(PMKPersistence);
            var selectPKMTypes = new ChoisirPKMTypes();
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var gererResultatTiragePKMTypes = new GererResultatTiragePKMTypes();
            var handler = new ChoisirMeilleuresCombinaisonsTypes(loadPKMTypes, selectPKMTypes, resultatCombatPKMTypes,
                            gererResultatTiragePKMTypes);

            var typesChoisis = handler.Choisir();

            Assert.Equal(10, typesChoisis.Count);
            foreach (var tirage in typesChoisis)
            {
                Assert.True(tirage.NoteTirage >= 90);
            }
        }

        [Fact]
        public void ChoixMeilleuresCombinaisonsTypesSeTermineJamais()
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
            var handler = new ChoisirMeilleuresCombinaisonsTypes(loadPKMTypes, selectPKMTypes, resultatCombatPKMTypes,
                            gererResultatTiragePKMTypes);

            var result = Assert.Throws<CombinaisonParfaitesIntrouvablesException>(() => handler.Choisir());


            Assert.Equal("Les 10 combinaisons parfaites n'ont pas été trouvé", result.CustomMessage);
            Assert.Equal(TypeErreur.NoCombinaisonsParfaitesTrouvees, result.TypeErreur);
        }
    }
}