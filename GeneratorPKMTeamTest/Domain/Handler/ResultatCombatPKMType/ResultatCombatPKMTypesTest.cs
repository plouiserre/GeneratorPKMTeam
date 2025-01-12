using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler.ResultatCombatPKMType
{
    public class ResultatCombatPKMTypesTest
    {
        private List<PKMType> peuPKMTypesFaibles;
        private List<PKMType> quelquesPKMTypesFaibles;
        private List<PKMType> bcpPKMTypesFaibles;
        private List<PKMType> peuPKMTypesDangereux;
        private List<PKMType> quelquesPKMTypesDangereux;
        private List<PKMType> bcpPKMTypesDangereux;
        private List<PKMType> pKMTypes;

        public ResultatCombatPKMTypesTest()
        {
            peuPKMTypesFaibles = PKMDonneesPersonas.RetournerPKMType(FrequenceRelPKMType.Peu, NomListRelPKMType.Faibles);
            quelquesPKMTypesFaibles = PKMDonneesPersonas.RetournerPKMType(FrequenceRelPKMType.Quelques, NomListRelPKMType.Faibles);
            bcpPKMTypesFaibles = PKMDonneesPersonas.RetournerPKMType(FrequenceRelPKMType.Beaucoup, NomListRelPKMType.Faibles);
            peuPKMTypesDangereux = PKMDonneesPersonas.RetournerPKMType(FrequenceRelPKMType.Peu, NomListRelPKMType.Dangereux);
            quelquesPKMTypesDangereux = PKMDonneesPersonas.RetournerPKMType(FrequenceRelPKMType.Quelques, NomListRelPKMType.Dangereux);
            bcpPKMTypesDangereux = PKMDonneesPersonas.RetournerPKMType(FrequenceRelPKMType.Beaucoup, NomListRelPKMType.Dangereux);
            var pKMDonnees = PKMDonneesPersonas.GetPersonas();
            pKMTypes = pKMDonnees.PKMTypes;
        }
        [Fact]
        public void ResultatTiragePkmFaiblesPeuPkmDangereuxBcpResultatFaibles()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(19, ResultatTirageStatus.Faible);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(19, ResultatTirageStatus.Faible);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(peuPKMTypesFaibles, bcpPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Faible, resultat.ResultatStatus);
            Assert.Equal(19, resultat.NoteResultatTirage);
        }


        [Fact]
        public void ResultatTiragePKMFaiblesQuelquesPKMDangereuxQuelquesResultatAcceptable()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(59, ResultatTirageStatus.Acceptable);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(59, ResultatTirageStatus.Acceptable);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(quelquesPKMTypesFaibles, quelquesPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.Equal(59, resultat.NoteResultatTirage);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesBcpPKMDangereuxPeuResultatExcellent()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(100, ResultatTirageStatus.Parfait);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(100, ResultatTirageStatus.Parfait);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles, peuPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Parfait, resultat.ResultatStatus);
            Assert.Equal(100, resultat.NoteResultatTirage);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesFaiblesPKMDangereuxAcceptablesObtientResultatPassables()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(19.33, ResultatTirageStatus.Faible);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(55.87, ResultatTirageStatus.Acceptable);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(peuPKMTypesFaibles, quelquesPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Passables, resultat.ResultatStatus);
            Assert.Equal(37.60, resultat.NoteResultatTirage);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesQuelquesPKMDangereuxBcpObtientResultatPassables()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(55, ResultatTirageStatus.Acceptable);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(19, ResultatTirageStatus.Faible);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(quelquesPKMTypesFaibles, bcpPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Passables, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 40);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesParfaitPkmDangereuxBcpObtientResultatAcceptables()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(100, ResultatTirageStatus.Parfait);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(19, ResultatTirageStatus.Faible);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles, bcpPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 60);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesPeuPkmDangereuxPeuObtientResultatAcceptables()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(19, ResultatTirageStatus.Faible);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(100, ResultatTirageStatus.Faible);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles, bcpPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 60);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesBcpPkmDangereuxQuelquesObtientResultatBons()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(100, ResultatTirageStatus.Parfait);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(50, ResultatTirageStatus.Acceptable);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles, bcpPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Bonnes, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 80);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesQuelquesPkmDangereuxPeuObtientResultatBons()
        {
            var resultatCombatPKMTypeATK = MockIResultatCombatPKMTypeATK(50, ResultatTirageStatus.Acceptable);
            var resultatCombatPKMTypeDEF = MockIResultatCombatPKMTypeDEF(100, ResultatTirageStatus.Parfait);
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(quelquesPKMTypesFaibles, peuPKMTypesDangereux, pKMTypes);

            Assert.Equal(ResultatTirageStatus.Bonnes, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 80);
        }

        private IResultatCombatPKMTypeATK MockIResultatCombatPKMTypeATK(double NoteResultatTirage, ResultatTirageStatus resultatTirageStatus)
        {
            var resultatCombatPKMTypeATK = Substitute.For<IResultatCombatPKMTypeATK>();
            resultatCombatPKMTypeATK.NoterResultatTirage(Arg.Any<List<PKMType>>())
            .Returns(new ResultatTirage() { NoteResultatTirage = NoteResultatTirage, ResultatStatus = resultatTirageStatus });
            return resultatCombatPKMTypeATK;
        }

        private IResultatCombatPKMTypeDEF MockIResultatCombatPKMTypeDEF(double NoteResultatTirage, ResultatTirageStatus resultatTirageStatus)
        {
            var resultatCombatPKMTypeDEF = Substitute.For<IResultatCombatPKMTypeDEF>();
            resultatCombatPKMTypeDEF.NoterResultatTirage(Arg.Any<List<PKMType>>(), Arg.Any<List<PKMType>>())
            .Returns(new ResultatTirage() { NoteResultatTirage = NoteResultatTirage, ResultatStatus = resultatTirageStatus });
            return resultatCombatPKMTypeDEF;
        }
    }
}