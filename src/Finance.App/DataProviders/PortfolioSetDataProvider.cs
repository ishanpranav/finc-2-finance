// PortfolioSetDataProvider.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Finance.App.ChartJS;
using System.Runtime.InteropServices;

namespace Finance.App.DataProviders;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class PortfolioSetDataProvider : DataProvider
{
    private readonly PortfolioSet _set;

    public PortfolioSetDataProvider(PortfolioSet set, string title)
    {
        Title = title;
        _set = set;
    }

    public override string Title { get; }

    private ChartJSPoint CreatePoint(int portfolio)
    {
        return new ChartJSPoint(_set.GetStandardDeviation(portfolio), _set.GetMean(portfolio));
    }

    protected override object GetChartData()
    {
        ChartJSPoint[] data = new ChartJSPoint[_set.Portfolios];
        ChartJSPoint[] minimumVariance = new ChartJSPoint[1];
        ChartJSPoint[] minimumVarianceEfficient = new ChartJSPoint[1];

        for (int portfolio = 0; portfolio < data.Length; portfolio++)
        {
            data[portfolio] = CreatePoint(portfolio);
        }

        minimumVariance[0] = CreatePoint(_set.MinimumVariancePortfolio);
        minimumVarianceEfficient[0] = CreatePoint(_set.MinimumVarianceEfficientPortfolio);

        return new ChartJSChartData<ChartJSPoint>(new ChartJSChartDataset<ChartJSPoint>[]
        {
            new ChartJSChartDataset<ChartJSPoint>(data)
            {
                Label = "Investment Opportunity Set"
            },
            new ChartJSChartDataset<ChartJSPoint>(minimumVariance)
            {
                Label = "Minimum Variance Portfolio (MVP)",
                PointRadius = 10
            },
            new ChartJSChartDataset<ChartJSPoint>(minimumVarianceEfficient)
            {
                Label = "Minimum Variance Efficient (MVE) Portfolio",
                PointRadius = 10
            }
        });
    }
}
