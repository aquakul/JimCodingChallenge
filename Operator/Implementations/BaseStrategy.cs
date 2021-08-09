using System;
using Operator.CloudIntegrations.Models;
using Operator.Lambda;

namespace Operator.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseStrategy<T> : IStrategy where T: BaseStrategy<T>
    {
        public abstract int? AllowedLengthOfInput {get; }

        public abstract int MinLengthOfInput {get;}
        protected ICloudCalculateService Lambda { get; }

        public BaseStrategy(ICloudCalculateService lambda)
        {
            Lambda = lambda;
        }

        public abstract int Compute(int[] input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        protected int Compute(int[] input, string op)
        {
            var result = Lambda.Calculate(new PayLoad()
            {
                Input = input,
                Op = op.ToLower()
            }).Result;

            return result;
        }

        public virtual bool ValidateInput(int[] input)
        {
            if(input == null || input.Length < MinLengthOfInput) throw new ArgumentException("Input empty or not enough inputs provided");
            
            if(AllowedLengthOfInput.HasValue && input.Length != AllowedLengthOfInput) throw new ArgumentException("Incorect input length");

            return true;
        }
    }
}