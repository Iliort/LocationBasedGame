using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using Android.Support.V7.App;
using Android.Views.Animations;
using Android.Widget;
using Clans.Fab;
using ActionBar = Android.Support.V7.App.ActionBar;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Quest.Activities
{
    [Activity(Label = "Test", MainLauncher = false, Icon = "@drawable/icon")]
    public class SendAnsActivity : AppCompatActivity
    {

        private List<Data> dataList = new List<Data>();
        private RecyclerView recyclerView;
        private int imageCounter = 1;
        private string subject = "";
        private int position = -1;
        private string srcName = "";

        protected override void OnCreate(Bundle bundle)
        {
            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SendAnsFragment);

            var toolbar = FindViewById<Toolbar>(Resource.Id.SendAnsToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            subject = Intent.Extras.GetString("SubjectToSend");
            position = Intent.Extras.GetInt("PositionToSend", -1);
            srcName = Intent.Extras.GetString("SrcNameToSend", "");

            if (position != -1)
            {
                if (srcName == "Tasks") subject = Globals.Tasks[position].Name;
                else if (srcName == "Bonuses") subject = Globals.Bonuses[position].Name;
            }
            toolbar.Title = subject;

            recyclerView = FindViewById<RecyclerView>(Resource.Id.SendAnsRecyclerView);

            RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            DataListAdapter adapter = new DataListAdapter(dataList);
            recyclerView.SetAdapter(adapter);

            FloatingActionMenu fabMenu = FindViewById<FloatingActionMenu>(Resource.Id.SendAnsFabMenu);
            fabMenu.SetMenuButtonShowAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.abc_fade_in));
            fabMenu.SetMenuButtonHideAnimation(AnimationUtils.LoadAnimation(this, Resource.Animation.abc_fade_out));

            FloatingActionButton sendFab = FindViewById<FloatingActionButton>(Resource.Id.SendAnsFab);
            sendFab.Click += SendFab_Click;

            OnScrollListenerFab scrollListener = new OnScrollListenerFab();
            scrollListener.AddFabMenu(fabMenu);
            scrollListener.AddFab(sendFab);
            recyclerView.SetOnScrollListener(scrollListener);

            FloatingActionButton addImageButton = FindViewById<FloatingActionButton>(Resource.Id.AddImageButton);
            addImageButton.Click += AddImageButton_Click;

            FloatingActionButton addTextButton = FindViewById<FloatingActionButton>(Resource.Id.AddTextButton);
            addTextButton.Click += AddTextButton_Click;
        }

        private void SendFab_Click(object sender, EventArgs e)
        {
            if (dataList.Count != 0)
            {
                Globals.Messages.Add(new Message(subject, Globals.MyProfile.Team.Name, MessageStatus.Seen, DateTime.Now, dataList));

                if (position != -1)
                {
                    if (srcName == "Tasks") Globals.Tasks[position].Status = TaskStatus.Sent;
                    else if (srcName == "Bonuses") Globals.Bonuses[position].Status = TaskStatus.Sent;
                }
                StartActivity(typeof (TaskListActivity));
                StartActivity(typeof (MessagesListActivity));
                Finish();
            }
            else
            {
                Toast toast = Toast.MakeText(this, Resource.String.CantSendEmptyMessage, ToastLength.Short);
               
                toast.Show();
            }
        }

        private void AddTextButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(TextEditorActivity));
            intent.PutExtra("textToEdit", "");
            StartActivityForResult(intent, 0);
        }

        private void AddImageButton_Click(object sender, EventArgs e)
        {
            var getIntent = new Intent(Intent.ActionGetContent);
            getIntent.SetType("image/*");
            StartActivityForResult(Intent.CreateChooser(getIntent, ""), 1);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                switch (requestCode)
                {
                    case 0:
                        dataList.Add(new Data(GetString(Resource.String.Text), data.Data.ToString()));
                        recyclerView.SetAdapter(new DataListAdapter(dataList));
                        break;
                    case 1:
                        dataList.Add(new Data(GetString(Resource.String.Photo) + " #" + imageCounter.ToString(), MediaStore.Images.Media.GetBitmap(ContentResolver, data.Data)));
                        imageCounter++;
                        recyclerView.SetAdapter(new DataListAdapter(dataList));
                        break;
                        
                }
            }
        }

        
    }
}