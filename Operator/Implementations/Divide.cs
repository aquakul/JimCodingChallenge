using System;
using Operator.Implementations;
using Operator.Lambda;

namespace Operator
{
    public class Divide : BaseStrategy<Divide>
    {
        public Divide(ICloudCalculateService lambda) : base(lambda)
        {
        }

        public static string Name =>  typeof(Divide).Name;

        public static string Shorthand => "d";

        // maxValue means there is no restriction on the length
        public override int? AllowedLengthOfInput => 2;

        public override int MinLengthOfInput => 2;

        public override int Compute(int[] input)
        {
            ValidateInput(input);
            return Compute(input, Name);
        }

        public override bool ValidateInput(int[] input)
        {
            base.ValidateInput(input);

            // divisor cannot be 0
            if(input[1] == 0) throw new ArgumentException("Invalid divisor");

            return true;
        }
    }
}