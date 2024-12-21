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
        public void DoitRetournerDeNeufATreizePKMTypes()
        {
            var starterPKM = Substitute.For<IGererStarterPKM>();
            starterPKM.RecupererStarter().Returns(new PKM() { Nom = "Carapuce", PKMTypes = new List<string>() { "Eau" } });

            var choisirPKMTypes = new ChoisirPKMTypes(starterPKM);
            var resultats = new List<List<PKMType>>();
            for (var i = 0; i < 100; i++)
            {
                var resultat = choisirPKMTypes.SelectionnerPKMTypes(_datasFake);
                resultats.Add(resultat);
            }

            Assert.True(resultats.All(o => o.Count >= 9 && o.Count <= 12));
            Assert.True(resultats.Any(o => o.Count == 9));
            Assert.True(resultats.Any(o => o.Count == 10));
            Assert.True(resultats.Any(o => o.Count == 11));
            Assert.True(resultats.Any(o => o.Count == 12));
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

            Assert.True(typeChoisis.Count >= 9 && typeChoisis.Count <= 13);
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

            Assert.True(typeChoisis.Count >= 9 && typeChoisis.Count <= 13);
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