using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    //TODO se débarasser de cette classe
    public class GererResultatTiragePKMTypes : IGererResultatTiragePKMTypes
    {

        public GererResultatTiragePKMTypes()
        {

        }

        public bool GarderTirage(TiragePKMTypes tirageATraiter)
        {
            if (tirageATraiter.NoteTirage >= 90)
                return true;
            else
                return false;
        }
    }
}