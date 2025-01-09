using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler.SelectionPKM
{
    //TODO factoriser la partie des assert
    public class DeterminerClassementPKMParStatTest
    {
        [Fact]
        public void ComparerPKMATKSansEgalite()
        {
            var pkms = DatasHelperTest.MockPKMPlantePoisonAvecStats();
            var determiner = new DeterminerClassementPKMParStat();

            var pkmsClasses = determiner.Classer(pkms, PKMStatsLabel.Attaque);

            Assert.Equal(0, pkmsClasses[pkms.First(o => o.Nom == "Bulbizarre")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Ortide")]);
            Assert.Equal(2, pkmsClasses[pkms.First(o => o.Nom == "Roserade")]);
            Assert.Equal(3, pkmsClasses[pkms.First(o => o.Nom == "Emplifor")]);
        }

        [Fact]
        public void ComparerPKMSpeATKSansEgalite()
        {
            var pkms = DatasHelperTest.MockPKMPlantePoisonAvecStats();
            var determiner = new DeterminerClassementPKMParStat();

            var pkmsClasses = determiner.Classer(pkms, PKMStatsLabel.SPAttaque);

            Assert.Equal(0, pkmsClasses[pkms.First(o => o.Nom == "Bulbizarre")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Ortide")]);
            Assert.Equal(2, pkmsClasses[pkms.First(o => o.Nom == "Emplifor")]);
            Assert.Equal(3, pkmsClasses[pkms.First(o => o.Nom == "Roserade")]);
        }

        [Fact]
        public void ComparerPKMSpeDEFSansEgalite()
        {
            var pkms = DatasHelperTest.MockPKMPlantePoisonAvecStats();
            var determiner = new DeterminerClassementPKMParStat();

            var pkmsClasses = determiner.Classer(pkms, PKMStatsLabel.SPDefense);

            Assert.Equal(0, pkmsClasses[pkms.First(o => o.Nom == "Bulbizarre")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Emplifor")]);
            Assert.Equal(2, pkmsClasses[pkms.First(o => o.Nom == "Ortide")]);
            Assert.Equal(3, pkmsClasses[pkms.First(o => o.Nom == "Roserade")]);
        }

        [Fact]
        public void ComparerPKMSpeVitessSansEgalite()
        {
            var pkms = DatasHelperTest.MockPKMPlantePoisonAvecStats();
            var determiner = new DeterminerClassementPKMParStat();

            var pkmsClasses = determiner.Classer(pkms, PKMStatsLabel.Vitesse);

            Assert.Equal(0, pkmsClasses[pkms.First(o => o.Nom == "Ortide")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Bulbizarre")]);
            Assert.Equal(2, pkmsClasses[pkms.First(o => o.Nom == "Emplifor")]);
            Assert.Equal(3, pkmsClasses[pkms.First(o => o.Nom == "Roserade")]);
        }

        [Fact]
        public void ComparerPKMPVAvecEgalite()
        {
            var pkms = DatasHelperTest.MockPKMPlantePoisonAvecStats();
            var determiner = new DeterminerClassementPKMParStat();

            var pkmsClasses = determiner.Classer(pkms, PKMStatsLabel.PV);

            Assert.Equal(0, pkmsClasses[pkms.First(o => o.Nom == "Bulbizarre")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Ortide")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Roserade")]);
            Assert.Equal(3, pkmsClasses[pkms.First(o => o.Nom == "Emplifor")]);
        }

        [Fact]
        public void ComparerPKMDEFAvecEgalite()
        {
            var pkms = DatasHelperTest.MockPKMPlantePoisonAvecStats();
            var determiner = new DeterminerClassementPKMParStat();

            var pkmsClasses = determiner.Classer(pkms, PKMStatsLabel.Defense);

            Assert.Equal(0, pkmsClasses[pkms.First(o => o.Nom == "Bulbizarre")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Emplifor")]);
            Assert.Equal(1, pkmsClasses[pkms.First(o => o.Nom == "Roserade")]);
            Assert.Equal(3, pkmsClasses[pkms.First(o => o.Nom == "Ortide")]);
        }
    }
}