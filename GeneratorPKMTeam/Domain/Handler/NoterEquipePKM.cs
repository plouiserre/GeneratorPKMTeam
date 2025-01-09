using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler
{
    //TODo gérer le cas quand pKMs est NULL
    public class NoterEquipePKM : INoterEquipePKM
    {
        private IResultatCombatPKMTypes _resultCombatPKMTypes;
        private IPKMTypePersistence _pKMTypePersistence;
        private List<PKMType> _tousPKMTypes;

        public NoterEquipePKM(IResultatCombatPKMTypes resultCombatPKMTypes, IPKMTypePersistence pKMTypePersistence)
        {
            _resultCombatPKMTypes = resultCombatPKMTypes;
            _pKMTypePersistence = pKMTypePersistence;
        }

        public bool AccepterCetteEquipe(List<PKM> pKMs)
        {
            _tousPKMTypes = _pKMTypePersistence.GetPKMDonnees().PKMTypes;
            var pKMTypes = RecupererPKMTypes(pKMs);
            var evaluation = new EvaluerPKMChoisis(_resultCombatPKMTypes, _tousPKMTypes);
            var tirageANoter = new TiragePKMTypes();
            tirageANoter = evaluation.Evaluer(pKMTypes);
            return DeciderAcceptationTirage(tirageANoter);
        }

        private List<PKMType> RecupererPKMTypes(List<PKM> pKMs)
        {
            var pkmsType = new List<PKMType>();
            foreach (var pkm in pKMs)
            {
                foreach (var pkmTypeNom in pkm.PKMTypes)
                {
                    var pkmType = _tousPKMTypes.First(o => o.Nom == pkmTypeNom);
                    pkmsType.Add(pkmType);
                }
            }
            return pkmsType;
        }

        private bool DeciderAcceptationTirage(TiragePKMTypes tirage)
        {
            if (tirage.ResultatTirageStatus == ResultatTirageStatus.Parfait || tirage.ResultatTirageStatus == ResultatTirageStatus.Excellent)
                return true;
            else
                return false;
        }
    }
}