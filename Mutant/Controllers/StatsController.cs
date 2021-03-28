using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mutant.Models.Database;
using Mutant.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace Mutant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ApiContext _context;

        public StatsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Stats()
        {
            var results = _context.DnaRecords.AsQueryable();

            var mutants = await results.CountAsync(x => x.IsMutant);
            var humans = await results.CountAsync() - mutants;
            var gcd = GCD(mutants, humans);

            var statDTO = new StatDTO()
            {
                CountMutantDNA = mutants,
                CountHumanDNA = humans,
                Ratio = string.Format("{0}.{1}", mutants / gcd, humans / gcd)
            };

            return Ok(statDTO);
        }

        private int GCD(int A, int B)
        {
            return B == 0 ? Math.Abs(A) : GCD(B, A % B);
        }
    }
}
