using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SampleTodoXForms.UWP.ToDoStorage))]

namespace SampleTodoXForms.UWP
{
    public class ToDoStorage : SampleTodoXForms.Views.IToDoStorage
    {
        public Stream OpenReader(string file)
        {
            return null;
        }
        public Stream OpenWriter(string file)
        {
            return null;
        }
    }
}
