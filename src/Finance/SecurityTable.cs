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

    private SecurityTable(string?[] symbols, DateTime[] timestamps, decimal[,] adjustedCloses)
    {
        int count = timestamps.Length - 1;
        int n = symbols.Length;

        _symbols = symbols;
        _timestamps = timestamps;
        _adjustedCloses = adjustedCloses;
        _changes = new double[timestamps.Length, n];
        _means = new double[n];
        _covariances = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            _changes[0, i] = double.NaN;
        }

        for (int k = 1; k <= count; k++)
        {
            for (int i = 0; i < n; i++)
            {
                double x1 = decimal.ToDouble(adjustedCloses[k - 1, i]);
                double x2 = decimal.ToDouble(adjustedCloses[k, i]);

                _changes[k, i] = (x2 - x1) / x1;
            }
        }

        for (int k = 1; k <= count; k++)
        {
            for (int i = 0; i < n; i++)
            {
                double change = Change(k, i);

                _means[i] += change;
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
                    _covariances[i, j] += di * (Change(k, j) - Mean(j));
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

    public static async Task<SecurityTable?> LoadAsync(string path)
    {
        using StreamReader streamReader = File.OpenText(path);
        using CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        if (!await csvReader.ReadAsync())
        {
            return null;
        }

        int rows = csvReader.GetField<int>(index: 0);
        int n = csvReader.GetField<int>(index: 1);

        if (!await csvReader.ReadAsync())
        {
            return null;
        }

        DateTime[] timestamps = new DateTime[rows];
        string?[] symbols = new string?[n];

        for (int i = 0; i < n; i++)
        {
            symbols[i] = csvReader.GetField<string>(i + 1);
        }

        if (!await csvReader.ReadAsync())
        {
            return null;
        }

        decimal[,] adjustedCloses = new decimal[rows, n];

        timestamps[0] = csvReader.GetField<DateTime>(index: 0);

        for (int i = 0; i < n; i++)
        {
            adjustedCloses[0, i] = csvReader.GetField<decimal>(i + 1);
        }

        int k = 1;

        while (await csvReader.ReadAsync())
        {
            timestamps[k] = csvReader.GetField<DateTime>(index: 0);

            for (int i = 0; i < n; i++)
            {
                adjustedCloses[k, i] = csvReader.GetField<decimal>(i + 1);
            }

            k++;
        }

        return new SecurityTable(symbols, timestamps, adjustedCloses);
    }

    private static async Task SaveAsync(string path, AlphaVantageClient client, params string[] symbols)
    {
        SortedDictionary<DateTime, decimal[]> timeSeries = new SortedDictionary<DateTime, decimal[]>();
        int n = symbols.Length;

        for (int i = 0; i < n; i++)
        {
            IReadOnlyList<AlphaVantageAdjustedTimeSeriesResponse> responses = await client.GetTimeSeriesAsync(AlphaVantageAdjustedTimeSeries.Weekly, new AlphaVantageTimeSeriesRequest()
            {
                Symbol = symbols[i],
                OutputSize = AlphaVantageOutputSize.Compact
            });

            foreach (AlphaVantageAdjustedTimeSeriesResponse response in responses)
            {
                if (!timeSeries.TryGetValue(response.Timestamp, out decimal[]? vector))
                {
                    vector = new decimal[n];
                    timeSeries[response.Timestamp] = vector;
                }

                vector[i] = response.AdjustedClose;
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
