// SummaryForm.Designer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Finance.App
{
    partial class SummaryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            _dataGridView = new System.Windows.Forms.DataGridView();
            symbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            weightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            meanColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            standardDeviationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            sharpeRatioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            meanAnnualColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            standardDeviationAnnualColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // _dataGridView
            // 
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { symbolColumn, weightColumn, meanColumn, standardDeviationColumn, sharpeRatioColumn, meanAnnualColumn, standardDeviationAnnualColumn });
            _dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            _dataGridView.Location = new System.Drawing.Point(0, 0);
            _dataGridView.Name = "_dataGridView";
            _dataGridView.ReadOnly = true;
            _dataGridView.RowHeadersWidth = 51;
            _dataGridView.RowTemplate.Height = 29;
            _dataGridView.Size = new System.Drawing.Size(800, 450);
            _dataGridView.TabIndex = 0;
            // 
            // symbolColumn
            // 
            symbolColumn.HeaderText = "Symbol";
            symbolColumn.MinimumWidth = 6;
            symbolColumn.Name = "symbolColumn";
            symbolColumn.ReadOnly = true;
            symbolColumn.Width = 125;
            // 
            // weightColumn
            // 
            dataGridViewCellStyle1.Format = "p4";
            weightColumn.DefaultCellStyle = dataGridViewCellStyle1;
            weightColumn.HeaderText = "Weight";
            weightColumn.MinimumWidth = 6;
            weightColumn.Name = "weightColumn";
            weightColumn.ReadOnly = true;
            weightColumn.Width = 125;
            // 
            // meanColumn
            // 
            dataGridViewCellStyle2.Format = "p4";
            meanColumn.DefaultCellStyle = dataGridViewCellStyle2;
            meanColumn.HeaderText = "Mean";
            meanColumn.MinimumWidth = 6;
            meanColumn.Name = "meanColumn";
            meanColumn.ReadOnly = true;
            meanColumn.Width = 125;
            // 
            // standardDeviationColumn
            // 
            dataGridViewCellStyle3.Format = "p4";
            standardDeviationColumn.DefaultCellStyle = dataGridViewCellStyle3;
            standardDeviationColumn.HeaderText = "Standard Deviation";
            standardDeviationColumn.MinimumWidth = 6;
            standardDeviationColumn.Name = "standardDeviationColumn";
            standardDeviationColumn.ReadOnly = true;
            standardDeviationColumn.Width = 125;
            // 
            // sharpeRatioColumn
            // 
            dataGridViewCellStyle4.Format = "n2";
            sharpeRatioColumn.DefaultCellStyle = dataGridViewCellStyle4;
            sharpeRatioColumn.HeaderText = "Sharpe Ratio";
            sharpeRatioColumn.MinimumWidth = 6;
            sharpeRatioColumn.Name = "sharpeRatioColumn";
            sharpeRatioColumn.ReadOnly = true;
            sharpeRatioColumn.Width = 125;
            // 
            // meanAnnualColumn
            // 
            dataGridViewCellStyle5.Format = "p4";
            meanAnnualColumn.DefaultCellStyle = dataGridViewCellStyle5;
            meanAnnualColumn.HeaderText = "Mean (Annual)";
            meanAnnualColumn.MinimumWidth = 6;
            meanAnnualColumn.Name = "meanAnnualColumn";
            meanAnnualColumn.ReadOnly = true;
            meanAnnualColumn.Width = 125;
            // 
            // standardDeviationAnnualColumn
            // 
            dataGridViewCellStyle6.Format = "p4";
            standardDeviationAnnualColumn.DefaultCellStyle = dataGridViewCellStyle6;
            standardDeviationAnnualColumn.HeaderText = "Standard Deviation (Annual)";
            standardDeviationAnnualColumn.MinimumWidth = 6;
            standardDeviationAnnualColumn.Name = "standardDeviationAnnualColumn";
            standardDeviationAnnualColumn.ReadOnly = true;
            standardDeviationAnnualColumn.Width = 125;
            // 
            // SummaryForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(_dataGridView);
            Name = "SummaryForm";
            Text = "Summary";
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn meanColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn standardDeviationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sharpeRatioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn meanAnnualColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn standardDeviationAnnualColumn;
    }
}