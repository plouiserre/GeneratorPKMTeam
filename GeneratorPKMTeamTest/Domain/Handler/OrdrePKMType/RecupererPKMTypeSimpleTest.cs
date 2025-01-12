using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeamTest.Utils.Helper;

namespace GeneratorPKMTeamTest.Domain.Handler.OrdrePKMTypeTest
{
    public class RecupererPKMTypeSimpleTest
    {
        private Dictionary<string, List<PKMType>> _tousLesTypesConstruits;
        [Fact]
        public void RecupererPKMTypeSimpleSansRienDejaSelectionne()
        {
            var recupererPKMTypeSimple = InitRecupererPKMTypeSimpleEttousLesTypesConstruits("Fée Dragon Combat Plante Feu Poison Glace Eau Psy Acier Ténèbres Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal");

            var typesDoublesRecuperes = recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, _tousLesTypesConstruits);

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
            var recupererPKMTypeSimple = InitRecupererPKMTypeSimpleEttousLesTypesConstruits("Fée Dragon Combat Plante Feu Eau Poison Glace Psy Acier Ténèbres Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal");
            var pkmTypesDoubleDejaRecuperes = new Dictionary<string, List<PKMType>>()
            {
                { "Dragon-Psy", new List<PKMType>() { new PKMType() { Nom = "Dragon" }, new PKMType() { Nom = "Psy" } }},
                { "Plante-Combat", new List<PKMType>() { new PKMType() { Nom = "Plante" }, new PKMType() { Nom = "Combat" } }},
                { "Poison-Spectre", new List<PKMType>() { new PKMType() { Nom = "Poison" }, new PKMType() { Nom = "Spectre" } }},
                { "Acier-Fée", new List<PKMType>() { new PKMType() { Nom = "Acier" }, new PKMType() { Nom = "Fée" } }},
            };
            recupererPKMTypeSimple.RecupererPKMTypeDoublesDejaCalcules(pkmTypesDoubleDejaRecuperes);

            var typesDoublesRecuperes = recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, _tousLesTypesConstruits);

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
            var recupererPKMTypeSimple = InitRecupererPKMTypeSimpleEttousLesTypesConstruits("Fée Dragon Plante Combat Feu Poison Glace Psy Eau Acier Ténèbres Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal");
            var pkmTypesDoubleDejaRecuperes = new Dictionary<string, List<PKMType>>()
            {
                { "Feu-Combat", new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }},
                { "Dragon-Psy", new List<PKMType>() { new PKMType() { Nom = "Dragon" }, new PKMType() { Nom = "Psy" } }},
                { "Poison-Spectre", new List<PKMType>() { new PKMType() { Nom = "Poison" }, new PKMType() { Nom = "Spectre" } }},
                { "Acier-Fée", new List<PKMType>() { new PKMType() { Nom = "Acier" }, new PKMType() { Nom = "Fée" } }},
            };

            recupererPKMTypeSimple.RecupererPKMTypeDoublesDejaCalcules(pkmTypesDoubleDejaRecuperes);

            var typesDoublesRecuperes = recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }, _tousLesTypesConstruits);

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
            var recupererPKMTypeSimple = InitRecupererPKMTypeSimpleEttousLesTypesConstruits("Fée Dragon Combat Feu Poison Plante Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal");
            var pkmTypesDoubleDejaRecuperes = new Dictionary<string, List<PKMType>>()
            {
                { "Feu-Combat", new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }},
                { "Dragon-Psy", new List<PKMType>() { new PKMType() { Nom = "Dragon" }, new PKMType() { Nom = "Psy" } }},
                { "Poison-Spectre", new List<PKMType>() { new PKMType() { Nom = "Poison" }, new PKMType() { Nom = "Spectre" } }},
                { "Acier-Fée", new List<PKMType>() { new PKMType() { Nom = "Acier" }, new PKMType() { Nom = "Fée" } }},
            };

            recupererPKMTypeSimple.RecupererPKMTypeDoublesDejaCalcules(pkmTypesDoubleDejaRecuperes);

            var exception = Assert.Throws<PasAssezPKMTypeSimpleSelectionnableException>(() => recupererPKMTypeSimple.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Feu" }, new PKMType() { Nom = "Combat" } }, _tousLesTypesConstruits));

            Assert.Equal("Il n'y a pas assez eu de PKM Type Simple sélectionné pour faire une équipe de 6 PKM", exception.CustomMessage);
            Assert.Equal(TypeErreur.PasAssezPKMTypeSimpleSelectionnable, exception.TypeErreur);
        }

        private RecupererPKMTypeSimple InitRecupererPKMTypeSimpleEttousLesTypesConstruits(string tousLesTypesPossibles)
        {
            _tousLesTypesConstruits = PKMHelper.ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
            return recupererPKMTypeSimple;
        }

    }
}