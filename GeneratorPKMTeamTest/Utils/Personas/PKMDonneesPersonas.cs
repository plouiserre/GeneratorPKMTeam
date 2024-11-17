using GeneratorPKMTeam;

namespace GeneratorPKMTeamTest.Utils.Personas
{
    public class PKMDonneesPersonas
    {
        private static string[] types = {
                "Acier", "Combat", "Dragon", "Eau", "Electrik", "Fée",
                "Feu", "Glace", "Insecte", "Normal", "Plante", "Poison",
                "Psy", "Roche", "Sol", "Spectre", "Ténèbres", "Vol"
            };
        private static List<PKMType> peuPKMTypesFaibles;
        private static List<PKMType> quelquesPKMTypesFaibles;
        private static List<PKMType> bcpPKMTypesFaibles;
        private static List<PKMType> peuPKMTypesDangereux;
        private static List<PKMType> quelquesPKMTypesDangereux;
        private static List<PKMType> bcpPKMTypesDangereux;

        public static PKMDonnees GetPersonas()
        {
            var personas = new PKMDonnees();
            personas.PKMTypes = new List<PKMType>();

            foreach (var type in types)
            {
                var pkmType = new PKMType { Nom = type };
                pkmType.RelPKMTypes = GenererRelPKMTypes(types);
                personas.PKMTypes.Add(pkmType);
            }
            return personas;
        }

        public static List<PKMType> RecupererTypesPKM(int nombre)
        {
            var PKMTypes = new List<PKMType>();
            for (int i = 0; i < nombre; i++)
            {
                var pkmType = new PKMType { Nom = types[i] };
                PKMTypes.Add(pkmType);
            }
            return PKMTypes;
        }


        private static List<RelPKMType> GenererRelPKMTypes(string[] types)
        {
            var relPKMTypesGeneres = new List<RelPKMType>();
            float[] relValeurs = { 0.5f, 1, 2 };
            foreach (string typePKM in types)
            {
                Random randomIndex = new Random();
                int index = randomIndex.Next(0, 3);
                float valeur = relValeurs[index];
                relPKMTypesGeneres.Add(new RelPKMType() { TypePKM = typePKM, ModeImpact = valeur });
            }
            return relPKMTypesGeneres;
        }

        private static void GenererRelPKMTypePersonas()
        {
            var peuPKMTypes = new List<PKMType>(){
                new PKMType(){Nom="Acier"},
                new PKMType(){Nom="Combat"},
                new PKMType(){Nom="Dragon"},
                new PKMType(){Nom="Eau"},
                new PKMType(){Nom="Electrik"}
            };
            peuPKMTypesFaibles = peuPKMTypes;
            peuPKMTypesDangereux = peuPKMTypes;
            var quelquesPKMTypes = new List<PKMType>(peuPKMTypes){
                new PKMType(){Nom="Fée"},
                new PKMType(){Nom="Feu"},
                new PKMType(){Nom="Glace"},
                new PKMType(){Nom="Insecte"},
                new PKMType(){Nom="Normal"},
                new PKMType(){Nom="Plante"},
                new PKMType(){Nom="Poison"},
            };
            quelquesPKMTypesFaibles = quelquesPKMTypes;
            quelquesPKMTypesDangereux = quelquesPKMTypes;
            var bcpPKMTypes = new List<PKMType>(quelquesPKMTypes){
                 new PKMType(){Nom="Psy"},
                new PKMType(){Nom="Roche"},
                new PKMType(){Nom="Sol"},
                new PKMType(){Nom="Spectre"},
                new PKMType(){Nom="Tenèbres"},
                new PKMType(){Nom="Vol"},
            };
            bcpPKMTypesFaibles = bcpPKMTypes;
            bcpPKMTypesDangereux = bcpPKMTypes;
        }

        public static List<PKMType> RetournerPKMType(FrequenceRelPKMType frequence, NomListRelPKMType nomList)
        {
            if (bcpPKMTypesDangereux == null)
                GenererRelPKMTypePersonas();
            if (frequence == FrequenceRelPKMType.Peu && nomList == NomListRelPKMType.Faibles)
                return peuPKMTypesFaibles;
            else if (frequence == FrequenceRelPKMType.Peu && nomList == NomListRelPKMType.Dangereux)
                return peuPKMTypesDangereux;
            else if (frequence == FrequenceRelPKMType.Quelques && nomList == NomListRelPKMType.Faibles)
                return quelquesPKMTypesFaibles;
            else if (frequence == FrequenceRelPKMType.Quelques && nomList == NomListRelPKMType.Dangereux)
                return quelquesPKMTypesDangereux;
            else if (frequence == FrequenceRelPKMType.Beaucoup && nomList == NomListRelPKMType.Faibles)
                return bcpPKMTypesFaibles;
            else
                return bcpPKMTypesDangereux;
        }
    }
}