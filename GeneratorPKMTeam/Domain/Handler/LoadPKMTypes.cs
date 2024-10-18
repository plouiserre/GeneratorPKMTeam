using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class LoadPKMTypes : ILoadPKMTypes
    {
        private IPKMTypePersistence _persistence;
        public LoadPKMTypes(IPKMTypePersistence persistence)
        {
            _persistence = persistence;
        }

        public PKMDatas GetPKMDatas()
        {
            var data = _persistence.GetPKMDatas();
            if (data == null || data.PKMTypes == null || data.PKMTypes.Count == 0)
                throw new LoadingPKMTypesException();
            return data;
        }
    }
}