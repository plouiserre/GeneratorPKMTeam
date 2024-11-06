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
        private IRechercherPKMType _rechercherPKMTypeFaibles;
        private IRechercherPKMType _rechercherPKMTypeDangereux;
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
            InitiateRecherchePKMType(tousPKMTypes.PKMTypes);
            while (ContinuerACalculer())
            {
                if (comptage >= 100)
                    throw new CombinaisonParfaitesIntrouvablesException();
                var PKMTypesChoisis = _choisirPKMTypes.SelectionnerPKMTypes(tousPKMTypes);
                var PKMTypesfaibles = _rechercherPKMTypeFaibles.TrouverPKMType(PKMTypesChoisis);
                var PKMTypesDangereux = _rechercherPKMTypeDangereux.TrouverPKMType(PKMTypesChoisis);
                var classificationResult = _resultCombatPKMTypes.NoterResultatTirage(PKMTypesfaibles, PKMTypesDangereux, PKMTypesChoisis);
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

        private void InitiateRecherchePKMType(List<PKMType> PKMTypes)
        {
            _rechercherPKMTypeFaibles = new RechercherPKMTypeFaibles();
            _rechercherPKMTypeDangereux = new RechercherPKMTypeDangereux(PKMTypes);
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