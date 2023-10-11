// ChartForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Finance.App.DataProviders;
using System;
using System.Windows.Forms;

namespace Finance.App;

internal sealed partial class ChartForm : Form
{
    private readonly string _path;
    private readonly DataProvider _dataProvider;

    private FormWindowState _previousWindowState;

    public ChartForm(string path, DataProvider dataProvider)
    {
        InitializeComponent();

        _path = path;
        _dataProvider = dataProvider;
        _previousWindowState = WindowState;
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        Text = _dataProvider.Title;

        await _chart.NavigateAsync(_path, _dataProvider);
    }

    private void OnResize(object sender, EventArgs e)
    {
        if (WindowState != _previousWindowState)
        {
            _chart.Reload();

            _previousWindowState = WindowState;
        }
    }

    private void OnResizeEnd(object sender, EventArgs e)
    {
        _chart.Reload();
    }
}
