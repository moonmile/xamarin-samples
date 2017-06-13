using SampleTodoXForms.Models;
using SampleTodoXForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Microsoft.WindowsAzure.MobileServices;

namespace SampleTodoXForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        // Azure Mobile Service に接続する
        private MobileServiceClient client;
        private IMobileServiceTable<ToDo> todoTable;
        // URL of the mobile app backend.
        const string applicationURL = @"https://sampletodomobileapp.azurewebsites.net";

        public MainPage()
        {
            InitializeComponent();
            // Azure Mobile Service を使う
            client = new MobileServiceClient(applicationURL);
            // ToDo テーブルを更新対象にする
            todoTable = client.GetTable<ToDo>();
            viewModel = new MainViewModel();
            viewModel.Items = new ObservableCollection<ToDo>();
            this.BindingContext = viewModel;
            // メッセージの受信の設定
            receiveMessage();
            RefreshItemsFromTableAsync();
        }

        /// <summary>
        /// MessagingCenter を利用して、画面間のデータをやり取りする
        /// </summary>
        private void receiveMessage()
        {
            MessagingCenter.Subscribe<DetailPage, ToDo>(this, "UpdateItem", async (page, item) => {
                // データを更新する
                await todoTable.UpdateAsync(item);
                // 表示を更新する
                await RefreshItemsFromTableAsync();
            });
            MessagingCenter.Subscribe<DetailPage, ToDo>(this, "AddItem", async (page, item) => {
                item.Id = Guid.NewGuid().ToString();
                // データを更新する
                await todoTable.InsertAsync(item);
                // 表示を更新する
                await RefreshItemsFromTableAsync();
            });
            MessagingCenter.Subscribe<SettingPage>(this, "UpdateSetting", async (page) => {
                // 表示を更新する
                await RefreshItemsFromTableAsync();
            });
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
                if (setting.DispCompleted == true)
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

                this.viewModel.Items.Clear();
                foreach (var it in list)
                {
                    viewModel.Items.Add(it);
                }
            }
            catch (Exception ex)
            {

            }
        }

        // 表示するデータ
        MainViewModel viewModel;

        // 設定
        Setting setting = new Setting()
        {
            DispCompleted = true,
            SortOrder = 0,              // 作成日順
        };

        /// <summary>
        /// 項目を選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ToDo;
            if (item == null)
                return;
            await Navigation.PushAsync(new DetailPage(item));
            listView.SelectedItem = null;

        }

        /// <summary>
        /// 新規ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            var item = ToDo.CreateNew();
            await Navigation.PushAsync(new DetailPage(item));
        }

        /// <summary>
        /// 設定ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Setting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage(setting));
        }

#if NOT_USE_AZURE
        IToDoStorage storage = DependencyService.Get<IToDoStorage>();

        /// <summary>
        /// 内部ストレージに保存
        /// </summary>
        void Save()
        {
            using (var st = storage.OpenWriter("save.xml"))
            {
                items.Save(st);
            }
        }
        /// <summary>
        /// 内部ストレージから読み込み
        /// </summary>
        void Load()
        {
            if (items == null)
            {
                items = new ToDoFiltableCollection();
            }
            using (var st = storage.OpenReader("save.xml"))
            {
                if (st == null || items.Load(st) == false)
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
#endif
    }

    /// <summary>
    /// ストレージ用のインターフェース
    /// </summary>
    public interface IToDoStorage
    {
        System.IO.Stream OpenReader(string file);
        System.IO.Stream OpenWriter(string file);
    }
}

