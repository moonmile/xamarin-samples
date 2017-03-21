using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SampleTodoForms
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();

            this.BindingContext = this.Item;
        }

        private ToDo _item;
        public ToDo Item {
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
    }
}
