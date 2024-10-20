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
        private IFightPKMTypes _fightPKMTypes;

        public GeneratePKMTeamHandler(ILoadPKMTypes loadPKMTypes, ISelectPKMTypes selectPKMTypes, IFightPKMTypes fightPKMTypes)
        {
            _loadPKMTypes = loadPKMTypes;
            _selectPKMTypes = selectPKMTypes;
            _fightPKMTypes = fightPKMTypes;
        }

        public void Generate()
        {
            var allPKMTypes = _loadPKMTypes.GetPKMDatas();
            var PKMTypesChoisis = _selectPKMTypes.ChoosePKMTypes(allPKMTypes);
            var faiblesPKMTypes = _fightPKMTypes.RetournerTousFaiblesPKMTypes(PKMTypesChoisis);
        }
    }
}