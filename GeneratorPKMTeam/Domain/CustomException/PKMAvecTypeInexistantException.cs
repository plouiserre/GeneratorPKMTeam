using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException
{
    public class PKMAvecTypeInexistantException : Exception
    {
        public string CustomMessage { get; set; }
        public TypeErreur TypeErreur { get; set; }

        public PKMAvecTypeInexistantException(string pkmTypeInexistant)
        {
            CustomMessage = "Aucun PKM trouvé avec le type " + pkmTypeInexistant;
            TypeErreur = TypeErreur.PKMAvecPKMTypeInexistant;
        }


    }
}