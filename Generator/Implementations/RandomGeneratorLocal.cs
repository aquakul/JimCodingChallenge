using System;
using System.Threading.Tasks;
using Generator.Interfaces;

namespace Generator.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomGeneratorLocal : IGenerator
    {
        // private static Dictionary<Type, object> numericRandoms = new Dictionary<Type, object>() 
        // {
            
        // };

        private static readonly Random random = new();

        public static string Name => "local";

        public static string Shorthand => "l";

        /// <summary>
        /// Generates random numbers between 0 and 1000. The upper limit is kept low to
        /// avoid Arithmatic overlfows.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public Task<int[]> Generate(int min = 0, int max = 1000, int howMany = 2)
        {
            int[] values = new int[howMany];
            for(int i = 0; i < howMany; i++)
            {
                values[i] = random.Next(min, max);
            }

            return Task.FromResult(values);
        }
    }
}