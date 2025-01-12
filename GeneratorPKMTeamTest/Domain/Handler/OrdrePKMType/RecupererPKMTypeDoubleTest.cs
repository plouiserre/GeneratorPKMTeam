using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler.OrdrePKMTypeTest
{
    public class RecupererPKMTypeDoubleTest
    {
        private Dictionary<string, List<PKMType>> _tousLesTypesConstruits;

        [Fact]
        public void RecupererPKMTypeDoubleAvecStarterSimple()
        {
            var recupererPKMTypeDouble = InitRecupererPKMTypeDoubleEttousLesTypesConstruits("Eau Plante Roche Combat Sol Fée Insecte Ténèbres Glace Plante-Combat Plante-Ténèbres Roche-Plante Roche-Sol Roche-Insecte Roche-Ténèbres Sol-Roche Insecte-Plante Insecte-Roche Insecte-Combat Insecte-Sol Ténèbres-Glace Glace-Sol");

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, _tousLesTypesConstruits);

            Assert.Equal(3, typesDoublesRecuperes.Count);
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Roche-Sol"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Ténèbres-Glace"));
        }

        [Fact]
        public void RecupererPKMTypeDoubleAvecStarterDouble()
        {
            var recupererPKMTypeDouble = InitRecupererPKMTypeDoubleEttousLesTypesConstruits("Combat Spectre Sol Normal Ténèbres Feu Glace Fée Plante-Poison Normal-Fée Ténèbres-Spectre Ténèbres-Feu Ténèbres-Glace Feu-Combat Feu-Sol Glace-Sol");

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Plante" },
                                        new PKMType() { Nom = "Poison" } }, _tousLesTypesConstruits);

            Assert.Equal(5, typesDoublesRecuperes.Count);
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante-Poison"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Normal-Fée"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Ténèbres-Spectre"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Feu-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Glace-Sol"));
        }

        [Fact]
        public void RecupererPKMTypeDoubleAvecTropTypeDouble()
        {
            var recupererPKMTypeDouble = InitRecupererPKMTypeDoubleEttousLesTypesConstruits("Fée Dragon Combat Plante Feu Poison Glace Psy Acier Ténèbres Eau-Sol Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal");

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Vol" } }, _tousLesTypesConstruits);

            Assert.Equal(5, typesDoublesRecuperes.Count);
            Assert.True(typesDoublesRecuperes.ContainsKey("Eau-Sol"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Dragon-Psy"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Poison-Spectre"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Acier-Fée"));
            Assert.False(typesDoublesRecuperes.ContainsKey("Ténèbres-Feu"));
        }

        [Fact]
        public void RecupererPKMTypeDoubleAvecStarterSimpleEtRefusantDoubleTypeAvecStarter()
        {
            var recupererPKMTypeDouble = InitRecupererPKMTypeDoubleEttousLesTypesConstruits("Eau Plante Roche Combat Sol Fée Insecte Ténèbres Glace Plante-Combat Eau-Ténèbres Roche-Plante Roche-Sol Eau-Insecte Roche-Ténèbres Sol-Roche Insecte-Plante Insecte-Roche Insecte-Combat Insecte-Sol Ténèbres-Glace Glace-Sol");

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, _tousLesTypesConstruits);

            Assert.Equal(3, typesDoublesRecuperes.Count);
            Assert.False(typesDoublesRecuperes.ContainsKey("Eau-Ténèbres"));
            Assert.False(typesDoublesRecuperes.ContainsKey("Eau-Insecte"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Roche-Sol"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Ténèbres-Glace"));
        }

        private RecupererPKMTypeDouble InitRecupererPKMTypeDoubleEttousLesTypesConstruits(string tousLesTypesPossibles)
        {
            _tousLesTypesConstruits = PKMHelper.ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();
            return recupererPKMTypeDouble;
        }
    }
}