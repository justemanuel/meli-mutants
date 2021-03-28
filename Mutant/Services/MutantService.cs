using Microsoft.EntityFrameworkCore;
using Mutant.Contracts;
using Mutant.Models.Database;
using Mutant.Models.Database.Entity;
using Mutant.Models.DTOs;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Mutant.Services
{
    public class MutantService : IMutantService
    {
        private readonly ApiContext _context;
        private readonly static string[] MutantSequence = { "AAAA", "CCCC", "GGGG", "TTTT" };

        public MutantService(ApiContext context)
        {
            _context = context;
        }

        public async Task<StatDTO> GetStats()
        {
            var results = _context.DnaRecords.AsQueryable();

            var mutants = await results.CountAsync(x => x.IsMutant);
            var humans = await results.CountAsync() - mutants;

            var statDTO = new StatDTO()
            {
                CountMutantDNA = mutants,
                CountHumanDNA = humans,
                Ratio = humans > 0 ?
                    (mutants / (double)humans).ToString("0.#", CultureInfo.InvariantCulture) : "0.0"
            };

            return statDTO;
        }

        public async Task<bool> Exist(string dna)
        {
            return await _context.DnaRecords.AnyAsync(x => x.DNA == dna);
        }

        public void AddRecord(DnaRecord record)
        {
            _context.DnaRecords.Add(record);
        }

        public bool AnalyzeSequence(string[] dna)
        {
            var matrix = GetMatrix(dna);
            var hs = matrix.ToHashSet();

            var result = hs.Where(a => MutantSequence.Any(w => a.Contains(w))).Count();

            return result > 1;
        }

        public List<string> GetMatrix(string[] dna)
        {
            var lstWords = new List<string>();

            foreach (string sequence in dna)
            {
                lstWords.Add(sequence);
            }

            for (int row = 0; row < dna.Length; row++)
            {
                string strColumn = string.Empty;

                for (int column = 0; column < dna.Length; column++)
                {
                    strColumn += dna[column].ElementAt(row);
                }

                lstWords.Add(strColumn);
            }

            for (int i = 0; i < dna.Length / 2; i++)
            {
                string obliqueRight = string.Empty;
                string obliqueLeft = string.Empty;
                for (int j = 0; j < dna.Length - i; j++)
                {
                    obliqueRight += dna[j].ElementAt(j + i);

                    if (i != 0)
                    {
                        obliqueLeft += dna[j + i].ElementAt(j);
                    }
                }

                lstWords.Add(obliqueRight);

                if (!string.IsNullOrEmpty(obliqueLeft))
                    lstWords.Add(obliqueLeft);
            }


            return lstWords;
        }
    }
}
