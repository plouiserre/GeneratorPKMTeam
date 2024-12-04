using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public interface IStarterPKM
    {
        PKM ChoisirStarter(string nomPKM);
        PKM RecupererStarter();
    }
}