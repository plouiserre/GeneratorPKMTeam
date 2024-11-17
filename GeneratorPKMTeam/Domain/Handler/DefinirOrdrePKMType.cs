using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class DefinirOrdrePKMType : IDefinirOrdrePKMType
    {
        public Dictionary<int, List<PKMType>> Generer(List<PKMType> TypesAOrdonnerParPKM)
        {
            var typesRegrouper = new Dictionary<int, List<PKMType>>();
            int indexPKMTypes = 0;
            var indexs = AvoirTousIndexPKMTypes();
            for (int i = 0; i < 6; i++)
            {
                if (i != 2 && i != 4 && i != 5)
                {
                    var pkmType = TypesAOrdonnerParPKM[indexs[0]];
                    typesRegrouper.Add(i, new List<PKMType>() { pkmType });
                    indexPKMTypes += 1;
                    indexs.RemoveAt(0);
                }
                else
                {
                    var premierPKMType = TypesAOrdonnerParPKM[indexs[0]];
                    var deuxiemePKMType = TypesAOrdonnerParPKM[indexs[1]];
                    typesRegrouper.Add(i, new List<PKMType>() { premierPKMType, deuxiemePKMType });
                    indexPKMTypes += 2;
                    indexs.RemoveAt(0);
                    indexs.RemoveAt(0);
                }
            }
            return typesRegrouper;
        }

        private List<int> AvoirTousIndexPKMTypes()
        {
            List<int> indexs = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            List<int> newIndexs = new List<int>() { };
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                int index = random.Next(0, indexs.Count);
                int number = indexs[index];
                newIndexs.Add(number);
                indexs.RemoveAt(index);
            }
            return newIndexs;
        }
    }
}