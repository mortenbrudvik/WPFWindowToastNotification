using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using Prism.Commands;
using WPFWindowToastNotification.Annotations;

namespace WPFWindowToastNotification;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private string _title;
    private string _message;
    private string _heroImagePath;
    private string _inlineImagePath;
    private string _logoImagePath;

    public MainViewModel()
    {
        _title = "Morten sent you a picture";
        _message = "Awesome robot sculpture!";
        
        _logoImagePath = Path.GetFullPath(@"images\morten_brudvik.jpg");
        _heroImagePath = Path.GetFullPath(@"images\robot.jpg");
        _inlineImagePath = "";
        
        ToastNotificationManagerCompat.OnActivated += toastArgs =>
        {
            var args = ToastArguments.Parse(toastArgs.Argument);
            Application.Current.Dispatcher.Invoke(() =>
                MessageBox.Show("Toast activated. Args: " + args)); 
        };
    }
    
    public DelegateCommand CreateNotificationCommand => new(CreateNotification, () => true);

    public string Title
    {
        get => _title;
        set
        {
            if (value == _title) return;
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => _message;
        set
        {
            if (value == _message) return;
            _message = value;
            OnPropertyChanged();
        }
    }

    public string LogoImagePath
    {
        get => _logoImagePath;
        set
        {
            if (value == _logoImagePath) return;
            _logoImagePath = value;
            OnPropertyChanged();
        }
    }

    public string HeroImagePath
    {
        get => _heroImagePath;
        set
        {
            if (value == _heroImagePath) return;
            _heroImagePath = value;
            OnPropertyChanged();
        }
    }

    public string InlineImagePath
    {
        get => _inlineImagePath;
        set
        {
            if (value == _inlineImagePath) return;
            _inlineImagePath = value;
            OnPropertyChanged();
        }
    }

    private void CreateNotification()
    {

        var toastBuilder = new ToastContentBuilder()
            .AddArgument("action", "")
            .AddArgument("correlationId", 9813)
            .AddText(Title)
            .AddText(Message)
            .AddButton(new ToastButton()
                .SetContent("Show")
                .AddArgument("action", "showImage")
                .SetBackgroundActivation())
            .AddButton(new ToastButton()
                .SetContent("Dismiss")
                .AddArgument("action", "dismiss")
                .SetBackgroundActivation());
        
        
        if(!string.IsNullOrWhiteSpace(LogoImagePath) && File.Exists(LogoImagePath))
            toastBuilder.AddAppLogoOverride(new Uri(LogoImagePath), ToastGenericAppLogoCrop.Circle);
        if(!string.IsNullOrWhiteSpace(HeroImagePath) && File.Exists(HeroImagePath))
            toastBuilder.AddHeroImage(new Uri(HeroImagePath));
        if(!string.IsNullOrWhiteSpace(InlineImagePath) && File.Exists(InlineImagePath))
            toastBuilder.AddInlineImage(new Uri(InlineImagePath));

        toastBuilder.Show();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}