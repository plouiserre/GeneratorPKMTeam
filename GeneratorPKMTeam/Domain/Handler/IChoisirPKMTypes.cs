using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public interface IChoisirPKMTypes
    {
        List<PKMType> SelectionnerPKMTypes(PKMDonnees datas);
    }
}