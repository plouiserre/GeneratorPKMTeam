using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.CustomException
{
    public class PasAssezPKMTypeSimpleSelectionnableException : Exception
    {
        public string CustomMessage { get; set; }
        public TypeErreur TypeErreur { get; set; }
        public List<PKMType> PKMTypesSimplesDejaTrouves { get; set; }
        public PasAssezPKMTypeSimpleSelectionnableException(List<PKMType> pKMTypesSimplesDejaTrouves)
        {
            PKMTypesSimplesDejaTrouves = pKMTypesSimplesDejaTrouves;
            CustomMessage = "Il n'y a pas assez eu de PKM Type Simple sélectionné pour faire une équipe de 6 PKM";
            TypeErreur = TypeErreur.PasAssezPKMTypeSimpleSelectionnable;
        }
    }
}