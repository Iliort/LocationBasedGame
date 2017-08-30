//using System.Collections.Generic;
//using Android.App;
//using Android.Widget;
//using Android.OS;
//using Android.Support.V7.Widget;
//using Android.Views;
//using System;

//namespace Quest
//{
//    [Activity(Label = "Тест", MainLauncher = true, Icon = "@drawable/icon")]
//    public class MainActivity : Activity
//    {

//        private List<Data> dataList = new List<Data>();
//        private List<Data> dataList2 = new List<Data>();

//        protected override void OnCreate(Bundle bundle)
//        {
//            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

//            base.OnCreate(bundle);
//            SetContentView(Resource.Layout.LocationFragment1);



//            dataList.Add(new Data("Локация",
//                "В бурные 90-е годы представительницы древнейшей профессии облюбовали северное направление нашей столицы. Найдите вот такое неожиданное место, расположенное по этому направлению. Часть его мы стыдливо прикрыли."));

//            dataList.Add(new Data("Media", Resource.Drawable.test));
//            dataList.Add(new Data("TestCaption", "TestParagraph"));

//            dataList.Add(new Data("Media", Resource.Drawable.test));
//            dataList.Add(new Data("TestCaption", "TestParagraph"));

//            dataList2.Add(new Data("TestCaption2", "TestParagraph2"));
//            dataList2.Add(new Data("TestCaption2", "TestParagraph2"));
//            dataList2.Add(new Data("TestCaption2", "TestParagraph2"));

//            RecyclerView recyclerView = FindViewById<RecyclerView>(Resource.Id.LocationRecyclerView);


//            RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(this);
//            recyclerView.SetLayoutManager(layoutManager);

//            DataListAdapter adapter = new DataListAdapter(dataList);
//            recyclerView.SetAdapter(adapter);


//        }
//    }


//    public class Data
//    {
//        public string Caption { get; private set; }
//        public string Text { get; private set; }
//        public int ImageID { get; private set; }
//        public int Type { get; private set; }

//        public Data(string caption, string text)
//        {
//            Caption = caption;
//            Text = text;
//            Type = 0;
//        }

//        public Data(string caption, int imageID)
//        {
//            Caption = caption;
//            ImageID = imageID;
//            Type = 1;
//        }
//    }

//    public class TextCardViewHolder : RecyclerView.ViewHolder
//    {
//        public TextView Caption { get; private set; }
//        public TextView Text { get; private set; }

//        public TextCardViewHolder(View itemView) : base(itemView)
//        {
//            Caption = itemView.FindViewById<TextView>(Resource.Id.TextCardCaption);
//            Text = itemView.FindViewById<TextView>(Resource.Id.TextCardText);
//        }
//    }

//    public class ImageCardViewHolder : RecyclerView.ViewHolder
//    {
//        public TextView Caption { get; private set; }
//        public ImageView Image { get; private set; }

//        public ImageCardViewHolder(View itemView) : base(itemView)
//        {
//            Caption = itemView.FindViewById<TextView>(Resource.Id.ImageCardCaption);
//            Image = itemView.FindViewById<ImageView>(Resource.Id.IamgeCardImage);

//        }
//    }

//    public class DataListAdapter : RecyclerView.Adapter
//    {
//        public List<Data> DataList = new List<Data>();

//        public DataListAdapter(List<Data> dataList)
//        {
//            foreach (var data in dataList) DataList.Add(data);
//        }

//        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
//        {
//            RecyclerView.ViewHolder vh = null;
//            View itemView;
//            switch (viewType)
//            {
//                case 0:
//                    itemView = LayoutInflater.From(parent.Context).
//                        Inflate(Resource.Layout.TextCardView, parent, false);
//                    vh = new TextCardViewHolder(itemView);
//                    break;
//                case 1:
//                    itemView = LayoutInflater.From(parent.Context).
//                        Inflate(Resource.Layout.ImageCardView, parent, false);
//                    vh = new ImageCardViewHolder(itemView);
//                    break;
//                default:
//                    throw new Exception("Data Type undefined!");
//            }
//            return vh;
//        }

//        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
//        {
//            int Type = GetItemViewType(position);
//            switch (Type)
//            {
//                case 0:
//                    TextCardViewHolder vh = holder as TextCardViewHolder;
//                    vh.Caption.Text = DataList[position].Caption;
//                    vh.Text.Text = DataList[position].Text;
//                    break;
//                case 1:
//                    ImageCardViewHolder vh1 = holder as ImageCardViewHolder;
//                    vh1.Caption.Text = DataList[position].Caption;
//                    vh1.Image.SetImageResource(DataList[position].ImageID);
//                    break;
//                default:
//                    throw new Exception("Data Type undefined!");

//            }
//        }

//        public override int ItemCount
//        {
//            get { return DataList.Count; }
//        }

//        public override int GetItemViewType(int position)
//        {
//            return DataList[position].Type;
//        }
//    }
//}