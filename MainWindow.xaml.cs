using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;

namespace WPFWindowToastNotification;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainViewModel();

        DataContext = viewModel;
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        

    }

    protected override void OnClosing(CancelEventArgs e)
    {
        // Clean up lingering messages
        ToastNotificationManagerCompat.Uninstall();
        base.OnClosing(e);
    }
}