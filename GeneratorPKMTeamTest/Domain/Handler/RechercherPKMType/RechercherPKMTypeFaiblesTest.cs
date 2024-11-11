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
            Assert.Equal("Feu", typesFaibles[0].Nom);
            Assert.Equal("Roche", typesFaibles[1].Nom);
            Assert.Equal("Sol", typesFaibles[2].Nom);
            Assert.Equal("Eau", typesFaibles[3].Nom);
            Assert.Equal("Combat", typesFaibles[4].Nom);
            Assert.Equal("Dragon", typesFaibles[5].Nom);
            Assert.Equal("Ténèbres", typesFaibles[6].Nom);
        }
    }
}