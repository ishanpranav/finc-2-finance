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

        await SaveAsync(client, "BK", "STT", "CL", "HIG", "BG", "ED", "KEY", "CFG", "MCK", "DE", "PG", "BRK.B", "SWK", "PNC", "CHD", "CME", "CMA", "PFE", "AXP", "GLW", "WFC", "TRV", "GIS", "MTB", "FITB", "IR", "UNP", "HBAN", "SHW", "MET", "CPB", "GS", "BF-B", "TT", "KMB", "TFC", "ZION", "PRU", "LLY", "MHK", "CVX", "LIN", "PFG", "BALL", "BWA", "CNP", "FMC", "KR", "PPG", "JCI", "AWK", "CMS", "EIX", "JNJ", "KO", "MRO", "ABT", "EQT", "MKC", "NTRS", "EMR", "HRL", "MRK", "AIZ", "GE", "AMP", "HSY", "WEC", "BDX", "SJM", "IP", "PEP", "EFX", "GD", "VFC", "GL", "WY", "ADM", "AEE", "TGT", "F", "PEG", "ROK", "DUK", "CHRW", "MMC", "PCAR", "PCG", "AEP", "ATO", "CBRE", "HON", "K", "OKE", "UPS", "ALLE", "GM", "WMB", "EVRG", "MCO", "VMC", "XEL", "ETN", "IBM", "WHR", "FCX", "ITW", "NI", "CLX", "ETR", "AOS", "BA", "LNT", "PH", "SPGI", "CE", "AIG", "CAG", "CMI", "HAL", "HES", "HLT", "EMN", "OXY", "PPL", "SNA", "NEM", "RTX", "DIS", "ECL", "HAS", "TXT", "WST", "CAT", "GPC", "SLB", "AJG", "GWW", "MAR", "ZBH", "CTAS", "DAL", "MAS", "AME", "TXN", "ALL", "BAX", "APH", "HSIC", "AAL", "ODFL", "IVZ", "MS", "NDSN", "TSN", "PGR", "RVTY", "TROW", "DRI", "TSCO", "BRO", "DG", "APD", "MCD", "NUE", "SYK", "TFX", "MTD", "SO", "STZ", "CF", "EL", "BEN", "CRL", "J", "RHI", "ROL", "ADP", "MDT", "CINF", "IRM", "BIO", "ZTS", "NXPI", "APA", "LEN", "AFL", "DOV", "FICO", "PHM", "ORLY", "COO", "EW", "KIM", "V", "WAT", "GNRC", "L", "PKG", "DPZ", "SEE", "TDY", "TER", "AVGO", "HUM", "JBHT", "VTRS", "BR", "FRT", "RJF", "WMT", "BBWI", "CMCSA", "REG", "NKE", "ADI", "BBY", "ES", "MA", "PNR", "TYL", "AMAT", "DGX", "FAST", "LUV", "RL", "UAL", "WRB", "FIS", "HCA", "INTC", "USB", "WM", "AMD", "ANSS", "DHR", "EQR", "LDOS", "MSCI", "O", "SYY", "ZBRA", "BXP", "WDC", "WELL", "CAH", "DVN", "ESS", "FDX", "NDAQ", "PAYX", "RF", "SBUX", "SCHW", "VRSK", "CCL", "PSA", "UDR", "CBOE", "CZR", "EG", "MSFT", "COST", "JKHY", "TECH", "AAPL", "EXR", "MAA", "ORCL", "UNH", "AVB", "BIIB", "DHI", "FDS", "HD", "LH", "MU", "TRMB", "AZO", "BSX", "DVA", "EXPD", "IT", "STX", "UHS", "AMGN", "CSX", "LRCX", "MOH", "NVR", "VLO", "AES", "CPT", "KDP", "ROP", "ADBE", "ADSK", "CI", "CPRT", "EA", "GEN", "IQV", "ROST", "D", "IDXX", "INTU", "PLD", "CDW", "CNC", "CSCO", "FI", "ALK", "BX", "CB", "COR", "DFS", "HOLX", "MO", "PEAK", "PNW", "PTC", "QCOM", "STE", "DLTR", "MGM", "MTCH", "OMC", "SNPS", "CSGP", "GILD", "TJX", "BLK", "CDNS", "IEX", "LVS", "REGN", "ACN", "GRMN", "MCHP", "RMD", "SBAC", "VRTX", "MMM", "AVY", "ULTA", "INCY", "CDAY", "NRG", "NTAP", "AXON", "CHTR", "CMG", "EPAM", "HST", "KMX", "MLM", "NVDA", "POOL", "STLD", "TDG", "TTWO", "ALB", "AMZN", "APTV", "ARE", "CCI", "COF", "CTSH", "GEHC", "TMUS", "ACGL", "AMT", "DTE", "EBAY", "ISRG", "LMT", "VRSN", "BKNG", "CVS", "EXPE", "FFIV", "JNPR", "ALGN", "FE", "KMI", "MPWR", "NFLX", "PWR", "PXD", "RCL", "URI", "YUM", "AKAM", "C", "EQIX", "GOOG", "GOOGL", "ILMN", "LKQ", "PAYC", "PYPL", "SRE", "VTR", "A", "CEG", "CRM", "DXCM", "EOG", "FSLR", "ON", "XOM", "EXC", "FLT", "FTNT", "GPN", "ICE", "MKTX", "PODD", "COP", "SWKS", "WYNN", "NOW", "SPG", "SYF", "TSLA", "ANET", "DLR", "META", "ETSY", "PANW", "TRGP", "ENPH", "SEDG", "CTLT", "FANG", "LYB", "TEL", "ABNB", "ATVI", "LYV", "MRNA", "HII", "XYL", "INVH", "MDLZ", "WBA", "HPE", "QRVO", "WRK", "FTV", "HWM", "WTW", "BKR", "DD", "TPR", "VICI", "CTVA", "DOW", "FOX", "FOXA", "OGN", "KVUE", "WBD", "VLTO", "NSC", "LOW", "MSI", "HPQ", "IFF", "IPG", "KLAC", "AON", "VZ", "T", "NEE", "BMY", "NOC", "BAC", "RSG", "WAB", "JPM", "MOS", "TAP", "TMO", "PM", "MPC", "NCLH", "PSX", "MNST", "ABBV", "NWS", "NWSA", "KEYS", "ELV", "KHC", "LW", "XRAY", "AMCR", "LHX", "PARA", "OTIS", "CARR", "CTRA");
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
                responses = await client.GetTimeSeriesAsync(AlphaVantageAdjustedTimeSeries.Weekly, new AlphaVantageTimeSeriesRequest()
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
