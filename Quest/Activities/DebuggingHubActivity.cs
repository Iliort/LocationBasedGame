using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using System.IO;
using System.Net;
using Android.Test.Suitebuilder;

namespace Quest.Activities
{
    [Activity(Label = "Hub", MainLauncher = true, Icon = "@drawable/icon")]
    public class DebuggingHubActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            Tools.Activity = this;

            Message msg = new Message("Test", "igolod", MessageStatus.NotSeen, DateTime.Now, new List<Data>());
            Globals.Messages.Add(msg);

            SetContentView(Resource.Layout.DebuggingHub);
            Button taskActivityButton = FindViewById<Button>(Resource.Id.hub_button_TaskActivity);
            Button taskListActivityButton = FindViewById<Button>(Resource.Id.hub_button_TaskListActivity);
            Button sendAnsActivityButton = FindViewById<Button>(Resource.Id.hub_button_SendAnsActivity);
            Button messagesListActivityButton = FindViewById<Button>(Resource.Id.hub_button_MessagesListActivity);
            Button tabbedTaskActivityButton = FindViewById<Button>(Resource.Id.hub_button_TabbedTaskActivity);
            Button signInActivityButton = FindViewById<Button>(Resource.Id.hub_button_SignInActivity);
            Button profileViewActivityButton = FindViewById<Button>(Resource.Id.hub_button_ProfileViewActivity);
            Button httpTestActivityButton = FindViewById<Button>(Resource.Id.hub_button_HttpTest);


            taskActivityButton.Click += delegate { StartActivity(typeof(TaskActivity)); };
            taskListActivityButton.Click += delegate { StartActivity(typeof(TaskListActivity)); };
            sendAnsActivityButton.Click += delegate { StartActivity(typeof(SendAnsActivity)); };
            messagesListActivityButton.Click += delegate { StartActivity(typeof(MessagesListActivity)); };
            tabbedTaskActivityButton.Click += delegate { StartActivity(typeof(TabbedTaskActivity)); };
            signInActivityButton.Click += delegate { StartActivity(typeof(SignInActivity)); };
            profileViewActivityButton.Click += delegate { StartActivity(typeof(ProfileViewActivity));};

            httpTestActivityButton.Click += delegate
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://192.168.1.39/Quest/LoginHandler.ashx");
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    httpTestActivityButton.Text = reader.ReadToEnd();
                }
                else httpTestActivityButton.Text = "ERROR";
            };
        }

    }
}