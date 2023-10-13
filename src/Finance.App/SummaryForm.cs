// SummaryForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;

namespace Finance.App;

internal sealed partial class SummaryForm : Form
{
    private static readonly double s_sqrt52 = Math.Sqrt(52);

    public SummaryForm(PortfolioSet set, int portfolio)
    {
        InitializeComponent();

        double mean;
        double annualMean;
        double standardDeviation;
        double annualStandardDeviation;
        double totalWeight = 0;

        for (int i = 0; i < set.Table.N; i++)
        {
            double weight = set.Weight(portfolio, i);

            mean = set.Table.Mean(i);
            annualMean = Math.Pow(1 + mean, 52) - 1;
            standardDeviation = Math.Sqrt(set.Table.Variance(i));
            annualStandardDeviation = standardDeviation * s_sqrt52;

            _dataGridView.Rows.Add(
                set.Table.Symbols[i],
                weight,
                mean,
                standardDeviation,
                annualMean / annualStandardDeviation,
                annualMean,
                annualStandardDeviation);

            totalWeight += weight;
        }

        mean = set.Mean(portfolio);
        annualMean = Math.Pow(1 + mean, 52) - 1;
        standardDeviation = Math.Sqrt(set.Variance(portfolio));
        annualStandardDeviation = standardDeviation * s_sqrt52;

        _dataGridView.Rows.Add("(Portfolio)",
            totalWeight,
            mean,
            standardDeviation,
            annualMean / annualStandardDeviation,
            annualMean,
            annualStandardDeviation);
    }
}
