using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class TrouverEquipePKM : ITrouverEquipePKM
    {
        private INoterEquipePKM _noterEquipePKM;
        private IAssemblerEquipePKM _assemblerEquipePKM;
        private IResultatCombatPKMTypes _resultCombatPKMTypes;


        public TrouverEquipePKM(INoterEquipePKM noterEquipePKM, IAssemblerEquipePKM assemblerEquipePKM)
        {
            _noterEquipePKM = noterEquipePKM;
            _assemblerEquipePKM = assemblerEquipePKM;
        }


        public List<PKM> GenererEquipePKM()
        {
            bool equipePKMTrouve = false;
            List<PKM> equipePKM = new List<PKM>();
            while (!equipePKMTrouve)
            {
                equipePKM = _assemblerEquipePKM.Assembler();
                if (equipePKM != null)
                {
                    bool tirageAccepter = _noterEquipePKM.AccepterCetteEquipe(equipePKM);
                    if (tirageAccepter)
                        equipePKMTrouve = true;
                }
            }
            return equipePKM;
        }
    }
}