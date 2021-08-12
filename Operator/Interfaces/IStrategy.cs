namespace Operator
{
    // delegate which takes in a key and resolves to one of the implementations of the service based on the key
    public delegate IStrategy OperationResolver(string key);

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