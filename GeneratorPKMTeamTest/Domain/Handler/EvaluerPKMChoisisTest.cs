using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class EvaluerPKMChoisisTest
    {
        [Fact]
        public void EvaluerExcellentTirage()
        {
            var PKMChoisis = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Sol", "Feu", "Psy", "Ténèbres", "Poison", "Combat", "Fée", "Eau", "Glace" });
            var tousPKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var evaluerPKMChoisis = new EvaluerPKMChoisis(resultatCombatPKMTypes, tousPKMTypes);

            var tirage = evaluerPKMChoisis.Evaluer(PKMChoisis);

            Assert.Equal(ResultatTirageStatus.Excellent, tirage.ResultatTirageStatus);
            Assert.Equal(94.1, tirage.NoteTirage);
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Sol"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Feu"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Psy"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Ténèbres"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Poison"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Combat"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Fée"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Eau"));
            Assert.True(tirage.PKMTypes.Any(o => o.Nom == "Glace"));
        }
    }
}