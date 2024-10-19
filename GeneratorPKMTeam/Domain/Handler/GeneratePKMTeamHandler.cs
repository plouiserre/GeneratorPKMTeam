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
        private ISelectPKMTypes _selectPKMTypes;

        public GeneratePKMTeamHandler(ILoadPKMTypes loadPKMTypes, ISelectPKMTypes selectPKMTypes)
        {
            _loadPKMTypes = loadPKMTypes;
            _selectPKMTypes = selectPKMTypes;
        }

        public void Generate()
        {
            var allPKMTypes = _loadPKMTypes.GetPKMDatas();
            _selectPKMTypes.ChoosePKMTypes(allPKMTypes);
        }
    }
}