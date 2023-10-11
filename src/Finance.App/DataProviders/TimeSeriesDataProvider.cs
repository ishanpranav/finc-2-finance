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
    private readonly Lookup _getPoint;

    public TimeSeriesDataProvider(SecurityTable table, string title, Lookup getPoint)
    {
        _table = table;
        Title = title;
        _getPoint = getPoint;
    }

    public override string Title { get; }

    protected override object GetChartData()
    {
        string[] labels = new string[_table.Rows];
        ChartJSChartDataset[] datasets = new ChartJSChartDataset[_table.Securities];

        for (int row = 0; row < _table.Rows; row++)
        {
            labels[row] = _table.Timestamps[row].ToShortDateString();
        }

        for (int column = 0; column < _table.Securities; column++)
        {
            double[] data = new double[_table.Rows];

            for (int row = 0; row < _table.Rows; row++)
            {
                data[row] = _getPoint(_table, row, column);
            }

            datasets[column] = new ChartJSChartDataset(data)
            {
                Label = _table.Symbols[column] ?? string.Empty,
                BorderWidth = 1
            };
        }

        return new ChartJSChartData(labels, datasets);
    }
}
