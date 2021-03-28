using Microsoft.AspNetCore.Mvc;
using Mutant.Contracts;
using System.Threading.Tasks;

namespace Mutant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IMutantService _mutantService;

        public StatsController(IMutantService mutantService)
        {
            _mutantService = mutantService;
        }

        #region API Endpoints

        [HttpGet]
        public async Task<IActionResult> Stats()
        {
            return Ok(await _mutantService.GetStats());
        }

        #endregion
    }
}
