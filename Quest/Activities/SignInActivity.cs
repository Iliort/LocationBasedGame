using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Net;
using Android.Support.V7.App;
using Android.Widget;
using Clans.Fab;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ActionBar = Android.Support.V7.App.ActionBar;
using Environment = System.Environment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Quest.Activities
{
    [Activity(Label = "Hub", MainLauncher = false, Icon = "@drawable/icon")]
    public class SignInActivity : AppCompatActivity
    {
        private EditText emailText, pwdText;
        private string hashedPwd;
        private Button submitButton, toSignUpButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (Tools.FileExists("Credentials.json"))
            {
                try
                {
                    string json = Tools.ReadFromFile("Credentials.json");
                    Profile profile = JsonConvert.DeserializeObject<Profile>(json);
                    Tools.SignIn(profile.Email, profile.HashedPassword);
                    StartActivity(typeof(ProfileViewActivity));
                    Finish();
                }
                catch (Exception)
                {
                   
                }
            }

            SetContentView(Resource.Layout.SignIn);

            toSignUpButton = FindViewById<Button>(Resource.Id.btnLinkToRegisterScreen);
            emailText = FindViewById<EditText>(Resource.Id.SignInEmail);
            pwdText = FindViewById<EditText>(Resource.Id.SignInPassword);
            submitButton = FindViewById<Button>(Resource.Id.btnLogin);
            
            toSignUpButton.Click += delegate { StartActivity(typeof(SignUpActivity));};
            submitButton.Click += SubmitButton_Click;
            
        }

        private void SubmitButton_Click(object sender, System.EventArgs e)
        {
            hashedPwd = Tools.HashString(pwdText.Text);
            try
            {
                Tools.SignIn(emailText.Text, hashedPwd);
                StartActivity(typeof(ProfileViewActivity));
            }
            catch (Exception)
            {
                Toast.MakeText(this, Resource.String.WrongCredentials, ToastLength.Long).Show();
            }
        }
    }
}