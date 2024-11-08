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
            Assert.Equal("Acier", typesDangereux[0].Nom);
            Assert.Equal("Combat", typesDangereux[1].Nom);
            Assert.Equal("Eau", typesDangereux[2].Nom);
            Assert.Equal("Fée", typesDangereux[3].Nom);
            Assert.Equal("Feu", typesDangereux[4].Nom);
            Assert.Equal("Plante", typesDangereux[5].Nom);
            Assert.Equal("Psy", typesDangereux[6].Nom);
            Assert.Equal("Sol", typesDangereux[7].Nom);
            Assert.Equal("Vol", typesDangereux[8].Nom);
        }
    }
}