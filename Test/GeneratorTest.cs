using System;
using Xunit;

using Generator.Implementations;

namespace Test
{
    public class GeneratorTest
    {
        private const string NameLocal = "local";
        private const string ShorthandLocal = "l";

        private const string NameAPI = "api";
        private const string ShorthandAPI = "a";

        [Fact]
        public void Name_RandomGeneratorLocal()
        {
            Assert.Equal(NameLocal, RandomGeneratorLocal.Name);
        }

        [Fact]
        public void Shorthand_RandomGeneratorLocal()
        {
            Assert.Equal(ShorthandLocal, RandomGeneratorLocal.Shorthand);
        }


        [Fact]
        public async void Generate_RandomGeneratorLocal_GivenInput()
        {
            var generator = new RandomGeneratorLocal();
            var result = await generator.Generate(0, 500, 2);

            Assert.Equal(2, result.Length);
            Assert.True(result[0] >= 0 && result[0] <= 500, "The generated number must be between 0 and 500");
            Assert.True(result[1] >= 0 && result[1] <= 500, "The generated number must be between 0 and 500");
        }

        [Fact]
        public void Name_RandomGeneratorAPI()
        {
            Assert.Equal(NameAPI, RandomGeneratorViaAPI.Name);
        }

        [Fact]
        public void Shorthand_RandomGeneratorAPI()
        {
            Assert.Equal(ShorthandAPI, RandomGeneratorViaAPI.Shorthand);
        }


        [Fact]
        public async void Generate_RandomGeneratorAPI_GivenInput()
        {
            // The 3rd API based random generator returns a set of random integers based on input.
            // 3rd party API is not owned by us. No testing for now
        }
    }
}
