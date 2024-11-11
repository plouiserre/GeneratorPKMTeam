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
            var pkmTypesDangereux = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Acier", "Dragon", "Eau", "Glace", "Poison", "Psy", "Roche", "Sol", "Vol" });
            var pkmTypesContrables = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Glace", "Psy" });

            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();

            var resultat = resultatCombatPKMTypeDEF.NoterResultatTirage(pkmTypesDangereux, pkmTypesContrables);

            Assert.Equal(ResultatTirageStatus.Faible, resultat.ResultatStatus);
            Assert.Equal(22.22, resultat.NoteResultatTirage);
        }

        [Fact]
        public void ResultatTiragePkmDangereuxQuelquesContrable()
        {
            var pkmTypesDangereux = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Acier", "Dragon", "Eau", "Glace", "Poison", "Psy", "Roche", "Sol", "Vol" });
            var pkmTypesContrables = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Dragon", "Eau", "Glace", "Psy", "Roche", "Sol", "Vol" });

            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();

            var resultat = resultatCombatPKMTypeDEF.NoterResultatTirage(pkmTypesDangereux, pkmTypesContrables);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.Equal(77.78, resultat.NoteResultatTirage);
        }

        [Fact]
        public void ResultatTiragePkmDangereuxTousContrable()
        {
            var pkmTypesDangereux = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Acier", "Dragon", "Eau", "Glace", "Poison", "Psy", "Roche", "Sol", "Vol" });
            var pkmTypesContrables = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Acier", "Dragon", "Eau", "Glace", "Poison", "Psy", "Roche", "Sol", "Vol" });

            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();

            var resultat = resultatCombatPKMTypeDEF.NoterResultatTirage(pkmTypesDangereux, pkmTypesContrables);

            Assert.Equal(ResultatTirageStatus.Parfait, resultat.ResultatStatus);
            Assert.Equal(100, resultat.NoteResultatTirage);
        }
    }
}