using Moq;
using Operator;
using Operator.CloudIntegrations.Models;
using Operator.Lambda;
using System;
using Xunit;

namespace Test
{
    /// <summary>
    /// Test all the Invalid cases. The mathemtical cases are taken care in Lambda test
    /// </summary>
    public class OperatorTest
    {

        [Theory]
        [InlineData(null)]
        [InlineData(new int[0])]
        [InlineData(new int[1] { 1 })]
        public void Add_InValidValues(int[] input)
        {
            var lambdaSettings = new LambdaConnectionSettings() { AccessKey = "abcd", Secret = "abcd", Region = "cds" };
            var lambda = new Mock<ICloudCalculateService>();
            var add = new Add(lambda.Object);

            Assert.Throws<ArgumentException>(() => add.Compute(input));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new int[0])]
        [InlineData(new int[1] { 1 })]
        [InlineData(new int[3] { 1, 2, 3 })]
        public void Subtract_InValidValues(int[] input)
        {
            var lambdaSettings = new LambdaConnectionSettings() { AccessKey = "abcd", Secret = "abcd", Region = "cds" };

            var lambda = new Mock<ICloudCalculateService>();
            var subtract = new Subtract(lambda.Object);

            Assert.Throws<ArgumentException>(() => subtract.Compute(input));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new int[0])]
        [InlineData(new int[1] { 1 })]
        [InlineData(new int[3] { 1, 2, 3 })]
        [InlineData(new int[2] { 1, 0 })]
        public void Divide_InValidValues(int[] input)
        {
            var lambda = new Mock<ICloudCalculateService>();
            lambda.Setup(x => x.Calculate(new PayLoad() { Input = input, Op = "divide" }).Result).Returns(1);
            var divide = new Divide(lambda.Object);

            Assert.Throws<ArgumentException>(() => divide.Compute(input));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new int[0])]
        [InlineData(new int[1] { 1 })]
        public void Multiply_InValidValues(int[] input)
        {
            var lambda = new Mock<ICloudCalculateService>();
            var multiply = new Multiply(lambda.Object);

            Assert.Throws<ArgumentException>(() => multiply.Compute(input));
        }
    }
}