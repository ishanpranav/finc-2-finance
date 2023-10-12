// Program.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using AlphaVantageCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.AlphaVantage;

internal static class Program
{
    private static async Task Main()
    {
        string? apiKey = Environment.GetEnvironmentVariable("API_KEY");

        if (apiKey == null)
        {
            throw new Exception();
        }

        AlphaVantageClient client = new AlphaVantageClient(apiKey);

        await SaveAsync(client, "AAPL", "MSFT", "AMZN", "NVDA", "GOOG", "META", "TSLA", "GOOGL", "BRK.B", "UNH", "JPM", "JNJ", "XOM", "V", "AVGO");
    }

    private static async Task SaveAsync(AlphaVantageClient client, params string[] symbols)
    {
        FileSystemCache cache = new FileSystemCache("../../../../tables");

        foreach (string symbol in symbols)
        {
            await Console.Out.WriteAsync($"GET [ {symbol,-6}]");

            if (cache.Contains(symbol))
            {
                await Console.Out.WriteLineAsync("             DONE");

                continue;
            }

            IReadOnlyList<AlphaVantageAdjustedTimeSeriesResponse> responses;

            try
            {
                responses = await client.GetTimeSeriesAsync(AlphaVantageAdjustedTimeSeries.Monthly, new AlphaVantageTimeSeriesRequest()
                {
                    Symbol = symbol,
                    OutputSize = AlphaVantageOutputSize.Compact
                });
            }
            catch
            {
                await Console.Out.WriteLineAsync("             JUMP");

                continue;
            }

            using (FileSystemCacheContext context = cache.Create(symbol))
            {
                foreach (AlphaVantageAdjustedTimeSeriesResponse response in responses)
                {
                    context[response.Timestamp] = response.AdjustedClose;
                }
            }

            for (int i = 0; i < 13; i++)
            {
                await Console.Out.WriteAsync(".");
                await Task.Delay(1000);
            }

            await Console.Out.WriteLineAsync("DONE");
        }
    }
}
