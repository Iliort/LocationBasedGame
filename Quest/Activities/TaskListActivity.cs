using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using System.Collections;
using System.Linq.Expressions;
using Android.Content;
using Android.Graphics;
using Android.Support.V7.App;
using Android.Widget;
using ActionBar = Android.Support.V7.App.ActionBar;
using Toolbar = Android.Support.V7.Widget.Toolbar;

using System.Xml.Linq;
using System.IO;
using System.Net;
using Android.Content.Res;
using Android.Media;
using Java.Lang;
using Java.Net;
using Mikepenz.MaterialDrawer;
using Mikepenz.MaterialDrawer.Models;
using Newtonsoft.Json.Linq;
using Thread = System.Threading.Thread;

namespace Quest.Activities
{
    [Activity(Label = "Test", MainLauncher = false, Icon = "@drawable/icon")]
    public class TaskListActivity : AppCompatActivity
    {
        private Thread timeUpdateThread;
        private bool countdown = false;
        protected override void OnCreate(Bundle bundle)
        {
            //ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TaskList);

            var toolbar = FindViewById<Toolbar>(Resource.Id.TaskListToolbar);
            SetSupportActionBar(toolbar);
            ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = GetString(Resource.String.TasksNotFound);

            Tools.SetDrawer(this, toolbar);

            LoadTasks();

            RecyclerView recyclerView1 = FindViewById<RecyclerView>(Resource.Id.LocationListRecyclerView);
            recyclerView1.SetLayoutManager(new UnscrollableLayoutManager(this));
            recyclerView1.SetAdapter(new TaskListAdapter(this, Globals.Tasks, "Tasks"));
            RecyclerView recyclerView2 = FindViewById<RecyclerView>(Resource.Id.BonusListRecyclerView);
            recyclerView2.SetLayoutManager(new UnscrollableLayoutManager(this));
            recyclerView2.SetAdapter(new TaskListAdapter(this, Globals.Bonuses, "Bonuses"));

            timeUpdateThread = new Thread(UpdateTime);
            timeUpdateThread.Start();
        }
        

        private void JSONLoad()
        {
            Globals.TasksJson = new WebClient().DownloadString(Globals.TasksUrl);
            dynamic json = JObject.Parse(Globals.TasksJson);
            
            foreach (var Task in json.tasks)
            {
                Task _task = new Task("", new List<Data>(), new List<Data>());
                _task.Name = Task.name;
                _task.Status = Task.status;
                foreach (var data in Task.task.datas)
                {
                    Data _data = new Data("", "");
                    _data.Caption = data.caption;
                    if (data.type == 0) _data.Text = data.text;
                    else if (data.type == 1)
                    {
                        //_data.ImageBitmap = GetImageBitmapFromUrl(data.imageURL);
                        _data.ImageBitmap = Tools.GetImageBitmapFromUrl(data.imageURL.ToString());
                        _data.Type = 1;
                    }
                    _task.TaskDataList.Add(_data);
                }

                foreach (var data in Task.location.datas)
                {
                    Data _data = new Data("", "");
                    _data.Caption = data.caption;
                    if (data.type == 0) _data.Text = data.text;
                    else if (data.type == 1)
                    {
                        _data.ImageBitmap = Tools.GetImageBitmapFromUrl(data.imageURL.ToString());
                        _data.Type = 1;
                    }
                    _task.LocationDataList.Add(_data);
                }
                Globals.Tasks.Add(_task);
            }

            foreach (var Task in json.bonuses)
            {
                Task _task = new Task("", new List<Data>(), new List<Data>());
                _task.Name = Task.name;
                _task.Status = Task.status;
                foreach (var data in Task.task.datas)
                {
                    Data _data = new Data("", "");
                    _data.Caption = data.caption;
                    if (data.type == 0) _data.Text = data.text;
                    else if (data.type == 1)
                    {
                        _data.ImageBitmap = Tools.GetImageBitmapFromUrl(data.imageURL.ToString());
                        _data.Type = 1;
                    }
                    _task.TaskDataList.Add(_data);
                }

                foreach (var data in Task.location.datas)
                {
                    Data _data = new Data("", "");
                    _data.Caption = data.caption;
                    if (data.type == 0) _data.Text = data.text;
                    else if (data.type == 1)
                    {
                        _data.ImageBitmap = Tools.GetImageBitmapFromUrl(data.imageURL.ToString());
                        _data.Type = 1;
                    }
                    _task.LocationDataList.Add(_data);
                }
                Globals.Bonuses.Add(_task);
            }
        }

        private void LoadTasks()
        {
            string []response = Tools.GetWebResponse("QuestInfoHandler.ashx?Type=GetLast").Split('\t');
            Globals.EndTime = DateTime.Parse(response[3]);
            if (Globals.EndTime < DateTime.Now)
            {
                countdown = false;
                Toast.MakeText(this, "OK", ToastLength.Long).Show();
            }
            else
            {
                countdown = true;
                Globals.StartTime = DateTime.Parse(response[2]);
                Globals.QuestName = response[1];
                Globals.TasksUrl = response[4];
                string taskJson = new WebClient().DownloadString(Globals.TasksUrl);
                
                
                if (taskJson != Globals.TasksJson)
                {
                    JSONLoad();
                }
            }

        }

