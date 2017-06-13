using SampleTodoXForms.Models;
using SampleTodoXForms.ViewModels;
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
        public SettingPage(Setting item)
        {
            InitializeComponent();
            viewModel = new SettingViewModel(item);
            BindingContext = viewModel;
        }
        SettingViewModel viewModel;

        /// <summary>
        /// 画面を非表示にするとき
        /// </summary>
        protected override void OnDisappearing()
        {
            MessagingCenter.Send(this, "UpdateSetting");
            base.OnDisappearing();
        }
    }
}
