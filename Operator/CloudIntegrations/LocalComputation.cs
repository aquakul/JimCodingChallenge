using Operator.CloudIntegrations.Models;
using Operator.Lambda;
using System;
using System.Threading.Tasks;

namespace Operator.CloudIntegrations
{
    // this would be a fallback
    class LocalComputation : ICloudCalculateService
    {
        public Task<int> Calculate(PayLoad data)
        {
            throw new NotImplementedException();
        }
    }
}
