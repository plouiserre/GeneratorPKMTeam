using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;

namespace GeneratorPKMTeamTest.Utils.Personas
{
    //TODO retravailler cette classe
    public static class RelPKMTypePersonas
    {
        private static List<RelPKMType> peuPKMTypesFaibles;
        private static List<RelPKMType> quelquesPKMTypesFaibles;
        private static List<RelPKMType> bcpPKMTypesFaibles;
        private static List<RelPKMType> peuPKMTypesDangereux;
        private static List<RelPKMType> quelquesPKMTypesDangereux;
        private static List<RelPKMType> bcpPKMTypesDangereux;

        private static void GenererRelPKMTypePersonas()
        {
            var peuRelTypes = new List<RelPKMType>(){
                new RelPKMType(){TypePKM="Acier", ModeImpact=2},
                new RelPKMType(){TypePKM="Combat", ModeImpact=2},
                new RelPKMType(){TypePKM="Dragon", ModeImpact=2},
                new RelPKMType(){TypePKM="Eau", ModeImpact=2},
                new RelPKMType(){TypePKM="Electrik", ModeImpact=2}
            };
            peuPKMTypesFaibles = peuRelTypes;
            peuPKMTypesDangereux = peuRelTypes;
            var quelquesRelTypes = new List<RelPKMType>(peuRelTypes){
                new RelPKMType(){TypePKM="Fée", ModeImpact=2},
                new RelPKMType(){TypePKM="Feu", ModeImpact=2},
                new RelPKMType(){TypePKM="Glace", ModeImpact=2},
                new RelPKMType(){TypePKM="Insecte", ModeImpact=2},
                new RelPKMType(){TypePKM="Normal", ModeImpact=2},
                new RelPKMType(){TypePKM="Plante", ModeImpact=2},
                new RelPKMType(){TypePKM="Poison", ModeImpact=2},
            };
            quelquesPKMTypesFaibles = quelquesRelTypes;
            quelquesPKMTypesDangereux = quelquesRelTypes;
            var bcpRelTypes = new List<RelPKMType>(quelquesRelTypes){
                 new RelPKMType(){TypePKM="Psy", ModeImpact=2},
                new RelPKMType(){TypePKM="Roche", ModeImpact=2},
                new RelPKMType(){TypePKM="Sol", ModeImpact=2},
                new RelPKMType(){TypePKM="Spectre", ModeImpact=2},
                new RelPKMType(){TypePKM="Tenèbres", ModeImpact=2},
                new RelPKMType(){TypePKM="Vol", ModeImpact=2},
            };
            bcpPKMTypesFaibles = bcpRelTypes;
            bcpPKMTypesDangereux = bcpRelTypes;
        }

        public static List<RelPKMType> RetournerRelPKMType(FrequenceRelPKMType frequence, NomListRelPKMType nomList)
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

    public enum FrequenceRelPKMType
    {
        Peu,
        Quelques,
        Beaucoup
    }

    public enum NomListRelPKMType
    {
        Faibles,
        Dangereux
    }
}