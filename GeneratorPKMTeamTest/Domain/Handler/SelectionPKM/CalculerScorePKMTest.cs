using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Helper;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler.SelectionPKM
{
    public class CalculerScorePKMTest
    {
        [Fact]
        public void CalculerStatsPKMPlantePoison()
        {
            var mockStats = new Dictionary<int, PKMStatsLabel>(){
                {6,PKMStatsLabel.SPAttaque},
                {5,PKMStatsLabel.Attaque},
                {4,PKMStatsLabel.PV},
                {3,PKMStatsLabel.Vitesse},
                {2,PKMStatsLabel.SPDefense},
                {1,PKMStatsLabel.Defense}
            };
            var pkms = DatasHelperTest.MockPKMPlantePoisonAvecStats();
            var persistence = Substitute.For<IPKMStatsPersistence>();
            persistence.AvoirConfigurationStats().Returns(new ConfigurationStats() { StatsParImportance = mockStats });
            var calculerScore = new CalculerScorePKM(persistence);

            var scorePourChaquePKM = calculerScore.CalculerScore(pkms);

            Assert.Equal(24, scorePourChaquePKM[pkms.First(o => o.Nom == "Bulbizarre")]);
            Assert.Equal(43, scorePourChaquePKM[pkms.First(o => o.Nom == "Ortide")]);
            Assert.Equal(69, scorePourChaquePKM[pkms.First(o => o.Nom == "Emplifor")]);
            Assert.Equal(69, scorePourChaquePKM[pkms.First(o => o.Nom == "Roserade")]);
        }
    }
}