        private void UpdateTime()
        {
            while (DateTime.Now < Globals.EndTime)
            {
               // RunOnUiThread(delegate
                //{
                    if (DateTime.Now < Globals.StartTime)
                    {
                        RunOnUiThread(() => SupportActionBar.Title = new TimeSpan((Globals.StartTime - DateTime.Now).Ticks).ToString(@"d\.hh\:mm\:ss"));
                    }
                    else
                    {
                        RunOnUiThread(() => SupportActionBar.Title = new TimeSpan((Globals.EndTime - DateTime.Now).Ticks).ToString(@"hh\:mm\:ss"));
                    }
                    Thread.Sleep(1000);
                //});
            }
        }
    }

    public enum TaskStatus
    {
        NotSent = 0,
        Sent = 1,
        Accepted = 2,
        Rejected = 3,
        Partially = 4
    }
    public class Task
    {
        public string Name { get; set; }
        private TaskStatus _status;

        public TaskStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                switch (_status)
                {
                    case TaskStatus.NotSent:
                        StatusColor = Color.ParseColor("#9E9E9E");
                        BackgroundColor = Color.ParseColor("#F5F5F5");
                        StatusStr = Tools.Activity.GetString(Resource.String.NotSent);
                        break;
                    case TaskStatus.Sent:
                        StatusColor = Color.ParseColor("#FFC107");
                        BackgroundColor = Color.ParseColor("#FFECB3");
                        StatusStr = Tools.Activity.GetString(Resource.String.Sent);
                        break;
                    case TaskStatus.Accepted:
                        StatusColor = Color.ParseColor("#4CAF50");
                        BackgroundColor = Color.ParseColor("#C8E6C9");
                        StatusStr = Tools.Activity.GetString(Resource.String.Accepted);
                        break;
                    case TaskStatus.Rejected:
                        StatusColor = Color.ParseColor("#F44336");
                        BackgroundColor = Color.ParseColor("#FFCDD2");
                        StatusStr = Tools.Activity.GetString(Resource.String.Rejected);
                        break;
                    case TaskStatus.Partially:
                        StatusColor = Color.ParseColor("#9C27B0");
                        BackgroundColor = Color.ParseColor("#E1BEE7");
                        StatusStr = Tools.Activity.GetString(Resource.String.Partly);
                        break;

                }
            }
        }

        public string StatusStr { get; protected set; }
        public Color StatusColor { get; protected set; }
        public Color BackgroundColor { get; protected set; }

        public List<Data> LocationDataList { get; set; }
        public List<Data> TaskDataList { get; set; }

        public Task(string name, List<Data> locationDataList, List<Data> taskDataList )
        {
            LocationDataList = new List<Data>();
            TaskDataList = new List<Data>();

            locationDataList.ForEach(i => LocationDataList.Add(i));
            taskDataList.ForEach(i => TaskDataList.Add(i));

            Name = name;
            StatusColor = Color.ParseColor("#000000");
            BackgroundColor = Color.ParseColor("#FFFFFF");
            Status = TaskStatus.NotSent;
        }
    }

    public class TaskItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView Status { get; private set; }
        public CardView Card { get; private set; }

        public TaskItemViewHolder(View itemView) : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.TaskItemName);
            Status = itemView.FindViewById<TextView>(Resource.Id.TaskItemStatus);
            Card = itemView.FindViewById<CardView>(Resource.Id.TaskItemCardView);
        }
    }

    public class TaskListAdapter : RecyclerView.Adapter
    {
        public List<Task> TaskList = new List<Task>();
        public Context Context { get; private set; }
        public string SrcName { get; private set; }
        public TaskListAdapter(Context context, List<Task> taskList, string srcName)
        {
            SrcName = srcName;
            Context = context;
            foreach (var data in taskList) TaskList.Add(data);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.TaskItemView, parent, false);
            RecyclerView.ViewHolder vh = new TaskItemViewHolder(itemView);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int Type = GetItemViewType(position);
            TaskItemViewHolder vh = holder as TaskItemViewHolder;
            vh.Name.Text = TaskList[position].Name;
            vh.Status.Text = TaskList[position].StatusStr;
            vh.Status.SetTextColor(TaskList[position].StatusColor);
            vh.Card.SetBackgroundColor(TaskList[position].BackgroundColor);
            vh.Card.Click += delegate
            {
                var intent = new Intent(Context, typeof(TabbedTaskActivity));
                intent.PutExtra("TaskToViewNumber", position);
                intent.PutExtra("TaskToViewSrcName", SrcName);
                Context.StartActivity(intent);
            };
        }

        public override int ItemCount
        {
            get { return TaskList.Count; }
        }

    }

    public class UnscrollableLayoutManager : LinearLayoutManager
    {
        public UnscrollableLayoutManager(Android.Content.Context context) : base(context) { }

        public override bool CanScrollVertically()
        {
            return false;
        }
    }
}
