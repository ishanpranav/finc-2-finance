// FileSystemCache.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Finance;

public class FileSystemCache
{
    private readonly string _path;

    public FileSystemCache(string path)
    {
        _path = Path.GetFullPath(path);
    }

    private string GetPath(string symbol)
    {
        return Path.Combine(_path, Path.ChangeExtension(symbol, "csv"));
    }

    public bool Contains(string symbol)
    {
        return File.Exists(GetPath(symbol));
    }

    public FileSystemCacheContext Create(string symbol)
    {
        return new FileSystemCacheContext(GetPath(symbol));
    }

    public async Task<IReadOnlyDictionary<DateTime, decimal>> ReadAsync(string symbol)
    {
        SortedDictionary<DateTime, decimal> result = new SortedDictionary<DateTime, decimal>();

        using StreamReader streamReader = File.OpenText(GetPath(symbol));
        using CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        while (await csvReader.ReadAsync())
        {
            result[csvReader.GetField<DateTime>(index: 0)] =
              csvReader.GetField<decimal>(index: 1);
        }

        return result;
    }

    public async Task<SecurityTable> ToTableAsync()
    {
        string[] symbols = Directory
            .EnumerateFiles(_path, "*.csv")
            .Select(x => Path.GetFileNameWithoutExtension(x))
            .ToArray();
        int n = symbols.Length;
        SortedDictionary<DateTime, decimal[]> timeSeries = new SortedDictionary<DateTime, decimal[]>();

        for (int i = 0; i < n; i++)
        {
            foreach (KeyValuePair<DateTime, decimal> entry in await ReadAsync(symbols[i]))
            {
                if (!timeSeries.TryGetValue(entry.Key, out decimal[]? vector))
                {
                    vector = new decimal[n];
                    timeSeries[entry.Key] = vector;
                }

                vector[i] = entry.Value;
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

        int count = timeSeries.Count;
        decimal[,] adjustedCloses = new decimal[count, n];

        timestamps = new DateTime[count];

        timeSeries.Keys.CopyTo(timestamps, index: 0);

        for (int k = 0; k < count; k++)
        {
            for (int i = 0; i < n; i++)
            {
                adjustedCloses[k, i] = timeSeries[timestamps[k]][i];
            }
        }

        return new SecurityTable(symbols, timestamps, adjustedCloses);
    }
}
