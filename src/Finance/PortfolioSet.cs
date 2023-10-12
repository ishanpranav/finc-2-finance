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

    public double GetWeight(int portfolio, int security)
    {
        return _matrix[portfolio, security];
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

    public static PortfolioSet Generate(SecurityTable table, Random random, int portfolios, double riskFreeRate)
    {
        int n = table.N;
        PortfolioSet result = new PortfolioSet(table, portfolios);

        for (int k = 0; k < portfolios; k++)
        {
            Portfolio portfolio = Portfolio.Generate(random, n);

            portfolio.MeanVarianceSharpeRatio(
                table,
                riskFreeRate,
                out double mean,
                out double variance,
                out double sharpeRatio);

            for (int i = 0; i < n; i++)
            {
                result._matrix[k, i] = portfolio.Weight(i);
            }

            result._matrix[k, n + MeanOffset] = mean;
            result._matrix[k, n + VarianceOffset] = variance;
            result._matrix[k, n + SharpeRatioOffset] = sharpeRatio;
        }

        return result;
    }
}
