using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mutant.Models.Database;
using Mutant.Models.Database.Entity;
using Mutant.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        private readonly static string[] MutantSequence = { "AAAA", "CCCC", "GGGG", "TTTT" };
        private readonly ApiContext _context;

        public MutantController(ApiContext context)
        {
            _context = context;
        }

        #region API Endpoints
        [HttpPost]
        public async Task<IActionResult> Mutant([FromBody] SequenceDTO sequence)
        {
            if (sequence.DNA.Any(x => x.Length != sequence.DNA[0].Length))
                return BadRequest("Invalid dna chain. All elements must contain the same length.");

            var isMutant = AnalyzeSequence(sequence.DNA);

            DnaRecord record = new DnaRecord()
            {
                DNA = string.Join(",", sequence.DNA),
                IsMutant = isMutant
            };

            _context.DnaRecords.Add(record);
            await _context.SaveChangesAsync();

            if (isMutant) return Ok();
            else return StatusCode(403);
        }
        #endregion


        #region Private Methods
        private static bool AnalyzeSequence(string[] dna)
        {
            var matrix = GetMatrix(dna);
            var hs = matrix.ToHashSet();

            var result = hs.Where(a => MutantSequence.Where(w => a.Contains(w)).Any()).Count();

            return result > 1;
        }

        private static List<string> GetMatrix(string[] dna)
        {
            var lstWords = new List<string>();

            foreach(string sequence in dna)
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

            for (int i = 0; i < dna.Length /2; i++)
            {
                string obliqueRight = string.Empty;
                string obliqueLeft = string.Empty;
                for (int j = 0; j < dna.Length - i; j++)
                {
                    obliqueRight += dna[j].ElementAt(j + i);

                    if(i != 0)
                    {
                        obliqueLeft += dna[j + i].ElementAt(j);
                    }
                }

                lstWords.Add(obliqueRight);

                if(!string.IsNullOrEmpty(obliqueLeft))
                    lstWords.Add(obliqueLeft);
            }


            return lstWords;
        }
        #endregion
    }

    
}
