using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.CustomException.Starter;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driven;
using GeneratorPKMTeam.Infrastructure.Services;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GererStarterPKM : IGererStarterPKM
    {
        private IPKMPersistence _pkmPersistence;
        private IEnumerable<PKM> _pkms;
        private PKM _pkmChoisi;

        public GererStarterPKM(IPKMPersistence persistence)
        {
            _pkmChoisi = new PKM();
            _pkmPersistence = persistence;
        }

        public PKM ChoisirStarter(string nomPKM)
        {
            _pkms = _pkmPersistence.GetPKMs().TousPKMs;
            if (!string.IsNullOrEmpty(_pkmChoisi.Nom))
                throw new StarterDejaExistantException();
            foreach (var pkm in _pkms)
            {
                if (pkm.Nom == nomPKM)
                {
                    _pkmChoisi = pkm;
                    break;
                }
            }
            if (string.IsNullOrEmpty(_pkmChoisi.Nom))
                throw new StarterIntrouvableException(nomPKM);
            return _pkmChoisi;
        }

        public PKM RecupererStarter()
        {
            if (string.IsNullOrEmpty(_pkmChoisi.Nom))
                throw new StarterAbsentException();
            return _pkmChoisi;
        }
    }
}