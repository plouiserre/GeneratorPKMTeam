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
        private PKMDatas _fakeDatas;
        private IPKMTypePersistence _PMKPersistence;

        public GererResultatTiragePKMTypesTest()
        {
            _fakeDatas = PKMDatasPersonas.GetPersonas();
            _PMKPersistence = Substitute.For<IPKMTypePersistence>();
            _PMKPersistence.GetPKMDatas().Returns(_fakeDatas);
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
            var premierTirageFaible = BuildTiragePKMTypesDataTest(10, ResultatTirageStatus.Faible);
            var secondTirageAcceptable = BuildTiragePKMTypesDataTest(67, ResultatTirageStatus.Acceptable);
            var troisiemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var quatriemeTirageFaible = BuildTiragePKMTypesDataTest(6.66, ResultatTirageStatus.Faible);
            var cinquièmeTirageAcceptable = BuildTiragePKMTypesDataTest(77, ResultatTirageStatus.Acceptable);
            var sixiemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var septiemeTirageFaible = BuildTiragePKMTypesDataTest(24.87, ResultatTirageStatus.Faible);
            var huitiemeTirageAcceptable = BuildTiragePKMTypesDataTest(62, ResultatTirageStatus.Acceptable);
            var neuviemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var dixiemeTirageFaible = BuildTiragePKMTypesDataTest(8.65, ResultatTirageStatus.Faible);
            var precedentsTiragesSauvegardes = new List<TiragePKMTypes>() { premierTirageFaible, secondTirageAcceptable,
            troisiemeTirageParfait, quatriemeTirageFaible, cinquièmeTirageAcceptable, sixiemeTirageParfait,
            septiemeTirageFaible, huitiemeTirageAcceptable, neuviemeTirageParfait, dixiemeTirageFaible };
            var nouveauTirage = BuildTiragePKMTypesDataTest(29.9, ResultatTirageStatus.Faible);

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
            var premierTirageFaible = BuildTiragePKMTypesDataTest(10, ResultatTirageStatus.Faible);
            var secondTirageAcceptable = BuildTiragePKMTypesDataTest(67, ResultatTirageStatus.Acceptable);
            var troisiemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var quatriemeTirageFaible = BuildTiragePKMTypesDataTest(scorePlusFaible, ResultatTirageStatus.Faible);
            var cinquièmeTirageAcceptable = BuildTiragePKMTypesDataTest(77, ResultatTirageStatus.Acceptable);
            var sixiemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var septiemeTirageFaible = BuildTiragePKMTypesDataTest(24.87, ResultatTirageStatus.Faible);
            var huitiemeTirageAcceptable = BuildTiragePKMTypesDataTest(92.87, ResultatTirageStatus.Acceptable);
            var neuviemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var dixiemeTirageFaible = BuildTiragePKMTypesDataTest(88.65, ResultatTirageStatus.Acceptable);
            var precedentsTiragesSauvegardes = new List<TiragePKMTypes>() { premierTirageFaible, secondTirageAcceptable,
            troisiemeTirageParfait, quatriemeTirageFaible, cinquièmeTirageAcceptable, sixiemeTirageParfait,
            septiemeTirageFaible, huitiemeTirageAcceptable, neuviemeTirageParfait, dixiemeTirageFaible };
            var nouveauTirage = BuildTiragePKMTypesDataTest(62.0, ResultatTirageStatus.Acceptable);

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
            var premierTirageFaible = BuildTiragePKMTypesDataTest(66, ResultatTirageStatus.Acceptable);
            var secondTirageAcceptable = BuildTiragePKMTypesDataTest(67, ResultatTirageStatus.Acceptable);
            var troisiemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var quatriemeTirageFaible = BuildTiragePKMTypesDataTest(89.89, ResultatTirageStatus.Parfait);
            var cinquièmeTirageAcceptable = BuildTiragePKMTypesDataTest(77, ResultatTirageStatus.Acceptable);
            var sixiemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var septiemeTirageFaible = BuildTiragePKMTypesDataTest(69.0, ResultatTirageStatus.Acceptable);
            var huitiemeTirageAcceptable = BuildTiragePKMTypesDataTest(68.9, ResultatTirageStatus.Acceptable);
            var neuviemeTirageParfait = BuildTiragePKMTypesDataTest(100, ResultatTirageStatus.Parfait);
            var dixiemeTirageFaible = BuildTiragePKMTypesDataTest(65.65, ResultatTirageStatus.Acceptable);
            var precedentsTiragesSauvegardes = new List<TiragePKMTypes>() { premierTirageFaible, secondTirageAcceptable,
            troisiemeTirageParfait, quatriemeTirageFaible, cinquièmeTirageAcceptable, sixiemeTirageParfait,
            septiemeTirageFaible, huitiemeTirageAcceptable, neuviemeTirageParfait, dixiemeTirageFaible };
            var premierNouveauTirage = BuildTiragePKMTypesDataTest(premierNouveauTirageNote, ResultatTirageStatus.Acceptable);
            var secondNouveauTirage = BuildTiragePKMTypesDataTest(secondNouveauTirageNote, ResultatTirageStatus.Acceptable);

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

        private TiragePKMTypes BuildTiragePKMTypesDataTest(double NoteTirage, ResultatTirageStatus resultatTirageStatus)
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