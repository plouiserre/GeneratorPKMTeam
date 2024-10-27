using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driving;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GeneratePKMTeamHandler : IGeneratePKMTeamHandler
    {
        private IChargerPKMTypes _chargementPKMTypes;
        private IChoisirPKMTypes _choisirPKMTypes;
        private ICombattrePKMTypes _combattrePKMTypes;
        private IResultatCombatPKMTypes _resultCombatPKMTypes;
        private IGererResultatTiragePKMTypes _gererResultatTiragePKMTypes;
        public List<TiragePKMTypes> TiragePKMTypes;

        public GeneratePKMTeamHandler(IChargerPKMTypes chargementPKMTypes, IChoisirPKMTypes choisirPKMTypes,
                ICombattrePKMTypes combattrePKMTypes, IResultatCombatPKMTypes resultCombatPKMTypes,
                IGererResultatTiragePKMTypes gererResultatTiragePKMTypes)
        {
            _chargementPKMTypes = chargementPKMTypes;
            _choisirPKMTypes = choisirPKMTypes;
            _combattrePKMTypes = combattrePKMTypes;
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
                var PKMTypesfaibles = _combattrePKMTypes.RetournerTousFaiblesPKMTypes(PKMTypesChoisis);
                var PKMTypesDangereux = _combattrePKMTypes.RetournerPKMTypesDangereux(tousPKMTypes.PKMTypes, PKMTypesChoisis);
                var classificationResult = _resultCombatPKMTypes.NoterResultatTirage(PKMTypesfaibles, PKMTypesDangereux);
                var tirageATraiter = new TiragePKMTypes()
                {
                    ResultatTirageStatus = classificationResult.ResultatStatus,
                    NoteTirage = classificationResult.NoteResultatTirage,
                    PKMTypes = PKMTypesChoisis
                };
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
                    if (tirage.ResultatTirageStatus != ResultatTirageStatus.Excellent)
                        return true;
                }
                return false;
            }
        }
    }
}