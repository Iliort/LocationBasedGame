using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using System.Linq;
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Widget;
using Clans.Fab;
using Newtonsoft.Json.Linq;
using ActionBar = Android.Support.V7.App.ActionBar;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Quest;

namespace Quest.Activities
{
    [Activity(Label = "@string/Messages", MainLauncher = false, Icon = "@drawable/icon")]
    public class MessagesListActivity : AppCompatActivity
    {
        private RecyclerView recyclerView;
        protected override void OnCreate(Bundle bundle)
        {
            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MessagesList);

            var toolbar = FindViewById<Toolbar>(Resource.Id.MessagesListToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            //var test = Globals.Messages;
            actionBar.SetDisplayHomeAsUpEnabled(true);

            Tools.SetDrawer(this, toolbar);

            UpdateMessages();

            recyclerView = FindViewById<RecyclerView>(Resource.Id.MessagesRecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetAdapter(new MessageListAdapter(this));
            
            FloatingActionButton createFab = FindViewById<FloatingActionButton>(Resource.Id.MessagesListFab);
            createFab.Click += CreateFab_Click;

            OnScrollListenerFab scrollListener = new OnScrollListenerFab();
            scrollListener.AddFab(createFab);
            recyclerView.SetOnScrollListener(scrollListener);

        }

        private void CreateFab_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SendAnsActivity));
            intent.PutExtra("SubjectToSend", Resource.String.Message);
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            recyclerView.SetAdapter(new MessageListAdapter(this));
            
        }

        private void UpdateMessages ()
        {
            List<string> messagesFilenames =
                Tools.GetWebResponse($"FileHandler.ashx?Type=GetMessages&teamId={Globals.MyProfile.Team.Id}")
                    .Split(new []{"\t"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            //TODO Create a folder for each teamId 
            foreach (var i in messagesFilenames)
            {
                if (!Globals.MessagesFilenames.Contains(i))
                {
                    Globals.MessagesFilenames.Add(i);
                    MessageAdd(Tools.GetWebResponse($"FileHandler.ashx?Type=GetMessage&teamId={Globals.MyProfile.Team.Id}&filename={i}"));
                }
            }
        }

        private void MessageAdd(string jsonStr)
        {
            Globals.TasksJson = new WebClient().DownloadString(Globals.TasksUrl);
            dynamic json = JObject.Parse(jsonStr);
            Message msg = new Message("", "", MessageStatus.NotSeen, DateTime.Now, new List<Data>());

            msg.Sender = json.sender;
            msg.Status = json.status;
            msg.Subject = json.subject;
            msg.Time = json.time;

            foreach (var data in json.DataList)
            {
                Data _data = new Data("", "");
                _data.Caption = data.caption;
                if (data.type == 0) _data.Text = data.text;
                else if (data.type == 1)
                {
                    _data.ImageBitmap = Tools.GetImageBitmapFromUrl(data.imageURL.ToString());
                    _data.Type = 1;
                }
                msg.DataList.Add(_data);
            }

            Globals.Messages.Add(msg);
        }
    }

    public enum MessageStatus
    {
        Seen = 0,
        NotSeen = 1
    }
    public class Message
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        private MessageStatus _status;
        public MessageStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                switch (value)
                {
                    case MessageStatus.Seen:
                        StatusColor = Color.ParseColor("#9E9E9E");
                        BackgroundColor = Color.ParseColor("#F5F5F5");
                        StatusStr = Tools.Activity.GetString(Resource.String.Seen);
                        break;
                    case MessageStatus.NotSeen:
                        StatusColor = Color.ParseColor("#ff4081");
                        BackgroundColor = Color.ParseColor("#F5F5F5");
                        StatusStr = Tools.Activity.GetString(Resource.String.NotSeen);
                        break;
                }
            }
        }

        public string StatusStr { get; set; }
        public Color StatusColor {   get; set; }
        public Color BackgroundColor { get; set; }

        public DateTime Time { get; set; }

        public List<Data> DataList { get; set; } 

        public Message(string subject, string sender, MessageStatus status, DateTime time, List<Data> dataList)
        {
            DataList = new List<Data>();
            dataList.ForEach(i => DataList.Add(i));

            Subject = subject;
            Sender = sender;
            Status = status;
            Time = time;
        }
    }

    public class MessageItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView Sender { get; private set; }
        public TextView Subject { get; private set; }
        public TextView Status { get; private set; }
        public TextView Time { get; private set; }
        public CardView Card { get; private set; }

        public MessageItemViewHolder(View itemView) : base(itemView)
        {
            Sender = itemView.FindViewById<TextView>(Resource.Id.MessageItemSender);
            Status = itemView.FindViewById<TextView>(Resource.Id.MessageItemStatus);
            Card = itemView.FindViewById<CardView>(Resource.Id.MessageItemCardView);
            Subject = itemView.FindViewById<TextView>(Resource.Id.MessageItemSubj);
            Time = itemView.FindViewById<TextView>(Resource.Id.MessageItemTime);
        }
    }

    public class MessageListAdapter : RecyclerView.Adapter
    {
        public Context Context { get; private set; }

        public MessageListAdapter(Context context)
        {
            Context = context;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.MessageItemView, parent, false);
            RecyclerView.ViewHolder vh = new MessageItemViewHolder(itemView);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int Type = GetItemViewType(position);
            MessageItemViewHolder vh = holder as MessageItemViewHolder;

            vh.Subject.Text = Globals.Messages[position].Subject;
            vh.Sender.Text = Globals.Messages[position].Sender;
            vh.Status.Text = Globals.Messages[position].StatusStr;
            vh.Status.SetTextColor(Globals.Messages[position].StatusColor);
            vh.Time.Text = Globals.Messages[position].Time.ToShortTimeString();
            vh.Card.SetBackgroundColor(Globals.Messages[position].BackgroundColor);

            vh.Card.Click += delegate
            {
                var intent = new Intent(Context, typeof (DataViewActivity));
                intent.PutExtra("MessageToViewNumber", position);
                Globals.Messages[position].Status = MessageStatus.Seen;
                Context.StartActivity(intent);
            };
        }

        public override int ItemCount
        {
            get { return Globals.Messages.Count; }
        }

    }

}

