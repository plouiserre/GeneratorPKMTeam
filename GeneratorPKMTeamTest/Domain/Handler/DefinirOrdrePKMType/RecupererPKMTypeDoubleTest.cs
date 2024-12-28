using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;

namespace GeneratorPKMTeamTest.Domain.Handler.OrdrePKMTypeTest
{
    public class RecupererPKMTypeDoubleTest
    {
        //TODO checker avec un starter 1 type de ne pas avoir 6 doubles renvoyés
        [Fact]
        public void RecupererPKMTypeDoubleAvecStarterSimple()
        {
            string tousLesTypesPossibles = "Eau Plante Roche Combat Sol Fée Insecte Ténèbres Glace Plante-Combat Plante-Ténèbres Roche-Plante Roche-Sol Roche-Insecte Roche-Ténèbres Sol-Roche Insecte-Plante Insecte-Roche Insecte-Combat Insecte-Sol Ténèbres-Glace Glace-Sol";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, tousLesTypesConstruits);

            Assert.Equal(3, typesDoublesRecuperes.Count);
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Roche-Sol"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Ténèbres-Glace"));
        }

        [Fact]
        public void RecupererPKMTypeDoubleAvecStarterDouble()
        {
            string tousLesTypesPossibles = "Combat Spectre Sol Normal Ténèbres Feu Glace Fée Plante-Poison Normal-Fée Ténèbres-Spectre Ténèbres-Feu Ténèbres-Glace Feu-Combat Feu-Sol Glace-Sol";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Plante" },
                                        new PKMType() { Nom = "Poison" } }, tousLesTypesConstruits);

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
            string tousLesTypesPossibles = "Fée Dragon Combat Plante Feu Poison Glace Psy Acier Ténèbres Eau-Sol Dragon-Psy Combat-Psy Plante-Combat Plante-Poison Plante-Psy Plante-Ténèbres Feu-Combat Glace-Psy Psy-Fée Psy-Plante Poison-Spectre Acier-Fée Acier-Psy Ténèbres-Feu Ténèbres-Glace Electrique-Normal";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Vol" } }, tousLesTypesConstruits);

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
            string tousLesTypesPossibles = "Eau Plante Roche Combat Sol Fée Insecte Ténèbres Glace Plante-Combat Eau-Ténèbres Roche-Plante Roche-Sol Eau-Insecte Roche-Ténèbres Sol-Roche Insecte-Plante Insecte-Roche Insecte-Combat Insecte-Sol Ténèbres-Glace Glace-Sol";
            var tousLesTypesConstruits = ConstruireTousLesTypesPossibles(tousLesTypesPossibles);
            var recupererPKMTypeDouble = new RecupererPKMTypeDouble();

            var typesDoublesRecuperes = recupererPKMTypeDouble.RecupererPKMTypes(new List<PKMType>() { new PKMType() { Nom = "Eau" } }, tousLesTypesConstruits);

            Assert.Equal(3, typesDoublesRecuperes.Count);
            Assert.False(typesDoublesRecuperes.ContainsKey("Eau-Ténèbres"));
            Assert.False(typesDoublesRecuperes.ContainsKey("Eau-Insecte"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Plante-Combat"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Roche-Sol"));
            Assert.True(typesDoublesRecuperes.ContainsKey("Ténèbres-Glace"));
        }

        //TODO centraliser cette classe
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