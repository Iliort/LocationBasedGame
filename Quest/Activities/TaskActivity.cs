using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using Android.App.Admin;
using Android.Graphics;

namespace Quest.Activities
{
    [Activity(Label = "Тест", MainLauncher = false, Icon = "@drawable/icon")]
    public class TaskActivity : Activity
    {

        private List<Data> dataList = new List<Data>();
        private List<Data> dataList2 = new List<Data>();

        protected override void OnCreate(Bundle bundle)
        {
            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DataView);



            dataList.Add(new Data("Локация",
                "В бурные 90-е годы представительницы древнейшей профессии облюбовали северное направление нашей столицы. Найдите вот такое неожиданное место, расположенное по этому направлению. Часть его мы стыдливо прикрыли."));

            dataList.Add(new Data("Media", BitmapFactory.DecodeResource(Resources, Resource.Drawable.test)));
            dataList.Add(new Data("TestCaption", "TestParagraph"));

            dataList.Add(new Data("Media", BitmapFactory.DecodeResource(Resources, Resource.Drawable.test)));
            dataList.Add(new Data("TestCaption", "TestParagraph"));
            dataList2.Add(new Data("TestCaption2", "TestParagraph2"));
            dataList2.Add(new Data("TestCaption2", "TestParagraph2"));
            dataList2.Add(new Data("TestCaption2", "TestParagraph2"));

            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.DataRecyclerView);


            RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            DataListAdapter adapter = new DataListAdapter(dataList);
            recyclerView.SetAdapter(adapter);


        }
    }
}

    