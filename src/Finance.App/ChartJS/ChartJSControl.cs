// ChartJSControl.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Web.WebView2.Core;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finance.App.ChartJS;

internal sealed partial class ChartJSControl : UserControl
{
    public ChartJSControl()
    {
        InitializeComponent();
    }

    public async Task NavigateAsync(string path, object hostObject)
    {
        const string objectName = "page";

        try
        {
            await _webView.EnsureCoreWebView2Async();

            _webView.CoreWebView2.RemoveHostObjectFromScript(objectName);
        }
        catch (COMException) { }

        _webView.CoreWebView2.SetVirtualHostNameToFolderMapping("liber.example", Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!, CoreWebView2HostResourceAccessKind.DenyCors);
        _webView.CoreWebView2.AddHostObjectToScript(objectName, hostObject);
        _webView.CoreWebView2.Navigate(Path.GetFullPath(path));
    }

    public void Reload()
    {
        _webView.Reload();
    }
}
