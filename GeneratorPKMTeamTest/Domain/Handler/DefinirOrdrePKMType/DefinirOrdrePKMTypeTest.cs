using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Models;
using NSubstitute;

namespace GeneratorPKMTeamTest.Domain.Handler.DefinirOrdrePKMTypeTest
{
    public class DefinirOrdrePKMTypeTest
    {
        private int _nombreFoisLancementTest;
        private PKM _starterPKM;

        public DefinirOrdrePKMTypeTest()
        {
            _nombreFoisLancementTest = 20;
        }

        [Theory]
        [InlineData(1, new string[]{"Eau", "Plante", "Roche", "Combat", "Sol", "Fée", "Insecte", "Ténèbres", "Glace",
        "Plante-Combat", "Plante-Ténèbres", "Roche-Plante", "Roche-Sol", "Roche-Insecte", "Roche-Ténèbres", "Sol-Roche",
        "Insecte-Plante", "Insecte-Roche", "Insecte-Combat", "Insecte-Sol", "Ténèbres-Glace", "Glace-Sol"}, "Carapuce",
        new string[] { "Eau" })]
        [InlineData(1, new string[]{"Combat", "Spectre", "Sol", "Normal", "Ténèbres", "Feu", "Glace", "Fée", "Plante-Poison",
        "Normal-Fée", "Ténèbres-Spectre", "Ténèbres-Feu", "Ténèbres-Glace", "Feu-Combat", "Feu-Sol", "Glace-Sol"}, "Bulbizarre",
        new string[] { "Plante", "Poison" })]
        [InlineData(1, new string[]{"Combat", "Electrique", "Normal", "Psy", "Eau", "Plante", "Glace", "Spectre", "Poison", "Feu-Vol",
        "Combat-Psy",  "Normal-Psy", "Psy-Plante", "Eau-Electrique", "Eau-Combat", "Eau-Glace", "Eau-Plante", "Eau-Poison", "Eau-Psy",
        "Plante-Combat", "Plante-Poison", "Plante-Psy", "Glace-Psy", "Glace-Eau", "Spectre-Poison" }, "Dracaufeu",
        new string[] { "Feu", "Vol" })]
        [InlineData(1, new string[]{"Fée", "Dragon", "Combat", "Plante", "Feu", "Poison", "Glace", "Psy", "Acier", "Ténèbres", "Eau-Sol",
        "Dragon-Psy", "Combat-Psy", "Plante-Combat", "Plante-Poison", "Plante-Psy", "Plante-Ténèbres", "Feu-Combat", "Glace-Psy",
        "Psy-Fée", "Psy-Plante", "Acier-Fée", "Acier-Psy", "Ténèbres-Feu", "Ténèbres-Glace"}, "Laggron", new string[] { "Eau", "Sol" })]
        public void OnObtientPour6PkmsParGeneration9TypesOuDoublesTypes(int generation, string[] _toutesLesCombinaisonsPossibles, string nomStarter, string[] typesStarter)
        {
            for (int i = 0; i < _nombreFoisLancementTest; i++)
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

                //AssertTousPKMTypesChoisis(pkmTypes, regroupementTypesPKM);
                AssertDifferentsTypes(regroupementTypesPKM);
            }
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

        private void AssertTousPKMTypesChoisis(List<PKMType> pkmTypes, Dictionary<int, List<PKMType>> regroupementTypesPKM)
        {
            bool tousTypesRetournes = true;
            var tousTypesTrouves = new List<PKMType>();
            foreach (var regroupement in regroupementTypesPKM)
            {
                tousTypesTrouves.AddRange(regroupement.Value);
            }
            foreach (var pkmType in pkmTypes)
            {
                bool contains = tousTypesTrouves.Any(o => o.Nom == pkmType.Nom);
                if (!contains)
                {
                    tousTypesRetournes = false;
                    break;
                }
            }
            if (!tousTypesRetournes)
            {
                int test = 3 + 2;
            }
            Assert.Equal(6, regroupementTypesPKM.Count);
            Assert.True(tousTypesRetournes);
        }

        private void AssertDifferentsTypes(Dictionary<int, List<PKMType>> regroupementTypesPKM)
        {
            bool aucunPKMTypesEnDouble = true;
            var tousPKMTypesRegroupes = new List<PKMType>();
            var tousPKMTypes = regroupementTypesPKM.Values;
            foreach (var pkmType in tousPKMTypes)
            {
                tousPKMTypesRegroupes.AddRange(pkmType);
            }
            for (int i = 0; i < tousPKMTypesRegroupes.Count; i++)
            {
                for (int j = 0; j < tousPKMTypesRegroupes.Count; j++)
                {
                    if (i == j)
                        continue;
                    if (tousPKMTypesRegroupes[i].Nom == tousPKMTypesRegroupes[j].Nom)
                    {
                        aucunPKMTypesEnDouble = false;
                        break;
                    }
                }
            }
            Assert.Equal(6, regroupementTypesPKM.Count);
            Assert.True(aucunPKMTypesEnDouble);
            // for (int i = 0; i < regroupementTypesPKM.Count; i++)
            // {
            //     for (int j = 0; j < regroupementTypesPKM.Count; j++)
            //     {
            //         if (i == j)
            //             continue;
            //         var premierRegroupement = regroupementTypesPKM[i];
            //         var secondRegroupement = regroupementTypesPKM[j];

            //     }
            // }
        }


    }
}