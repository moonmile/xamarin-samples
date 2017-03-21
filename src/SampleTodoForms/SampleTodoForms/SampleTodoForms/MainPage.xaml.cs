using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleTodoForms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            items = new List<ToDo>();
            items.Add(new ToDo() { Id = 1, Text = "item no.1" });
            items.Add(new ToDo() { Id = 2, Text = "item no.2" });
            items.Add(new ToDo() { Id = 3, Text = "item no.3" });

            this.listview.ItemsSource = items;

        }


        List<ToDo> items;

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as ToDo;
            Navigation.PushAsync(new DetailPage() { Item = item });
        }

        private void add_Activated(object sender, EventArgs e)
        {
            var item = new ToDo();
            Navigation.PushAsync(new DetailPage());
        }

        private void tappedSetting(object sender, EventArgs e)
        {
            var item = new ToDo();
            Navigation.PushAsync(new SettingPage());
        }

        private void tappedNew(object sender, EventArgs e)
        {
            var item = new ToDo();
            Navigation.PushAsync(new DetailPage() { Item = item });


        }
    }
    public class ToDo
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
