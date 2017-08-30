using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using Android.App.Admin;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.App;
using ActionBar = Android.Support.V7.App.ActionBar;


namespace Quest.Activities
{
    [Activity(Label = "Тест", MainLauncher = false, Icon = "@drawable/icon")]
    public class DataViewActivity : AppCompatActivity  
    {
        private List<Data> dataList = new List<Data>();

        protected override void OnCreate(Bundle bundle)
        {
            var messageNumber = Intent.Extras.GetInt("MessageToViewNumber");
            foreach (var i in Globals.Messages[messageNumber].DataList) dataList.Add(i);
            
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DataView);

            var toolbar = FindViewById<Toolbar>(Resource.Id.DataViewToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.DataRecyclerView);

            RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            DataListAdapter adapter = new DataListAdapter(dataList);
            recyclerView.SetAdapter(adapter);


        }
    }
}

