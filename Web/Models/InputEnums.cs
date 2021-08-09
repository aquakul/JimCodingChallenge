using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Web.Extensions;

namespace Web.Models
{
    public class InputEnums
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [ModelBinder(typeof(OperatorJsonModelBinder))]
        public enum Operator
        {
            [EnumMember(Value = "add")]
            Add,

            [EnumMember(Value = "subtract")]
            Subtract,

            [EnumMember(Value = "divide")]
            Divide,

            [EnumMember(Value = "multiply")]
            Multiply
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [ModelBinder(typeof(GeneratorJsonModelBinder))]
        public enum RandomGeneratorSource
        {
            [EnumMember(Value = "local")]
            Local,

            [EnumMember(Value = "api")]
            Api
        }
    }
}
