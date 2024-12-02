using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class TrouverTypePKMEquipePKM : ITrouverTypePKMEquipePKM
    {
        private IChoisirMeilleuresCombinaisonsTypes _choisirMeilleuresCombinaisonsTypes;
        private IAssemblerEquipePKM _assemblerEquipePKM;

        public TrouverTypePKMEquipePKM(IChoisirMeilleuresCombinaisonsTypes choisirMeilleuresCombinaisonsTypes,
            IAssemblerEquipePKM assemblerEquipePKM)
        {
            _choisirMeilleuresCombinaisonsTypes = choisirMeilleuresCombinaisonsTypes;
            _assemblerEquipePKM = assemblerEquipePKM;
        }


        public List<PKM> GenererEquipePKM()
        {
            var equipePKM = new List<PKM>();
            bool equipePKMTrouve = false;
            while (!equipePKMTrouve)
            {
                var tirage = _choisirMeilleuresCombinaisonsTypes.GenererTirageParfait();
                equipePKM = _assemblerEquipePKM.Assembler(tirage);
                if (equipePKM != null)
                    equipePKMTrouve = true;
            }
            return equipePKM;
        }
    }
}