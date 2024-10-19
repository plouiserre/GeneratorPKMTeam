using GeneratorPKMTeam;

namespace GeneratorPKMTeamTest.Utils.Personas
{
    public class PKMDatasPersonas
    {
        public static PKMDatas GetPersonas()
        {
            var personas = new PKMDatas();
            personas.PKMTypes = new List<PKMType>();
            string[] types = {
                "Acier", "Combat", "Dragon", "Eau", "Electrik", "Fée",
                "Feu", "Glace", "Insecte", "Normal", "Plante", "Poison",
                "Psy", "Roche", "Sol", "Spectre", "Ténèbres", "Vol"
            };

            foreach (var type in types)
            {
                personas.PKMTypes.Add(new PKMType { Nom = type });
            }
            return personas;
        }
    }
}