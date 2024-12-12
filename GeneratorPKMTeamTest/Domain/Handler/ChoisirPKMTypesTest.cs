using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class ChoisirPKMTypesTest
    {
        private PKMDonnees _datasFake;
        public ChoisirPKMTypesTest()
        {
            _datasFake = PKMDonneesPersonas.GetPersonas();
        }

        [Fact]
        public void DoitRetournerNeufPKMTypes()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });

            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);

            var result = choisirPKMTypes.SelectionnerPKMTypes(_datasFake);

            Assert.Equal(9, result.Count);
        }

        [Fact]
        public void DoitRetournerNeufDifferentsPKMTypes()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });

            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);

            var result = choisirPKMTypes.SelectionnerPKMTypes(_datasFake);

            for (int i = 0; i < 9; i++)
            {
                if (i < 8)
                {
                    Assert.NotEqual(result[i].Nom, result[i + 1].Nom);
                }
            }
        }

        [Fact]
        public void DoitRetournerDifferentsResultatAChaqueLancement()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });

            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);

            var firstResult = choisirPKMTypes.SelectionnerPKMTypes(_datasFake);
            var secondResult = choisirPKMTypes.SelectionnerPKMTypes(_datasFake);

            bool toutEstEgal = true;
            for (int i = 0; i < 9; i++)
            {
                if (firstResult[i].Nom != secondResult[i].Nom)
                {
                    toutEstEgal = false;
                    break;
                }
            }
            Assert.False(toutEstEgal);
        }

        [Fact]
        public void ChoisirPKMTypesAvecStarterPKMUnType()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });

            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);
            var typeChoisis = choisirPKMTypes.SelectionnerPKMTypes(_datasFake);

            Assert.Equal(9, typeChoisis.Count);
            Assert.Equal("Eau", typeChoisis[0].Nom);
            for (int i = 0; i < typeChoisis.Count; i++)
            {
                if (i == 0)
                    continue;
                Assert.NotEqual("Eau", typeChoisis[i].Nom);
            }
        }

        [Fact]
        public void ChoisirPKMTypesAvecStarterPKMDeuxTypes()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Bulbizarre", PKMTypes = new List<string>() { "Plante", "Poison" } });

            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);
            var typeChoisis = choisirPKMTypes.SelectionnerPKMTypes(_datasFake);

            Assert.Equal(9, typeChoisis.Count);
            Assert.Equal("Plante", typeChoisis[0].Nom);
            Assert.Equal("Poison", typeChoisis[1].Nom);
            for (int i = 0; i < typeChoisis.Count; i++)
            {
                if (i == 0 || i == 1)
                    continue;
                Assert.NotEqual("Plante", typeChoisis[i].Nom);
                Assert.NotEqual("Poison", typeChoisis[i].Nom);
            }
        }
    }
}