using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler.RechercherPKMType
{
    public class RechercherPKMTypeDangereuxTest
    {
        [Fact]
        public void TrouverPKMTypesDangereux()
        {
            List<PKMType> PKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Acier", "Combat", "Roche" });
            List<PKMType> tousPKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);

            var rechercherPKMDangereux = new RechercherPKMTypeDangereux(tousPKMTypes);

            var typesDangereux = rechercherPKMDangereux.TrouverPKMType(PKMTypes);

            Assert.Equal(9, typesDangereux.Count);
            Assert.Equal("Acier", typesDangereux[0].TypePKM);
            Assert.Equal(2, typesDangereux[0].ModeImpact);
            Assert.Equal("Combat", typesDangereux[1].TypePKM);
            Assert.Equal(2, typesDangereux[1].ModeImpact);
            Assert.Equal("Eau", typesDangereux[2].TypePKM);
            Assert.Equal(2, typesDangereux[2].ModeImpact);
            Assert.Equal("Fée", typesDangereux[3].TypePKM);
            Assert.Equal(2, typesDangereux[3].ModeImpact);
            Assert.Equal("Feu", typesDangereux[4].TypePKM);
            Assert.Equal(2, typesDangereux[4].ModeImpact);
            Assert.Equal("Plante", typesDangereux[5].TypePKM);
            Assert.Equal(2, typesDangereux[5].ModeImpact);
            Assert.Equal("Psy", typesDangereux[6].TypePKM);
            Assert.Equal(2, typesDangereux[6].ModeImpact);
            Assert.Equal("Sol", typesDangereux[7].TypePKM);
            Assert.Equal(2, typesDangereux[7].ModeImpact);
            Assert.Equal("Vol", typesDangereux[8].TypePKM);
            Assert.Equal(2, typesDangereux[8].ModeImpact);
        }
    }
}