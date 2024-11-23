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

        public Dictionary<int, List<PKM>> Assembler(List<TiragePKMTypes> tirages)
        {
            var resultat = new Dictionary<int, List<PKM>>();

            for (int i = 0; i < tirages.Count; i++)
            {
                var tirage = tirages[i];

                var typeOrdonnees = _definirOrdrePKMType.Generer(tirage.PKMTypes);

                var pkmsTrouves = _recuperationPKMs.Recuperer(typeOrdonnees);

                resultat.Add(i, pkmsTrouves);
            }
            return resultat;
        }
    }
}