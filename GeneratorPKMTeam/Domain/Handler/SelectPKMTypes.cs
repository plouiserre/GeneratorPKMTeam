using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class SelectPKMTypes : ISelectPKMTypes
    {
        private PKMDatas _datas;

        public SelectPKMTypes()
        {
        }

        public List<PKMType> ChoosePKMTypes(PKMDatas datas)
        {
            _datas = datas.Clone() as PKMDatas;
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