using System.Threading.Tasks;

namespace Generator.Interfaces
{
    // delegate which takes in a key and resolves to one of the implementations of the service based on the key
    public delegate IGenerator GeneratorResolver(string key);

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