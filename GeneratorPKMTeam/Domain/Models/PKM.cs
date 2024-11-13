using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorPKMTeam.Domain.Models
{
    public class PKM
    {
        public int Generation { get; set; }
        public string Nom { get; set; }
        public List<string> PKMTypes { get; set; }
    }
}