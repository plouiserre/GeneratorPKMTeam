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
        private PKMDonnees _datasFake;

        public SelectPKMTypesTest()
        {
            _datasFake = PKMDonneesPersonas.GetPersonas();
        }

        [Fact]
        public void DoitRetournerNeufPKMTypes()
        {
            var selectPKMTypes = new ChoisirPKMTypes();

            var result = selectPKMTypes.SelectionnerPKMTypes(_datasFake);

            Assert.Equal(9, result.Count);
        }

        [Fact]
        public void DoitRetournerNeufDifferentsPKMTypes()
        {
            var selectPKMTypes = new ChoisirPKMTypes();

            var result = selectPKMTypes.SelectionnerPKMTypes(_datasFake);

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
            var selectPKMTypes = new ChoisirPKMTypes();

            var firstResult = selectPKMTypes.SelectionnerPKMTypes(_datasFake);
            var secondResult = selectPKMTypes.SelectionnerPKMTypes(_datasFake);

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
    }
}