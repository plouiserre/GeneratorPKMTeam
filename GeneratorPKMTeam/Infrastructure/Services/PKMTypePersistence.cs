using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Mapper;

namespace GeneratorPKMTeam.Infrastructure.Services
{
    public class PKMTypePersistence : IPKMTypePersistence
    {

        private PKMDonnees? _pkmDonnees { get; set; }

        public PKMDonnees GetPKMDonnees()
        {
            if (_pkmDonnees == null)
            {
                var pkmsType = new PKMTypeJson();
                var pkmsTypeJson = pkmsType.GetPKMDatas();
                _pkmDonnees = PKMDonneesMapper.ToDomain(pkmsTypeJson);
            }
            return _pkmDonnees;
        }
    }
}