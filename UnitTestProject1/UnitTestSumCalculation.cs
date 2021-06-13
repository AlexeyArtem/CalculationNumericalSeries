using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestSumCalculation
    {
        //Расчет предела сходящегося ряда
        [TestMethod]
        public void TestCalculationLimit()
        {
            string function = "(1/2^n)+(1/3^(n+1))";
            NumericalSeries.NumericalSeries numericalSeries = new NumericalSeries.NumericalSeries(function, "n");
            double expected = 1.16;
            double result = Math.Round(numericalSeries.GetSum(1, 0.01).ValueLimit, 2);

            Assert.AreEqual(expected, result);
        }

        //Расчет частичной суммы ряда
        [TestMethod]
        public void TestCalculationPartialSum()
        {
            string function = "2^n";
            NumericalSeries.NumericalSeries numericalSeries = new NumericalSeries.NumericalSeries(function, "n");
            double expected = 2046;
            double result = numericalSeries.GetSum(1, 10).ValueLimit;

            Assert.AreEqual(expected, result);
        }

    }
}
