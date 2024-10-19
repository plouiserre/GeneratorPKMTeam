using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeamTest.Utils.Personas;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class SelectPKMTypesTest
    {
        private PKMDatas _datasFake;

        public SelectPKMTypesTest()
        {
            _datasFake = PKMDatasPersonas.GetPersonas();
        }

        [Fact]
        public void ShouldBeReturnNinePKMTypes()
        {
            var selectPKMTypes = new SelectPKMTypes();

            var result = selectPKMTypes.ChoosePKMTypes(_datasFake);

            Assert.Equal(9, result.Count);
        }

        [Fact]
        public void ShouldBeReturnNineDifferentsPKMTypes()
        {
            var selectPKMTypes = new SelectPKMTypes();

            var result = selectPKMTypes.ChoosePKMTypes(_datasFake);

            for (int i = 0; i < 9; i++)
            {
                if (i < 8)
                {
                    Assert.NotEqual(result[i].Nom, result[i + 1].Nom);
                }
            }
        }

        [Fact]
        public void ShouldBeReturnDifferentsResultInEachRun()
        {
            var selectPKMTypes = new SelectPKMTypes();

            var firstResult = selectPKMTypes.ChoosePKMTypes(_datasFake);
            var secondResult = selectPKMTypes.ChoosePKMTypes(_datasFake);

            for (int i = 0; i < 9; i++)
            {
                Assert.NotEqual(firstResult[i].Nom, secondResult[i].Nom);
            }
        }
    }
}