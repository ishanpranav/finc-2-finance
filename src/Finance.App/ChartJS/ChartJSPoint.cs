// ChartJSPoint.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Finance.App.ChartJS;

internal readonly struct ChartJSPoint
{
    public ChartJSPoint(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }
}
