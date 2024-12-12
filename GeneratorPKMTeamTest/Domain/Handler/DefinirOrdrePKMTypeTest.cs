using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Models;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class DefinirOrdrePKMTypeTest
    {
        private PKM _starterPKM;
        public DefinirOrdrePKMTypeTest()
        {
        }

        [Theory]
        [InlineData(1, new string[]{"Feu", "Eau", "Psy", "Sol", "Roche", "Plante", "Poison", "Glace", "Vol",
        "Feu-Vol", "Eau-Vol", "Eau-Psy", "Eau-Poison", "Eau-Glace", "Sol-Roche", "Roche-Eau",
        "Roche-Vol","Roche-Sol", "Plante-Psy", "Plante-Poison", "Poison-Vol", "Poison-Sol", "Glace-Vol", "Glace-Psy"},
        "Carapuce", new string[] { "Eau" })]
        [InlineData(3, new string[]{"Eau", "Ténèbres", "Psy", "Sol", "Fée", "Plante", "Dragon", "Glace", "Insecte",
        "Eau-Ténèbres", "Eau-Psy", "Eau-Sol", "Eau-Fée", "Eau-Plante", "Eau-Dragon", "Eau-Glace", "Ténèbres-Glace",
        "Psy-Fée", "Psy-Plante", "Sol-Psy", "Sol-Dragon", "Plante-Ténèbres", "Plante-Psy", "Dragon-Psy", "Glace-Eau",
        "Glace-Psy", "Glace-Sol", "Insecte-Eau", "Insecte-Sol", "Insecte-Plante"}, "Laggron", new string[] { "Eau", "Sol" })]
        [InlineData(6, new string[]{"Electrique", "Spectre", "Feu", "Acier", "Roche", "Combat", "Normal", "Vol", "Poison",
        "Electrique-Spectre", "Electrique-Acier", "Electrique-Normal", "Electrique-Vol", "Spectre-Feu", "Spectre-Vol",
        "Spectre-Poison", "Feu-Acier", "Feu-Roche", "Feu-Combat", "Feu-Normal", "Feu-Vol", "Acier-Spectre", "Acier-Roche",
        "Acier-Combat", "Acier-Vol", "Roche-Acier", "Roche-Combat", "Roche-Vol", "Combat-Acier", "Combat-Vol", "Normal-Vol",
        "Poison-Combat", "Poison-Vol"}, "Feunec", new string[] { "Feu" })]
        [InlineData(3, new string[]{"Insecte", "Feu", "Dragon", "Roche", "Ténèbres", "Psy", "Plante-Poison",
        "Insecte-Roche", "Feu-Roche", "Dragon-Psy", "Roche-Insecte", "Roche-Ténèbres", "Roche-Psy", "Ténèbres-Feu"},
        "Bulbizarre", new string[] { "Plante", "Poison" })]
        public void OnObtientPour6PkmsParGeneration9TypesOuDoublesTypes(int generation, string[] _toutesLesCombinaisonsPossibles, string nomStarter, string[] typesStarter)
        {
            _starterPKM = ConstruireStarter(nomStarter, generation, typesStarter);
            var gererStarterPKM = Substitute.For<IGererStarterPKM>();
            gererStarterPKM.RecupererStarter().Returns(_starterPKM);
            var pkmTypes = GenererPKMTypesListes(_toutesLesCombinaisonsPossibles);
            var tousLesTypesPossibles = AvoirTousLesTypesPossibles(_toutesLesCombinaisonsPossibles);
            var determinerTousLesTypesExistant = Substitute.For<IDeterminerTousLesTypesExistant>();
            determinerTousLesTypesExistant.Calculer(generation, pkmTypes).Returns(tousLesTypesPossibles);

            var definirOrdre = new DefinirOrdrePKMType(determinerTousLesTypesExistant, gererStarterPKM, generation);
            Dictionary<int, List<PKMType>> regroupementTypesPKM = definirOrdre.Generer(pkmTypes);

            AssertOrdrePKMType(regroupementTypesPKM, pkmTypes);
        }

        private PKM ConstruireStarter(string nom, int generation, string[] types)
        {
            return types.Length == 1 ? new PKM()
            {
                Nom = nom,
                Generation = generation,
                PKMTypes = new List<string>()
            { types[0] }
            } : new PKM() { Nom = nom, Generation = generation, PKMTypes = new List<string>() { types[0], types[1] } };
        }

        private Dictionary<string, List<PKMType>> AvoirTousLesTypesPossibles(string[] pkmTypes)
        {
            var tousLesTypesPossibles = new Dictionary<string, List<PKMType>>();
            foreach (var pkmType in pkmTypes)
            {
                if (pkmType.Contains('-'))
                {
                    string[] deuxPKMTypes = pkmType.Split('-');
                    tousLesTypesPossibles.Add(pkmType, new List<PKMType>() { new PKMType() { Nom = deuxPKMTypes[0] }, new PKMType() { Nom = deuxPKMTypes[1] } });
                }
                else
                {
                    tousLesTypesPossibles.Add(pkmType, new List<PKMType>() { new PKMType() { Nom = pkmType } });
                }
            }
            return tousLesTypesPossibles;
        }

        private List<PKMType> GenererPKMTypesListes(string[] nomPKMTypes)
        {
            var types = new List<PKMType>();
            foreach (string nom in nomPKMTypes)
            {
                if (!nom.Contains("-"))
                    types.Add(new PKMType() { Nom = nom });
                else
                {
                    var noms = nom.Split("-");
                    var typeNoms = types.Select(o => o.Nom);
                    if (!typeNoms.Contains(noms[0]))
                    {
                        types.Add(new PKMType() { Nom = noms[0] });
                    }
                    if (!typeNoms.Contains(noms[1]))
                    {
                        types.Add(new PKMType() { Nom = noms[1] });
                    }
                }
            }
            return types;
        }

        private void AssertOrdrePKMType(Dictionary<int, List<PKMType>> regroupementTypesPKM, List<PKMType> pkmTypes)
        {
            var pkmTypesNoms = pkmTypes.Select(o => o.Nom);

            Assert.True(regroupementTypesPKM.Count > 0);

            AssertStarterPKMTypesRetournes(regroupementTypesPKM);

            AssertBonsPKMTypesRetournes(regroupementTypesPKM, pkmTypesNoms);

            AssertPKMTypesRetournesUneSeuleFois(regroupementTypesPKM);
        }

        private void AssertStarterPKMTypesRetournes(Dictionary<int, List<PKMType>> regroupementTypesPKM)
        {
            var pkmTypesStarter = GenererPKMTypesListes(_starterPKM.PKMTypes.ToArray());
            var premierTypeStarterAttendu = pkmTypesStarter[0];
            var premierTypeStarterCalculer = regroupementTypesPKM[1][0];
            Assert.Equal(pkmTypesStarter.Count, regroupementTypesPKM[1].Count);
            if (pkmTypesStarter.Count == 1)
            {
                Assert.Equal(premierTypeStarterAttendu.Nom, premierTypeStarterCalculer.Nom);
            }
            if (pkmTypesStarter.Count == 2)
            {
                var deuxiemeTypeStarterAttendu = pkmTypesStarter[1];
                var deuxiemeTypeStarterCalculer = regroupementTypesPKM[1][1];
                Assert.Equal(deuxiemeTypeStarterAttendu.Nom, deuxiemeTypeStarterCalculer.Nom);
            }
        }

        private void AssertBonsPKMTypesRetournes(Dictionary<int, List<PKMType>> regroupementTypesPKM, IEnumerable<string> pkmTypesNoms)
        {
            for (int i = 0; i < regroupementTypesPKM.Count; i++)
            {
                if (i == 0)
                    continue;
                var regroupement = regroupementTypesPKM[i];
                if (regroupement.Count == 1)
                {
                    Assert.True(pkmTypesNoms.Contains(regroupement[0].Nom));
                }
                else
                {
                    Assert.True(pkmTypesNoms.Contains(regroupement[0].Nom) && pkmTypesNoms.Contains(regroupement[1].Nom));
                }
            }
        }

        private void AssertPKMTypesRetournesUneSeuleFois(Dictionary<int, List<PKMType>> regroupementTypesPKM)
        {
            for (int i = 1; i <= regroupementTypesPKM.Count; i++)
            {
                for (int j = 1; j <= regroupementTypesPKM.Count; j++)
                {
                    if (i == j)
                        continue;
                    Assert.NotEqual(regroupementTypesPKM[i][0].Nom, regroupementTypesPKM[j][0].Nom);
                    if (regroupementTypesPKM[i].Count == 2)
                    {
                        Assert.NotEqual(regroupementTypesPKM[i][1].Nom, regroupementTypesPKM[j][0].Nom);
                        if (regroupementTypesPKM[j].Count == 2)
                        {
                            Assert.NotEqual(regroupementTypesPKM[i][1].Nom, regroupementTypesPKM[j][1].Nom);
                        }
                    }
                    if (regroupementTypesPKM[j].Count == 2)
                    {
                        Assert.NotEqual(regroupementTypesPKM[i][0].Nom, regroupementTypesPKM[j][1].Nom);
                    }
                }
            }
        }
    }
}