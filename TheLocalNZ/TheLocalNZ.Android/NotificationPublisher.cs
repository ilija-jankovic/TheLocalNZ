using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheLocalNZ.Droid
{
    [BroadcastReceiver]
    public class NotificationPublisher : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var resultIntent = new Intent(context, typeof(MainActivity));
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            var pending = PendingIntent.GetActivity(context, 0,
                resultIntent,
                PendingIntentFlags.CancelCurrent);

            // Instantiate the builder and set notification elements, including pending intent:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, "reminder")
                .SetContentIntent(pending)
                .SetContentTitle("Browse Local!")
                .SetContentText("See what NZ's local businesses have in store.")
                .SetSmallIcon(Resource.Drawable.SmallIcon)
                .SetLargeIcon(BitmapFactory.DecodeResource(context.Resources, Resource.Mipmap.icon));

            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            // Build the notification:
            Notification notification = builder.Build();

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}