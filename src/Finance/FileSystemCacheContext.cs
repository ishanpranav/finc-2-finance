// CacheContext.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Finance;

public class FileSystemCacheContext : IDisposable
{
    private readonly string _path;
    private readonly SortedDictionary<DateTime, decimal> _adjustedCloses = new SortedDictionary<DateTime, decimal>();

    private bool _disposed;

    internal FileSystemCacheContext(string path)
    {
        _path = path;
    }

    public decimal this[DateTime timestamp]
    {
        get
        {
            return _adjustedCloses[timestamp];
        }
        set
        {
            _adjustedCloses[timestamp] = value;
        }
    }

    public void Flush()
    {
        using StreamWriter streamWriter = File.CreateText(_path);
        using CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        foreach (KeyValuePair<DateTime, decimal> entry in _adjustedCloses)
        {
            csvWriter.WriteField(entry.Key);
            csvWriter.WriteField(entry.Value);
            csvWriter.NextRecord();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Flush();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
