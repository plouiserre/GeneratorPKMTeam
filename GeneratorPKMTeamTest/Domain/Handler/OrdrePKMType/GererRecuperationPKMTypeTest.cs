using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace GeneratorPKMTeamTest.Domain.Handler.OrdrePKMTypeTest
{
    public class GererRecuperationPKMTypeTest
    {
        [Fact]
        public void PKMTypeSimpleEtDoubleBienRecupere()
        {
            string tousLesTypesPossibles = "Eau Plante Roche Combat Sol Fée Insecte Ténèbres Glace Plante-Combat Plante-Ténèbres Roche-Plante Roche-Sol Roche-Insecte Roche-Ténèbres Sol-Roche Insecte-Plante Insecte-Roche Insecte-Combat Insecte-Sol Ténèbres-Glace Glace-Sol";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);

            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();
            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
            var gerer = new GererRecuperationPKMType(recupererPKMTypeDouble, recupererPKMTypeSimple);

            var pkmTypesRecuperes = gerer.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, tousLesTypesConstruits);

            Assert.Equal(6, pkmTypesRecuperes.Count);
            Assert.True(pkmTypesRecuperes.ContainsKey("Plante-Combat"));
            Assert.True(pkmTypesRecuperes.ContainsKey("Roche-Sol"));
            Assert.True(pkmTypesRecuperes.ContainsKey("Ténèbres-Glace"));
            Assert.True(pkmTypesRecuperes.ContainsKey("Eau"));
            Assert.True(pkmTypesRecuperes.ContainsKey("Fée"));
            Assert.True(pkmTypesRecuperes.ContainsKey("Insecte"));
        }

        [Fact]
        public void PKMTypeSimpleEtDoubleMalRecupere()
        {
            string tousLesTypesPossibles = "Combat Spectre Sol Normal Ténèbres Feu Glace Fée Plante-Poison Normal-Fée Ténèbres-Spectre Ténèbres-Feu Ténèbres-Glace Feu-Combat Feu-Sol Glace-Sol";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);

            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();
            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
            var gerer = new GererRecuperationPKMType(recupererPKMTypeDouble, recupererPKMTypeSimple);

            var pkmTypesRecuperes = gerer.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Plante" }, new PKMType() { Nom = "Poison" } }, tousLesTypesConstruits);

            Assert.Equal(6, pkmTypesRecuperes.Count);
            for (int i = 0; i < pkmTypesRecuperes.Count; i++)
            {
                for (int j = 0; j < pkmTypesRecuperes.Count; j++)
                {
                    if (i == j)
                        continue;
                    Assert.NotEqual(pkmTypesRecuperes.ElementAt(i).Value[0].Nom, pkmTypesRecuperes.ElementAt(j).Value[0].Nom);
                    if (pkmTypesRecuperes.ElementAt(i).Value.Count == 2 && pkmTypesRecuperes.ElementAt(j).Value.Count == 2)
                        Assert.NotEqual(pkmTypesRecuperes.ElementAt(i).Value[1].Nom, pkmTypesRecuperes.ElementAt(j).Value[1].Nom);
                }
            }
        }

        [Fact]
        public void PKMTypeSimpleEtDoubleBienRecupereAvecThrowsPasAssezPKMTypeSimpleSelectionnableException()
        {
            string tousLesTypesPossibles = "Eau Acier Dragon Poison Combat Sol Eau-Dragon Eau-Poison Eau-Combat Eau-Sol Acier-Sol Poison-Sol Sol-Dragon";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);

            var gerer = new GererRecuperationPKMType(new RecupererPKMTypeDouble(), new RecupererPKMTypeSimple());

            var pkmTypesRecuperes = gerer.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Vol" } }, tousLesTypesConstruits);

            Assert.Equal(6, pkmTypesRecuperes.Count);
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Feu")));
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Vol")));
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Eau")));
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Dragon")));
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Acier")));
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Poison")));
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Combat")));
            Assert.True(pkmTypesRecuperes.Keys.Any(o => o.Contains("Sol")));

        }


        private Dictionary<string, List<PKMType>> ConstruireTousLesTypesPossibles(string tousLesTypesPossibles)
        {
            Dictionary<string, List<PKMType>> tousLesTypesConstruits = new Dictionary<string, List<PKMType>>();
            string[] typesDecomposes = tousLesTypesPossibles.Split(' ');
            foreach (string type in typesDecomposes)
            {
                if (type.Contains('-'))
                {
                    string[] types = type.Split('-');
                    tousLesTypesConstruits.Add(type, new List<PKMType>() {
                        new PKMType() { Nom = types[0] },
                        new PKMType() { Nom = types[1] }
                        });
                }
                else
                {
                    tousLesTypesConstruits.Add(type, new List<PKMType>() { new PKMType() { Nom = type } });
                }
            }
            return tousLesTypesConstruits;
        }
    }
}