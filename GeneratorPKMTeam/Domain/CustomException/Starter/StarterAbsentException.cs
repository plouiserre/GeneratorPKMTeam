using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException.Starter
{
    public class StarterAbsentException : Exception
    {
        public string CustomMessage { get; set; }
        public TypeErreur TypeErreur { get; set; }

        public StarterAbsentException()
        {
            CustomMessage = "Aucun starter n'a été choisi pour le moment.";
            TypeErreur = TypeErreur.StarterAbsent;
        }
    }
}