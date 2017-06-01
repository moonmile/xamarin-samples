using System;
using System.IO;
using SampleTodoXForms.Views;

namespace SampleTodoXForms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new SampleTodoXForms.App());
        }
    }
}