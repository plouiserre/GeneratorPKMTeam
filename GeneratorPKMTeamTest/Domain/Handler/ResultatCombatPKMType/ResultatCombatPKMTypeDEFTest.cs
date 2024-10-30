using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeamTest.Utils.Helper;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler.ResultatCombatPKMType
{
    public class ResultatCombatPKMTypeDEFTest
    {
        private List<RelPKMType> peuPKMTypesDangereux;
        private List<PKMType> tiragePKMTypes;

        public ResultatCombatPKMTypeDEFTest()
        {
            peuPKMTypesDangereux = RelPKMTypePersonas.RetournerRelPKMType(FrequenceRelPKMType.Peu, NomListRelPKMType.Dangereux);
            tiragePKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Eau", "Insecte", "Dragon", "Combat", "Ténèbres", "Fée" });
        }

        [Fact]
        public void ResultatTiragePkmDangereuxPeuContrable()
        {
            var pkmTypesContres = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Acier" });
            var relPKMTypesContres = pkmTypesContres.Select(o => new RelPKMType() { TypePKM = o.Nom, ModeImpact = 2 }).ToList();
            var combattrePKMTypes = Substitute.For<ICombattrePKMTypes>();
            combattrePKMTypes.RetournerPKMTypesContres(Arg.Any<List<RelPKMType>>(), Arg.Any<List<PKMType>>()).Returns(relPKMTypesContres);

            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF(combattrePKMTypes);

            var resultat = resultatCombatPKMTypeDEF.NoterResultatTirage(peuPKMTypesDangereux, tiragePKMTypes);

            Assert.Equal(ResultatTirageStatus.Faible, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 30);
        }

        [Fact]
        public void ResultatTiragePkmDangereuxQuelquesContrable()
        {
            var pkmTypesContres = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Acier", "Combat", "Dragon" });
            var relPKMTypesContres = pkmTypesContres.Select(o => new RelPKMType() { TypePKM = o.Nom, ModeImpact = 2 }).ToList();
            var combattrePKMTypes = Substitute.For<ICombattrePKMTypes>();
            combattrePKMTypes.RetournerPKMTypesContres(Arg.Any<List<RelPKMType>>(), Arg.Any<List<PKMType>>()).Returns(relPKMTypesContres);

            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF(combattrePKMTypes);

            var resultat = resultatCombatPKMTypeDEF.NoterResultatTirage(peuPKMTypesDangereux, tiragePKMTypes);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 100 && resultat.NoteResultatTirage > 30);
        }

        [Fact]
        public void ResultatTiragePkmDangereuxTousContrable()
        {
            var pkmTypesContres = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Acier", "Combat", "Dragon", "Eau", "Electrique" });
            var relPKMTypesContres = pkmTypesContres.Select(o => new RelPKMType() { TypePKM = o.Nom, ModeImpact = 2 }).ToList();
            var combattrePKMTypes = Substitute.For<ICombattrePKMTypes>();
            combattrePKMTypes.RetournerPKMTypesContres(Arg.Any<List<RelPKMType>>(), Arg.Any<List<PKMType>>()).Returns(relPKMTypesContres);

            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF(combattrePKMTypes);

            var resultat = resultatCombatPKMTypeDEF.NoterResultatTirage(peuPKMTypesDangereux, tiragePKMTypes);

            Assert.Equal(ResultatTirageStatus.Parfait, resultat.ResultatStatus);
            Assert.Equal(100, resultat.NoteResultatTirage);
        }
    }
}