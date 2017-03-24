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

namespace SampleTodoXForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // items = new ObservableCollection<ToDo>(); // MVVM時
            // 最初のアイテムを追加
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "item no.1", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 3, 1) });
            lst.Add(new ToDo() { Id = 2, Text = "item no.2", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 3, 2) });
            lst.Add(new ToDo() { Id = 3, Text = "item no.3", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 3, 3) });
            // this.listView.ItemsSource = items = new ToDoFiltableCollection(lst);

            viewModel = new MainViewModel();
            viewModel.Items = items = new ToDoFiltableCollection(lst);
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
            await Navigation.PushAsync(new DetailPage(item, () => items.Update()));
            listView.SelectedItem = null;

        }

        /// <summary>
        /// 新規ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            var item = new ToDo() {
                Id = items.Count + 1,
                Text = "New ToDo",
                DueDate = null,         // 期限なし
                Completed = false,
                CreatedAt = DateTime.Now
            };
            await Navigation.PushAsync(new DetailPage(item, ()=> {
                items.Insert(0, item);
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
    }
}
