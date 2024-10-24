using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeamTest.Utils.Personas;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class GererResultatTiragePKMTypesTest
    {
        private PKMDonnees _fakeDatas;
        private IPKMTypePersistence _PMKPersistence;

        public GererResultatTiragePKMTypesTest()
        {
            _fakeDatas = PKMDonneesPersonas.GetPersonas();
            _PMKPersistence = Substitute.For<IPKMTypePersistence>();
            _PMKPersistence.GetPKMDonnees().Returns(_fakeDatas);
        }

        [Fact]
        public void GererPremierTirage()
        {
            var gererTirage = new GererResultatTiragePKMTypes();
            var tirageATraiter = new TiragePKMTypes()
            {
                ResultatTirageStatus = ResultatTirageStatus.Faible,
                NoteTirage = 22.2,
                PKMTypes = new List<PKMType>()
            };

            List<TiragePKMTypes> tirages = gererTirage.TirerPKMTypes(new List<TiragePKMTypes>(), tirageATraiter);

            Assert.Single(tirages);
            Assert.Equal(22.2, tirages[0].NoteTirage);
            Assert.Equal(ResultatTirageStatus.Faible, tirages[0].ResultatTirageStatus);
        }

        [Fact]
        public void GererTirageFaibleAvecListeTiragePleine()
        {
            var premierTirageFaible = ConstruireTiragePKMTypesDataTest(10, ResultatTirageStatus.Faible);
            var secondTirageAcceptable = ConstruireTiragePKMTypesDataTest(67, ResultatTirageStatus.Acceptable);
            var troisiemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var quatriemeTirageFaible = ConstruireTiragePKMTypesDataTest(6.66, ResultatTirageStatus.Faible);
            var cinquièmeTirageAcceptable = ConstruireTiragePKMTypesDataTest(77, ResultatTirageStatus.Acceptable);
            var sixiemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var septiemeTirageFaible = ConstruireTiragePKMTypesDataTest(24.87, ResultatTirageStatus.Faible);
            var huitiemeTirageAcceptable = ConstruireTiragePKMTypesDataTest(62, ResultatTirageStatus.Acceptable);
            var neuviemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var dixiemeTirageFaible = ConstruireTiragePKMTypesDataTest(8.65, ResultatTirageStatus.Faible);
            var precedentsTiragesSauvegardes = new List<TiragePKMTypes>() { premierTirageFaible, secondTirageAcceptable,
            troisiemeTirageParfait, quatriemeTirageFaible, cinquièmeTirageAcceptable, sixiemeTirageParfait,
            septiemeTirageFaible, huitiemeTirageAcceptable, neuviemeTirageParfait, dixiemeTirageFaible };
            var nouveauTirage = ConstruireTiragePKMTypesDataTest(29.9, ResultatTirageStatus.Faible);

            var gererTirage = new GererResultatTiragePKMTypes();

            List<TiragePKMTypes> tirages = gererTirage.TirerPKMTypes(precedentsTiragesSauvegardes, nouveauTirage);

            Assert.True(10 == tirages.Count);
            foreach (var tirage in tirages)
            {
                Assert.NotEqual(29.9, tirage.NoteTirage);
            }
        }

        [Fact]
        public void GererTirageAcceptableAvecListeTiragePleine()
        {
            double scorePlusFaible = 6.66;
            var premierTirageFaible = ConstruireTiragePKMTypesDataTest(10, ResultatTirageStatus.Faible);
            var secondTirageAcceptable = ConstruireTiragePKMTypesDataTest(67, ResultatTirageStatus.Acceptable);
            var troisiemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var quatriemeTirageFaible = ConstruireTiragePKMTypesDataTest(scorePlusFaible, ResultatTirageStatus.Faible);
            var cinquièmeTirageAcceptable = ConstruireTiragePKMTypesDataTest(77, ResultatTirageStatus.Acceptable);
            var sixiemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var septiemeTirageFaible = ConstruireTiragePKMTypesDataTest(24.87, ResultatTirageStatus.Faible);
            var huitiemeTirageAcceptable = ConstruireTiragePKMTypesDataTest(92.87, ResultatTirageStatus.Acceptable);
            var neuviemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var dixiemeTirageFaible = ConstruireTiragePKMTypesDataTest(88.65, ResultatTirageStatus.Acceptable);
            var precedentsTiragesSauvegardes = new List<TiragePKMTypes>() { premierTirageFaible, secondTirageAcceptable,
            troisiemeTirageParfait, quatriemeTirageFaible, cinquièmeTirageAcceptable, sixiemeTirageParfait,
            septiemeTirageFaible, huitiemeTirageAcceptable, neuviemeTirageParfait, dixiemeTirageFaible };
            var nouveauTirage = ConstruireTiragePKMTypesDataTest(62.0, ResultatTirageStatus.Acceptable);

            var gererTirage = new GererResultatTiragePKMTypes();

            List<TiragePKMTypes> tirages = gererTirage.TirerPKMTypes(precedentsTiragesSauvegardes, nouveauTirage);

            Assert.True(10 == tirages.Count);
            foreach (var tirage in tirages)
            {
                Assert.NotEqual(scorePlusFaible, tirage.NoteTirage);
            }
            Assert.Equal(62.0, tirages[9].NoteTirage);
            Assert.Equal(ResultatTirageStatus.Acceptable, tirages[9].ResultatTirageStatus);
        }

        [Fact]
        public void GererDeuxTirageAcceptablesAvecListeTiragePleineComposesAcceptablesParfaits()
        {
            double premierNouveauTirageNote = 79.99;
            double secondNouveauTirageNote = 64.98;
            var premierTirageFaible = ConstruireTiragePKMTypesDataTest(66, ResultatTirageStatus.Acceptable);
            var secondTirageAcceptable = ConstruireTiragePKMTypesDataTest(67, ResultatTirageStatus.Acceptable);
            var troisiemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var quatriemeTirageFaible = ConstruireTiragePKMTypesDataTest(89.89, ResultatTirageStatus.Parfait);
            var cinquièmeTirageAcceptable = ConstruireTiragePKMTypesDataTest(77, ResultatTirageStatus.Acceptable);
            var sixiemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var septiemeTirageFaible = ConstruireTiragePKMTypesDataTest(69.0, ResultatTirageStatus.Acceptable);
            var huitiemeTirageAcceptable = ConstruireTiragePKMTypesDataTest(68.9, ResultatTirageStatus.Acceptable);
            var neuviemeTirageParfait = ConstruireTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var dixiemeTirageFaible = ConstruireTiragePKMTypesDataTest(65.65, ResultatTirageStatus.Acceptable);
            var precedentsTiragesSauvegardes = new List<TiragePKMTypes>() { premierTirageFaible, secondTirageAcceptable,
            troisiemeTirageParfait, quatriemeTirageFaible, cinquièmeTirageAcceptable, sixiemeTirageParfait,
            septiemeTirageFaible, huitiemeTirageAcceptable, neuviemeTirageParfait, dixiemeTirageFaible };
            var premierNouveauTirage = ConstruireTiragePKMTypesDataTest(premierNouveauTirageNote, ResultatTirageStatus.Acceptable);
            var secondNouveauTirage = ConstruireTiragePKMTypesDataTest(secondNouveauTirageNote, ResultatTirageStatus.Acceptable);

            var gererTirage = new GererResultatTiragePKMTypes();

            List<TiragePKMTypes> tirages = gererTirage.TirerPKMTypes(precedentsTiragesSauvegardes, premierNouveauTirage);
            tirages = gererTirage.TirerPKMTypes(tirages, secondNouveauTirage);

            Assert.True(10 == tirages.Count);
            foreach (var tirage in tirages)
            {
                Assert.NotEqual(secondNouveauTirageNote, tirage.NoteTirage);
            }
            Assert.Equal(premierNouveauTirageNote, tirages[9].NoteTirage);
            Assert.Equal(ResultatTirageStatus.Acceptable, tirages[9].ResultatTirageStatus);
        }

        private TiragePKMTypes ConstruireTiragePKMTypesDataTest(double NoteTirage, ResultatTirageStatus resultatTirageStatus)
        {
            return new TiragePKMTypes()
            {
                ResultatTirageStatus = resultatTirageStatus,
                NoteTirage = NoteTirage,
                PKMTypes = new List<PKMType>()
            };
        }
    }
}