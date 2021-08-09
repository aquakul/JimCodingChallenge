using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Extensions
{
    public class OperatorJsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string rawData = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            rawData = JsonConvert.SerializeObject(rawData);
            try
            {
                InputEnums.Operator result = JsonConvert.DeserializeObject<InputEnums.Operator>(rawData); 
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (JsonSerializationException)
            {
                //do nothing since "failed" result is set by default
            }


            return Task.CompletedTask;
        }
    }

    public class GeneratorJsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string rawData = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
            rawData = JsonConvert.SerializeObject(rawData);
            try
            {
                InputEnums.RandomGeneratorSource result = JsonConvert.DeserializeObject<InputEnums.RandomGeneratorSource>(rawData);
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (JsonSerializationException)
            {
                //do nothing since "failed" result is set by default
            }


            return Task.CompletedTask;
        }

    }
}
