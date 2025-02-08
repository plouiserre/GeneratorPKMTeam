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
            var tousPKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);
            var PKMTypesDangereux = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Eau", "Fée", "Roche", "Spectre", "Normal", "Insecte" });
            var EquipePKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string> { "Electrik", "Feu", "Plante", "Vol", "Poison", "Glace" });

            var rechercherPKMTypeDangereux = new RechercherPKMTypeDangereux(tousPKMTypes);
            var rechercherPKMTypeContres = new RechercherPKMTypeContres(EquipePKMTypes, rechercherPKMTypeDangereux);

            var PKMTypesContres = rechercherPKMTypeContres.TrouverPKMType(PKMTypesDangereux);

            Assert.Equal(4, PKMTypesContres.Count);
            Assert.Contains("Eau", PKMTypesContres.Select(o => o.Nom));
            Assert.Contains("Roche", PKMTypesContres.Select(o => o.Nom));
            Assert.Contains("Insecte", PKMTypesContres.Select(o => o.Nom));
            Assert.Contains("Fée", PKMTypesContres.Select(o => o.Nom));
        }
    }
}