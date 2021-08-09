
using Xunit;
using Amazon.Lambda.TestUtilities;
using static CalculatorLambda.Function;
using System;

namespace CalculatorLambda.Tests
{
    public class DataFixture : IDisposable
    {
        public Function f => new Function();
        public TestLambdaContext context => new TestLambdaContext();

        public void Dispose()
        {
           
        }
    }


    /// <summary>
    /// This set of tests just checks the output against the inputs provided. No input validation is done
    /// </summary>
    public class FunctionTest : IClassFixture<DataFixture>, IDisposable
    {
        private readonly DataFixture _fixture;

        public FunctionTest(DataFixture fixture)
        {
            _fixture = fixture;
        }


        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-1, 2, 1)]
        public void AddTest_GivenInput(int a, int b, int expected)
        {
            var payload = new MyData()
            {
                input = new int[] { a, b },
                op = "add"
            };
            
            var result = _fixture.f.FunctionHandler(payload, _fixture.context);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, -1)]
        [InlineData(5, -1, 6)]
        [InlineData(5, 1, 4)]
        public void SubtractTest_GivenInput(int a, int b, int expected)
        {
            var payload = new MyData()
            {
                input = new int[] { a, b },
                op = "subtract"
            };

            var result = _fixture.f.FunctionHandler(payload, _fixture.context);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(5, -1, -5)]
        [InlineData(5, 0, 0)]
        public void MultiplyTest_GivenInput(int a, int b, int expected)
        {
            var payload = new MyData()
            {
                input = new int[] { a, b },
                op = "multiply"
            };

            var result = _fixture.f.FunctionHandler(payload, _fixture.context);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(1, 2, 0)]
        [InlineData(5, -1, -5)]
        [InlineData(5, 0, 0)]
        [InlineData(5, 2, 2)]
        [InlineData(6, 2, 3)]
        public void DivideTest_GivenInput(int a, int b, int expected)
        {
            var payload = new MyData()
            {
                input = new int[] { a, b },
                op = "multiply"
            };

            var result = _fixture.f.FunctionHandler(payload, _fixture.context);
            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            
        }
    }
}
