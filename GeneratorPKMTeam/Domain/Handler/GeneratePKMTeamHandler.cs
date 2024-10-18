using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Port.Driving;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GeneratePKMTeamHandler : IGeneratePKMTeamHandler
    {
        private ILoadPKMTypes _loadPKMTypes;

        public GeneratePKMTeamHandler(ILoadPKMTypes loadPKMTypes)
        {
            _loadPKMTypes = loadPKMTypes;
        }

        public void Generate()
        {
            _loadPKMTypes.GetPKMDatas();
        }
    }
}