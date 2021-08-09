using Operator.Implementations;
using Operator.Lambda;

namespace Operator
{
    public class Add : BaseStrategy<Add>
    {
        public Add(ICloudCalculateService lambda) : base(lambda)
        {
        }

        public static string Name => typeof(Add).Name;

        public static string Shorthand => "a";

        // maxValuemeans there is no restriction on the length
        public override int? AllowedLengthOfInput => null;

        public override int MinLengthOfInput => 2;

        public override int Compute(int[] input)
        {
            ValidateInput(input);
            return Compute(input, Name);
        }
    }
}