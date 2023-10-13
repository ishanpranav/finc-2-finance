// PortfolioSet.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Finance;

public class PortfolioSet
{
    private const int MeanOffset = 0;
    private const int VarianceOffset = 1;
    private readonly double[,] _matrix;

    private PortfolioSet(SecurityTable table, int portfolios)
    {
        Table = table;
        _matrix = new double[portfolios, table.N + 2];
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

    public double Weight(int portfolio, int i)
    {
        return _matrix[portfolio, i];
    }

    public double Mean(int portfolio)
    {
        return _matrix[portfolio, Table.N + MeanOffset];
    }

    public double Variance(int portfolio)
    {
        return _matrix[portfolio, Table.N + VarianceOffset];
    }

    public double StandardDeviation(int portfolio)
    {
        return Math.Sqrt(Variance(portfolio));
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

    public static PortfolioSet Generate(SecurityTable table, Random random, int portfolios)
    {
        int n = table.N;
        PortfolioSet result = new PortfolioSet(table, portfolios);
        double minimumVariance = double.PositiveInfinity;
        double maximumSharpeProxy = double.NegativeInfinity;

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

            double sharpeProxy = mean / variance;

            result._matrix[portfolio, n + MeanOffset] = mean;
            result._matrix[portfolio, n + VarianceOffset] = variance;
            
            if (variance < minimumVariance)
            {
                minimumVariance = variance;
                result.MinimumVariancePortfolio = portfolio;
            }
            
            if (sharpeProxy > maximumSharpeProxy)
            {
                maximumSharpeProxy = sharpeProxy;
                result.MinimumVarianceEfficientPortfolio = portfolio;
            }
        }

        return result;
    }
}
