// DataProvider.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Finance.App.DataProviders;

public abstract class DataProvider
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new JsonSerializerOptions()
    {
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    protected DataProvider() { }

    public abstract string Title { get; }

    protected abstract object GetChartData();

    public string GetChartDataJson()
    {
        return JsonSerializer.Serialize(GetChartData(), s_jsonSerializerOptions);
    }
}
