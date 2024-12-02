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
        public void GererTirageExcellentParfait()
        {
            var gererTirage = new GererResultatTiragePKMTypes();
            var tirageATraiter = new TiragePKMTypes()
            {
                ResultatTirageStatus = ResultatTirageStatus.Excellent,
                NoteTirage = 95,
                PKMTypes = new List<PKMType>()
            };

            bool accepter = gererTirage.GarderTirage(tirageATraiter);

            Assert.True(accepter);
        }

        [Fact]
        public void GererTirageAcceptable()
        {
            var gererTirage = new GererResultatTiragePKMTypes();
            var tirageATraiter = new TiragePKMTypes()
            {
                ResultatTirageStatus = ResultatTirageStatus.Acceptable,
                NoteTirage = 62,
                PKMTypes = new List<PKMType>()
            };

            bool accepter = gererTirage.GarderTirage(tirageATraiter);

            Assert.False(accepter);
        }

        [Fact]
        public void GererTirageFaible()
        {
            var gererTirage = new GererResultatTiragePKMTypes();
            var tirageATraiter = new TiragePKMTypes()
            {
                ResultatTirageStatus = ResultatTirageStatus.Faible,
                NoteTirage = 22,
                PKMTypes = new List<PKMType>()
            };

            bool accepter = gererTirage.GarderTirage(tirageATraiter);

            Assert.False(accepter);
        }
    }
}