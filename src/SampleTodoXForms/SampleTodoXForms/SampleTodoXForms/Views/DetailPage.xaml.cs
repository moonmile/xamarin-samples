using SampleTodoXForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleTodoXForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();
        }
        public DetailPage(ToDo item, Action saved = null )
        {
            InitializeComponent();
            this.BindingContext = _item = item;
            this._saved = saved;
        }

        ToDo _item;
        Action _saved;

        /// <summary>
        /// 保存ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Save_Clicked(object sender, EventArgs e)
        {
            // 元に戻る
            this.Navigation.PopAsync();
            // 保存時のコールバックを呼び出し
            if ( this._saved != null )
            {
                this._saved();
            }
        }
        /// <summary>
        /// 戻るボタンを押したとき
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // 保存時のコールバックを呼び出し
            if (this._saved != null)
            {
                this._saved();
            }
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// 期日のトグルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if ( swDue.IsToggled )
            {
                if (_item.DueDate == null)
                {
                    _item.DueDate = DateTime.Now;
                }
                dpDue.IsVisible = true;
            }
            else
            {
                _item.DueDate = null;
                dpDue.IsVisible = false;
            }
        }
    }
}
