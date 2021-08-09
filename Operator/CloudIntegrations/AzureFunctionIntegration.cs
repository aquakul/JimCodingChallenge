using Operator.CloudIntegrations.Models;
using Operator.Lambda;
using System.Threading.Tasks;

namespace Operator.CloudIntegrations
{
    /// <summary>
    /// This could be an alternate to AWS Lambda
    /// </summary>
    class AzureFunctionIntegration : ICloudCalculateService
    {
        public Task<int> Calculate(PayLoad data)
        {
            throw new System.NotImplementedException();
        }
    }
}
