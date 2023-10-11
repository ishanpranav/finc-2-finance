// TimeSeriesDataProvider.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Finance.App.ChartJS;
using System.Collections.Generic;
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
        List<string> labels = new List<string>(_table.Rows);
        ChartJSChartDataset<double>[] datasets = new ChartJSChartDataset<double>[_table.Securities];
        ChartJSChartData<double> result = new ChartJSChartData<double>(datasets);

        for (int row = 0; row < _table.Rows; row++)
        {
            labels.Add(_table.Timestamps[row].ToShortDateString());
        }

        for (int column = 0; column < _table.Securities; column++)
        {
            double[] data = new double[_table.Rows];

            for (int row = 0; row < _table.Rows; row++)
            {
                data[row] = _locator(_table, row, column);
            }

            datasets[column] = new ChartJSChartDataset<double>(data)
            {
                Label = _table.Symbols[column] ?? string.Empty,
                BorderWidth = 1
            };
        }

        return result;
    }
}
