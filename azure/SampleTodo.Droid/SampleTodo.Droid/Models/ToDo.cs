using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SampleTodo.Droid.Helpers;

namespace SampleTodo.Droid.Models
{
    /// <summary>
    /// ToDo のアイテムクラス
    /// </summary>
    public class ToDo : ObservableObject
    {
        // ユニークID
        public string Id { get; set; }
        // 項目名
        string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        // 期日
        DateTime? dueDate;
        public DateTime? DueDate
        {
            get { return dueDate; }
            set
            {
                SetProperty(ref dueDate, value);
                this.OnPropertyChanged("DispDueDate");
                this.OnPropertyChanged("StrDueDate");
            }
        }
        public bool UseDueDate
        {
            get
            {
                return this.DueDate != null;
            }
        }
        public DateTime DispDueDate
        {
            get
            {
                return this.DueDate == null ? DateTime.Now : DueDate.Value;
            }
        }
        public string StrDueDate
        {
            get
            {
                return this.DueDate == null ? "" : DueDate.Value.ToString("yyyy-MM-dd");
            }
        }
        // 完了
        public bool Completed { get; set; }
        // 作成日
        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// 編集用のコピーメソッド
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public ToDo Copy(ToDo target = null)
        {
            if (target == null)
            {
                target = new ToDo();
            }
            target.Id = this.Id;
            target.text = this.text;
            target.DueDate = this.DueDate;
            target.Completed = this.Completed;
            target.CreatedAt = this.CreatedAt;
            return target;
        }
    }

    /// <summary>
    /// 設定クラス
    /// </summary>
    public class Setting
    {
        // 完了の表示
        public bool DispCompleted { get; set; }
        // 表示順 (0:作成順, 1:項目名順, 2:期日順)
        public int SortOrder { get; set; }
    }
}