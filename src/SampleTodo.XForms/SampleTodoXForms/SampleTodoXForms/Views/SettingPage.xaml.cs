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
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }
        public SettingPage(Setting item, Action callback = null )
        {
            InitializeComponent();
            this.BindingContext = _item = item;

            pickOrder.Items.Add("作成順");
            pickOrder.Items.Add("項目名順");
            pickOrder.Items.Add("期日順");
            pickOrder.SelectedIndex = _item.SortOrder;
            // コールバックを設定
            _callback = callback;

        }
        Setting _item;
        Action _callback;

        /// <summary>
        /// 前の画面に戻る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            /*
            if ( _callback != null )
            {
                // コールバックの呼び出し
                _callback();
            }
            */
            MessagingCenter.Send(this, "UpdateSetting");
        }
        /// <summary>
        /// 戻るボタンをタップ
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // 戻るボタンでも反映する
            /*
            if (_callback != null)
            {
                // コールバックの呼び出し
                _callback();
            }
            */
            MessagingCenter.Send(this, "UpdateSetting");
            return base.OnBackButtonPressed();
        }
        /// <summary>
        /// 表示順の選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pickOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( pickOrder.SelectedIndex != -1 )
            {
                _item.SortOrder = pickOrder.SelectedIndex;
            }
        }
    }
}
