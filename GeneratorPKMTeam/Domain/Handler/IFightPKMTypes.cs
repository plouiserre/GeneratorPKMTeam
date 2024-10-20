using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public interface IFightPKMTypes
    {
        public List<RelPKMType> RetournerTousFaiblesPKMTypes(List<PKMType> pkmTypes);
    }
}