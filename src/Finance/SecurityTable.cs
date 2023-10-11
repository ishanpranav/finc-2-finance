// SecurityTable.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using AlphaVantageCore;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Finance;

public class SecurityTable
{
    private readonly string?[] _symbols;
    private readonly DateTime[] _timestamps;
    private readonly decimal[,] _adjustedCloses;
    private readonly double[,] _changes;
    private readonly double[] _means;
    private readonly double[,] _covariances;

    private SecurityTable(string?[] symbols, DateTime[] timestamps, decimal[,] adjustedCloses, double[,] changes)
    {
        int count = timestamps.Length - 1;
        int n = symbols.Length;

        _symbols = symbols;
        _timestamps = timestamps;
        _adjustedCloses = adjustedCloses;
        _changes = changes;
        _means = new double[n];
        _covariances = new double[n, n];

        for (int k = 1; k <= count; k++)
        {
            for (int security = 0; security < n; security++)
            {
                double change = Change(k, security);

                _means[security] += change;
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
                double di = Change(k, i) - _means[i];

                for (int j = 0; j < n; j++)
                {
                    _covariances[i, j] += di * (Change(k, j) - _means[j]);
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

    public int Rows
    {
        get
        {
            return _timestamps.Length;
        }
    }

    public int Securities
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

    public static async Task<SecurityTable?> LoadAsync(string path)
    {
        using StreamReader streamReader = File.OpenText(path);
        using CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        if (!await csvReader.ReadAsync())
        {
            return null;
        }

        int rows = csvReader.GetField<int>(index: 0);
        int securities = csvReader.GetField<int>(index: 1);

        if (!await csvReader.ReadAsync())
        {
            return null;
        }

        DateTime[] timestamps = new DateTime[rows];
        string?[] symbols = new string?[securities];

        for (int security = 0; security < securities; security++)
        {
            symbols[security] = csvReader.GetField<string>(security + 1);
        }

        if (!await csvReader.ReadAsync())
        {
            return null;
        }

        decimal[,] adjustedCloses = new decimal[rows, securities];
        double[,] changes = new double[rows, securities];

        for (int security = 0; security < securities; security++)
        {
            changes[0, security] = double.NaN;
        }

        timestamps[0] = csvReader.GetField<DateTime>(index: 0);

        for (int security = 0; security < securities; security++)
        {
            adjustedCloses[0, security] = csvReader.GetField<decimal>(security + 1);
        }

        int row = 1;
        double totalChange;

        while (await csvReader.ReadAsync())
        {
            totalChange = 0;
            timestamps[row] = csvReader.GetField<DateTime>(index: 0);

            for (int security = 0; security < securities; security++)
            {
                decimal adjustedClose = csvReader.GetField<decimal>(security + 1);
                double x1 = decimal.ToDouble(adjustedCloses[row - 1, security]);
                double x2 = decimal.ToDouble(adjustedClose);
                double change = (x2 - x1) / x1;

                adjustedCloses[row, security] = adjustedClose;
                changes[row, security] = change;
                totalChange += change;
            }

            row++;
        }

        return new SecurityTable(symbols, timestamps, adjustedCloses, changes);
    }

    private static async Task SaveAsync(string path, AlphaVantageClient client, params string[] symbols)
    {
        SortedDictionary<DateTime, decimal[]> timeSeries = new SortedDictionary<DateTime, decimal[]>();

        for (int security = 0; security < symbols.Length; security++)
        {
            IReadOnlyList<AlphaVantageAdjustedTimeSeriesResponse> responses = await client.GetTimeSeriesAsync(AlphaVantageAdjustedTimeSeries.Weekly, new AlphaVantageTimeSeriesRequest()
            {
                Symbol = symbols[security],
                OutputSize = AlphaVantageOutputSize.Compact
            });

            foreach (AlphaVantageAdjustedTimeSeriesResponse response in responses)
            {
                if (!timeSeries.TryGetValue(response.Timestamp, out decimal[]? row))
                {
                    row = new decimal[symbols.Length];
                    timeSeries[response.Timestamp] = row;
                }

                row[security] = response.AdjustedClose;
            }
        }

        DateTime[] timestamps = new DateTime[timeSeries.Count];

        timeSeries.Keys.CopyTo(timestamps, index: 0);

        foreach (DateTime timestamp in timestamps)
        {
            if (Array.TrueForAll(timeSeries[timestamp], x => x != 0))
            {
                break;
            }

            timeSeries.Remove(timestamp);
        }

        await using StreamWriter streamWriter = File.CreateText(path);
        await using CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteField(timeSeries.Count);
        csvWriter.WriteField(symbols.Length);

        await csvWriter.NextRecordAsync();

        csvWriter.WriteField(string.Empty);

        foreach (string? symbol in symbols)
        {
            csvWriter.WriteField(symbol);
        }

        await csvWriter.NextRecordAsync();

        foreach (KeyValuePair<DateTime, decimal[]> date in timeSeries)
        {
            csvWriter.WriteField(date.Key);

            foreach (decimal point in date.Value)
            {
                csvWriter.WriteField(point);
            }

            await csvWriter.NextRecordAsync();
        }
    }

    public static async Task<SecurityTable?> CreateAsync(string path, AlphaVantageClient client, params string[] symbols)
    {
        await SaveAsync(path, client, symbols);

        return await LoadAsync(path);
    }
}
