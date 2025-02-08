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

        [Fact]
        public void TrouverPKMTypesDangereuxPourChaquePKMType()
        {
            List<PKMType> PKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Acier", "Combat", "Roche" });
            List<PKMType> tousPKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);

            var rechercherPKMDangereux = new RechercherPKMTypeDangereux(tousPKMTypes);

            var typesDangereux = rechercherPKMDangereux.TrouverPKMTypeDangereuxPourChaqueType(PKMTypes);
            var acierPKMType = typesDangereux.First(o => o.Key.Nom == "Acier");
            var combatPKMType = typesDangereux.First(o => o.Key.Nom == "Combat");
            var rochePKMType = typesDangereux.First(o => o.Key.Nom == "Roche");

            Assert.Equal(3, typesDangereux.Count);
            Assert.Equal("Combat", acierPKMType.Value[0].Nom);
            Assert.Equal("Feu", acierPKMType.Value[1].Nom);
            Assert.Equal("Sol", acierPKMType.Value[2].Nom);

            Assert.Equal("Fée", combatPKMType.Value[0].Nom);
            Assert.Equal("Psy", combatPKMType.Value[1].Nom);
            Assert.Equal("Vol", combatPKMType.Value[2].Nom);

            Assert.Equal("Acier", rochePKMType.Value[0].Nom);
            Assert.Equal("Combat", rochePKMType.Value[1].Nom);
            Assert.Equal("Eau", rochePKMType.Value[2].Nom);
            Assert.Equal("Plante", rochePKMType.Value[3].Nom);
            Assert.Equal("Sol", rochePKMType.Value[4].Nom);
        }
    }
}