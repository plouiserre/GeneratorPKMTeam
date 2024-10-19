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
            string[] expectedTypes = {
                    "Acier", "Combat", "Dragon", "Eau", "Electrik", "Fée",
                    "Feu", "Glace", "Insecte", "Normal", "Plante", "Poison",
                    "Psy", "Roche", "Sol", "Spectre", "Ténèbres", "Vol"
                };
            var persistence = Substitute.For<IPKMTypePersistence>();
            persistence.GetPKMDatas().Returns<PKMDatas>(_datasFake);
            var load = new LoadPKMTypes(persistence);

            var result = load.GetPKMDatas();

            Assert.NotNull(result);
            Assert.Equal(18, result.PKMTypes.Count);
            for (int i = 0; i < expectedTypes.Length; i++)
            {
                Assert.Equal(expectedTypes[i], result.PKMTypes[i].Nom);
            }
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