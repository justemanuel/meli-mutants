using Microsoft.AspNetCore.Mvc;
using Mutant.Contracts;
using Mutant.Models.Database;
using Mutant.Models.Database.Entity;
using Mutant.Models.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Mutant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMutantService _mutantService;

        public MutantController(ApiContext context, IMutantService mutantService)
        {
            _context = context;
            _mutantService = mutantService;
        }

        #region API Endpoints

        [HttpPost]
        public async Task<IActionResult> Mutant([FromBody] SequenceDTO sequence)
        {
            if (sequence.DNA.Any(x => x.Length != sequence.DNA[0].Length))
                return BadRequest("Invalid dna chain. All elements must contain the same length.");

            var isMutant = _mutantService.AnalyzeSequence(sequence.DNA);

            var dna = string.Join(",", sequence.DNA);

            if (!await _mutantService.Exist(dna))
            {
                _mutantService.AddRecord(new DnaRecord()
                {
                    DNA = dna,
                    IsMutant = isMutant
                });

                await _context.SaveChangesAsync();
            }

            if (isMutant) return Ok();
            else return StatusCode(403);
        }

        #endregion
    }


}
