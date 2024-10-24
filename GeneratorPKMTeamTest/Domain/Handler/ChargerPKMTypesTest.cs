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
    public class ChargerPKMTypesTest
    {

        private PKMDonnees _datasFake;

        public ChargerPKMTypesTest()
        {
            _datasFake = PKMDonneesPersonas.GetPersonas();
        }

        [Fact]
        public void ChargerPKMTypesFromJson()
        {
            string[] expectedTypes = {
                    "Acier", "Combat", "Dragon", "Eau", "Electrik", "Fée",
                    "Feu", "Glace", "Insecte", "Normal", "Plante", "Poison",
                    "Psy", "Roche", "Sol", "Spectre", "Ténèbres", "Vol"
                };
            var persistence = Substitute.For<IPKMTypePersistence>();
            persistence.GetPKMDonnees().Returns<PKMDonnees>(_datasFake);
            var load = new ChargerPKMTypes(persistence);

            var result = load.AvoirPKMDatas();

            Assert.NotNull(result);
            Assert.Equal(18, result.PKMTypes.Count);
            for (int i = 0; i < expectedTypes.Length; i++)
            {
                Assert.Equal(expectedTypes[i], result.PKMTypes[i].Nom);
            }
        }


        [Fact]
        public void LancerChargementTypesPKMExceptionQuandChargementEchoue()
        {
            var persistence = Substitute.For<IPKMTypePersistence>();
            persistence.GetPKMDonnees().Returns<PKMDonnees>(new PKMDonnees());
            var load = new ChargerPKMTypes(persistence);

            var result = Assert.Throws<ChargementTypesPKMException>(() => load.AvoirPKMDatas());

            Assert.Equal("Aucune donnée n'a été récupérée", result.CustomMessage);
            Assert.Equal(TypeErreur.NoPKMTypesData, result.TypeErreur);
        }
    }
}