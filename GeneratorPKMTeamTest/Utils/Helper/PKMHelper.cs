using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam;

namespace GeneratorPKMTeamTest.Utils.Helper
{
    public class PKMHelper
    {
        public static Dictionary<string, List<PKMType>> ConstruireTousLesTypesPossibles(string tousLesTypesPossibles)
        {
            Dictionary<string, List<PKMType>> tousLesTypesConstruits = new Dictionary<string, List<PKMType>>();
            string[] typesDecomposes = tousLesTypesPossibles.Split(' ');
            foreach (string type in typesDecomposes)
            {
                if (type.Contains('-'))
                {
                    string[] types = type.Split('-');
                    tousLesTypesConstruits.Add(type, new List<PKMType>() {
                        new PKMType() { Nom = types[0] },
                        new PKMType() { Nom = types[1] }
                        });
                }
                else
                {
                    tousLesTypesConstruits.Add(type, new List<PKMType>() { new PKMType() { Nom = type } });
                }
            }
            return tousLesTypesConstruits;
        }
    }
}