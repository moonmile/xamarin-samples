using SampleTodoXForms.Helpers;
using SampleTodoXForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTodoXForms.ViewModels
{

    /// <summary>
    /// SettingPageのViewModelクラス
    /// </summary>
    public class SettingViewModel : ObservableObject
    {
        private Setting setting;
        public SettingViewModel( Setting item )
        {
            setting = item;
            SortItems = new List<string> {
                "作成順", "項目名順", "期日順"
            };
        }
        /// <summary>
        /// 完了項目の表示状態
        /// </summary>
        public bool DispCompleted
        {
            get {
                return setting.DispCompleted;
            }
            set
            {
                if ( setting.DispCompleted !=  value )
                {
                    setting.DispCompleted = value;
                    OnPropertyChanged(nameof(DispCompleted));
                }
            }
        }
        /// <summary>
        /// 表示順のインデックス
        /// </summary>
        public int SortOrder
        {
            get
            {
                return setting.SortOrder;
            }
            set
            {
                if (setting.SortOrder != value)
                {
                    setting.SortOrder = value;
                    OnPropertyChanged(nameof(SortOrder));
                }
            }
        }
        /// <summary>
        /// 表示順のリスト
        /// </summary>
        public List<string> SortItems { get; }
    }
}
