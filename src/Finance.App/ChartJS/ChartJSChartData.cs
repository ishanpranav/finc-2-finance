// ChartJSChartData.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Finance.App.ChartJS;

internal sealed class ChartJSChartData<T>
{
    public ChartJSChartData(IReadOnlyList<ChartJSChartDataset<T>> datasets)
    {
        Datasets = datasets;
    }

    public IList<string> Labels { get; } = new List<string>();
    public IReadOnlyList<ChartJSChartDataset<T>> Datasets { get; }
}
