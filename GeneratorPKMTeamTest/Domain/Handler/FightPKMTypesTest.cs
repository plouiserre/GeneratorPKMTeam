using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class FightPKMTypesTest
    {
        [Fact]
        public void ShouldBeReturnAllWeakPkmTypes()
        {
            var eauPkmType = new PKMType();
            eauPkmType.Nom = "Eau";
            eauPkmType.RelPKMTypes = new List<RelPKMType>(){
                new RelPKMType(){TypePKM="Plante", ModeImpact=0.5f},
                new RelPKMType(){TypePKM="Fée", ModeImpact=1},
                new RelPKMType(){TypePKM="Feu", ModeImpact=2},
                new RelPKMType(){TypePKM="Poison", ModeImpact=1},
                new RelPKMType(){TypePKM="Roche", ModeImpact=2},
                new RelPKMType(){TypePKM="Dragon", ModeImpact=0.5f}
            };

            var plantePKMType = new PKMType();
            plantePKMType.Nom = "Plante";
            plantePKMType.RelPKMTypes = new List<RelPKMType>(){
                new RelPKMType(){TypePKM="Eau", ModeImpact=2},
                new RelPKMType(){TypePKM="Electrik", ModeImpact=1},
                new RelPKMType(){TypePKM="Feu",ModeImpact=0.5f},
                new RelPKMType(){TypePKM="Vol",ModeImpact=0.5f},
                new RelPKMType(){TypePKM="Roche", ModeImpact=2},
                new RelPKMType(){TypePKM="Psy", ModeImpact=1}
            };

            var feePkmType = new PKMType();
            feePkmType.Nom = "Fée";
            feePkmType.RelPKMTypes = new List<RelPKMType>(){
                new RelPKMType(){TypePKM="Insecte", ModeImpact=1},
                new RelPKMType(){TypePKM="Combat", ModeImpact=2},
                new RelPKMType(){TypePKM="Poison",ModeImpact=0.5f},
                new RelPKMType(){TypePKM="Spectre",ModeImpact=1},
                new RelPKMType(){TypePKM="Acier", ModeImpact=0.5f},
                new RelPKMType(){TypePKM="Dragon", ModeImpact=2}
            };

            var PKMTypes = new List<PKMType>();
            PKMTypes.Add(eauPkmType);
            PKMTypes.Add(plantePKMType);
            PKMTypes.Add(feePkmType);

            //init class
            var fightPkmType = new FightPKMTypes();

            var typesFaibles = fightPkmType.RetournerTousFaiblesPKMTypes(PKMTypes);

            Assert.Equal(5, typesFaibles.Count);
            Assert.Equal("Feu", typesFaibles[0].TypePKM);
            Assert.Equal(2, typesFaibles[0].ModeImpact);
            Assert.Equal("Roche", typesFaibles[1].TypePKM);
            Assert.Equal(2, typesFaibles[1].ModeImpact);
            Assert.Equal("Eau", typesFaibles[2].TypePKM);
            Assert.Equal(2, typesFaibles[2].ModeImpact);
            Assert.Equal("Combat", typesFaibles[3].TypePKM);
            Assert.Equal(2, typesFaibles[3].ModeImpact);
            Assert.Equal("Dragon", typesFaibles[4].TypePKM);
            Assert.Equal(2, typesFaibles[4].ModeImpact);
        }
    }
}