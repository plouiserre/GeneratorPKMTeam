using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeamTest.Utils.Personas;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class DefinirOrdrePKMTypeTest
    {
        private List<PKMType> PKMTypesPKM;

        public DefinirOrdrePKMTypeTest()
        {
            PKMTypesPKM = PKMDonneesPersonas.RecupererTypesPKM(9);
        }

        [Fact]
        public void OnObtientBienSixRegroupementTypesPKM()
        {
            var definirOrdre = new DefinirOrdrePKMType();

            Dictionary<int, List<PKMType>> regroupmentTypesPKM = definirOrdre.Generer(PKMTypesPKM);

            Assert.Equal(6, regroupmentTypesPKM.Count);
        }

        [Fact]
        public void OnRecupereBienTousTypesPKMEnParametresDansRegroupement()
        {
            //"Acier", "Combat", "Dragon", "Eau", "Electrik", "Fée", "Feu", "Glace", "Insecte"
            var definirOrdre = new DefinirOrdrePKMType();

            Dictionary<int, List<PKMType>> regroupmentTypesPKM = definirOrdre.Generer(PKMTypesPKM);

            foreach (var regroupement in regroupmentTypesPKM)
            {
                Assert.True(regroupement.Value.Count > 0);
                foreach (var pkmTypeRegroupement in regroupement.Value)
                {
                    Assert.Contains(pkmTypeRegroupement.Nom, PKMTypesPKM.Select(o => o.Nom));
                }
            }
        }

        [Fact]
        public void OnRecupereBienDesDoublesTypesPourLesResultats356()
        {
            var definirOrdre = new DefinirOrdrePKMType();

            Dictionary<int, List<PKMType>> regroupmentTypesPKM = definirOrdre.Generer(PKMTypesPKM);

            Assert.Equal(2, regroupmentTypesPKM[2].Count);
            Assert.Equal(2, regroupmentTypesPKM[4].Count);
            Assert.Equal(2, regroupmentTypesPKM[5].Count);
        }

        [Fact]
        public void OnRecupereBienDesTypesDansDesOrdresDifferents()
        {
            var definirOrdre = new DefinirOrdrePKMType();

            Dictionary<int, List<PKMType>> premierRegroupmentTypesPKM = definirOrdre.Generer(PKMTypesPKM);
            Dictionary<int, List<PKMType>> deuxièmeRegroupmentTypesPKM = definirOrdre.Generer(PKMTypesPKM);

            Assert.NotEqual(premierRegroupmentTypesPKM[0][0].Nom, deuxièmeRegroupmentTypesPKM[0][0].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[1][0].Nom, deuxièmeRegroupmentTypesPKM[1][0].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[2][0].Nom, deuxièmeRegroupmentTypesPKM[2][0].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[2][1].Nom, deuxièmeRegroupmentTypesPKM[2][1].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[3][0].Nom, deuxièmeRegroupmentTypesPKM[3][0].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[4][0].Nom, deuxièmeRegroupmentTypesPKM[4][0].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[4][1].Nom, deuxièmeRegroupmentTypesPKM[4][1].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[5][0].Nom, deuxièmeRegroupmentTypesPKM[5][0].Nom);
            Assert.NotEqual(premierRegroupmentTypesPKM[5][1].Nom, deuxièmeRegroupmentTypesPKM[5][1].Nom);
        }
    }
}