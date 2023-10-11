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
    private readonly Portfolio _minimumVariancePortfolio;
    private readonly Portfolio _maximumSharpeRatioPortfolio;

    public PortfolioSetDataProvider(SecurityTable table, string title)
    {
        _set = PortfolioSet.Generate(table, Random.Shared, 20000, 0.0178);
        _minimumVariancePortfolio = Portfolio.CreateMinimumVariancePortfolio(_set.Table);
        _maximumSharpeRatioPortfolio = Portfolio.CreateMaximumSharpeRatioPortfolio(_set.Table);

        Title = title;
    }

    public override string Title { get; }

    protected override object GetChartData()
    {
        ChartJSPoint[] data = new ChartJSPoint[_set.Portfolios];
        ChartJSPoint[] minimumVariance = new ChartJSPoint[1];
        ChartJSPoint[] maximumSharpeRatio = new ChartJSPoint[1];

        for (int portfolio = 0; portfolio < data.Length; portfolio++)
        {
            double mean = _set.GetMean(portfolio);

            data[portfolio] = new ChartJSPoint(_set.GetStandardDeviation(portfolio), mean);
        }

        minimumVariance[0] = new ChartJSPoint(_minimumVariancePortfolio.StandardDeviation(_set.Table), _minimumVariancePortfolio.Mean(_set.Table));
        maximumSharpeRatio[0] = new ChartJSPoint(_maximumSharpeRatioPortfolio.StandardDeviation(_set.Table), _maximumSharpeRatioPortfolio.Mean(_set.Table));

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
            new ChartJSChartDataset<ChartJSPoint>(maximumSharpeRatio)
            {
                Label = "Minimum Variance Efficient (MVE) Portfolio",
                PointRadius = 10
            }
        });
    }
}
