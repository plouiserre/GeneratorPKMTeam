using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException
{
    public class CombinaisonParfaitesIntrouvablesException : Exception
    {
        public string CustomMessage { get; set; }
        public ErrorType ErrorType { get; set; }

        public CombinaisonParfaitesIntrouvablesException()
        {
            CustomMessage = "Les 10 combinaisons parfaites n'ont pas été trouvé";
            ErrorType = ErrorType.NoCombinaisonsParfaitesTrouvees;
        }
    }
}