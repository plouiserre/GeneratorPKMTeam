using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Infrastructure.Services;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam;
using NSubstitute;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeamTest.Utils.Personas;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class LoadPKMTypesTest
    {

        private PKMDatas _datasFake;

        public LoadPKMTypesTest()
        {
            _datasFake = PKMDatasPersonas.GetPersonas();
        }

        [Fact]
        public void LoadPKMTypesFromJson()
        {
            var persistence = Substitute.For<IPKMTypePersistence>();
            persistence.GetPKMDatas().Returns<PKMDatas>(_datasFake);
            var load = new LoadPKMTypes(persistence);

            var result = load.GetPKMDatas();

            Assert.NotNull(result);
            Assert.Equal(3, result.PKMTypes.Count);
            Assert.Equal("Feu", result.PKMTypes[0].Nom);
            Assert.Equal("Eau", result.PKMTypes[1].Nom);
            Assert.Equal("Plante", result.PKMTypes[2].Nom);
        }


        [Fact]
        public void ThrowsLoadingPKMTypesExceptionWhenLoadingFailed()
        {
            var persistence = Substitute.For<IPKMTypePersistence>();
            persistence.GetPKMDatas().Returns<PKMDatas>(new PKMDatas());
            var load = new LoadPKMTypes(persistence);

            var result = Assert.Throws<LoadingPKMTypesException>(() => load.GetPKMDatas());

            Assert.Equal("Aucune donnée n'a été récupérée", result.CustomMessage);
            Assert.Equal(ErrorType.NoPKMTypesData, result.ErrorType);
        }
    }
}