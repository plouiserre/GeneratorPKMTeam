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
        private IResultFightPKMTypes _resultFightPKMTypes;

        public GeneratePKMTeamHandler(ILoadPKMTypes loadPKMTypes, ISelectPKMTypes selectPKMTypes,
                IFightPKMTypes fightPKMTypes, IResultFightPKMTypes resultFightPKMTypes)
        {
            _loadPKMTypes = loadPKMTypes;
            _selectPKMTypes = selectPKMTypes;
            _fightPKMTypes = fightPKMTypes;
            _resultFightPKMTypes = resultFightPKMTypes;
        }

        public void Generate()
        {
            var allPKMTypes = _loadPKMTypes.GetPKMDatas();
            var PKMTypesChoisis = _selectPKMTypes.ChoosePKMTypes(allPKMTypes);
            var PKMTypesfaibles = _fightPKMTypes.RetournerTousFaiblesPKMTypes(PKMTypesChoisis);
            var classificationResult = _resultFightPKMTypes.NoterResultatTirage(PKMTypesfaibles);
        }
    }
}