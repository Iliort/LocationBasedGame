using System.Collections.Generic;
using Android.OS;
using Android.Views;
using System;
using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views.TextService;
using Android.Widget;
using ActionBar = Android.Support.V7.App.ActionBar;
using FloatingActionButton = Clans.Fab.FloatingActionButton;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Quest.Activities
{
    [Activity(Label = "Test", MainLauncher = false, Icon = "@drawable/icon")]
    public class TabbedTaskActivity : AppCompatActivity
    {

        private Toolbar toolbar;
        private TabLayout tabLayout;
        private ViewPager viewPager;
        private FloatingActionButton fab;

        private List<Data> locationDataList = new List<Data>();
        private List<Data> taskDataList = new List<Data>();

        DataViewFragment locationFragment;
        DataViewFragment taskFragment;

        private int position;
        private string srcName;

        protected override void OnCreate(Bundle bundle)
        {
            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            //try
            {

                base.OnCreate(bundle);
                SetContentView(Resource.Layout.TabbedTaskView);

                srcName = Intent.GetStringExtra("TaskToViewSrcName");
                position = Intent.GetIntExtra("TaskToViewNumber", -1);

                if (srcName == "Tasks")
                {
                    Globals.Tasks[position].LocationDataList.ForEach(i => locationDataList.Add(i));
                    Globals.Tasks[position].TaskDataList.ForEach(i => taskDataList.Add(i));

                }
                else if (srcName == "Bonuses")
                {
                    Globals.Bonuses[position].LocationDataList.ForEach(i => locationDataList.Add(i));
                    Globals.Bonuses[position].TaskDataList.ForEach(i => taskDataList.Add(i));

                }

                toolbar = FindViewById<Toolbar>(Resource.Id.TaskViewToolbar);
                SetSupportActionBar(toolbar);
                ActionBar actionBar = SupportActionBar;
                actionBar.SetDisplayHomeAsUpEnabled(true);

                Tools.SetDrawer(this, toolbar);

                viewPager = FindViewById<ViewPager>(Resource.Id.TaskViewViewPager);
                SetupViewPager(viewPager);

                tabLayout = FindViewById<TabLayout>(Resource.Id.TaskViewTabLayout);
                tabLayout.SetupWithViewPager(viewPager);

                fab = FindViewById<FloatingActionButton>(Resource.Id.TaskViewFab);
                fab.Click += Fab_Click;
            }
            //catch (Exception e)
            {
                //throw new Exception(e.Message + " (My exception)");
            }
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof (SendAnsActivity));
            intent.PutExtra("PositionToSend", position);
            intent.PutExtra("SrcNameToSend", srcName);
            StartActivity(intent);
        }

        private void SetupViewPager(ViewPager viewPager)
        {
            locationFragment = new DataViewFragment(locationDataList);
            taskFragment = new DataViewFragment(taskDataList);
            
            ViewPagerAdapter adapter = new ViewPagerAdapter(SupportFragmentManager);
            adapter.AddFragment(locationFragment, GetString(Resource.String.Location));
            adapter.AddFragment(taskFragment, GetString(Resource.String.Task));
            viewPager.Adapter = adapter;

           
        }
    }
}

