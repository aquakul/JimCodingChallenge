using System.Threading.Tasks;
using Generator.Interfaces;
using Generator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Operator;
using Web.Models;
using static Web.Extensions.ApplicationServiceExtensions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IOptions<RandomGeneratorSelectorSettings> _randomGeneratorSelector;
        private readonly OperationResolver _operationResolver;
        private readonly GeneratorResolver _generatorResolver;

        public CalculatorController(IOptions<RandomGeneratorSelectorSettings> randomGeneratorSelector,
            GeneratorResolver generatorResolver, OperationResolver operationResolver)
        {
            _generatorResolver = generatorResolver;
            _randomGeneratorSelector = randomGeneratorSelector;
            _operationResolver = operationResolver;
        }

        [HttpGet(Name = nameof(Calculate))]
        public async Task<ActionResult<long>> Calculate([FromQuery] string op = "add", 
            [FromQuery] string source = "local")
        {
            var generator = _generatorResolver(source.ToString());
            // generator would never be null as we are defaulting to a known source type 'local'. Just a sanity check
            if(generator == null) return BadRequest("Invlaid number generator provided");

            var numbers = await generator.Generate();

            var strategy = _operationResolver(op.ToString());
            // strategy would never be null as we are defaulting to a known operation type 'local'. Just a sanity check
            if(strategy == null) return BadRequest("Invalid operation provided.");

            var result = strategy.Compute(numbers);

            return Ok($"Inputs are {string.Join(",", numbers)}.\n\nThe result of compute is {result}");
        }
    }
}