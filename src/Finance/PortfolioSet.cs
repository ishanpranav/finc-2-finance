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

    private readonly SecurityTable _table;
    private readonly double[,] _matrix;

    private PortfolioSet(SecurityTable table, int portfolios)
    {
        _table = table;
        _matrix = new double[portfolios, table.Securities + 3];
    }

    public int Portfolios
    {
        get
        {
            return _matrix.GetLength(dimension: 0);
        }
    }

    public double GetWeight(int portfolio, int security)
    {
        return _matrix[portfolio, security];
    }

    public void SetWeight(int portfolio, int security, double value)
    {
        _matrix[portfolio, security] = value;
    }

    public double GetMean(int portfolio)
    {
        return _matrix[portfolio, _table.Securities + MeanOffset];
    }

    public double GetVariance(int portfolio)
    {
        return _matrix[portfolio, _table.Securities + VarianceOffset];
    }

    public double GetStandardDeviation(int portfolio)
    {
        return Math.Sqrt(GetVariance(portfolio));
    }

    public double GetSharpeRatio(int portfolio)
    {
        return _matrix[portfolio, _table.Securities + SharpeRatioOffset];
    }

    public static PortfolioSet Generate(SecurityTable table, Random random, int portfolios, double riskFreeRate)
    {
        int count = table.Rows - 1;
        double[] means = new double[table.Securities];
        double[,] covariances = new double[table.Securities, table.Securities];
        PortfolioSet result = new PortfolioSet(table, portfolios);

        for (int row = 1; row < table.Rows; row++)
        {
            for (int security = 0; security < table.Securities; security++)
            {
                double change = table[row, security];

                means[security] += change;
            }
        }

        for (int security = 0; security < table.Securities; security++)
        {
            means[security] /= count;
        }

        for (int row = 1; row < table.Rows; row++)
        {
            for (int i = 0; i < table.Securities; i++)
            {
                double iDeviation = table[row, i] - means[i];

                for (int j = 0; j < table.Securities; j++)
                {
                    covariances[i, j] += iDeviation * (table[row, j] - means[j]);
                }
            }
        }

        for (int i = 0; i < table.Securities; i++)
        {
            for (int j = 0; j < table.Securities; j++)
            {
                covariances[i, j] /= count;
            }
        }

        for (int portfolio = 0; portfolio < portfolios; portfolio++)
        {
            double totalWeight = 0;
            double mean = 0;
            double variance = 0;

            for (int security = 0; security < table.Securities; security++)
            {
                double weight = random.NextDouble();

                result.SetWeight(portfolio, security, weight);

                totalWeight += weight;
            }

            for (int security = 0; security < table.Securities; security++)
            {
                double weight = result.GetWeight(portfolio, security) / totalWeight;

                result.SetWeight(portfolio, security, weight);

                mean += weight * means[security];
            }

            for (int i = 0; i < table.Securities; i++)
            {
                for (int j = 0; j < table.Securities; j++)
                {
                    variance += result.GetWeight(portfolio, i) * result.GetWeight(portfolio, j) * covariances[i, j];
                }
            }

            result._matrix[portfolio, table.Securities + MeanOffset] = mean;
            result._matrix[portfolio, table.Securities + VarianceOffset] = variance;
            result._matrix[portfolio, table.Securities + SharpeRatioOffset] = (mean - riskFreeRate) / Math.Sqrt(variance);
        }

        return result;
    }
}
