// PortfolioSetDataProvider.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Finance.App.ChartJS;
using System;
using System.Runtime.InteropServices;

namespace Finance.App.DataProviders;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class PortfolioSetDataProvider : DataProvider
{
    private readonly PortfolioSet _set;

    public PortfolioSetDataProvider(SecurityTable table, string title)
    {
        _set = PortfolioSet.Generate(table, Random.Shared, 25000, 0.0178);
        Title = title;
    }

    public override string Title { get; }

    protected override object GetChartData()
    {
        ChartJSPoint[] data = new ChartJSPoint[_set.Portfolios];

        for (int portfolio = 0; portfolio < data.Length; portfolio++)
        {
            data[portfolio] = new ChartJSPoint(_set.GetStandardDeviation(portfolio), _set.GetMean(portfolio));
        }

        return data;
    }
}
