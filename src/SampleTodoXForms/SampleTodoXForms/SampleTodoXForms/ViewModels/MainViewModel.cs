using SampleTodoXForms.Helpers;
using SampleTodoXForms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTodoXForms.ViewModels
{
    /// <summary>
    /// フィルターができるコレクションクラス
    /// </summary>
    public class ToDoFiltableCollection : ObservableCollection<ToDo>
    {
        // 元のリストデータ
        private List<ToDo> _items;
        // ソート用の項目
        bool _dispComplted = true;
        int _sortOrder = 0;

        public ToDoFiltableCollection(List<ToDo> items) : base(items)
        {
            _items = items;
        }
        public new void Add(ToDo item)
        {
            _items.Add(item);
            SetFilter(_dispComplted, _sortOrder);
        }
        public new bool Remove(ToDo item)
        {
            bool b = _items.Remove(item);
            this.Remove(item);
            return b;
        }
        public new void Insert(int index, ToDo item)
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

        public void SetFilter(bool dispCompleted, int sortOrder)
        {
            _dispComplted = dispCompleted;
            _sortOrder = sortOrder;

            List<ToDo> lst = _items;
            switch (sortOrder)
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

    /// <summary>
    /// MainPageのViewModelクラス
    /// </summary>
    public class MainViewModel : ObservableObject 
    {
        public ToDoFiltableCollection Items { get; set; }

    }
}
