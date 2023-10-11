// ChartJSChartDataset.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Finance.App.ChartJS;

internal sealed class ChartJSChartDataset<T>
{
    public ChartJSChartDataset(IReadOnlyList<T> data)
    {
        Data = data;
    }

    public string? Label { get; set; }
    public int BorderWidth { get; set; }
    public int PointRadius { get; set; } = 1;
    public IReadOnlyList<T> Data { get; }
}
