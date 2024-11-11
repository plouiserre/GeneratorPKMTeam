using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driving;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GeneratePKMTeamHandler : IGeneratePKMTeamHandler
    {
        private IChargerPKMTypes _chargementPKMTypes;
        private IChoisirPKMTypes _choisirPKMTypes;
        private IResultatCombatPKMTypes _resultCombatPKMTypes;
        private IGererResultatTiragePKMTypes _gererResultatTiragePKMTypes;
        public List<TiragePKMTypes> TiragePKMTypes;

        public GeneratePKMTeamHandler(IChargerPKMTypes chargementPKMTypes, IChoisirPKMTypes choisirPKMTypes,
                IResultatCombatPKMTypes resultCombatPKMTypes, IGererResultatTiragePKMTypes gererResultatTiragePKMTypes)
        {
            _chargementPKMTypes = chargementPKMTypes;
            _choisirPKMTypes = choisirPKMTypes;
            _resultCombatPKMTypes = resultCombatPKMTypes;
            _gererResultatTiragePKMTypes = gererResultatTiragePKMTypes;
            TiragePKMTypes = new List<TiragePKMTypes>();
        }

        public void Generer()
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
                TiragePKMTypes = _gererResultatTiragePKMTypes.TirerPKMTypes(TiragePKMTypes, tirageATraiter);
                comptage += 1;
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
                    if (tirage.NoteTirage < 90)
                        return true;
                }
                return false;
            }
        }
    }
}