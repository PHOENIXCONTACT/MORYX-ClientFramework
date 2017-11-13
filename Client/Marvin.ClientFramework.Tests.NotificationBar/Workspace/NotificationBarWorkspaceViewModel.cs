using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using C4I;
using Marvin.ClientFramework.Base;
using Marvin.Container;

namespace Marvin.ClientFramework.Tests.NotificationBar
{
    [Plugin(LifeCycle.Singleton, typeof(IModuleWorkspace), Name = ScreenName)]
    public class NotificationBarWorkspaceViewModel : ModuleWorkspace
    {
        internal const string ScreenName = "NotificationBarTestScreen";

        public NotificationBarWorkspaceViewModel()
        {
            LoadTestDataEntries();
            RequestToViewAction = DoRequestToViewAction;
        }
        
        #region ViewProperties
        private readonly ObservableCollection<NotificationEntry> _notifications = new ObservableCollection<NotificationEntry>();
        public ObservableCollection<NotificationEntry> Notifications { get { return _notifications; } }
        #endregion

        #region TestData

        private void LoadTestDataEntries()
        {
            int i = 0;
            Notifications.Add(new NotificationEntry { Title = "Error " + i++, BackgroundColor = Colors.OrangeRed, ForegroundColor = Colors.White, Content = CreateBorder( new SolidColorBrush(Colors.DarkRed) ) });
            Notifications.Add(new NotificationEntry { Title = "Fatal " + i++, BackgroundColor = Colors.BlueViolet, ForegroundColor = Colors.White, Content = CreateBorder(new SolidColorBrush(Colors.DarkTurquoise)) });
            Notifications.Add(new NotificationEntry { Title = "Info " + i++, BackgroundColor = Colors.GreenYellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.DarkGreen)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Error " + i++, BackgroundColor = Colors.OrangeRed, ForegroundColor = Colors.White, Content = CreateBorder(new SolidColorBrush(Colors.DarkRed)) });
            Notifications.Add(new NotificationEntry { Title = "Fatal " + i++, BackgroundColor = Colors.BlueViolet, ForegroundColor = Colors.White, Content = CreateBorder(new SolidColorBrush(Colors.DarkTurquoise)) });
            Notifications.Add(new NotificationEntry { Title = "Info " + i++, BackgroundColor = Colors.GreenYellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.DarkGreen)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Incredibly long Text going nowhere: Some moreText over here... " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
            Notifications.Add(new NotificationEntry { Title = "Warning " + i++, BackgroundColor = Colors.Yellow, ForegroundColor = Colors.Black, Content = CreateBorder(new SolidColorBrush(Colors.LightGoldenrodYellow)) });
        }

        private Border CreateBorder(Brush background)
        {
            return new Border { Background = background, Width = 250, Height = 200 };
        }

        public Action RequestToViewAction { get; set; } 
        public void DoRequestToViewAction()
        {
            MessageBox.Show("It's so nice, request full screen view of it.");
        }
        #endregion

    }
}