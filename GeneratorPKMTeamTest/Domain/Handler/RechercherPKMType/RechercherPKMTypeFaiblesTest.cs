using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler.RechercherPKMType
{
    public class RechercherPKMTypeFaiblesTest
    {
        [Fact]
        public void TrouverPKMTypesFaibles()
        {
            List<PKMType> PKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Eau", "Plante", "Fée" });

            var rechercherPKMTypeFaibles = new RechercherPKMTypeFaibles();

            var typesFaibles = rechercherPKMTypeFaibles.TrouverPKMType(PKMTypes);

            Assert.Equal(7, typesFaibles.Count);
            Assert.Equal("Feu", typesFaibles[0].TypePKM);
            Assert.Equal(2, typesFaibles[0].ModeImpact);
            Assert.Equal("Roche", typesFaibles[1].TypePKM);
            Assert.Equal(2, typesFaibles[1].ModeImpact);
            Assert.Equal("Sol", typesFaibles[2].TypePKM);
            Assert.Equal(2, typesFaibles[2].ModeImpact);
            Assert.Equal("Eau", typesFaibles[3].TypePKM);
            Assert.Equal(2, typesFaibles[3].ModeImpact);
            Assert.Equal("Combat", typesFaibles[4].TypePKM);
            Assert.Equal(2, typesFaibles[4].ModeImpact);
            Assert.Equal("Dragon", typesFaibles[5].TypePKM);
            Assert.Equal(2, typesFaibles[5].ModeImpact);
            Assert.Equal("Ténèbres", typesFaibles[6].TypePKM);
            Assert.Equal(2, typesFaibles[6].ModeImpact);
        }
    }
}