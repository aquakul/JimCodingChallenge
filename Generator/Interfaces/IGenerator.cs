using System.Threading.Tasks;

namespace Generator.Interfaces
{
    /// <summary>
    /// This is a generic interface that outlines the required methods for a random number generator service
    /// </summary>
    public interface IGenerator
    {
        public static string Name {
            get;
        }

        public static string Shorthand {
            get;
        }
        
        public Task<int[]> Generate(int min = 0, int max = 1000, int howMany = 2);
    }
}