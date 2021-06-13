using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;

namespace NumericalSeries
{
    public class NumericalSeriesNotConvergent : Exception
    {
        public NumericalSeriesNotConvergent() : base() { }
        public NumericalSeriesNotConvergent(string msg) : base(msg) { }
        public NumericalSeriesNotConvergent(string msg, SystemException inner) : base(msg, inner) { }
        public NumericalSeriesNotConvergent(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class NegativeMemberNumberException : Exception
    {
        public NegativeMemberNumberException() : base() { }
        public NegativeMemberNumberException(string msg) : base(msg) { }
        public NegativeMemberNumberException(string msg, SystemException inner) : base(msg, inner) { }
        public NegativeMemberNumberException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public struct SumNumericalSeries
    {
        public double ValueLimit { get; }
        public Point[] PointsPartialSums { get; }
        public Point[] DerivativePoints { get; }

        public SumNumericalSeries(double valueLimit, Point[] pointsPartialSums, Point[] derivativePoints)
        {
            ValueLimit = valueLimit;
            PointsPartialSums = pointsPartialSums;
            DerivativePoints = derivativePoints;
        }
    }

    public class NumericalSeries
    {
        private Dictionary<string, FloatingPoint> variable;
        private SymbolicExpression funcExpression;
        private string nameVariable;

        public NumericalSeries(string function, string nameVariable)
        {
            this.nameVariable = nameVariable;
            funcExpression = SymbolicExpression.Parse(function);
            variable = new Dictionary<string, FloatingPoint> { { nameVariable, 0 } };
        }

        private double GetFunctionValue(int value)
        {
            if (value < 0) throw new NegativeMemberNumberException("Номер члена ряда не может быть отрицательным числом");

            variable[nameVariable] = value;
            return funcExpression.Evaluate(variable).RealValue;
        }

        private bool СheckNecessaryConvergenceCondition(int countElements)
        {
            bool isConvergent = true;
            double curValue, nextValue;
            for (int i = 1; i < countElements; i++)
            {
                curValue = GetFunctionValue(i);
                nextValue = GetFunctionValue(i + 1);
                if (Math.Abs(curValue) < Math.Abs(nextValue))
                {
                    isConvergent = false;
                    break;
                }
            }

            return isConvergent;
        }

        private bool CheckDalembertAttribute(int countElements)
        {
            bool isConvergent = true;
            int k = 0;
            for (int i = 1; i < countElements; i++)
            {
                if (Math.Abs(GetFunctionValue(i + 1) / GetFunctionValue(i)) < 1) k++;
                else k = 0;
            }
            if (k < 2) isConvergent = false;

            return isConvergent;
        }

        private bool CheckRaabeCondition(int countElements)
        {
            bool isConvergent = true;
            int k = 0;
            for (int i = 1; i < countElements; i++)
            {
                double r = i * (Math.Abs(GetFunctionValue(i)) / Math.Abs(GetFunctionValue(i + 1)) - 1);
                if (r <= 1) k++;
            }
            if (k > 1) isConvergent = false;

            return isConvergent;
        }

        public bool CheckConvergence(int countElements)
        {
            if (countElements < 0) throw new NegativeMemberNumberException("Количество элементов не может быть отрицательным числом");

            bool isConvergent;
            if (СheckNecessaryConvergenceCondition(countElements))
            {
                if (CheckDalembertAttribute(countElements))
                    if (CheckRaabeCondition(countElements)) isConvergent = true;
                    else isConvergent = false;

                else isConvergent = false;
            }
            else isConvergent = false;

            return isConvergent;
        }

        public SumNumericalSeries GetSum(int startNumber, int endNumber)
        {
            if (startNumber < 0 || endNumber < 0) throw new NegativeMemberNumberException("Номер члена ряда не может быть отрицательным числом");

            double sum = 0;
            List<Point> pointsPartialSums = new List<Point>();
            List<Point> points = new List<Point>();
            for (int i = startNumber; i <= endNumber; i++)
            {
                variable[nameVariable] = i;
                points.Add(new Point(i, GetFunctionValue(i)));
                pointsPartialSums.Add(new Point(i, GetPartialSum(i)));
                sum += funcExpression.Evaluate(variable).RealValue;
            }

            Derivative derivative = new Derivative(points);
            DifferentiationResult result = derivative.QuadraticInterpolation(1);

            return new SumNumericalSeries(sum, pointsPartialSums.ToArray(), result.DerivativePoints.ToArray());
        }

        public SumNumericalSeries GetSum(int startNumber, double accuracy)
        {
            if (startNumber < 0) throw new NegativeMemberNumberException("Номер члена ряда не может быть отрицательным числом");
            if (!CheckConvergence(1000)) throw new NumericalSeriesNotConvergent("Нельзя найти приблизительное значение предела расходящегося ряда");

            List<Point> pointsPartialSums = new List<Point>();
            List<Point> points = new List<Point>();
            double curSum, nextSum;
            int i = startNumber;
            do
            {
                curSum = GetPartialSum(i);
                pointsPartialSums.Add(new Point(i, curSum));
                points.Add(new Point(i, GetFunctionValue(i)));
                nextSum = GetPartialSum(i + 1);
                i++;
            }
            while (Math.Abs(Math.Abs(nextSum) - Math.Abs(curSum)) > Math.Abs(accuracy));
            pointsPartialSums.Add(new Point(i, nextSum));

            Derivative derivative = new Derivative(points);
            DifferentiationResult result = derivative.QuadraticInterpolation(1);

            return new SumNumericalSeries(nextSum, pointsPartialSums.ToArray(), result.DerivativePoints.ToArray());
        }

        public double GetPartialSum(int sumNumber)
        {
            if (sumNumber < 0) throw new NegativeMemberNumberException("Номер частичной суммы ряда не может быть отрицательным числом");

            double sum = 0;
            for (int i = 1; i <= sumNumber; i++)
                sum += GetFunctionValue(i);

            return sum;
        }
    }
}
