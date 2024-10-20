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
                var pkmType = new PKMType { Nom = type };
                pkmType.RelPKMTypes = GenererRelPKMTypes(types);
                personas.PKMTypes.Add(pkmType);
            }
            return personas;
        }


        private static List<RelPKMType> GenererRelPKMTypes(string[] types)
        {
            var relPKMTypesGeneres = new List<RelPKMType>();
            float[] relValeurs = { 0.5f, 1, 2 };
            foreach (string typePKM in types)
            {
                Random randomIndex = new Random();
                int index = randomIndex.Next(0, 2);
                float valeur = relValeurs[index];
                relPKMTypesGeneres.Add(new RelPKMType() { TypePKM = typePKM, ModeImpact = valeur });
            }
            return relPKMTypesGeneres;
        }
    }
}