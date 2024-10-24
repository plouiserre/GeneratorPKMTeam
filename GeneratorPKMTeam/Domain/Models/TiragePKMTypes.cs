using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Models
{
    public class TiragePKMTypes
    {
        public List<PKMType> PKMTypes { get; set; }
        public double NoteTirage { get; set; }
        public ResultatTirageStatus ResultatTirageStatus { get; set; }
    }
}