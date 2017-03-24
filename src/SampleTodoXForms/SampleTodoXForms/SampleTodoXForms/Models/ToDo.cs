using SampleTodoXForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTodoXForms.Models
{

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
        public DateTime? DueDate {
            get { return dueDate; }
            set {
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
            set
            {
                this.DueDate = value;
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
    }

    public class Setting
    {
        // 完了の表示
        public bool DispCompleted { get; set; }
        // 表示順 (0:作成順, 1:項目名順, 2:期日順)
        public int SortOrder { get; set; }
    }
}
