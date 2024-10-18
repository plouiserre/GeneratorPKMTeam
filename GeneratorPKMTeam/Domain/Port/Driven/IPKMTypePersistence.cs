using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Port.Driven
{
    public interface IPKMTypePersistence
    {
        public PKMDatas GetPKMDatas();
    }
}