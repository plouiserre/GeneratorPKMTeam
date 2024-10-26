using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class CombattrePKMTypesTest
    {
        public CombattrePKMTypesTest()
        {

        }

        [Fact]
        public void DoitRetournerTouslesPkmTypesFaiblesDeCeType()
        {
            List<PKMType> PKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Eau", "Plante", "Fée" });

            var combattrePkmType = new CombattrePKMTypes();

            var typesFaibles = combattrePkmType.RetournerTousFaiblesPKMTypes(PKMTypes);

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

        [Fact]
        public void DoitRetournerTouslesPKMDangereux()
        {
            List<PKMType> PKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(new List<string>() { "Acier", "Combat", "Roche" });
            List<PKMType> tousPKMTypes = DatasHelperTest.RetournerDonneesPKMTypes(null);

            var combattrePkmType = new CombattrePKMTypes();

            var typesDangereux = combattrePkmType.RetournerPKMTypesDangereux(tousPKMTypes, PKMTypes);

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