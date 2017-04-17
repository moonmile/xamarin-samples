using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(SampleTodoXForms.Droid.ToDoStorage))]

namespace SampleTodoXForms.Droid
{
    public class ToDoStorage : SampleTodoXForms.Views.IToDoStorage
    {
        public Stream OpenReader(string file)
        {
            var docs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = System.IO.Path.Combine(docs, file);
            if (System.IO.File.Exists(path))
            {
                return System.IO.File.OpenRead(path);
            }
            else
            {
                return null;
            }
        }

        public Stream OpenWriter(string file)
        {
            var docs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = System.IO.Path.Combine(docs, file);
            return System.IO.File.OpenWrite(path);
        }
    }
}