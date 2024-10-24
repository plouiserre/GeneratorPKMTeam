using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class ChoisirPKMTypes : IChoisirPKMTypes
    {
        private PKMDonnees _datas;

        public ChoisirPKMTypes()
        {
        }

        public List<PKMType> SelectionnerPKMTypes(PKMDonnees datas)
        {
            _datas = datas.Clone() as PKMDonnees;
            var pkmTypes = new List<PKMType>();
            var allPKMTypes = _datas.PKMTypes;
            for (int i = 0; i < 9; i++)
            {
                var newIndex = RandomIndex(allPKMTypes.Count - 1);
                pkmTypes.Add(_datas.PKMTypes[newIndex]);
                allPKMTypes.Remove(allPKMTypes[newIndex]);
            }
            return pkmTypes;
        }

        private int RandomIndex(int maxIdPossible)
        {
            var random = new Random();
            int newIndex = random.Next(0, maxIdPossible);
            return newIndex;
        }
    }
}