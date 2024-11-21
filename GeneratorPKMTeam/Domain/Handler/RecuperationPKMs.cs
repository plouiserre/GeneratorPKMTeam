using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class RecuperationPKMs : IRecuperationPKMs
    {
        private List<PKM> _pkms;
        private int _generation;

        public RecuperationPKMs(List<PKM> pKMs, int generation)
        {
            _pkms = pKMs;
            _generation = generation;
        }

        public List<PKM> Recuperer(Dictionary<int, List<PKMType>> PKMTypesOrdonnees)
        {
            var pkmsRecherches = new List<PKM>();
            foreach (var PKMTypes in PKMTypesOrdonnees)
            {
                if (PKMTypes.Value.Count == 1)
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
    }
}