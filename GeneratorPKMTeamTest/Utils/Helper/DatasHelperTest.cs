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
            string data = File.ReadAllText(@"../../../PKMs.json");
            var json = JsonSerializer.Deserialize<PKMsInf>(data);
            PKMs = PKMMapper.ToDomain(json);
        }
    }
}