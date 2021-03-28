using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutant.Models.DTOs
{
    public class StatDTO
    {
        public int CountMutantDNA { get; set; }
        public int CountHumanDNA { get; set; }
        public string Ratio { get; set; }
    }
}
