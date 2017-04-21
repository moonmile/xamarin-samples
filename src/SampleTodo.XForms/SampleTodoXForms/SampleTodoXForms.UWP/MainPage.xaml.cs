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


    public class ToDoStorage : SampleTodoXForms.Views.IToDoStorage
    {

        private string xml = "";


        public Stream OpenReader(string file)
        {
            if (xml == "")
            {
                return null;
            }
            else
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                var st = new MemoryStream(data);
                return st;
            }
        }


        public Stream OpenWriter(string file)
        {
            var mem = new MemoryStream();
            return mem;
        }
    }

}