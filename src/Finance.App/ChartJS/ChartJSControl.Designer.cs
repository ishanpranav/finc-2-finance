using System.Drawing;
using System.Windows.Forms;

namespace Finance.App.ChartJS;

partial class ChartJSControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _webView = new Microsoft.Web.WebView2.WinForms.WebView2();
        ((System.ComponentModel.ISupportInitialize)_webView).BeginInit();
        SuspendLayout();
        // 
        // _webView
        // 
        _webView.AllowExternalDrop = true;
        _webView.CreationProperties = null;
        _webView.DefaultBackgroundColor = Color.White;
        _webView.Dock = DockStyle.Fill;
        _webView.Location = new Point(0, 0);
        _webView.Name = "_webView";
        _webView.Size = new Size(150, 150);
        _webView.TabIndex = 0;
        _webView.ZoomFactor = 1D;
        // 
        // ChartJSControl
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_webView);
        Name = "ChartJSControl";
        ((System.ComponentModel.ISupportInitialize)_webView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Microsoft.Web.WebView2.WinForms.WebView2 _webView;
}
