using Mutant.Models.Database.Entity;
using Mutant.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mutant.Contracts
{
    public interface IMutantService
    {
        Task<StatDTO> GetStats();
        void AddRecord(DnaRecord record);
        Task<bool> Exist(string dna);
        bool AnalyzeSequence(string[] dna);
        List<string> GetMatrix(string[] dna);
    }
}
