using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeamTest.Utils.Personas;

namespace GeneratorPKMTeamTest.Domain.Handler.ResultatCombatPKMType
{
    public class ResultatCombatPKMTypeATKTest
    {
        private List<RelPKMType> peuPKMTypesFaibles;
        private List<RelPKMType> quelquesPKMTypesFaibles;
        private List<RelPKMType> bcpPKMTypesFaibles;

        public ResultatCombatPKMTypeATKTest()
        {
            peuPKMTypesFaibles = RelPKMTypePersonas.RetournerRelPKMType(FrequenceRelPKMType.Peu, NomListRelPKMType.Faibles);
            quelquesPKMTypesFaibles = RelPKMTypePersonas.RetournerRelPKMType(FrequenceRelPKMType.Quelques, NomListRelPKMType.Faibles);
            bcpPKMTypesFaibles = RelPKMTypePersonas.RetournerRelPKMType(FrequenceRelPKMType.Beaucoup, NomListRelPKMType.Faibles);
        }

        [Fact]
        public void ResultatTiragePkmFaiblesPeu()
        {
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();

            var resultat = resultatCombatPKMTypeATK.NoterResultatTirage(peuPKMTypesFaibles);

            Assert.Equal(ResultatTirageStatus.Faible, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 60);
        }

        [Fact]
        public void ResultatTiragePkmFaiblesQuelques()
        {
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();

            var resultat = resultatCombatPKMTypeATK.NoterResultatTirage(quelquesPKMTypesFaibles);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage <= 100);
        }

        [Fact]
        public void ResultatTiragePkmFaiblesParfait()
        {
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();

            var resultat = resultatCombatPKMTypeATK.NoterResultatTirage(bcpPKMTypesFaibles);

            Assert.Equal(ResultatTirageStatus.Parfait, resultat.ResultatStatus);
            Assert.Equal(resultat.NoteResultatTirage, 100);
        }
    }
}