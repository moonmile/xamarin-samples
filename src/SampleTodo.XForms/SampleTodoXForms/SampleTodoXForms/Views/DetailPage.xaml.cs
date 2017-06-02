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
            this.BindingContext = _item = item.Copy();
            this._saved = saved;
            this._item_org = item;
        }

        ToDo _item, _item_org;
        Action _saved;

        /// <summary>
        /// 保存ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // 元の画面を表示
            await this.Navigation.PopAsync();
            if ( _item.Id == 0 )
            {
                // 項目を追加
                MessagingCenter.Send(this, "AddItem", _item);
            }
            else
            {
                // メイン画面から渡されたデータを更新する
                _item.Copy(_item_org);
                // 既存項目の更新
                MessagingCenter.Send(this, "UpdateItem", _item_org);
            }
        }
        /// <summary>
        /// 戻るボタンを押したとき
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // 保存せず前の画面に戻る
            return base.OnBackButtonPressed();
        }
    }
}
