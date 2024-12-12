using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException.Starter
{
    public class StarterDejaExistantException : Exception
    {
        public string CustomMessage { get; set; }
        public TypeErreur TypeErreur { get; set; }

        public StarterDejaExistantException()
        {
            CustomMessage = "Il y a déjà un starter sélectionné.";
            TypeErreur = TypeErreur.StarterDejaExistant;
        }
    }
}