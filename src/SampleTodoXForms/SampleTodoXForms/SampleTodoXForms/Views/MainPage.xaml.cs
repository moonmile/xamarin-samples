using SampleTodoXForms.Models;
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
            items = new List<ToDo>();
            // items = new ObservableCollection<ToDo>(); // MVVM時
            items.Add(new ToDo() { Id = 1, Text = "item no.1" });
            items.Add(new ToDo() { Id = 2, Text = "item no.2" });
            items.Add(new ToDo() { Id = 3, Text = "item no.3" });
            this.listView.ItemsSource = items;
        }
        List<ToDo> items;
        // ObservableCollection<ToDo> items; // MVVM時
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
            await Navigation.PushAsync(new DetailPage() { Item = item });
            listView.SelectedItem = null;
        }

        /// <summary>
        /// 新規ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddItem_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new DetailPage());
            items.Insert(0, new ToDo() { Id = items.Count + 1, Text = DateTime.Now.ToString() });
            // MVVM時は以下の2行を消す
            this.listView.ItemsSource = null;
            this.listView.ItemsSource = items;
        }
        /// <summary>
        /// 設定ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Setting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage());
        }
    }
}
