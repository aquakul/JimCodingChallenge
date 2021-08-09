using Operator.Implementations;
using Operator.Lambda;

namespace Operator
{
    public class Subtract : BaseStrategy<Subtract>
    {
        public Subtract(ICloudCalculateService lambda) : base(lambda)
        {
        }

        public static string Name => typeof(Subtract).Name;

        public static string Shorthand => "s";

        // maxValue means there is no restriction on the length
        public override int? AllowedLengthOfInput => 2;

        public override int MinLengthOfInput => 2;

        public override int Compute(int[] input)
        {
            ValidateInput(input);
            return Compute(input, Name);
        }
    }
}