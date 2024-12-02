using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GererResultatTiragePKMTypes : IGererResultatTiragePKMTypes
    {
        private TiragePKMTypes tirageEtudie;
        private double notePireTirage;

        public GererResultatTiragePKMTypes()
        {

        }

        public bool GarderTirage(TiragePKMTypes tirageATraiter)
        {
            tirageEtudie = tirageATraiter;

            if (tirageATraiter.NoteTirage >= 90)
                return true;
            else
                return false;
        }
    }
}