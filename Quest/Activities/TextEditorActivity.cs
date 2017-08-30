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
    [Activity(Label = "Hub", MainLauncher = true, Icon = "@drawable/icon")]
    public class TextEditorActivity : AppCompatActivity
    {
        private EditText edit;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TextEditor);

            var toolbar = FindViewById<Toolbar>(Resource.Id.TextEditorToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            edit = FindViewById<EditText>(Resource.Id.TextEditor);
            edit.Text = Intent.Extras.GetString("textToEdit");

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.TextDoneFab);
            fab.Click += delegate
            {
                Intent data = new Intent();
                data.SetData(Uri.Parse(edit.Text));
                if (edit.Text != "") SetResult(Result.Ok, data);
                else SetResult(Result.Canceled);
                Finish();
            };
        }


    }
}