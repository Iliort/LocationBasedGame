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
    [Activity(Label = "@string/CreateTeam", MainLauncher = false, Icon = "@drawable/icon")]
    public class CreateTeamActivity : AppCompatActivity
    {
        private EditText nameText, pwdText;
        private Button submitButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CreateTeamView);
            
            var toolbar = FindViewById<Toolbar>(Resource.Id.CreateTeamViewToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            nameText = FindViewById<EditText>(Resource.Id.CreateTeamName);
            pwdText = FindViewById<EditText>(Resource.Id.CreateTeamPwd);
            submitButton = FindViewById<Button>(Resource.Id.CreateTeamViewButton);

            nameText.TextChanged += NameText_TextChanged;
            pwdText.TextChanged += PwdText_TextChanged;
            submitButton.Click += SubmitButton_Click;

            submitButton.Enabled = false;
            submitButton.Alpha = 0.5f;
        }

        private void SubmitButton_Click(object sender, System.EventArgs e)
        {
            string response =Tools.GetWebResponse(
                    $"TeamInfoHandler.ashx?Type=Add&Name={nameText.Text}&PWD={pwdText.Text}");
            if (response == "NotUniqueName")
                Toast.MakeText(this, Resource.String.MakeUpUniqueName, ToastLength.Long).Show();
            else
            {
                Tools.SetTeam(nameText.Text, pwdText.Text);
                StartActivity(typeof(ProfileViewActivity));
                Finish();
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