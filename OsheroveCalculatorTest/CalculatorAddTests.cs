namespace OsheroveCalculatorTest
{
    using NUnit.Framework;
    using OsheroveCalculator;

    public class CalculatorAddTests
    {
        [Test]
        public void ReturnsZeroWithEmptyString()
        {
            Assert.AreEqual(0, Calculator.Add(string.Empty));
        }

        [Test]
        public void ReturnsSingleNumber()
        {
            Assert.AreEqual(4, Calculator.Add("4"));
        }

        [Test]
        public void ReturnsSumOfTwoCommaDelimitedNumbers()
        {
            Assert.AreEqual(5, Calculator.Add("2,3"));
        }
    }
}