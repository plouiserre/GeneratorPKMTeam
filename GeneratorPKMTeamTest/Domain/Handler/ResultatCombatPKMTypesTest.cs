using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class ResultatCombatPKMTypesTest
    {
        private List<RelPKMType> peuPKMTypesFaibles;
        private List<RelPKMType> quelquesPKMTypesFaibles;
        private List<RelPKMType> bcpPKMTypesFaibles;
        private List<RelPKMType> peuPKMTypesDangereux;
        private List<RelPKMType> quelquesPKMTypesDangereux;
        private List<RelPKMType> bcpPKMTypesDangereux;

        public ResultatCombatPKMTypesTest()
        {
            var peuRelTypes = new List<RelPKMType>(){
                new RelPKMType(){TypePKM="Acier", ModeImpact=2},
                new RelPKMType(){TypePKM="Combat", ModeImpact=2},
                new RelPKMType(){TypePKM="Dragon", ModeImpact=2},
                new RelPKMType(){TypePKM="Eau", ModeImpact=2},
                new RelPKMType(){TypePKM="Electrik", ModeImpact=2}
            };
            peuPKMTypesFaibles = peuRelTypes;
            peuPKMTypesDangereux = peuRelTypes;
            var quelquesRelTypes = new List<RelPKMType>(peuRelTypes){
                new RelPKMType(){TypePKM="Fée", ModeImpact=2},
                new RelPKMType(){TypePKM="Feu", ModeImpact=2},
                new RelPKMType(){TypePKM="Glace", ModeImpact=2},
                new RelPKMType(){TypePKM="Insecte", ModeImpact=2},
                new RelPKMType(){TypePKM="Normal", ModeImpact=2},
                new RelPKMType(){TypePKM="Plante", ModeImpact=2},
                new RelPKMType(){TypePKM="Poison", ModeImpact=2},
            };
            quelquesPKMTypesFaibles = quelquesRelTypes;
            quelquesPKMTypesDangereux = quelquesRelTypes;
            var bcpRelTypes = new List<RelPKMType>(quelquesRelTypes){
                 new RelPKMType(){TypePKM="Psy", ModeImpact=2},
                new RelPKMType(){TypePKM="Roche", ModeImpact=2},
                new RelPKMType(){TypePKM="Sol", ModeImpact=2},
                new RelPKMType(){TypePKM="Spectre", ModeImpact=2},
                new RelPKMType(){TypePKM="Tenèbres", ModeImpact=2},
                new RelPKMType(){TypePKM="Vol", ModeImpact=2},
            };
            bcpPKMTypesFaibles = bcpRelTypes;
            bcpPKMTypesDangereux = bcpRelTypes;
        }
        [Fact]
        public void ResultatTiragePkmFaiblesPeuPkmDangereuxBcpResultatFaibles()
        {
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(peuPKMTypesFaibles, bcpPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Faible, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 20);
        }


        [Fact]
        public void ResultatTiragePKMFaiblesQuelquesPKMDangereuxQuelquesResultatAcceptable()
        {
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(quelquesPKMTypesFaibles, quelquesPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 60);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesBcpPKMDangereuxPeuResultatExcellent()
        {
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles, peuPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Excellent, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage > 80);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesFaiblesPKMDangereuxAcceptablesObtientResultatPassables()
        {
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(peuPKMTypesFaibles, quelquesPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Passables, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 40);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesQuelquesPKMDangereuxBcpObtientResultatPassables()
        {
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(quelquesPKMTypesFaibles, bcpPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Passables, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 40);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesBcpPkmDangereuxBcpObtientResultatAcceptables()
        {
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles, bcpPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 60);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesPeuPkmDangereuxPeuObtientResultatAcceptables()
        {
            var resultatCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultatCombatPKMTypes.NoterResultatTirage(peuPKMTypesFaibles, peuPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 60);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesBcpPkmDangereuxQuelquesObtientResultatBons()
        {
            var resultCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultCombatPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles, quelquesPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Bonnes, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 80);
        }

        [Fact]
        public void ResultatTiragePKMFaiblesQuelquesPkmDangereuxPeuObtientResultatBons()
        {
            var resultCombatPKMTypes = new ResultatCombatPKMTypes();

            var resultat = resultCombatPKMTypes.NoterResultatTirage(quelquesPKMTypesFaibles, peuPKMTypesDangereux);

            Assert.Equal(ResultatTirageStatus.Bonnes, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 80);
        }
    }
}