using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;

namespace GeneratorPKMTeam.Domain.Handler.SelectionPKM
{
    public class RecuperationPKMs : IRecuperationPKMs
    {
        private IPKMPersistence _pkmPersistence;
        private IGererStarterPKM _gererStarterPKM;
        private IDeterminerMeilleurPKMParStats _determinerMeilleurPKMParStats;
        private List<PKM> _pkms;
        private List<PKM> _pkmsRecherches;
        private int _generation;
        private PKM _starterPKM;

        public RecuperationPKMs(IPKMPersistence pkmPersistence, IGererStarterPKM gererStarterPKM,
                    IDeterminerMeilleurPKMParStats determinerMeilleurPKMParStats, int generation)
        {
            _pkmPersistence = pkmPersistence;
            _generation = generation;
            _gererStarterPKM = gererStarterPKM;
            _determinerMeilleurPKMParStats = determinerMeilleurPKMParStats;
        }

        public List<PKM> Recuperer(Dictionary<int, List<PKMType>> PKMTypesOrdonnees)
        {
            RecupererPKMDonnees();
            _starterPKM = _gererStarterPKM.RecupererStarter();
            _pkmsRecherches = new List<PKM>();
            foreach (var PKMTypes in PKMTypesOrdonnees)
            {
                if (StarterType(PKMTypes.Value))
                {
                    _pkmsRecherches.Add(_starterPKM);
                }
                else if (PKMTypes.Value.Count == 1)
                {
                    SelectionnerPKMTypeSimple(PKMTypes.Value[0].Nom);
                }
                else
                {
                    SelectionnerPKMTypeDouble(PKMTypes.Value[0].Nom, PKMTypes.Value[1].Nom);
                }
            }
            return _pkmsRecherches;
        }

        private void SelectionnerPKMTypeSimple(string pkmTypeNom)
        {
            var pkmSelectionnes = _pkms.OrderBy(o => o.Nom).Where(o => o.PKMTypes.Count == 1 &&
                                        o.PKMTypes[0] == pkmTypeNom && o.Generation <= _generation).ToList();
            if (pkmSelectionnes == null || pkmSelectionnes.Count == 0)
                throw new PKMAvecTypeInexistantException(pkmTypeNom);
            else
            {
                var meilleurPkm = SelectionnerMeilleurPkm(pkmSelectionnes);
                _pkmsRecherches.Add(meilleurPkm);
            }
        }

        private void SelectionnerPKMTypeDouble(string premierPKMTypeNom, string deuxiemePKMTypeNom)
        {
            var pkmSelectionnes = _pkms.OrderBy(o => o.Nom).Where(o => o.PKMTypes.Count > 1 && o.PKMTypes[0]
                                            == premierPKMTypeNom && o.PKMTypes[1] == deuxiemePKMTypeNom
                                            && o.Generation <= _generation).ToList();
            if (pkmSelectionnes == null || pkmSelectionnes.Count == 0)
                throw new PKMAvecTypeInexistantException(premierPKMTypeNom + "-" + deuxiemePKMTypeNom);
            else
            {
                var meilleurPkm = SelectionnerMeilleurPkm(pkmSelectionnes);
                _pkmsRecherches.Add(meilleurPkm);
            }
        }

        private PKM SelectionnerMeilleurPkm(List<PKM> pKMs)
        {
            var meilleurPkms = _determinerMeilleurPKMParStats.Calculer(pKMs);
            var pkmSelectionne = meilleurPkms.First();
            return pkmSelectionne;
        }

        private bool StarterType(List<PKMType> types)
        {
            if (_starterPKM.PKMTypes.Count != types.Count)
                return false;
            else
            {
                for (int i = 0; i < types.Count; i++)
                {
                    if (types[i].Nom == _starterPKM.PKMTypes[i])
                        continue;
                    else
                        return false;
                }
                return true;
            }
        }

        private void RecupererPKMDonnees()
        {
            var pkms = _pkmPersistence.GetPKMs();
            _pkms = pkms.TousPKMs;
        }
    }
}