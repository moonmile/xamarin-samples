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

namespace SampleTodoXForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // items = new ObservableCollection<ToDo>(); // 単純なMVVMパターンの場合

            // 内部ストレージから読み込み
            items = new ToDoFiltableCollection();
            this.Load();
            /*
            // 最初のアイテムを追加
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "item no.1", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 3, 1) });
            lst.Add(new ToDo() { Id = 2, Text = "item no.2", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 3, 2) });
            lst.Add(new ToDo() { Id = 3, Text = "item no.3", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 3, 3) });
            */
            viewModel = new MainViewModel();
            viewModel.Items = items; // = new ToDoFiltableCollection(lst);
            this.BindingContext = viewModel;
        }


        // 表示するデータ
        // ObservableCollection<ToDo> items;
        ToDoFiltableCollection items;
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
            await Navigation.PushAsync(
                new DetailPage(item, 
                () => {
                    items.UpdateFilter();
                    // 内部ストレージに保存
                    this.Save();
                }));
            listView.SelectedItem = null;

        }

        /// <summary>
        /// 新規ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            var item = new ToDo()
            {
                Id = items.Count + 1,
                Text = "New ToDo",
                DueDate = null,         // 期限なし
                Completed = false,
                CreatedAt = DateTime.Now
            };
            await Navigation.PushAsync(new DetailPage(item, () =>
            {
                items.Add(item);
                // 内部ストレージに保存
                this.Save();
            }));
        }

        /// <summary>
        /// 設定ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Setting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage(setting, () =>
            {
                items.SetFilter(setting.DispCompleted, setting.SortOrder);
            }));
        }

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

