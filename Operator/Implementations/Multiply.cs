using Operator.Implementations;
using Operator.Lambda;

namespace Operator
{
    public class Multiply : BaseStrategy<Multiply>
    {
        public Multiply(ICloudCalculateService lambda) : base(lambda)
        {
        }

        public static string Name => typeof(Multiply).Name;

        public static string Shorthand => "m";

        // maxValue means there is no restriction on the length
        public override int? AllowedLengthOfInput => null;

        public override int MinLengthOfInput => 2;

        public override int Compute(int[] input)
        {
            ValidateInput(input);
            return Compute(input, Name);
        }
    }
}