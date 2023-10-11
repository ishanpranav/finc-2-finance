// MainForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using AlphaVantageCore;
using Finance.App.DataProviders;
using System;
using System.IO;
using System.Windows.Forms;

namespace Finance.App;

internal sealed partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        SecurityTable? table = null;

        const string tablePath = "table.csv";

        if (File.Exists(tablePath))
        {
            table = await SecurityTable.LoadAsync(tablePath);
        }

        if (table == null)
        {
            string? apiKey = Environment.GetEnvironmentVariable("API_KEY");

            if (apiKey == null)
            {
                throw new Exception();
            }

            AlphaVantageClient client = new AlphaVantageClient(apiKey);

            table = await SecurityTable.CreateAsync(tablePath, client,
                "AAPL",
                "AMZN",
                "META",
                "GOOGL");
        }

        if (table == null)
        {
            return;
        }

        new ChartForm("pages/time-series.html", new TimeSeriesDataProvider(table, "Weekly Adjusted Close", (table, row, security) => decimal.ToDouble(table.GetAdjustedClose(row, security))))
        {
            MdiParent = this
        }.Show();

        new ChartForm("pages/time-series.html", new TimeSeriesDataProvider(table, "Weekly Change", (table, row, security) => table[row, security]))
        {
            MdiParent = this
        }.Show();

        new ChartForm("pages/portfolio-set.html", new PortfolioSetDataProvider(table, "Efficient Frontier"))
        {
            MdiParent = this
        }.Show();

        PortfolioSet.Generate(table, Random.Shared, 100, 0.0);
    }
}
