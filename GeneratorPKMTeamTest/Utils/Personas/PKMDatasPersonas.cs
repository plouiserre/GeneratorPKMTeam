using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;

namespace GeneratorPKMTeamTest.Utils.Personas
{
    public class PKMDatasPersonas
    {
        public static PKMDatas GetPersonas()
        {
            var personas = new PKMDatas();
            personas.PKMTypes = new List<PKMType>();
            var firstPKMType = new PKMType();
            firstPKMType.Nom = "Feu";
            var secondPKMType = new PKMType();
            secondPKMType.Nom = "Eau";
            var thirdPKMType = new PKMType();
            thirdPKMType.Nom = "Plante";
            personas.PKMTypes.Add(firstPKMType);
            personas.PKMTypes.Add(secondPKMType);
            personas.PKMTypes.Add(thirdPKMType);
            return personas;
        }
    }
}