using SampleTodoXForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTodoXForms.Models
{
    /// <summary>
    /// ToDo のアイテムクラス
    /// </summary>
    public class ToDo : ObservableObject
    {
        // ユニークID
        public int Id { get; set; }
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
                this.OnPropertyChanged(nameof(DispDueDate));
                this.OnPropertyChanged(nameof(StrDueDate));
            }
        }
        // 完了フラグ
        public bool Completed { get; set; }
        // 作成日
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 期日の表示/非表示
        /// </summary>
        public bool UseDueDate
        {
            get
            {
                return this.DueDate != null;
            }
            set
            {
                if (value == false)
                {
                    this.dueDate = null;
                }
                else
                {
                    this.dueDate = DateTime.Now;
                }
                this.OnPropertyChanged(nameof(UseDueDate));
            }
        }
        /// <summary>
        /// Xamarin.Forms の DatePicker 用
        /// </summary>
        public DateTime DispDueDate
        {
            get
            {
                return this.DueDate == null ? DateTime.Now : DueDate.Value;
            }
            set
            {
                this.dueDate = value;
            }
        }
        public string StrDueDate
        {
            get
            {
                return this.DueDate == null ? "" : DueDate.Value.ToString("yyyy-MM-dd");
            }
        }


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
        /// <summary>
        /// 初期化メソッド
        /// </summary>
        /// <returns></returns>
        public static ToDo CreateNew()
        {
            return new ToDo()
            {
                Id = 0,
                Text = "New ToDo",
                DueDate = null,         // 期限なし
                Completed = false,
                CreatedAt = DateTime.Now
            };
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
