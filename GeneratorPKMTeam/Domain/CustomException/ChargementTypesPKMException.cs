using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException
{
    public class ChargementTypesPKMException : Exception
    {
        public string CustomMessage { get; set; }
        public TypeErreur TypeErreur { get; set; }

        public ChargementTypesPKMException()
        {
            CustomMessage = "Aucune donnée n'a été récupérée";
            TypeErreur = TypeErreur.NoPKMTypesData;
        }
    }
}