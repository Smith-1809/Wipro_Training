using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CalculatorApp;

namespace CalculatorAppTest
{
    [TestClass]
    public class UnitTest1
    {
        private Calculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [TestMethod]
        public void Add_ValidNumbers_ReturnsCorrectSum()
        {
            double result = _calculator.Add(10, 5);
            Assert.AreEqual(15, result, "Addition logic failed.");
        }

        [TestMethod]
        public void Add_EdgeCaseZero_ReturnsSameNumber()
        {
            double result = _calculator.Add(15, 0);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Subtract_ValidNumbers_ReturnsCorrectDifference()
        {
            double result = _calculator.Subtract(10, 4);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Multiply_ValidNumbers_ReturnsCorrectProduct()
        {
            double result = _calculator.Multiply(5, 5);
            Assert.AreEqual(25, result);
        }

        [TestMethod]
        public void Divide_ValidNumbers_ReturnsCorrectQuotient()
        {
            double result = _calculator.Divide(10, 2);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            _calculator.Divide(10, 0);
        }
    }
}
