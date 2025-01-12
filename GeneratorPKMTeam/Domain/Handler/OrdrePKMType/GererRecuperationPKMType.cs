using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler.OrdrePKMType
{
    public class GererRecuperationPKMType : IGererRecuperationPKMType
    {
        private IRecupererPKMTypeSimple _recupererPKMTypeSimple;
        private IRecupererPKMTypeDouble _recupererPKMTypeDouble;
        private Dictionary<string, List<PKMType>> _pkmTypesEnregistresRecuperes;
        private List<PKMType> _pkmTypeDoubles;
        private List<PKMType> _starterType;

        public GererRecuperationPKMType(IRecupererPKMTypeDouble recupererPKMTypeDouble, IRecupererPKMTypeSimple recupererPKMTypeSimple)
        {
            _recupererPKMTypeDouble = recupererPKMTypeDouble;
            _recupererPKMTypeSimple = recupererPKMTypeSimple;
        }


        public Dictionary<string, List<PKMType>> RecupererPKMTypes(List<PKMType> starterType, Dictionary<string, List<PKMType>> tousLesTypesPossibles)
        {
            var tousPKMTypesRecuperes = new Dictionary<string, List<PKMType>>();
            _starterType = starterType;
            try
            {
                _pkmTypesEnregistresRecuperes = _recupererPKMTypeDouble.RecupererPKMTypes(_starterType, tousLesTypesPossibles);
                _recupererPKMTypeSimple.RecupererPKMTypeDoublesDejaCalcules(_pkmTypesEnregistresRecuperes);
                var tousLesPkmTypes = _recupererPKMTypeSimple.RecupererPKMTypes(_starterType, tousLesTypesPossibles);

                foreach (var pkmType in tousLesPkmTypes)
                {
                    tousPKMTypesRecuperes.Add(pkmType.Key, pkmType.Value);
                }
            }
            catch (PasAssezPKMTypeSimpleSelectionnableException ex)
            {
                tousPKMTypesRecuperes = GererPasAssezPKMTypeSimpleSelectionnableException(ex.PKMTypesSimplesDejaTrouves);
            }
            return tousPKMTypesRecuperes;
        }

        private Dictionary<string, List<PKMType>> GererPasAssezPKMTypeSimpleSelectionnableException(List<PKMType> pKMTypes)
        {
            AjouterPKMTypeSimpleRecuperesDeLerreur(pKMTypes);
            int pkmTypeSimpleManquant = 6 - _pkmTypesEnregistresRecuperes.Count;
            for (int i = 0; i < pkmTypeSimpleManquant; i++)
            {
                TrouverPkmTypeDoubleADiviser();

                AffecterNouveauPKMTypesSimples();
            }
            return _pkmTypesEnregistresRecuperes;
        }

        private void AjouterPKMTypeSimpleRecuperesDeLerreur(List<PKMType> pKMTypes)
        {
            foreach (var pkmType in pKMTypes)
            {
                _pkmTypesEnregistresRecuperes.Add(pkmType.Nom, new List<PKMType>() { pkmType });
            }
        }

        private string pKMTypesDoubleADiviser()
        {
            var pkmDoubles = _pkmTypesEnregistresRecuperes.Where(o => o.Value.Count == 2).ToDictionary(o => o.Key, o => o.Value);
            var random = new Random();
            int index = random.Next(pkmDoubles.Count);
            return pkmDoubles.ElementAt(index).Key;
        }

        private void TrouverPkmTypeDoubleADiviser()
        {
            var pkmTypeDoubleDivisable = pKMTypesDoubleADiviser();
            _pkmTypeDoubles = _pkmTypesEnregistresRecuperes[pkmTypeDoubleDivisable];

            while (PKMTypeCorrespondStarter(pkmTypeDoubleDivisable))
            {
                pkmTypeDoubleDivisable = pKMTypesDoubleADiviser();
                _pkmTypeDoubles = _pkmTypesEnregistresRecuperes[pkmTypeDoubleDivisable];
            }

            _pkmTypesEnregistresRecuperes.Remove(pkmTypeDoubleDivisable);
        }

        private void AffecterNouveauPKMTypesSimples()
        {
            var premierPKMTypeSimple = _pkmTypeDoubles[0];
            var deuxiemePKMTypeSimple = _pkmTypeDoubles[1];
            _pkmTypesEnregistresRecuperes.Add(premierPKMTypeSimple.Nom, new List<PKMType>() { premierPKMTypeSimple });
            _pkmTypesEnregistresRecuperes.Add(deuxiemePKMTypeSimple.Nom, new List<PKMType>() { deuxiemePKMTypeSimple });
        }

        private bool PKMTypeCorrespondStarter(string key)
        {
            var pkmTypeDoubleADiviser = _pkmTypesEnregistresRecuperes[key];
            if (_starterType.Count == 1)
                return false;
            else if ((pkmTypeDoubleADiviser[0].Nom == _starterType[0].Nom && pkmTypeDoubleADiviser[1].Nom == _starterType[1].Nom) || (pkmTypeDoubleADiviser[0].Nom == _starterType[1].Nom && pkmTypeDoubleADiviser[0].Nom == _starterType[1].Nom))
                return true;
            else
                return false;
        }

    }
}