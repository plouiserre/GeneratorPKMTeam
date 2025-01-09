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

        //TODO revoir comment est appelée cette classe
        public PKMDonnees GetPKMDonnees()
        {
            var pkmsType = new PKMTypeJson();
            var pkmsTypeJson = pkmsType.GetPKMDatas();
            var PKMDonnees = PKMDonneesMapper.ToDomain(pkmsTypeJson);
            return PKMDonnees;
        }
    }
}