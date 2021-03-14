using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace TheLocalNZ.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            // Version check
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request notification permissions from the user
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {
                    // Handle approval
                    CreateNotification();
                });
            }

            return base.FinishedLaunching(app, options);
        }

        const int NOTIFICATION_DELAY = 172800; //2 days

        void CreateNotification()
        {
            //remove previous notification
            var requests = new string[] { "reminder" };
            UNUserNotificationCenter.Current.RemovePendingNotificationRequests(requests);

            // Rebuild notification
            var content = new UNMutableNotificationContent();
            content.Title = "Browse Local!";
            content.Subtitle = "See what NZ's local businesses have in store.";
            content.Badge = 1;

            // New trigger time
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(NOTIFICATION_DELAY, false);

            // ID of Notification to be updated
            var requestID = "reminder";
            var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

            // Add to system to modify existing Notification
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
            {
                if (err != null)
                {
                    System.Console.WriteLine("Notification creation error: " + err);
                    // Do something with error...
                }
            });
        }
    }
}
