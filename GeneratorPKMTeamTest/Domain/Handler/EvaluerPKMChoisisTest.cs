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
            var PKMChoisis = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Feu", "Vol", "Sol", "Roche", "Insecte", "Glace", "Eau", "Normal", "Acier" });
            var tousPKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);
            var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
            var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
            var evaluerPKMChoisis = new EvaluerPKMChoisis(resultatCombatPKMTypes, tousPKMTypes);

            var tirage = evaluerPKMChoisis.Evaluer(PKMChoisis);

            Assert.Equal(ResultatTirageStatus.Excellent, tirage.ResultatTirageStatus);
            Assert.Equal(86.66, tirage.NoteTirage);
            Assert.Contains("Feu", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Vol", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Sol", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Roche", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Insecte", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Glace", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Eau", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Normal", tirage.PKMTypes.Select(o => o.Nom));
            Assert.Contains("Acier", tirage.PKMTypes.Select(o => o.Nom));
        }
    }
}