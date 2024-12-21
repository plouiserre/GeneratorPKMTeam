using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class ChoisirPKMTypes : IChoisirPKMTypes
    {
        private PKMDonnees _datas;
        private IGererStarterPKM _gererStarterPKM;
        private PKM _starterPKM;
        private List<PKMType> _tousLesTypesPKM;
        private List<PKMType> _PKMTypesStarter;
        private int _nombrePKMTypeChoisis;
        private int _tirageMiniPKMType;
        private int _tirageMaxPKMType;

        public ChoisirPKMTypes(IGererStarterPKM starterPKM)
        {
            _gererStarterPKM = starterPKM;
            _tirageMiniPKMType = 9;
            _tirageMaxPKMType = 12;
        }

        public List<PKMType> SelectionnerPKMTypes(PKMDonnees datas)
        {
            _PKMTypesStarter = new List<PKMType>();
            _tousLesTypesPKM = new List<PKMType>();
            _datas = datas.Clone() as PKMDonnees;
            RandomNombrePKMTypes();
            var pkmTypes = new List<PKMType>();
            _starterPKM = _gererStarterPKM.RecupererStarter();
            DeterminerPKMTypesPossibles();
            pkmTypes.AddRange(_PKMTypesStarter);
            int nombreTypesADecouvrir = _nombrePKMTypeChoisis - _starterPKM.PKMTypes.Count;
            for (int i = 0; i < nombreTypesADecouvrir; i++)
            {
                var newIndex = RandomIndex(_tousLesTypesPKM.Count - 1);
                pkmTypes.Add(_tousLesTypesPKM[newIndex]);
                _tousLesTypesPKM.Remove(_tousLesTypesPKM[newIndex]);
            }
            return pkmTypes;
        }

        private void RandomNombrePKMTypes()
        {
            var random = new Random();
            _nombrePKMTypeChoisis = random.Next(_tirageMiniPKMType, _tirageMaxPKMType + 1);
        }

        private int RandomIndex(int maxIdPossible)
        {
            var random = new Random();
            int newIndex = random.Next(0, maxIdPossible);
            return newIndex;
        }

        private void DeterminerPKMTypesPossibles()
        {
            RecupererPKMTypeStarter();
            foreach (var typePKM in _datas.PKMTypes)
            {
                if (!_PKMTypesStarter.Contains(typePKM))
                    _tousLesTypesPKM.Add(typePKM);
            }
        }

        private void RecupererPKMTypeStarter()
        {
            foreach (var typePKM in _datas.PKMTypes)
            {
                if (typePKM.Nom == _starterPKM.PKMTypes[0])
                {
                    _PKMTypesStarter.Add(typePKM);
                }
                else if (_starterPKM.PKMTypes.Count > 1 && typePKM.Nom == _starterPKM.PKMTypes[1])
                {
                    _PKMTypesStarter.Add(typePKM);
                }
            }
        }
    }
}