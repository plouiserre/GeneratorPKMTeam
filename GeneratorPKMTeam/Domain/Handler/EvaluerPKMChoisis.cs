using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class EvaluerPKMChoisis : IEvaluerPKMChoisis
    {
        private RechercherPKMTypeFaibles _rechercherPKMTypeFaibles;
        private RechercherPKMTypeDangereux _rechercherPKMTypeDangereux;
        private IResultatCombatPKMTypes _resultCombatPKMTypes;
        private List<PKMType> _PKMTypes;

        public EvaluerPKMChoisis(IResultatCombatPKMTypes resultCombatPKMTypes, List<PKMType> TousPKMTypes)
        {
            _resultCombatPKMTypes = resultCombatPKMTypes;
            _PKMTypes = TousPKMTypes;
        }

        public TiragePKMTypes Evaluer(List<PKMType> PKMTypesChoisis)
        {
            InitiateRecherchePKMType(_PKMTypes);
            var PKMTypesfaibles = _rechercherPKMTypeFaibles.TrouverPKMType(PKMTypesChoisis);
            var PKMTypesDangereux = _rechercherPKMTypeDangereux.TrouverPKMType(PKMTypesChoisis);
            var rechercherPKMTypeContres = new RechercherPKMTypeContres(PKMTypesDangereux, _rechercherPKMTypeFaibles);
            var PKMTypesContres = rechercherPKMTypeContres.TrouverPKMType(PKMTypesChoisis);
            var classificationResult = _resultCombatPKMTypes.NoterResultatTirage(PKMTypesfaibles, PKMTypesDangereux, PKMTypesContres);
            var tirageATraiter = new TiragePKMTypes()
            {
                ResultatTirageStatus = classificationResult.ResultatStatus,
                NoteTirage = classificationResult.NoteResultatTirage,
                PKMTypes = PKMTypesChoisis
            };
            return tirageATraiter;
        }

        private void InitiateRecherchePKMType(List<PKMType> PKMTypes)
        {
            _rechercherPKMTypeFaibles = new RechercherPKMTypeFaibles();
            _rechercherPKMTypeDangereux = new RechercherPKMTypeDangereux(PKMTypes);
        }
    }
}