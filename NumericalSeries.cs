using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;

namespace CalculationNumericalSeries
{
    class NumericalSeries
    {
        private Dictionary<string, FloatingPoint> variable;
        private SymbolicExpression funcExpression;

        public NumericalSeries(string function) 
        {
            funcExpression = SymbolicExpression.Parse(function);
            variable = new Dictionary<string, FloatingPoint>();
            variable.Add("n", 0);
        }

        public double GetSum(int startNumber, int endNumber)
        {
            double sum = 0;
            for (int i = startNumber; i <= endNumber; i++)
            {
                variable["n"] = i;
                sum += funcExpression.Evaluate(variable).RealValue;
            }

            return sum;
        }
    }

}
