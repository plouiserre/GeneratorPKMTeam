using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException.Starter
{
    public class StarterIntrouvableException : Exception
    {
        public string CustomMessage { get; set; }
        public TypeErreur TypeErreur { get; set; }

        public StarterIntrouvableException(string nomPKM)
        {
            CustomMessage = nomPKM + " n'est pas le nom d'un PKM existant.";
            TypeErreur = TypeErreur.StarterIntrouvable;
        }
    }
}