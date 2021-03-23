using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading;
using Xamarin.Forms;
using Android.Content;
using Android.Support.V4.App;
using Plugin.LocalNotifications;

namespace TheLocalNZ.Droid
{
    [Activity(Label = "TheLocalNZ", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        long NOTIFICATION_DELAY = 10;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            LoadApplication(new App());

            //CreateNotificationChannel();
            //GenerateNotification();

            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.SmallIcon;
            CrossLocalNotifications.Current.Cancel(0);
            CrossLocalNotifications.Current.Show("Browse Local!", "See what NZ's local businesses have in store.", 0, DateTime.Now.AddSeconds(NOTIFICATION_DELAY));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /*void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = "Reminder";
            var channelDescription = "Reminds user of app every so often";
            var channel = new NotificationChannel("reminder", channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        void GenerateNotification()
        {
            var alarmIntent = new Intent(this, typeof(NotificationPublisher));

            var pending = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.OneShot);
            var alarmManager = GetSystemService(AlarmService).JavaCast<AlarmManager>();
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + NOTIFICATION_DELAY, pending);
        }*/
    }
}