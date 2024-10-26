using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public interface ICombattrePKMTypes
    {
        public List<RelPKMType> RetournerTousFaiblesPKMTypes(List<PKMType> pkmTypes);

        public List<RelPKMType> RetournerPKMTypesDangereux(List<PKMType> tousPKMTypes, List<PKMType> pkmTypes);
    }
}