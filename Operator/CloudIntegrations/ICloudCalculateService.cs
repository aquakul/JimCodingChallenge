using Operator.CloudIntegrations.Models;
using System.Threading.Tasks;

namespace Operator.Lambda
{
    public interface ICloudCalculateService
    {
        public Task<int> Calculate(PayLoad data);
    }
}
