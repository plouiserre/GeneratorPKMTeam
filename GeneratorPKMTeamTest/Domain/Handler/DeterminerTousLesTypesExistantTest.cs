using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Services;
using GeneratorPKMTeamTest.Utils.Helper;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class DeterminerTousLesTypesExistantTest
    {
        [Fact]
        public void CalculerSixTypesPKMPourLaGenerationUn()
        {
            var tousPKMs = DatasHelperTest.RetournersTousPKM();
            var pKMPersistence = Substitute.For<IPKMPersistence>();
            pKMPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var pkmTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Feu", "Eau", "Vol", "Psy", "Sol", "Roche", "Plante", "Poison", "Glace" });

            var determinerTypesExistant = new DeterminerTousLesTypesExistant(pKMPersistence);

            var resultat = determinerTypesExistant.Calculer(1, pkmTypes);

            Assert.Equal(23, resultat.Count);
            Assert.Equal("Feu", resultat["Feu"].First().Nom);
            Assert.Equal("Eau", resultat["Eau"].First().Nom);
            Assert.Equal("Psy", resultat["Psy"].First().Nom);
            Assert.Equal("Sol", resultat["Sol"].First().Nom);
            Assert.Equal("Roche", resultat["Roche"].First().Nom);
            Assert.Equal("Plante", resultat["Plante"].First().Nom);
            Assert.Equal("Poison", resultat["Poison"].First().Nom);
            Assert.Equal("Glace", resultat["Glace"].First().Nom);
            Assert.Equal("Feu", resultat["Feu-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Feu-Vol"][1].Nom);
            Assert.Equal("Plante", resultat["Plante-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Plante-Psy"][1].Nom);
            Assert.Equal("Plante", resultat["Plante-Poison"][0].Nom);
            Assert.Equal("Poison", resultat["Plante-Poison"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Poison"][0].Nom);
            Assert.Equal("Poison", resultat["Eau-Poison"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Eau-Psy"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Glace"][0].Nom);
            Assert.Equal("Glace", resultat["Eau-Glace"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Eau-Vol"][1].Nom);
            Assert.Equal("Sol", resultat["Sol-Roche"][0].Nom);
            Assert.Equal("Roche", resultat["Sol-Roche"][1].Nom);
            Assert.Equal("Roche", resultat["Roche-Eau"][0].Nom);
            Assert.Equal("Eau", resultat["Roche-Eau"][1].Nom);
            Assert.Equal("Roche", resultat["Roche-Sol"][0].Nom);
            Assert.Equal("Sol", resultat["Roche-Sol"][1].Nom);
            Assert.Equal("Roche", resultat["Roche-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Roche-Vol"][1].Nom);
            Assert.Equal("Poison", resultat["Poison-Sol"][0].Nom);
            Assert.Equal("Sol", resultat["Poison-Sol"][1].Nom);
            Assert.Equal("Poison", resultat["Poison-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Poison-Vol"][1].Nom);
            Assert.Equal("Glace", resultat["Glace-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Glace-Vol"][1].Nom);
            Assert.Equal("Glace", resultat["Glace-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Glace-Psy"][1].Nom);
        }

        [Fact]
        public void CalculerSixTypesPKMPourLaGenerationTrois()
        {
            var tousPKMs = DatasHelperTest.RetournersTousPKM();
            var pKMPersistence = Substitute.For<IPKMPersistence>();
            pKMPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var pkmTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Eau", "Ténèbres", "Psy", "Sol", "Fée", "Plante", "Dragon", "Glace", "Insecte" });

            var determinerTypesExistant = new DeterminerTousLesTypesExistant(pKMPersistence);

            var resultat = determinerTypesExistant.Calculer(3, pkmTypes);

            Assert.Equal(30, resultat.Count);
            Assert.Equal("Eau", resultat["Eau"].First().Nom);
            Assert.Equal("Ténèbres", resultat["Ténèbres"].First().Nom);
            Assert.Equal("Psy", resultat["Psy"].First().Nom);
            Assert.Equal("Sol", resultat["Sol"].First().Nom);
            Assert.Equal("Fée", resultat["Fée"].First().Nom);
            Assert.Equal("Plante", resultat["Plante"].First().Nom);
            Assert.Equal("Dragon", resultat["Dragon"].First().Nom);
            Assert.Equal("Glace", resultat["Glace"].First().Nom);
            Assert.Equal("Insecte", resultat["Insecte"].First().Nom);
            Assert.Equal("Eau", resultat["Eau-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Eau-Psy"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Glace"][0].Nom);
            Assert.Equal("Glace", resultat["Eau-Glace"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Fée"][0].Nom);
            Assert.Equal("Fée", resultat["Eau-Fée"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Sol"][0].Nom);
            Assert.Equal("Sol", resultat["Eau-Sol"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Plante"][0].Nom);
            Assert.Equal("Plante", resultat["Eau-Plante"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Dragon"][0].Nom);
            Assert.Equal("Dragon", resultat["Eau-Dragon"][1].Nom);
            Assert.Equal("Eau", resultat["Eau-Ténèbres"][0].Nom);
            Assert.Equal("Ténèbres", resultat["Eau-Ténèbres"][1].Nom);
            Assert.Equal("Ténèbres", resultat["Ténèbres-Glace"][0].Nom);
            Assert.Equal("Glace", resultat["Ténèbres-Glace"][1].Nom);
            Assert.Equal("Psy", resultat["Psy-Fée"][0].Nom);
            Assert.Equal("Fée", resultat["Psy-Fée"][1].Nom);
            Assert.Equal("Psy", resultat["Psy-Plante"][0].Nom);
            Assert.Equal("Plante", resultat["Psy-Plante"][1].Nom);
            Assert.Equal("Psy", resultat["Psy-Fée"][0].Nom);
            Assert.Equal("Fée", resultat["Psy-Fée"][1].Nom);
            Assert.Equal("Psy", resultat["Psy-Plante"][0].Nom);
            Assert.Equal("Plante", resultat["Psy-Plante"][1].Nom);
            Assert.Equal("Sol", resultat["Sol-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Sol-Psy"][1].Nom);
            Assert.Equal("Sol", resultat["Sol-Dragon"][0].Nom);
            Assert.Equal("Dragon", resultat["Sol-Dragon"][1].Nom);
            Assert.Equal("Plante", resultat["Plante-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Plante-Psy"][1].Nom);
            Assert.Equal("Plante", resultat["Plante-Ténèbres"][0].Nom);
            Assert.Equal("Ténèbres", resultat["Plante-Ténèbres"][1].Nom);
            Assert.Equal("Dragon", resultat["Dragon-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Dragon-Psy"][1].Nom);
            Assert.Equal("Glace", resultat["Glace-Eau"][0].Nom);
            Assert.Equal("Eau", resultat["Glace-Eau"][1].Nom);
            Assert.Equal("Glace", resultat["Glace-Psy"][0].Nom);
            Assert.Equal("Psy", resultat["Glace-Psy"][1].Nom);
            Assert.Equal("Glace", resultat["Glace-Sol"][0].Nom);
            Assert.Equal("Sol", resultat["Glace-Sol"][1].Nom);
            Assert.Equal("Insecte", resultat["Insecte-Eau"][0].Nom);
            Assert.Equal("Eau", resultat["Insecte-Eau"][1].Nom);
            Assert.Equal("Insecte", resultat["Insecte-Sol"][0].Nom);
            Assert.Equal("Sol", resultat["Insecte-Sol"][1].Nom);
            Assert.Equal("Insecte", resultat["Insecte-Plante"][0].Nom);
            Assert.Equal("Plante", resultat["Insecte-Plante"][1].Nom);
        }

        [Fact]
        public void CalculerSixTypesPKMPourLaGenerationSix()
        {
            var tousPKMs = DatasHelperTest.RetournersTousPKM();
            var pKMPersistence = Substitute.For<IPKMPersistence>();
            pKMPersistence.GetPKMs().Returns(new PKMs() { TousPKMs = tousPKMs.ToList() });
            var pkmTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Electrique", "Spectre", "Feu", "Acier", "Roche", "Combat", "Normal", "Vol", "Poison" });

            var determinerTypesExistant = new DeterminerTousLesTypesExistant(pKMPersistence);

            var resultat = determinerTypesExistant.Calculer(6, pkmTypes);

            Assert.Equal(33, resultat.Count);
            Assert.Equal("Electrique", resultat["Electrique"].First().Nom);
            Assert.Equal("Spectre", resultat["Spectre"].First().Nom);
            Assert.Equal("Feu", resultat["Feu"].First().Nom);
            Assert.Equal("Acier", resultat["Acier"].First().Nom);
            Assert.Equal("Roche", resultat["Roche"].First().Nom);
            Assert.Equal("Combat", resultat["Combat"].First().Nom);
            Assert.Equal("Normal", resultat["Normal"].First().Nom);
            Assert.Equal("Vol", resultat["Vol"].First().Nom);
            Assert.Equal("Poison", resultat["Poison"].First().Nom);
            Assert.Equal("Electrique", resultat["Electrique-Acier"][0].Nom);
            Assert.Equal("Acier", resultat["Electrique-Acier"][1].Nom);
            Assert.Equal("Electrique", resultat["Electrique-Spectre"][0].Nom);
            Assert.Equal("Spectre", resultat["Electrique-Spectre"][1].Nom);
            Assert.Equal("Electrique", resultat["Electrique-Normal"][0].Nom);
            Assert.Equal("Normal", resultat["Electrique-Normal"][1].Nom);
            Assert.Equal("Electrique", resultat["Electrique-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Electrique-Vol"][1].Nom);
            Assert.Equal("Spectre", resultat["Spectre-Feu"][0].Nom);
            Assert.Equal("Feu", resultat["Spectre-Feu"][1].Nom);
            Assert.Equal("Spectre", resultat["Spectre-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Spectre-Vol"][1].Nom);
            Assert.Equal("Spectre", resultat["Spectre-Poison"][0].Nom);
            Assert.Equal("Poison", resultat["Spectre-Poison"][1].Nom);
            Assert.Equal("Feu", resultat["Feu-Roche"][0].Nom);
            Assert.Equal("Roche", resultat["Feu-Roche"][1].Nom);
            Assert.Equal("Feu", resultat["Feu-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Feu-Vol"][1].Nom);
            Assert.Equal("Feu", resultat["Feu-Combat"][0].Nom);
            Assert.Equal("Combat", resultat["Feu-Combat"][1].Nom);
            Assert.Equal("Feu", resultat["Feu-Acier"][0].Nom);
            Assert.Equal("Acier", resultat["Feu-Acier"][1].Nom);
            Assert.Equal("Feu", resultat["Feu-Normal"][0].Nom);
            Assert.Equal("Normal", resultat["Feu-Normal"][1].Nom);
            Assert.Equal("Acier", resultat["Acier-Spectre"][0].Nom);
            Assert.Equal("Spectre", resultat["Acier-Spectre"][1].Nom);
            Assert.Equal("Acier", resultat["Acier-Roche"][0].Nom);
            Assert.Equal("Roche", resultat["Acier-Roche"][1].Nom);
            Assert.Equal("Acier", resultat["Acier-Combat"][0].Nom);
            Assert.Equal("Combat", resultat["Acier-Combat"][1].Nom);
            Assert.Equal("Acier", resultat["Acier-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Acier-Vol"][1].Nom);
            Assert.Equal("Roche", resultat["Roche-Acier"][0].Nom);
            Assert.Equal("Acier", resultat["Roche-Acier"][1].Nom);
            Assert.Equal("Roche", resultat["Roche-Combat"][0].Nom);
            Assert.Equal("Combat", resultat["Roche-Combat"][1].Nom);
            Assert.Equal("Roche", resultat["Roche-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Roche-Vol"][1].Nom);
            Assert.Equal("Combat", resultat["Combat-Acier"][0].Nom);
            Assert.Equal("Acier", resultat["Combat-Acier"][1].Nom);
            Assert.Equal("Combat", resultat["Combat-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Combat-Vol"][1].Nom);
            Assert.Equal("Normal", resultat["Normal-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Normal-Vol"][1].Nom);
            Assert.Equal("Poison", resultat["Poison-Combat"][0].Nom);
            Assert.Equal("Combat", resultat["Poison-Combat"][1].Nom);
            Assert.Equal("Poison", resultat["Poison-Vol"][0].Nom);
            Assert.Equal("Vol", resultat["Poison-Vol"][1].Nom);
        }
    }
}