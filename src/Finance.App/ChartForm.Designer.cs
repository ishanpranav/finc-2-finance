using Finance.App.ChartJS;
using System.Drawing;
using System.Windows.Forms;

namespace Finance.App
{
    partial class ChartForm
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
            _chart = new ChartJSControl();
            SuspendLayout();
            // 
            // _chart
            // 
            _chart.Dock = DockStyle.Fill;
            _chart.Location = new Point(0, 0);
            _chart.Name = "_chart";
            _chart.Size = new Size(685, 403);
            _chart.TabIndex = 0;
            // 
            // ChartForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(685, 403);
            Controls.Add(_chart);
            Name = "ChartForm";
            Load += OnLoad;
            ResizeEnd += OnResizeEnd;
            Resize += OnResize;
            ResumeLayout(false);
        }

        #endregion

        private ChartJSControl _chart;
    }
}
