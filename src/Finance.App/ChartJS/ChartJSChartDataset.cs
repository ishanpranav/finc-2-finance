// ChartJSChartDataset.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Finance.App.ChartJS;

internal sealed class ChartJSChartDataset
{
    public ChartJSChartDataset(IReadOnlyList<double> data)
    {
        Data = data;
    }

    public string? Label { get; set; }
    public int BorderWidth { get; set; }
    public IReadOnlyList<double> Data { get; }
}
