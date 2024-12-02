using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public ChoisirMeilleuresCombinaisonsTypes(IChargerPKMTypes chargementPKMTypes, IChoisirPKMTypes choisirPKMTypes,
                IResultatCombatPKMTypes resultCombatPKMTypes, IGererResultatTiragePKMTypes gererResultatTiragePKMTypes)
        {
            _chargementPKMTypes = chargementPKMTypes;
            _choisirPKMTypes = choisirPKMTypes;
            _resultCombatPKMTypes = resultCombatPKMTypes;
            _gererResultatTiragePKMTypes = gererResultatTiragePKMTypes;
        }

        public TiragePKMTypes GenererTirageParfait()
        {
            var tirageATraiter = new TiragePKMTypes();
            var tousPKMTypes = _chargementPKMTypes.AvoirPKMDatas();
            int comptage = 0;
            while (comptage < 20)
            {
                var PKMTypesChoisis = _choisirPKMTypes.SelectionnerPKMTypes(tousPKMTypes);
                var evaluation = new EvaluerPKMChoisis(_resultCombatPKMTypes, tousPKMTypes.PKMTypes);
                tirageATraiter = evaluation.Evaluer(PKMTypesChoisis);
                bool tirageAccepter = _gererResultatTiragePKMTypes.GarderTirage(tirageATraiter);
                if (tirageAccepter)
                    break;
                comptage += 1;
            }
            if (comptage == 20)
                throw new CombinaisonParfaitesIntrouvablesException();
            return tirageATraiter;
        }
    }
}