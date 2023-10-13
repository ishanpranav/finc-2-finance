// SecurityTable.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Finance;

public class SecurityTable
{
    private readonly string?[] _symbols;
    private readonly DateTime[] _timestamps;
    private readonly decimal[,] _adjustedCloses;
    private readonly double[,] _changes;
    private readonly double[] _means;
    private readonly double[,] _covariances;

    internal SecurityTable(string?[] symbols, DateTime[] timestamps, decimal[,] adjustedCloses)
    {
        int count = timestamps.Length - 1;
        int n = symbols.Length;

        _symbols = symbols;
        _timestamps = timestamps;
        _adjustedCloses = adjustedCloses;
        _changes = new double[timestamps.Length, n];
        _means = new double[n];
        _covariances = new double[n, n];

        for (int k = 1; k <= count; k++)
        {
            for (int i = 0; i < n; i++)
            {
                double x1 = decimal.ToDouble(adjustedCloses[k - 1, i]);
                double x2 = decimal.ToDouble(adjustedCloses[k, i]);
                double change = (x2 - x1) / x1;

                _changes[k, i] = change;
            }
        }

        for (int i = 0; i < n; i++)
        {
            _changes[0, i] = double.NaN;
        }

        for (int k = 1; k <= count; k++)
        {
            for (int i = 0; i < n; i++)
            {
                _means[i] += Change(k, i);
            }
        }

        for (int i = 0; i < n; i++)
        {
            _means[i] /= count;
        }

        for (int k = 1; k <= count; k++)
        {
            for (int i = 0; i < n; i++)
            {
                double di = Change(k, i) - Mean(i);

                for (int j = 0; j < n; j++)
                {
                    double dj = Change(k, j) - Mean(j);

                    _covariances[i, j] += di * dj;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _covariances[i, j] /= count;
            }
        }
    }

    public int Count
    {
        get
        {
            return _timestamps.Length;
        }
    }

    public int N
    {
        get
        {
            return _symbols.Length;
        }
    }

    public IReadOnlyList<DateTime> Timestamps
    {
        get
        {
            return _timestamps;
        }
    }

    public IReadOnlyList<string?> Symbols
    {
        get
        {
            return _symbols;
        }
    }

    public double Change(int k, int i)
    {
        return _changes[k, i];
    }

    public double Mean(int i)
    {
        return _means[i];
    }

    public double Covariance(int i, int j)
    {
        return _covariances[i, j];
    }

    public decimal AdjustedClose(int row, int security)
    {
        return _adjustedCloses[row, security];
    }
}
