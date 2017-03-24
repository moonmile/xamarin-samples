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
            // items = new ObservableCollection<ToDo>(); // MVVM時
            // 最初のアイテムを追加
            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "item no.1", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 3, 1) });
            lst.Add(new ToDo() { Id = 2, Text = "item no.2", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 3, 2) });
            lst.Add(new ToDo() { Id = 3, Text = "item no.3", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 3, 3) });
            items = new ToDoFiltableCollection(lst);
            this.listView.ItemsSource = items;
        }

        /// <summary>
        /// フィルターができるコレクションクラス
        /// </summary>
        class ToDoFiltableCollection : ObservableCollection<ToDo>
        {
            // 元のリストデータ
            private List<ToDo> _items;
            // ソート用の項目
            bool _dispComplted = true;
            int _sortOrder = 0;

            public ToDoFiltableCollection( List<ToDo> items ) : base(items)
            {
                _items = items;
            }
            public new void Add( ToDo item )
            {
                _items.Add(item);
                SetFilter(_dispComplted, _sortOrder);
            }
            public new bool Remove( ToDo item )
            {
                bool b = _items.Remove(item);
                this.Remove(item);
                return b;
            }
            public new void Insert( int index, ToDo item )
            {
                _items.Insert(index, item);
                SetFilter(_dispComplted, _sortOrder);
            }

            /// <summary>
            /// 項目のアップデート
            /// </summary>
            public void Update()
            {
                // ソートを反映させる
                SetFilter(_dispComplted, _sortOrder);
            }

            public void SetFilter( bool dispCompleted, int sortOrder )
            {
                _dispComplted = dispCompleted;
                _sortOrder = sortOrder;

                List<ToDo> lst = _items;
                switch ( sortOrder )
                {
                    case 0: // 作成日順/ID順
                        lst = _items.OrderByDescending(x => x.CreatedAt).ToList();
                        break;
                    case 1: // 項目名順
                        lst = _items.OrderBy(x => x.Text).ToList();
                        break;
                    case 2: // 期日順
                        lst = _items.OrderBy(x => x.DueDate).ToList();
                        break;
                }
                // 未完了だけを表示する
                if (dispCompleted == false)
                {
                    lst = lst.Where(x => x.Completed == false).ToList();
                }
                // 全てを追加し直す
                this.Clear();
                lst.All(x => { base.Add(x); return true; });
            }
        }

        // 表示するデータ
        // ObservableCollection<ToDo> items;
        ToDoFiltableCollection items;
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
