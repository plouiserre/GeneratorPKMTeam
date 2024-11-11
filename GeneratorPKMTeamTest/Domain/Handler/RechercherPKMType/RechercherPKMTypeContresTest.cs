using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler.RechercherPKMType
{
    public class RechercherPKMTypeContresTest
    {
        [Fact]
        public void TrouverPKMTypesContres()
        {
            var PKMTypesATK = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Eau", "Fée", "Roche", "Spectre", "Normal", "Insecte" });
            var PKMTypesDEF = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Electrik", "Feu", "Plante", "Vol", "Poison", "Glace" });

            var rechercherPKMTypeFaibles = new RechercherPKMTypeFaibles();
            var rechercherPKMTypeContres = new RechercherPKMTypeContres(PKMTypesATK, rechercherPKMTypeFaibles);

            var PKMTypesContres = rechercherPKMTypeContres.TrouverPKMType(PKMTypesDEF);

            Assert.Equal(4, PKMTypesContres.Count);
            Assert.Contains("Eau", PKMTypesContres.Select(o => o.Nom));
            Assert.Contains("Roche", PKMTypesContres.Select(o => o.Nom));
            Assert.Contains("Insecte", PKMTypesContres.Select(o => o.Nom));
            Assert.Contains("Fée", PKMTypesContres.Select(o => o.Nom));
        }
    }
}