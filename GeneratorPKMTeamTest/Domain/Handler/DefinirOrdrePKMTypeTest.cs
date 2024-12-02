using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler
{
    public class DefinirOrdrePKMTypeTest
    {
        public DefinirOrdrePKMTypeTest()
        {
        }

        [Theory]
        [InlineData(1, new string[]{"Feu", "Eau", "Psy", "Sol", "Roche", "Plante", "Poison", "Glace", "Vol",
        "Feu-Vol", "Eau-Vol", "Eau-Psy", "Eau-Poison", "Eau-Glace", "Sol-Roche", "Roche-Eau",
        "Roche-Vol","Roche-Sol", "Plante-Psy", "Plante-Poison", "Poison-Vol", "Poison-Sol", "Glace-Vol", "Glace-Psy"})]
        [InlineData(3, new string[]{"Eau", "Ténèbres", "Psy", "Sol", "Fée", "Plante", "Dragon", "Glace", "Insecte",
        "Eau-Ténèbres", "Eau-Psy", "Eau-Sol", "Eau-Fée", "Eau-Plante", "Eau-Dragon", "Eau-Glace", "Ténèbres-Glace",
        "Psy-Fée", "Psy-Plante", "Sol-Psy", "Sol-Dragon", "Plante-Ténèbres", "Plante-Psy", "Dragon-Psy", "Glace-Eau",
        "Glace-Psy", "Glace-Sol", "Insecte-Eau", "Insecte-Sol", "Insecte-Plante"})]
        [InlineData(6, new string[]{"Electrique", "Spectre", "Feu", "Acier", "Roche", "Combat", "Normal", "Vol", "Poison",
        "Electrique-Spectre", "Electrique-Acier", "Electrique-Normal", "Electrique-Vol", "Spectre-Feu", "Spectre-Vol",
        "Spectre-Poison", "Feu-Acier", "Feu-Roche", "Feu-Combat", "Feu-Normal", "Feu-Vol", "Acier-Spectre", "Acier-Roche",
        "Acier-Combat", "Acier-Vol", "Roche-Acier", "Roche-Combat", "Roche-Vol", "Combat-Acier", "Combat-Vol", "Normal-Vol",
        "Poison-Combat", "Poison-Vol"})]
        public void OnObtientPour6PkmsParGeneration9TypesOuDoublesTypes(int generation, string[] _toutesLesCombinaisonsPossibles)
        {
            var pkmTypes = GenererPKMTypesListes(_toutesLesCombinaisonsPossibles);
            var tousLesTypesPossibles = AvoirTousLesTypesPossibles(_toutesLesCombinaisonsPossibles);
            var determinerTousLesTypesExistant = Substitute.For<IDeterminerTousLesTypesExistant>();
            determinerTousLesTypesExistant.Calculer(generation, pkmTypes).Returns(tousLesTypesPossibles);

            var definirOrdre = new DefinirOrdrePKMType(determinerTousLesTypesExistant, generation);
            Dictionary<int, List<PKMType>> regroupementTypesPKM = definirOrdre.Generer(pkmTypes);

            AssertOrdrePKMType(regroupementTypesPKM, pkmTypes);
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
            }
            return types;
        }

        private void AssertOrdrePKMType(Dictionary<int, List<PKMType>> regroupementTypesPKM, List<PKMType> pkmTypes)
        {
            var pkmTypesNoms = pkmTypes.Select(o => o.Nom);

            Assert.True(regroupementTypesPKM.Count > 0);

            AssertBonsPKMTypesRetournes(regroupementTypesPKM, pkmTypesNoms);

            AssertPKMTypesRetournesUneSeuleFois(regroupementTypesPKM);
        }

        private void AssertBonsPKMTypesRetournes(Dictionary<int, List<PKMType>> regroupementTypesPKM, IEnumerable<string> pkmTypesNoms)
        {
            foreach (var regroupement in regroupementTypesPKM)
            {
                if (regroupement.Value.Count == 1)
                {
                    Assert.True(pkmTypesNoms.Contains(regroupement.Value[0].Nom));
                }
                else
                {
                    Assert.True(pkmTypesNoms.Contains(regroupement.Value[0].Nom) && pkmTypesNoms.Contains(regroupement.Value[1].Nom));
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