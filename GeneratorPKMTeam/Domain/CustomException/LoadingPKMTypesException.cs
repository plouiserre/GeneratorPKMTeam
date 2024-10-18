using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException
{
    public class LoadingPKMTypesException : Exception
    {
        public string CustomMessage { get; set; }
        public ErrorType ErrorType { get; set; }

        public LoadingPKMTypesException()
        {
            CustomMessage = "Aucune donnée n'a été récupérée";
            ErrorType = ErrorType.NoPKMTypesData;
        }
    }
}