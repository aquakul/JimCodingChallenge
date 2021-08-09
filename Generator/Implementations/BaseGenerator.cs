using System.Threading.Tasks;
using Generator.Interfaces;

namespace Generator.Implementations
{
    public class BaseGenerator : IGenerator
    {
        public virtual Task<int[]> Generate(int min = 1, int max = 1000, int howMany = 2)
        {
            throw new System.NotImplementedException();
        }
    }
}