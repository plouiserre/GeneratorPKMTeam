using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public interface IGererStarterPKM
    {
        PKM ChoisirStarter(string nomPKM);
        PKM RecupererStarter();
    }
}