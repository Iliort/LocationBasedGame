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
    [Activity(Label = "@string/Profile", MainLauncher = false, Icon = "@drawable/icon")]
    public class ProfileViewActivity : AppCompatActivity
    {
        private TextView username, teamName;
        private Button createTeamButton, joinTeamButton, myTeamButton, exitTeamButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.ProfileView);
            var toolbar = FindViewById<Toolbar>(Resource.Id.ProfileViewToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            Tools.SetDrawer(this, toolbar);

            username = FindViewById<TextView>(Resource.Id.ProfileViewUserName);
            teamName = FindViewById<TextView>(Resource.Id.ProfileViewTeamName);
            createTeamButton = FindViewById<Button>(Resource.Id.ProfileViewCreateButton);
            joinTeamButton = FindViewById<Button>(Resource.Id.ProfileViewJoinButton);
            myTeamButton = FindViewById<Button>(Resource.Id.ProfileViewMyTeamButton);
            exitTeamButton = FindViewById<Button>(Resource.Id.ProfileViewExitButton);

            if (Globals.MyProfile.Team.Id == -1)
            {
                exitTeamButton.Visibility = ViewStates.Gone;
                myTeamButton.Visibility = ViewStates.Gone;
                teamName.Visibility = ViewStates.Gone;
            }
            else
            {
                createTeamButton.Visibility = ViewStates.Gone;
                joinTeamButton.Visibility = ViewStates.Gone;
            }

            username.Text = Globals.MyProfile.Name;
            teamName.Text = Globals.MyProfile.Team.Name + " (" + Globals.MyProfile.Team.Score + ")";

            createTeamButton.Click += delegate { StartActivity(typeof(CreateTeamActivity)); };
            joinTeamButton.Click += delegate { StartActivity(typeof(JoinTeamActivity)); };
            myTeamButton.Click += delegate { StartActivity(typeof (MyTeamActivity)); };
            exitTeamButton.Click += ExitTeamButton_Click;
        }

        private void ExitTeamButton_Click(object sender, System.EventArgs e)
        {
            Globals.MyProfile.Team = new Team();
            Tools.GetWebResponse(
                $"http://192.168.1.39/Quest/LoginHandler.ashx?Type=ResetTeam&ID={Globals.MyProfile.Id}");
            Recreate();
        }
    }
}