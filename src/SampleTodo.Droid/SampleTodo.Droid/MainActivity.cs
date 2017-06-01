using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using System;
using SampleTodoXForms.Models;
using Android.Runtime;

namespace SampleTodo.Droid
{
    [Activity(Label = "SampleTodo.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // 内部ストレージから読み込み
            items = new ToDoFiltableCollection();
            this.Load();
            /*
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "item no.1", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 3, 1) });
            lst.Add(new ToDo() { Id = 2, Text = "item no.2", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 3, 2) });
            lst.Add(new ToDo() { Id = 3, Text = "item no.3", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 3, 3) });
            items = new ToDoFiltableCollection(lst);
            */
            listview = FindViewById<ListView>(Resource.Id.listView);
            listview.Adapter = adapter = new TodoAdapter(this, items);
            listview.ItemClick += Listview_ItemClick;

            btnNew = FindViewById<Button>(Resource.Id.buttonNew);
            btnSetting = FindViewById<Button>(Resource.Id.buttonSetting);

            btnNew.Click += BtnNew_Click;
            btnSetting.Click += BtnSetting_Click;
        }

        // 表示するデータ
        ToDoFiltableCollection items;

        // 設定
        Setting setting = new Setting()
        {
            DispCompleted = true,
            SortOrder = 0,              // 作成日順
        };

        ListView listview;
        Button btnNew, btnSetting;
        TodoAdapter adapter;

        /// <summary>
        /// 項目を選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = items[e.Position];
            var intent = new Intent(this, typeof(DetailActivity));
            // データをシリアライズして渡す
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            intent.PutExtra("data", data);
            StartActivityForResult(intent, 1);
        }
        /// <summary>
        /// 新規ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNew_Click(object sender, System.EventArgs e)
        {
            var item = new ToDo()
            {
                Id = items.Count + 1,
                Text = "New ToDo",
                DueDate = null,         // 期限なし
                Completed = false,
                CreatedAt = DateTime.Now
            };
            var intent = new Intent(this, typeof(DetailActivity));
            // データをシリアライズして渡す
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            intent.PutExtra("data", data);
            StartActivityForResult(intent, 2);
        }
        /// <summary>
        /// 設定ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetting_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingActivity));
            intent.PutExtra("DispCompleted", setting.DispCompleted);
            intent.PutExtra("SortOrder", setting.SortOrder);
            StartActivityForResult(intent, 3);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            switch (requestCode)
            {
                case 1: // 項目を選択時
                    if (resultCode == Result.Ok)
                    {
                        var v = data.GetStringExtra("data");
                        var item = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDo>(v);
                        // データを更新する
                        items.Update(item.Id, item);
                        // アダプターを更新
                        adapter.NotifyDataSetChanged();
                        // 内部ストレージに保存
                        this.Save();
                    }
                    break;
                case 2: // 新規作成時
                    if (resultCode == Result.Ok)
                    {
                        var v = data.GetStringExtra("data");
                        var item = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDo>(v);
                        // データを更新する
                        items.Add(item);
                        // アダプターを更新
                        adapter.NotifyDataSetChanged();
                        // 内部ストレージに保存
                        this.Save();
                    }
                    break;
                case 3: // 設定画面からの戻り
                    if (resultCode == Result.Ok)
                    {
                        setting.DispCompleted = data.GetBooleanExtra("DispCompleted", true);
                        setting.SortOrder = data.GetIntExtra("SortOrder", 0);
                        items.SetFilter(setting.DispCompleted, setting.SortOrder);
                        // アダプターを更新
                        adapter.NotifyDataSetChanged();
                    }
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// 内部ストレージに保存
        /// </summary>
        void Save()
        {
            var docs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var file = System.IO.Path.Combine(docs, "save.xml");
            using (var st = System.IO.File.OpenWrite(file))
            {
                items.Save(st);
            }
        }
        /// <summary>
        /// 内部ストレージから読み込み
        /// </summary>
        void Load()
        {
            var docs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var file = System.IO.Path.Combine(docs, "save.xml");
            if ( System.IO.File.Exists( file ))
            {
                using (var st = System.IO.File.OpenRead(file))
                {
                    if ( items == null )
                    {
                        items = new ToDoFiltableCollection();
                    }
                    if ( items.Load(st) == false )
                    {
                        // 失敗時には、初期データを作成する
                        System.IO.File.Delete(file);
                        // 初期データを作成する
                        var lst = new List<ToDo>();
                        lst.Add(new ToDo() { Id = 1, Text = "sample todo", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 3, 1) });
                        items = new ToDoFiltableCollection(lst);
                    }
                }
            }
            else
            {
                // 初期データを作成する
                var lst = new List<ToDo>();
                lst.Add(new ToDo() { Id = 1, Text = "sample no.1", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 3, 1) });
                lst.Add(new ToDo() { Id = 2, Text = "sample no.2", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 3, 2) });
                lst.Add(new ToDo() { Id = 3, Text = "sample no.3", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 3, 3) });
                items = new ToDoFiltableCollection(lst);
            }
        }
    }


    public class TodoAdapter : BaseAdapter<ToDo>
    {
        Activity _activity;
        ToDoFiltableCollection _items;

        public TodoAdapter(Activity act, ToDoFiltableCollection items)
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
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = it.DueDate == null ? "--" : it.DueDate.Value.ToString("yyyy-MM-dd");
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = it.Text;
            return view;
        }
    }
}

