using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
    [Activity(Label = "Hub", MainLauncher = false, Icon = "@drawable/icon")]
    public class SignUpActivity : AppCompatActivity
    {
        private EditText nickText, emailText, pwdText;
        private Button signUpButton, toSingInButton;
        private string  hashedPwd;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SignUp);
            signUpButton = FindViewById<Button>(Resource.Id.btnRegister);
            nickText = FindViewById<EditText>(Resource.Id.SignUpNickname);
            emailText = FindViewById<EditText>(Resource.Id.SignUpEmail);
            pwdText = FindViewById<EditText>(Resource.Id.SignUpPassword);
            toSingInButton = FindViewById<Button>(Resource.Id.btnLinkToLoginScreen);

            signUpButton.Enabled = false;
            signUpButton.Alpha = .5f;

            signUpButton.Click += SignUpButton_Click;
            nickText.TextChanged += NickText_TextChanged;
            emailText.TextChanged += EmailText_TextChanged;
            pwdText.TextChanged += PwdText_TextChanged;
            toSingInButton.Click += delegate { StartActivity(typeof(SignInActivity));};
            
        }

        private void PwdText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            signUpButton.Enabled = false;
            signUpButton.Alpha = 0.5f;
            if (nickText.Text != "" && pwdText.Text.Length >= 8)
            {
                try
                {
                    new MailAddress(emailText.Text);
                    signUpButton.Enabled = true;
                    signUpButton.Alpha = 1;
                }
                catch
                {
                    
                }
            }
        }

        private void EmailText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            signUpButton.Enabled = false;
            signUpButton.Alpha = 0.5f;
            if (nickText.Text != "" && pwdText.Text.Length >= 8)
            {
                try
                {
                    new MailAddress(emailText.Text);
                    signUpButton.Enabled = true;
                    signUpButton.Alpha = 1;
                }
                catch
                {

                }
            }
        }

        private void NickText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            signUpButton.Enabled = false;
            signUpButton.Alpha = 0.5f;
            if (nickText.Text != "" && pwdText.Text.Length >= 8)
            {
                try
                {
                    new MailAddress(emailText.Text);
                    signUpButton.Enabled = true;
                    signUpButton.Alpha = 1;
                }
                catch
                {

                }
            }
        }

        private void SignUpButton_Click(object sender, System.EventArgs e)
        {
            hashedPwd = Tools.HashString(pwdText.Text);
            HttpWebRequest req = 
                (HttpWebRequest)WebRequest.Create("LoginHandler.ashx?Type=SignUp&Nick="
                + nickText.Text + "&Email=" + emailText.Text + "&PWDHashed=" + hashedPwd);
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string respstr = reader.ReadToEnd();

            if (respstr == "Successful")
            {
                Tools.SignIn(emailText.Text, hashedPwd);
                StartActivity(typeof(ProfileViewActivity));
            }
            else if (respstr == "NotUniqueEmail")
            {
                Toast.MakeText(this, Resource.String.AlreadyExists, ToastLength.Long).Show();
            }
        }
    }
}