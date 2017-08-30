using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Java.Lang;

namespace Quest.Activities
{
    public class DataViewFragment : Fragment
    {
        public List<Data> DataList = new List<Data>();

        public DataViewFragment(List<Data> dataList) : base()
        {
            dataList.ForEach(i => DataList.Add(i));
        }

        public RecyclerView RecyclerView { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.DataView, container, false);

            RecyclerView = view.FindViewById<RecyclerView>(Resource.Id.DataRecyclerView);
            RecyclerView.SetAdapter(new DataListAdapter(DataList));
            RecyclerView.SetLayoutManager(new LinearLayoutManager(view.Context));

            var toolbar = view.FindViewById<Toolbar>(Resource.Id.DataViewToolbar);
            (toolbar.Parent as IViewManager).RemoveView(toolbar);

            return view;
        }
    }

    public class ViewPagerAdapter : FragmentStatePagerAdapter
    {
        public List<Fragment> Fragments { get; private set; }
        public List<string> FragmentsCaptions { get; private set; }

        public ViewPagerAdapter(FragmentManager fm) : base(fm)
        {
            Fragments = new List<Fragment>();
            FragmentsCaptions = new List<string>();
        }

        public override Fragment GetItem(int position)
        {
            return Fragments[position];
        }

        public override int Count
        {
            get { return Fragments.Count; }
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(FragmentsCaptions[position]);
        }

        public void AddFragment(Fragment fragment, string caption)
        {
            Fragments.Add(fragment);
            FragmentsCaptions.Add(caption);
        }
    }
}