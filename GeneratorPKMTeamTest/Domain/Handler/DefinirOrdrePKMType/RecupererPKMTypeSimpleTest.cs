using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;

namespace GeneratorPKMTeamTest.Domain.Handler.OrdrePKMTypeTest
{
    public class RecupererPKMTypeSimpleTest
    {
        [Fact]
        public void RecupererPKMTypeSimpleSansRienDejaSelectionne()
        {
            string tousLesTypesPossibles = "Fée Dragon Combat Plante Feu Poison Glace Eau Psy Acier Ténèbres Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();

            var typesDoublesRecuperes = recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, tousLesTypesConstruits);

            Assert.Equal(6, typesDoublesRecuperes.Count);
            Assert.True(typesDoublesRecuperes.ContainsKey("Eau"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Fée"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Dragon"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Feu"));
        }


        [Fact]
        public void RecupererPKMTypeSimpleAvecDejaUneSelectionEtStarterSimple()
        {
            string tousLesTypesPossibles = "Fée Dragon Combat Plante Feu Eau Poison Glace Psy Acier Ténèbres Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var pkmTypesDoubleDejaRecuperes = new Dictionary<string, List<PKMType>>()
            {
                { "Dragon-Psy", new List<PKMType>() { new PKMType() { Nom = "Dragon" }, new PKMType() { Nom = "Psy" } }},
                { "Plante-Combat", new List<PKMType>() { new PKMType() { Nom = "Plante" }, new PKMType() { Nom = "Combat" } }},
                { "Poison-Spectre", new List<PKMType>() { new PKMType() { Nom = "Poison" }, new PKMType() { Nom = "Spectre" } }},
                { "Acier-Fée", new List<PKMType>() { new PKMType() { Nom = "Acier" }, new PKMType() { Nom = "Fée" } }},
            };

            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
            recupererPKMTypeSimple.RecupererPKMTypeDoublesDejaCalcules(pkmTypesDoubleDejaRecuperes);

            var typesDoublesRecuperes = recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, tousLesTypesConstruits);

            Assert.Equal(6, typesDoublesRecuperes.Count);
            Assert.True(typesDoublesRecuperes.ContainsKey("Dragon-Psy"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Poison-Spectre"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Acier-Fée"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Eau"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Feu"));
        }

        [Fact]
        public void RecupererPKMTypeSimpleAvecDejaUneSelectionEtStarterDouble()
        {
            string tousLesTypesPossibles = "Fée Dragon Plante Combat Feu Poison Glace Psy Eau Acier Ténèbres Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var pkmTypesDoubleDejaRecuperes = new Dictionary<string, List<PKMType>>()
            {
                { "Feu-Combat", new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }},
                { "Dragon-Psy", new List<PKMType>() { new PKMType() { Nom = "Dragon" }, new PKMType() { Nom = "Psy" } }},
                { "Poison-Spectre", new List<PKMType>() { new PKMType() { Nom = "Poison" }, new PKMType() { Nom = "Spectre" } }},
                { "Acier-Fée", new List<PKMType>() { new PKMType() { Nom = "Acier" }, new PKMType() { Nom = "Fée" } }},
            };

            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
            recupererPKMTypeSimple.RecupererPKMTypeDoublesDejaCalcules(pkmTypesDoubleDejaRecuperes);

            var typesDoublesRecuperes = recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }, tousLesTypesConstruits);

            Assert.Equal(6, typesDoublesRecuperes.Count);
            Assert.True(typesDoublesRecuperes.ContainsKey("Feu-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Dragon-Psy"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Poison-Spectre"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Acier-Fée"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Glace"));
        }

        [Fact]
        public void PasAssezPKMTypeSimpleSelectionnable()
        {
            string tousLesTypesPossibles = "Fée Dragon Combat Feu Poison Plante Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var pkmTypesDoubleDejaRecuperes = new Dictionary<string, List<PKMType>>()
            {
                { "Feu-Combat", new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }},
                { "Dragon-Psy", new List<PKMType>() { new PKMType() { Nom = "Dragon" }, new PKMType() { Nom = "Psy" } }},
                { "Poison-Spectre", new List<PKMType>() { new PKMType() { Nom = "Poison" }, new PKMType() { Nom = "Spectre" } }},
                { "Acier-Fée", new List<PKMType>() { new PKMType() { Nom = "Acier" }, new PKMType() { Nom = "Fée" } }},
            };

            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
            recupererPKMTypeSimple.RecupererPKMTypeDoublesDejaCalcules(pkmTypesDoubleDejaRecuperes);

            var exception = Assert.Throws<PasAssezPKMTypeSimpleSelectionnableException>(() => recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }, tousLesTypesConstruits));

            Assert.Equal("Il n'y a pas assez eu de PKM Type Simple sélectionné pour faire une équipe de 6 PKM", exception.CustomMessage);
            Assert.Equal(TypeErreur.PasAssezPKMTypeSimpleSelectionnable, exception.TypeErreur);
        }

        //TODO fusionner avec celle de RecupererPKMTypeDoubleTest 
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