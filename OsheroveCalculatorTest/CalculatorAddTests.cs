namespace OsheroveCalculatorTest
{
    using System;
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

        [Test]
        public void ReturnsSumOfManyCommaDelimitedNumbers()
        {
            Assert.AreEqual(17, Calculator.Add("2,3,12"));
        }

        [Test]
        public void ReturnsSumOfNewLineAndCommaDelimitedNumbers()
        {
            Assert.AreEqual(6, Calculator.Add("1\n2,3"));
        }

        [Test]
        public void AllowsConfigurableDelimiter()
        {
            Assert.AreEqual(3, Calculator.Add("//;\n1;2"));
        }

        [Test]
        public void ThrowsExceptionWithNegativeNumber()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Add("1,-2,4"));
        }

        [Test]
        public void NegativeNumbersReturnedInExceptionMessage()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Add("-12,1,-2,4"));
            StringAssert.Contains("-2", exception.Message);
            StringAssert.Contains("-12", exception.Message);
        }

        [Test]
        public void NumbersGreaterThan1000AreIgnored()
        {
            Assert.AreEqual(2, Calculator.Add("1001,2"));
        }

        [Test]
        public void NumbersEqualTo1000AreIncluded()
        {
            Assert.AreEqual(1002, Calculator.Add("1000,2"));
        }

        [Test]
        public void AllowsArbitraryLengthCustomDelimiters()
        {
            Assert.AreEqual(6, Calculator.Add("//[***]\n1***2***3"));
        }
    }
}