using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driving;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GeneratePKMTeamHandler : IGeneratePKMTeamHandler
    {
        private ILoadPKMTypes _loadPKMTypes;
        private ISelectPKMTypes _selectPKMTypes;
        private IFightPKMTypes _fightPKMTypes;
        private IResultFightPKMTypes _resultFightPKMTypes;
        private IGererResultatTiragePKMTypes _gererResultatTiragePKMTypes;
        public List<TiragePKMTypes> TiragePKMTypes;

        public GeneratePKMTeamHandler(ILoadPKMTypes loadPKMTypes, ISelectPKMTypes selectPKMTypes,
                IFightPKMTypes fightPKMTypes, IResultFightPKMTypes resultFightPKMTypes, IGererResultatTiragePKMTypes
                gererResultatTiragePKMTypes)
        {
            _loadPKMTypes = loadPKMTypes;
            _selectPKMTypes = selectPKMTypes;
            _fightPKMTypes = fightPKMTypes;
            _resultFightPKMTypes = resultFightPKMTypes;
            _gererResultatTiragePKMTypes = gererResultatTiragePKMTypes;
            TiragePKMTypes = new List<TiragePKMTypes>();
        }

        public void Generate()
        {
            var allPKMTypes = _loadPKMTypes.GetPKMDatas();
            int count = 0;
            while (ContinuerACalculer())
            {
                if (count >= 100)
                    throw new CombinaisonParfaitesIntrouvablesException();
                var PKMTypesChoisis = _selectPKMTypes.ChoosePKMTypes(allPKMTypes);
                var PKMTypesfaibles = _fightPKMTypes.RetournerTousFaiblesPKMTypes(PKMTypesChoisis);
                var classificationResult = _resultFightPKMTypes.NoterResultatTirage(PKMTypesfaibles);
                var tirageATraiter = new TiragePKMTypes()
                {
                    ResultatTirageStatus = classificationResult.ResultatStatus,
                    NoteTirage = classificationResult.NoteResultatTirage,
                    PKMTypes = PKMTypesChoisis
                };
                TiragePKMTypes = _gererResultatTiragePKMTypes.TirerPKMTypes(TiragePKMTypes, tirageATraiter);
                count += 1;
            }
        }

        public bool ContinuerACalculer()
        {
            if (TiragePKMTypes.Count < 10)
                return true;
            else
            {
                foreach (var tirage in TiragePKMTypes)
                {
                    if (tirage.ResultatTirageStatus != ResultatTirageStatus.Parfait)
                        return true;
                }
                return false;
            }
        }
    }
}