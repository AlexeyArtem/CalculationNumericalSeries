using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class СonvergenceVerificationUnitTest
    {
        private bool CheckConvergence(string function)
        {
            NumericalSeries.NumericalSeries series = new NumericalSeries.NumericalSeries(function, "n");
            return series.CheckConvergence(1000);
        }

        //Знакочередующийся расходящийся ряд
        [TestMethod]
        public void TestAlternatingSeries_ReturnedFalse()
        {
            bool expected = false;
            bool result = CheckConvergence("((-2)^(n))*1/2^n");

            Assert.AreEqual(expected, result);
        }

        //Знакочередующийся сходящийся ряд
        [TestMethod]
        public void TestAlternatingSeries_ReturnedTrue()
        {
            bool expected = true;
            bool result = CheckConvergence("((-1)^(n+1))*1/n^2");

            Assert.AreEqual(expected, result);
        }

        //Знакопостоянный сходящийся ряд
        [TestMethod]
        public void TestConstantSeries_ReturnedTrue()
        {
            bool expected = true;
            bool result = CheckConvergence("1/2^n");

            Assert.AreEqual(expected, result);
        }

        //Знакопостоянный расходящийся ряд
        [TestMethod]
        public void TestConstantSeries_ReturnedFalse()
        {
            bool expected = false;
            bool result = CheckConvergence("1/n");

            Assert.AreEqual(expected, result);
        }
    }
}
