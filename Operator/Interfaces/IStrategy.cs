namespace Operator
{
    public interface IStrategy
    {
        // some sensible name for the strategy.
        public static string Name {
            get;
        }

        // some shorthand for the operation
        public static string Shorthand {
            get;
        }

        // allowed length of inputs for the operation
        public int? AllowedLengthOfInput
        {
            get;
        }

        // any input should be atleast of length. This is common for all strategy
        public int MinLengthOfInput
        {
            get;
        }

        public int Compute(int[] input);

        public bool ValidateInput(int[] input);
    }
}