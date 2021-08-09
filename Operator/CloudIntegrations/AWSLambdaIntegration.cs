using Amazon.Lambda;
using Amazon.Lambda.Model;
using Newtonsoft.Json;
using Operator.CloudIntegrations.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Operator.Lambda
{
    /// <summary>
    /// 
    /// </summary>
    public class AWSLambdaCalculateService : ICloudCalculateService
    {
        private readonly LambdaConnectionSettings _options;

        public AWSLambdaCalculateService(LambdaConnectionSettings options)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<int> Calculate(PayLoad data)
        {
            AmazonLambdaClient client = new(_options.AccessKey, _options.Secret, Amazon.RegionEndpoint.GetBySystemName(_options.Region));

            var inputPayload = JsonConvert.SerializeObject(data);

            InvokeRequest ir = new()
            {
                FunctionName = "Calculator",
                InvocationType = InvocationType.RequestResponse,
                Payload = inputPayload
            };

            InvokeResponse response = await client.InvokeAsync(ir);

            var sr = new StreamReader(response.Payload);
            JsonReader reader = new JsonTextReader(sr);

            var serilizer = new JsonSerializer();
            var result = serilizer.Deserialize(reader);

            return Convert.ToInt32(result);
        }
    }
}
