// SummaryForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Windows.Forms;

namespace Finance.App;

internal sealed partial class SummaryForm : Form
{
    public SummaryForm(PortfolioSet set, int portfolio)
    {
        InitializeComponent();

        double totalWeight = 0;

        for (int i = 0; i < set.Table.N; i++)
        {
            double weight = set.GetWeight(portfolio, i);

            _dataGridView.Rows.Add(set.Table.Symbols[i], weight);

            totalWeight += weight;
        }

        _dataGridView.Rows.Add("(Total)", totalWeight);
    }
}
