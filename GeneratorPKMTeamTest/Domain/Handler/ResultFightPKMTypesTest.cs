using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class ResultFightPKMTypesTest
    {
        private List<RelPKMType> peuPKMTypesFaibles;
        private List<RelPKMType> quelquesPKMTypesFaibles;
        private List<RelPKMType> bcpPKMTypesFaibles;

        public ResultFightPKMTypesTest()
        {
            peuPKMTypesFaibles = new List<RelPKMType>(){
                new RelPKMType(){TypePKM="Acier", ModeImpact=2},
                new RelPKMType(){TypePKM="Combat", ModeImpact=2},
                new RelPKMType(){TypePKM="Dragon", ModeImpact=2},
                new RelPKMType(){TypePKM="Eau", ModeImpact=2},
                new RelPKMType(){TypePKM="Electrik", ModeImpact=2}
            };
            quelquesPKMTypesFaibles = new List<RelPKMType>(peuPKMTypesFaibles){
                new RelPKMType(){TypePKM="Fée", ModeImpact=2},
                new RelPKMType(){TypePKM="Feu", ModeImpact=2},
                new RelPKMType(){TypePKM="Glace", ModeImpact=2},
                new RelPKMType(){TypePKM="Insecte", ModeImpact=2},
                new RelPKMType(){TypePKM="Normal", ModeImpact=2},
                new RelPKMType(){TypePKM="Plante", ModeImpact=2},
                new RelPKMType(){TypePKM="Poison", ModeImpact=2},
            };
            bcpPKMTypesFaibles = new List<RelPKMType>(quelquesPKMTypesFaibles){
                 new RelPKMType(){TypePKM="Psy", ModeImpact=2},
                new RelPKMType(){TypePKM="Roche", ModeImpact=2},
                new RelPKMType(){TypePKM="Sol", ModeImpact=2},
                new RelPKMType(){TypePKM="Spectre", ModeImpact=2},
                new RelPKMType(){TypePKM="Tenèbres", ModeImpact=2},
            };
        }
        [Fact]
        public void ResultatTirageFaible()
        {
            var resultFightPKMTypes = new ResultFightPKMTypes();

            var resultat = resultFightPKMTypes.NoterResultatTirage(peuPKMTypesFaibles);

            Assert.Equal(ResultatTirageStatus.Faible, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 30);
        }


        [Fact]
        public void ResultatTirageAcceptable()
        {
            var resultFightPKMTypes = new ResultFightPKMTypes();

            var resultat = resultFightPKMTypes.NoterResultatTirage(quelquesPKMTypesFaibles);

            Assert.Equal(ResultatTirageStatus.Acceptable, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage < 80);
        }

        [Fact]
        public void ResultatTirageParfait()
        {
            var resultFightPKMTypes = new ResultFightPKMTypes();

            var resultat = resultFightPKMTypes.NoterResultatTirage(bcpPKMTypesFaibles);

            Assert.Equal(ResultatTirageStatus.Parfait, resultat.ResultatStatus);
            Assert.True(resultat.NoteResultatTirage > 80);
        }
    }
}