using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using System;
using SampleTodoXForms.Models;
using Android.Runtime;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Linq;

namespace SampleTodo.Droid
{
    [Activity(Label = "SampleTodo.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // Azure Mobile Service に接続する
        private MobileServiceClient client;
        private IMobileServiceTable<ToDo> todoTable;
        // URL of the mobile app backend.
        const string applicationURL = @"https://sampletodomobileapp.azurewebsites.net";

        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Azure Mobile Service を使う
            client = new MobileServiceClient(applicationURL);
            // ToDo テーブルを更新対象にする
            todoTable = client.GetTable<ToDo>();
            items = new List<ToDo>();

            listview = FindViewById<ListView>(Resource.Id.listView);
            listview.Adapter = adapter = new TodoAdapter(this, items);
            listview.ItemClick += Listview_ItemClick;

            btnNew = FindViewById<Button>(Resource.Id.buttonNew);
            btnSetting = FindViewById<Button>(Resource.Id.buttonSetting);

            btnNew.Click += BtnNew_Click;
            btnSetting.Click += BtnSetting_Click;

            await RefreshItemsFromTableAsync();
        }

        /// <summary>
        /// Azure Mobile Service に接続して ToDo データを取得する
        /// </summary>
        /// <returns></returns>
        private async Task RefreshItemsFromTableAsync()
        {
            try
            {
                List<ToDo> list;
                if ( setting.DispCompleted == true )
                {
                    list = await todoTable.ToListAsync();
                }
                else
                {
                    // 未完了の項目のみ取得する
                    list = await todoTable.Where(x => x.Completed == false).ToListAsync();
                }
                // 表示順を変える
                switch (setting.SortOrder)
                {
                    case 0: // 作成日順/ID順
                        list = list.OrderByDescending(x => x.CreatedAt).ToList();
                        break;
                    case 1: // 項目名順
                        list = list.OrderBy(x => x.Text).ToList();
                        break;
                    case 2: // 期日順
                        list = list.OrderBy(x => x.DueDate).ToList();
                        break;
                }

                this.items.Clear();
                foreach ( var it in list )
                {
                    items.Add(it);
                }
                // アダプターを更新する
                adapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {

            }
        }

        // 表示するデータ
        List<ToDo> items;

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
            var item = ToDo.CreateNew();
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

        protected async override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
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
                        await todoTable.UpdateAsync(item);
                        // 表示を更新
                        await RefreshItemsFromTableAsync();
                    }
                    break;
                case 2: // 新規作成時
                    if (resultCode == Result.Ok)
                    {
                        var v = data.GetStringExtra("data");
                        var item = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDo>(v);
                        // データを更新する
                        item.Id = null;
                        await todoTable.InsertAsync(item);
                        // 表示を更新
                        await RefreshItemsFromTableAsync();
                    }
                    break;
                case 3: // 設定画面からの戻り
                    if (resultCode == Result.Ok)
                    {
                        setting.DispCompleted = data.GetBooleanExtra("DispCompleted", true);
                        setting.SortOrder = data.GetIntExtra("SortOrder", 0);
                        // 表示を更新
                        await RefreshItemsFromTableAsync();
                    }
                    break;
                default:
                    break;
            }

        }
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
            return position; 
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }
            var it = _items[position];
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = it.StrDueDate;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = it.Text;
            return view;
        }
    }
}

