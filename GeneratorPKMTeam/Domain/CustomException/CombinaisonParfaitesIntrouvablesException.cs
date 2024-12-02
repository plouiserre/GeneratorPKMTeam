using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException
{
    public class CombinaisonParfaitesIntrouvablesException : Exception
    {
        public string CustomMessage { get; set; }
        public TypeErreur TypeErreur { get; set; }

        public CombinaisonParfaitesIntrouvablesException()
        {
            CustomMessage = "Aucun tirage de PKM parfait n'a été trouvé";
            TypeErreur = TypeErreur.NoCombinaisonsParfaitesTrouvees;
        }
    }
}