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
        private List<PKM> _pkms;
        private int _generation;
        private PKM _starterPKM;

        public RecuperationPKMs(IPKMPersistence pkmPersistence, IGererStarterPKM gererStarterPKM, int generation)
        {
            _pkmPersistence = pkmPersistence;
            _generation = generation;
            _gererStarterPKM = gererStarterPKM;
        }

        public List<PKM> Recuperer(Dictionary<int, List<PKMType>> PKMTypesOrdonnees)
        {
            RecupererPKMDonnees();
            _starterPKM = _gererStarterPKM.RecupererStarter();
            var pkmsRecherches = new List<PKM>();
            foreach (var PKMTypes in PKMTypesOrdonnees)
            {
                if (StarterType(PKMTypes.Value))
                {
                    pkmsRecherches.Add(_starterPKM);
                }
                else if (PKMTypes.Value.Count == 1)
                {
                    var pkmSelectionne = _pkms.OrderBy(o => o.Nom).FirstOrDefault(o => o.PKMTypes.Count == 1 &&
                                        o.PKMTypes[0] == PKMTypes.Value[0].Nom && o.Generation <= _generation);
                    if (pkmSelectionne != null)
                        pkmsRecherches.Add(pkmSelectionne);
                    else
                        throw new PKMAvecTypeInexistantException(PKMTypes.Value[0].Nom);
                }
                else
                {
                    var pkmSelectionne = _pkms.OrderBy(o => o.Nom).FirstOrDefault(o => o.PKMTypes.Count > 1 && o.PKMTypes[0]
                                            == PKMTypes.Value[0].Nom && o.PKMTypes[1] == PKMTypes.Value[1].Nom
                                            && o.Generation <= _generation);
                    if (pkmSelectionne != null)
                        pkmsRecherches.Add(pkmSelectionne);
                    else
                        throw new PKMAvecTypeInexistantException(PKMTypes.Value[0].Nom + "-" + PKMTypes.Value[1].Nom);
                }
            }
            return pkmsRecherches;
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