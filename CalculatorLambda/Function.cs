using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CalculatorLambda
{
    public class Function
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public int FunctionHandler(MyData data, ILambdaContext context)
        {
            int[] input = data.input;
            string op = data.op;

            if (input == null || input.Length < 2) {
                return 0;
            }

            switch (op)
            {
                case "+":
                case "add":
                    return Add(input);
                case "-":
                case "subtract":
                    return Subtract(input);
                case "*":
                case "multiply":
                    return Multiply(input);
                case "/":
                case "divide":
                    return Divide(input);
                default:                    
                    return 0;
            }
        }

        public class MyData
        {
            public int[] input { get; set; }
            public string op { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int Add(int[] input)
        {
            int result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += input[i];
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int Multiply(int[] input)
        {
            int result = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                result *= input[i];
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int Subtract(int[] input)
        {
            int result = input[0] - input[1];
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int Divide(int[] input)
        {
            if (input[1] == 0) return 0;

            int result = input[0] / input[1];
            return result;
        }
    }
}
