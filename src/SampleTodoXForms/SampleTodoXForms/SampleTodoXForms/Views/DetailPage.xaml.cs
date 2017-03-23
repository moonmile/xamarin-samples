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
        public DetailPage(ToDo item)
        {
            InitializeComponent();
            this.BindingContext = _item = item;
        }

        private ToDo _item;
        public ToDo Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                this.BindingContext = _item;
            }
        }

        /// <summary>
        /// 保存ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Save_Clicked(object sender, EventArgs e)
        {
            //
        }

    }
}
