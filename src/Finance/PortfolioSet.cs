// PortfolioSet.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Finance;

public class PortfolioSet
{
    private const int MeanOffset = 0;
    private const int VarianceOffset = 1;
    private const int SharpeRatioOffset = 2;
    private readonly double[,] _matrix;

    private PortfolioSet(SecurityTable table, int portfolios)
    {
        Table = table;
        _matrix = new double[portfolios, table.N + 3];
    }

    public int Portfolios
    {
        get
        {
            return _matrix.GetLength(dimension: 0);
        }
    }

    public SecurityTable Table { get; }
    public int MinimumVariancePortfolio { get; private set; }
    public int MinimumVarianceEfficientPortfolio { get; private set; }

    public double GetWeight(int portfolio, int i)
    {
        return _matrix[portfolio, i];
    }

    public double GetMean(int portfolio)
    {
        return _matrix[portfolio, Table.N + MeanOffset];
    }

    public double GetVariance(int portfolio)
    {
        return _matrix[portfolio, Table.N + VarianceOffset];
    }

    public double GetStandardDeviation(int portfolio)
    {
        return Math.Sqrt(GetVariance(portfolio));
    }

    public double GetSharpeRatio(int portfolio)
    {
        return _matrix[portfolio, Table.N + SharpeRatioOffset];
    }

    private static double[] Randomize(Random random, int count)
    {
        double totalWeight = 0;
        double[] weights = new double[count];

        for (int i = 0; i < count; i++)
        {
            double weight = random.Next();

            weights[i] = weight;
            totalWeight += weight;
        }

        for (int i = 0; i < count; i++)
        {
            double weight = weights[i] / totalWeight;

            weights[i] = weight;
        }

        return weights;
    }

    public static PortfolioSet Generate(SecurityTable table, Random random, int portfolios, double riskFreeRate)
    {
        int n = table.N;
        PortfolioSet result = new PortfolioSet(table, portfolios);
        double minimumVariance = double.PositiveInfinity;
        double maximumSharpeRatio = double.NegativeInfinity;

        for (int portfolio = 0; portfolio < portfolios; portfolio++)
        {
            double[] weights = Randomize(random, n);
            double mean = 0;
            double variance = 0;

            for (int i = 0; i < n; i++)
            {
                result._matrix[portfolio, i] = weights[i];
            }

            for (int i = 0; i < n; i++)
            {
                mean += weights[i] * table.Mean(i);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    variance += weights[i] * weights[j] * table.Covariance(i, j);
                }
            }

            double sharpeRatio = (mean - riskFreeRate) / Math.Sqrt(variance);

            result._matrix[portfolio, n + MeanOffset] = mean;
            result._matrix[portfolio, n + VarianceOffset] = variance;
            result._matrix[portfolio, n + SharpeRatioOffset] = sharpeRatio;

            if (variance < minimumVariance)
            {
                minimumVariance = variance;
                result.MinimumVariancePortfolio = portfolio;
            }

            if (sharpeRatio > maximumSharpeRatio)
            {
                maximumSharpeRatio = sharpeRatio;
                result.MinimumVarianceEfficientPortfolio = portfolio;
            }
        }

        return result;
    }
}
