using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Helper;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler.SelectionPKM
{
    public class DeterminerMeilleurPKMParStatsTest
    {
        [Fact]
        public void DeterminerMeilleurPKMAvecPremiereConfiguration()
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
            var determiner = new DeterminerMeilleurPKMParStats(persistence);

            var pkmsPlusOpti = determiner.Calculer(pkms);

            Assert.Equal(2, pkmsPlusOpti.Count);
            Assert.True(pkmsPlusOpti.Any(o => o.Nom == "Emplifor"));
            Assert.True(pkmsPlusOpti.Any(o => o.Nom == "Roserade"));
        }
    }
}