using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class ChoisirMeilleuresCombinaisonsTypes : IChoisirMeilleuresCombinaisonsTypes
    {
        private IChargerPKMTypes _chargementPKMTypes;
        private IChoisirPKMTypes _choisirPKMTypes;
        private IResultatCombatPKMTypes _resultCombatPKMTypes;
        private IGererResultatTiragePKMTypes _gererResultatTiragePKMTypes;
        private List<TiragePKMTypes> _tiragePKMTypes;

        public ChoisirMeilleuresCombinaisonsTypes(IChargerPKMTypes chargementPKMTypes, IChoisirPKMTypes choisirPKMTypes,
                IResultatCombatPKMTypes resultCombatPKMTypes, IGererResultatTiragePKMTypes gererResultatTiragePKMTypes)
        {
            _chargementPKMTypes = chargementPKMTypes;
            _choisirPKMTypes = choisirPKMTypes;
            _resultCombatPKMTypes = resultCombatPKMTypes;
            _gererResultatTiragePKMTypes = gererResultatTiragePKMTypes;
            _tiragePKMTypes = new List<TiragePKMTypes>();
        }

        public List<TiragePKMTypes> Choisir()
        {
            var tousPKMTypes = _chargementPKMTypes.AvoirPKMDatas();
            int comptage = 0;
            while (ContinuerACalculer())
            {
                if (comptage >= 100)
                    throw new CombinaisonParfaitesIntrouvablesException();
                var PKMTypesChoisis = _choisirPKMTypes.SelectionnerPKMTypes(tousPKMTypes);
                var evaluation = new EvaluerPKMChoisis(_resultCombatPKMTypes, tousPKMTypes.PKMTypes);
                var tirageATraiter = evaluation.Evaluer(PKMTypesChoisis);
                _tiragePKMTypes = _gererResultatTiragePKMTypes.TirerPKMTypes(_tiragePKMTypes, tirageATraiter);
                comptage += 1;
            }
            return _tiragePKMTypes;
        }

        public bool ContinuerACalculer()
        {
            if (_tiragePKMTypes.Count < 10)
                return true;
            else
            {
                foreach (var tirage in _tiragePKMTypes)
                {
                    if (tirage.NoteTirage < 90)
                        return true;
                }
                return false;
            }
        }
    }
}