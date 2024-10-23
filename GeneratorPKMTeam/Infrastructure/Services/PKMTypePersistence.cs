using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Connector;

namespace GeneratorPKMTeam.Infrastructure.Services
{
    public class PKMTypePersistence : IPKMTypePersistence
    {

        public PKMDatas GetPKMDatas()
        {
            var pkms = new PKMTypeJson();
            return pkms.GetPKMDatas();
        }
    }
}