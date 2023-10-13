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
            _dataGridView = new System.Windows.Forms.DataGridView();
            symbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            weightColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // _dataGridView
            // 
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { symbolColumn, weightColumn });
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
            weightColumn.HeaderText = "Weight";
            weightColumn.MinimumWidth = 6;
            weightColumn.Name = "weightColumn";
            weightColumn.ReadOnly = true;
            weightColumn.Width = 125;
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
    }
}