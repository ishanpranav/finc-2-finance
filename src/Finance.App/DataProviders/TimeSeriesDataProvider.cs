// TimeSeriesDataProvider.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Finance.App.ChartJS;
using System.Runtime.InteropServices;

namespace Finance.App.DataProviders;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class TimeSeriesDataProvider : DataProvider
{
    private readonly SecurityTable _table;
    private readonly Locator _locator;

    public TimeSeriesDataProvider(SecurityTable table, string title, Locator locator)
    {
        _table = table;
        Title = title;
        _locator = locator;
    }

    public override string Title { get; }

    protected override object GetChartData()
    {
        ChartJSChartDataset<double>[] datasets = new ChartJSChartDataset<double>[_table.N];
        ChartJSChartData<double> result = new ChartJSChartData<double>(datasets);

        for (int k = 0; k < _table.Count; k++)
        {
            result.Labels.Add(_table.Timestamps[k].ToShortDateString());
        }

        for (int i = 0; i < _table.N; i++)
        {
            double[] data = new double[_table.Count];

            for (int k = 0; k < _table.Count; k++)
            {
                data[k] = _locator(_table, k, i);
            }

            datasets[i] = new ChartJSChartDataset<double>(data)
            {
                Label = _table.Symbols[i] ?? string.Empty,
                BorderWidth = 1,
            };
        }

        return result;
    }
}
