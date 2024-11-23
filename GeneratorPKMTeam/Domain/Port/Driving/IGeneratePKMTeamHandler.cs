using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Port.Driving
{
    public interface IGeneratePKMTeamHandler
    {
        Dictionary<int, List<PKM>> Generer();
    }
}