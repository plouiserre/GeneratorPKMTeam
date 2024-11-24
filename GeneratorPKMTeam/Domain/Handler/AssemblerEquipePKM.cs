using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class AssemblerEquipePKM : IAssemblerEquipePKM
    {
        private IDefinirOrdrePKMType _definirOrdrePKMType;
        private IRecuperationPKMs _recuperationPKMs;

        public AssemblerEquipePKM(IDefinirOrdrePKMType definirOrdrePKMType, IRecuperationPKMs recuperationPKMs)
        {
            _definirOrdrePKMType = definirOrdrePKMType;
            _recuperationPKMs = recuperationPKMs;
        }

        public List<PKM> Assembler(TiragePKMTypes tirage)
        {
            var resultat = new List<PKM>();

            try
            {
                var typeOrdonnees = _definirOrdrePKMType.Generer(tirage.PKMTypes);

                resultat = _recuperationPKMs.Recuperer(typeOrdonnees);

                return resultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}