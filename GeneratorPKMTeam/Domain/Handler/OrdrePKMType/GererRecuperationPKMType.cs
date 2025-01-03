using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public class GererRecuperationPKMType : IGererRecuperationPKMType
    {
        private RecupererPKMTypeSimple _recupererPKMTypeSimple;
        private RecupererPKMTypeDouble _recupererPKMTypeDouble;
        private Dictionary<string, List<PKMType>> _pkmTypesDoublesRecuperes;
        private List<PKMType> _pkmTypeDoubles;
        private List<PKMType> _starterType;

        public GererRecuperationPKMType()
        {
        }


        public Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType, Dictionary<string, List<PKMType>> tousLesTypesPossibles)
        {
            var tousPKMTypesRecuperes = new Dictionary<string, List<PKMType>>();
            _starterType = starterType;
            try
            {
                _recupererPKMTypeDouble = new RecupererPKMTypeDouble();
                _pkmTypesDoublesRecuperes = _recupererPKMTypeDouble.RecupererPKMTypes(_starterType, tousLesTypesPossibles);

                _recupererPKMTypeSimple = new RecupererPKMTypeSimple(_pkmTypesDoublesRecuperes);

                var tousLesPkmTypes = _recupererPKMTypeSimple.RecupererPKMTypes(_starterType, tousLesTypesPossibles);

                foreach (var pkmType in tousLesPkmTypes)
                {
                    tousPKMTypesRecuperes.Add(pkmType.Key, pkmType.Value);
                }
            }
            catch (PasAssezPKMTypeSimpleSelectionnableException ex)
            {
                tousPKMTypesRecuperes = GererPasAssezPKMTypeSimpleSelectionnableException();
            }
            return tousPKMTypesRecuperes;
        }

        private Dictionary<string, List<PKMType>> GererPasAssezPKMTypeSimpleSelectionnableException()
        {
            int pkmTypeSimpleManquant = 6 - _pkmTypesDoublesRecuperes.Count;
            for (int i = 0; i < pkmTypeSimpleManquant; i++)
            {
                TrouverPkmTypeDoubleADiviser();

                AffecterNouveauPKMTypesSimples();
            }
            return _pkmTypesDoublesRecuperes;
        }

        private string pKMTypesDoubleADiviser()
        {
            var random = new Random();
            int index = random.Next(_pkmTypesDoublesRecuperes.Count);
            return _pkmTypesDoublesRecuperes.ElementAt(index).Key;
        }

        private void TrouverPkmTypeDoubleADiviser()
        {
            var pkmTypeDoubleDivisable = pKMTypesDoubleADiviser();
            _pkmTypeDoubles = _pkmTypesDoublesRecuperes[pkmTypeDoubleDivisable];

            while (PKMTypeCorrespondStarter(pkmTypeDoubleDivisable))
            {
                pkmTypeDoubleDivisable = pKMTypesDoubleADiviser();
                _pkmTypeDoubles = _pkmTypesDoublesRecuperes[pkmTypeDoubleDivisable];
            }

            _pkmTypesDoublesRecuperes.Remove(pkmTypeDoubleDivisable);
        }

        private void AffecterNouveauPKMTypesSimples()
        {
            var premierPKMTypeSimple = _pkmTypeDoubles[0];
            var deuxiemePKMTypeSimple = _pkmTypeDoubles[1];
            _pkmTypesDoublesRecuperes.Add(premierPKMTypeSimple.Nom, new List<PKMType>() { premierPKMTypeSimple });
            _pkmTypesDoublesRecuperes.Add(deuxiemePKMTypeSimple.Nom, new List<PKMType>() { deuxiemePKMTypeSimple });
        }

        private bool PKMTypeCorrespondStarter(string key)
        {
            var pkmTypeDoubleADiviser = _pkmTypesDoublesRecuperes[key];
            if ((pkmTypeDoubleADiviser[0].Nom == _starterType[0].Nom && pkmTypeDoubleADiviser[1].Nom == _starterType[1].Nom) || (pkmTypeDoubleADiviser[0].Nom == _starterType[1].Nom && pkmTypeDoubleADiviser[0].Nom == _starterType[1].Nom))
                return true;
            else
                return false;
        }

    }
}