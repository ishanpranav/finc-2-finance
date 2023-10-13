// MainForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Finance.App.DataProviders;
using System;
using System.Collections.Generic;
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
        FileSystemCache cache = new FileSystemCache("../../../../tables");
        SecurityTable table = await cache.ToTableAsync();
        PortfolioSet set = PortfolioSet.Generate(table, new Random(1202004), 32787, 0.001);

        new ChartForm("pages/time-series.html", new TimeSeriesDataProvider(table, "Weekly Adjusted Close", (table, k, i) => decimal.ToDouble(table.AdjustedClose(k, i))))
        {
            MdiParent = this
        }.Show();

        new ChartForm("pages/time-series.html", new TimeSeriesDataProvider(table, "Weekly Change", (table, k, security) => table.Change(k, security)))
        {
            MdiParent = this
        }.Show();

        new ChartForm("pages/portfolio-set.html", new PortfolioSetDataProvider(set, "Efficient Frontier"))
        {
            MdiParent = this
        }.Show();

        new SummaryForm(set, set.MinimumVarianceEfficientPortfolio)
        {
            MdiParent = this,
            Text = "Minimum Variance Efficient Portfolio"
        }.Show();
    }
}
