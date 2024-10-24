using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class ChargerPKMTypes : IChargerPKMTypes
    {
        private IPKMTypePersistence _persistence;
        public ChargerPKMTypes(IPKMTypePersistence persistence)
        {
            _persistence = persistence;
        }

        public PKMDonnees AvoirPKMDatas()
        {
            var data = _persistence.GetPKMDonnees();
            if (data == null || data.PKMTypes == null || data.PKMTypes.Count == 0)
                throw new ChargementTypesPKMException();
            return data;
        }
    }
}