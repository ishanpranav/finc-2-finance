// Portfolio.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace Finance;

public readonly struct Portfolio
{
    private readonly IList<double> _weights;

    public Portfolio(IList<double> weights)
    {
        _weights = weights;
    }

    public int Count
    {
        get
        {
            return _weights.Count;
        }
    }

    public double this[int index]
    {
        get
        {
            return _weights[index];
        }
    }

    public double Mean(SecurityTable table)
    {
        double result = 0;

        for (int i = 0; i < Count; i++)
        {
            result += _weights[i] * table.Mean(i);
        }

        return result;
    }

    public double StandardDeviation(SecurityTable table)
    {
        return Math.Sqrt(Variance(table));
    }

    public double Variance(SecurityTable table)
    {
        double result = 0;

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < Count; j++)
            {
                result += _weights[i] * _weights[j] * table.Covariance(i, j);
            }
        }

        return result;
    }

    public void MeanVarianceSharpeRatio(
        SecurityTable table,
        double riskFreeRate,
        out double mean,
        out double variance,
        out double sharpeRatio)
    {
        mean = Mean(table);
        variance = Variance(table);
        sharpeRatio = (mean - riskFreeRate) / Math.Sqrt(variance);
    }

    public static Portfolio Generate(Random random, int count)
    {
        double totalWeight = 0;
        double[] weights = new double[count];

        for (int i = 0; i < count; i++)
        {
            double weight = random.NextDouble();

            weights[i] = weight;
            totalWeight += weight;
        }

        for (int i = 0; i < count; i++)
        {
            double weight = weights[i] / totalWeight;

            weights[i] = weight;
        }

        return new Portfolio(weights);
    }

    private static bool IsUnfeasible(Vector<double> x)
    {
        return Math.Abs(x.Sum() - 1) > 0.0001;
    }

    private static Vector<double> Guess(SecurityTable table)
    {
        int n = table.N;

        return CreateVector.Dense(n, 1d / n);
    }

    public static Portfolio CreateMinimumVariancePortfolio(SecurityTable table)
    {
        double objective(Vector<double> x)
        {
            if (IsUnfeasible(x))
            {
                return double.PositiveInfinity;
            }

            return new Portfolio(x).Variance(table);
        }

        int n = table.N;

        return new Portfolio(FindMinimum.OfFunction(objective, Guess(table)));
    }

    public static Portfolio CreateMaximumSharpeRatioPortfolio(SecurityTable table)
    {
        double objective(Vector<double> x)
        {
            if (IsUnfeasible(x))
            {
                return double.PositiveInfinity;
            }

            new Portfolio(x).MeanVarianceSharpeRatio(table, 0, out _, out _, out double sharpeRatio);

            return -sharpeRatio;
        }

        return new Portfolio(FindMinimum.OfFunction(objective, Guess(table)));
    }
}
