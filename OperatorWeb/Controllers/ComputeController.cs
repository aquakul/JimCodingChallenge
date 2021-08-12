using Microsoft.AspNetCore.Mvc;
using Operator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperatorWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputeController : ControllerBase
    {
        private readonly OperationResolver _operationResolver;

        public ComputeController(OperationResolver operationResolver)
        {
            _operationResolver = operationResolver;
        }

        [HttpPost]
        public ActionResult<IEnumerable<int>> Post([FromBody] int[] numbers, [FromQuery] string op = "add")
        {
            var strategy = _operationResolver(op.ToString());
            // strategy would never be null as we are defaulting to a known operation type 'local'. Just a sanity check
            if (strategy == null) return BadRequest("Invalid operation provided.");

            var result = strategy.Compute(numbers);

            return Ok($"Inputs are {string.Join(",", numbers)}.\n\nThe result of compute is {result}");
        }
    }
}
