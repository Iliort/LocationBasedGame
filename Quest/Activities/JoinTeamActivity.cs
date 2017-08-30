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
    [Activity(Label = "@string/JoinTeam", MainLauncher = false, Icon = "@drawable/icon")]
    public class JoinTeamActivity : AppCompatActivity
    {
        private EditText nameText, pwdText;
        private Button submitButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.JoinTeamView);

            var toolbar = FindViewById<Toolbar>(Resource.Id.JoinTeamViewToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            nameText = FindViewById<EditText>(Resource.Id.JoinTeamName);
            pwdText = FindViewById<EditText>(Resource.Id.JoinTeamPwd);
            submitButton = FindViewById<Button>(Resource.Id.JoinTeamViewButton);

            nameText.TextChanged += NameText_TextChanged;
            pwdText.TextChanged += PwdText_TextChanged;
            submitButton.Click += SubmitButton_Click;

            submitButton.Enabled = false;
            submitButton.Alpha = 0.5f;
        }

        private void SubmitButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                Tools.SetTeam(nameText.Text, pwdText.Text);
                StartActivity(typeof (ProfileViewActivity));
                Finish();
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Неправильное название/пароль", ToastLength.Long).Show();
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