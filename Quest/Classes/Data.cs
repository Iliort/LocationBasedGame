using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Java.IO;
using Java.Lang;
using Exception = System.Exception;

namespace Quest
{

    public class Data : Java.Lang.Object, IParcelable
    {
        public string Caption { get; set; }
        public string Text { get; set; }
        public Bitmap ImageBitmap { get; set; }
        public int Type { get; set; }

        public Action<ImageView> SetImage;

        public Data(string caption, string text)
        {
            Caption = caption;
            Text = text;
            Type = 0;
        }

        public Data(string caption, Bitmap imageBitmap)
        {
            Caption = caption;
            ImageBitmap = imageBitmap;
            Type = 1;
        }

        #region IParcelable implementation

        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
        {
            dest.WriteString(Caption);
            dest.WriteString(Text);
            ImageBitmap.WriteToParcel(dest, flags);
            dest.WriteInt(Type);
        }
        #endregion

        public static DataParcelableCreator InitializeCreator()
        {
            return new DataParcelableCreator();
        }
    }

    public class DataParcelableCreator : Java.Lang.Object, IParcelableCreator
    {
        public Java.Lang.Object CreateFromParcel(Parcel parcel)
        {
            /*if (parcel.ReadInt() == 0)*/ return new Data(parcel.ReadString(), parcel.ReadString());
            //else return new Data(parcel.ReadString(), parcel.ReadParcelable());
        }

        public Java.Lang.Object[] NewArray(int size)
        {
            return new Data[size];
        }
    }

    public class TextCardViewHolder : RecyclerView.ViewHolder
    {
        public TextView Caption { get; private set; }
        public TextView Text { get; private set; }

        public TextCardViewHolder(View itemView) : base(itemView)
        {
            Caption = itemView.FindViewById<TextView>(Resource.Id.TextCardCaption);
            Text = itemView.FindViewById<TextView>(Resource.Id.TextCardText);
        }
    }

    public class ImageCardViewHolder : RecyclerView.ViewHolder
    {
        public TextView Caption { get; private set; }
        public ImageView Image { get; private set; }

        public ImageCardViewHolder(View itemView) : base(itemView)
        {
            Caption = itemView.FindViewById<TextView>(Resource.Id.ImageCardCaption);
            Image = itemView.FindViewById<ImageView>(Resource.Id.ImageCardImage);
        }
    }

    public class DataListAdapter : RecyclerView.Adapter
    {
        public List<Data> DataList = new List<Data>();

        public DataListAdapter(List<Data> dataList)
        {
            foreach (var data in dataList) DataList.Add(data);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            RecyclerView.ViewHolder vh = null;
            View itemView;
            switch (viewType)
            {
                case 0:
                    itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.TextCardView, parent, false);
                    vh = new TextCardViewHolder(itemView);
                    break;
                case 1:
                    itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.ImageCardView, parent, false);
                    vh = new ImageCardViewHolder(itemView);
                    break;
                default:
                    throw new Exception("Data Type undefined!");
            }
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int Type = GetItemViewType(position);
            switch (Type)
            {
                case 0:
                    TextCardViewHolder vh = holder as TextCardViewHolder;
                    vh.Caption.Text = DataList[position].Caption;
                    vh.Text.Text = DataList[position].Text;
                    break;
                case 1:
                    ImageCardViewHolder vh1 = holder as ImageCardViewHolder;
                    vh1.Caption.Text = DataList[position].Caption;
                    vh1.Image.SetImageBitmap(DataList[position].ImageBitmap);
                    break;
                default:
                    throw new Exception("Data Type undefined!");

            }
        }

        public override int ItemCount
        {
            get { return DataList.Count; }
        }

        public override int GetItemViewType(int position)
        {
            return DataList[position].Type;
        }
    }
}