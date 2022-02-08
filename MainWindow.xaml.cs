using System;
using System.IO;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;

namespace WPFWindowToastNotification
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
                
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                var args = ToastArguments.Parse(toastArgs.Argument);
                Application.Current.Dispatcher.Invoke(() =>
                    MessageBox.Show("Toast activated. Args: " + args)); 
            };
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var fromPersonImage = Path.GetFullPath(@"images\morten_brudvik.jpg");
            var heroImage = Path.GetFullPath(@"images\robot.jpg");
            var toastBuilder = new ToastContentBuilder()
                .AddArgument("action", "")
                .AddArgument("correlationId", 9813)
                .AddText("Morten sent you a picture")
                .AddText("Awesome robot sculpture!")
                .AddAppLogoOverride(new Uri(fromPersonImage), ToastGenericAppLogoCrop.Circle)
                .AddHeroImage(new Uri(heroImage))
                .AddButton(new ToastButton()
                    .SetContent("Show")
                    .AddArgument("action", "showImage")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("Dismiss")
                    .AddArgument("action", "dismiss")
                    .SetBackgroundActivation());
                
            toastBuilder.Show();
        }
    }
}
