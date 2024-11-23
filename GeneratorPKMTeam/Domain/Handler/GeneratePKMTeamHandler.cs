using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driving;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GeneratePKMTeamHandler : IGeneratePKMTeamHandler
    {
        private IAssemblerEquipePKM _assemblerEquipePKM;
        private IChoisirMeilleuresCombinaisonsTypes _meilleursCombinaisonsTypes;
        public List<TiragePKMTypes> TiragePKMTypes;

        public GeneratePKMTeamHandler(IChoisirMeilleuresCombinaisonsTypes meilleursCombinaisonsTypes, IAssemblerEquipePKM assemblerEquipePKM)
        {
            _assemblerEquipePKM = assemblerEquipePKM;
            _meilleursCombinaisonsTypes = meilleursCombinaisonsTypes;
        }

        public Dictionary<int, List<PKM>> Generer()
        {
            TiragePKMTypes = _meilleursCombinaisonsTypes.Choisir();
            var pkmsChoisis = _assemblerEquipePKM.Assembler(TiragePKMTypes);
            return pkmsChoisis;
        }
    }
}