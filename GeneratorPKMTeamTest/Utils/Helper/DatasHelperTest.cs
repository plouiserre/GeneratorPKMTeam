using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Infrastructure.Mapper;
using GeneratorPKMTeam.Infrastructure.Models.PKMDonnees;
using GeneratorPKMTeam.Infrastructure.Models.PKMs;
using GeneratorPKMTeam.Infrastructure.Models.PKMStats;

namespace GeneratorPKMTeamTest.Utils.Helper
{
    public static class DatasHelperTest
    {

        private static PKMDonnees PKMDonnees { get; set; }
        private static PKMs PKMs { get; set; }

        public static List<PKMType> RetournerDonneesPKMTypes(List<string> typesPKMName)
        {
            var results = new List<PKMType>();
            if (PKMDonnees == null)
            {
                LoadDataPKMTypes();
            }
            if (typesPKMName != null && typesPKMName.Count > 0)
            {
                foreach (var pkmTypeName in typesPKMName)
                {
                    var donnees = PKMDonnees.PKMTypes.FirstOrDefault(o => o.Nom == pkmTypeName);
                    if (donnees != null)
                        results.Add(donnees);
                }
            }
            else
            {
                results.AddRange(PKMDonnees.PKMTypes);
            }
            return results;
        }

        private static void LoadDataPKMTypes()
        {
            string data = File.ReadAllText(@"../../../PKMType.json");
            var json = JsonSerializer.Deserialize<PKMDonneesInf>(data);
            PKMDonnees = PKMDonneesMapper.ToDomain(json);
        }

        public static IEnumerable<PKM> RetournersTousPKM()
        {
            LoadData();
            return PKMs.TousPKMs;
        }

        public static IEnumerable<PKM> RetournersPKMsOuChaqueTypeEstPresentUneOuPlusieursFois(int generation)
        {
            LoadDataPKMTypes();
            LoadData();

            List<PKM> pkmsChoisis = new List<PKM>();
            List<PKM> pkmsChoisisUnSeulType = RetrouverUnPKMAvecUnSeulTypePourChaqueType(generation);
            List<PKM> pkmsChoisisDeuxTypes = RetrouverUnPKMAvecDeuxTypesPourChaqueDuoTypes(generation);

            pkmsChoisis.AddRange(pkmsChoisisUnSeulType);
            pkmsChoisis.AddRange(pkmsChoisisDeuxTypes);

            return pkmsChoisis;
        }

        private static List<PKM> RetrouverUnPKMAvecUnSeulTypePourChaqueType(int generation)
        {
            List<PKM> pkmsChoisis = new List<PKM>();
            foreach (var type in PKMDonnees.PKMTypes)
            {
                var pkmsSearch = PKMs.TousPKMs.Where(o => o.PKMTypes.Count == 1 && o.Generation <= generation).OrderBy(o => o.Nom);
                foreach (var pkm in pkmsSearch)
                {
                    if (pkm.PKMTypes[0] == type.Nom)
                    {
                        pkmsChoisis.Add(pkm);
                    }
                }
            }
            return pkmsChoisis;
        }

        private static List<PKM> RetrouverUnPKMAvecDeuxTypesPourChaqueDuoTypes(int generation)
        {
            List<PKM> pkmsChoisis = new List<PKM>();
            foreach (var premierType in PKMDonnees.PKMTypes)
            {
                foreach (var secondType in PKMDonnees.PKMTypes)
                {
                    if (premierType.Nom == secondType.Nom)
                        continue;
                    var pkmSearch = PKMs.TousPKMs.Where(o => o.PKMTypes.Count == 2 && o.Generation <= generation).OrderBy(o => o.Nom);
                    foreach (var pkm in pkmSearch)
                    {
                        if (pkm.PKMTypes[0] == premierType.Nom && pkm.PKMTypes[1] == secondType.Nom)
                        {
                            pkmsChoisis.Add(pkm);
                        }
                    }
                }
            }
            return pkmsChoisis;
        }

        private static void LoadData()
        {
            string pkmData = File.ReadAllText(@"../../../PKMs.json");
            var pkmJson = JsonSerializer.Deserialize<PKMsInf>(pkmData);
            string pkmStatsData = File.ReadAllText(@"../../../PKMStats.json");
            var pkmStatJson = JsonSerializer.Deserialize<PKMDataStatsInf>(pkmStatsData);
            PKMs = PKMMapper.ToDomain(pkmJson, pkmStatJson);
        }

        //méthode temporaire
        public static List<PKM> MockPKMPlantePoisonAvecStats()
        {
            var bulbizarre = MockPKM("Bulbizarre", 45, 49, 65, 49, 65, 45);

            var ortide = MockPKM("Ortide", 60, 65, 85, 70, 75, 40);

            var emplifor = MockPKM("Emplifor", 80, 105, 100, 65, 70, 70);

            var roserade = MockPKM("Roserade", 60, 70, 125, 65, 105, 90);

            var mockPKMs = new List<PKM>() { bulbizarre, ortide, emplifor, roserade };
            return mockPKMs;
        }

        private static PKM MockPKM(string nom, int PV, int attaque, int speAttaque, int defense, int speDefense, int vitesse)
        {
            var pkm = new PKM()
            {
                Nom = nom,
                Stats = new PKMStats()
                {
                    PV = PV,
                    Attaque = attaque,
                    SpeAttaque = speAttaque,
                    Defense = defense,
                    SpeDefense = speDefense,
                    Vitesse = vitesse
                }
            };
            return pkm;
        }
    }
}