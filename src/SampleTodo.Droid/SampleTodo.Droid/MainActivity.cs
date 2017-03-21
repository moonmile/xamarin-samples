using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using System;

namespace SampleTodo.Droid
{
    [Activity(Label = "SampleTodo.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            items = new List<ToDo>();
            items.Add(new ToDo() { Id = 1, Text = "item no.1" });
            items.Add(new ToDo() { Id = 2, Text = "item no.2" });
            items.Add(new ToDo() { Id = 3, Text = "item no.3" });

            listview = FindViewById<ListView>(Resource.Id.tableview);
            listview.Adapter = new TodoAdapter(this, items);
            listview.ItemClick += Listview_ItemClick;

            btnNew = FindViewById<Button>(Resource.Id.buttonNew);
            btnSetting = FindViewById<Button>(Resource.Id.buttonSetting);

            btnNew.Click += BtnNew_Click;
            btnSetting.Click += BtnSetting_Click;
        }

        /// <summary>
        /// アイテムがクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var todo = items[e.Position];
            var intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("id", todo.Id);
            intent.PutExtra("text", todo.Text);
            StartActivity(intent);
        }

        private void BtnSetting_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingActivity));
            StartActivity(intent);
        }

        private void BtnNew_Click(object sender, System.EventArgs e)
        {
            // var intent = new Intent(this, typeof(DetailActivity));
            // StartActivity(intent);

            items.Insert(0, new ToDo() { Id = items.Count + 1, Text = DateTime.Now.ToString() });
            listview.Adapter = new TodoAdapter(this, items);
        }

        List<ToDo> items;
        ListView listview;
        Button btnNew, btnSetting;
    }

    /// <summary>
    /// ToDo用のデータクラス
    /// </summary>
    public class ToDo
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class TodoAdapter : BaseAdapter<ToDo>
    {

        Activity _activity;
        List<ToDo> _items;

        public TodoAdapter(Activity act, List<ToDo> items)
        {
            _activity = act;
            _items = items;
        }

        public override ToDo this[int position]
        {
            get
            {
                return _items[position];
            }
        }

        public override int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }
            var it = _items[position];
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = "-";
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = it.Text;

            return view;
        }
    }
}

