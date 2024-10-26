using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Infrastructure.Connector.JsonModels;
using GeneratorPKMTeam.Infrastructure.Mapper;

namespace GeneratorPKMTeamTest.Utils.Helper
{
    public static class DatasHelperTest
    {

        private static PKMDonnees PKMDonnees { get; set; }


        public static List<PKMType> RetournerDonneesPKMTypes(List<string> typesPKMName)
        {
            var results = new List<PKMType>();
            if (PKMDonnees == null)
            {
                LoadData();
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

        private static void LoadData()
        {
            string data = File.ReadAllText(@"../../../PKMType.json");
            var json = JsonSerializer.Deserialize<PKMDonneesInf>(data);
            PKMDonnees = PKMDonneesMapper.ToDomain(json);
        }
    }
}