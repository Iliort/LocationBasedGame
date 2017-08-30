using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Net;
using Android.Support.V7.App;
using Android.Widget;
using Clans.Fab;
using ActionBar = Android.Support.V7.App.ActionBar;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Quest.Activities
{
    [Activity(Label = "@string/MyTeam", MainLauncher = false, Icon = "@drawable/icon")]
    public class MyTeamActivity : AppCompatActivity
    {
        private EditText nameText, pwdText;
        private Button submitButton;
        private ListView membersListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.MyTeamView);

            var toolbar = FindViewById<Toolbar>(Resource.Id.MyTeamViewToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            nameText = FindViewById<EditText>(Resource.Id.MyTeamName);
            pwdText = FindViewById<EditText>(Resource.Id.MyTeamPwd);
            submitButton = FindViewById<Button>(Resource.Id.MyTeamViewChangeButton);
            membersListView = FindViewById<ListView>(Resource.Id.MyTeamMembers);

            nameText.Text = Globals.MyProfile.Team.Name;
            pwdText.Text = Globals.MyProfile.Team.Password;

            string[] members = Tools.GetMembers(Globals.MyProfile.Team.Id);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, members);
            membersListView.Adapter = adapter;
            membersListView.SetScrollContainer(false);

            submitButton.Enabled = false;
            submitButton.Alpha = .5f;
            submitButton.Click += SubmitButton_Click;
            pwdText.TextChanged += PwdText_TextChanged;
            nameText.TextChanged += NameText_TextChanged;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string response =
                Tools.GetWebResponse(
                    $"TeamInfoHandler.ashx?Type=Change&ID={Globals.MyProfile.Team.Id}&Name={nameText.Text}&PWD={pwdText.Text}");
            if (response == "Successful")
            {
                Tools.SignIn(Globals.MyProfile.Email, Globals.MyProfile.HashedPassword);
                StartActivity(typeof(ProfileViewActivity));
                Finish();
            }
            else if (response == "NotUniqueName")
            {
                Toast.MakeText(this, Resource.String.MakeUpUniqueName, ToastLength.Long).Show();
            }
        }

        private void PwdText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (nameText.Text.Length >= 1 && pwdText.Text.Length >= 1)
            {
                submitButton.Enabled = true;
                submitButton.Alpha = 1;
            }
            else
            {
                submitButton.Enabled = false;
                submitButton.Alpha = 0.5f;
            }
        }

        private void NameText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (nameText.Text.Length >= 1 && pwdText.Text.Length >= 1)
            {
                submitButton.Enabled = true;
                submitButton.Alpha = 1;
            }
            else
            {
                submitButton.Enabled = false;
                submitButton.Alpha = 0.5f;
            }
        }
    }
}