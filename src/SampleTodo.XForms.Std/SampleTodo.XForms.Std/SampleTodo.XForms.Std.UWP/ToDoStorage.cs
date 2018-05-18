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
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var t = folder.OpenStreamForReadAsync(file);
            try
            {
                t.Wait();
                return t.Result;
            }
            catch
            {
                return null;
            }

        }
        public Stream OpenWriter(string file)
        {
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var t = folder.OpenStreamForWriteAsync(file, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            t.Wait();
            var st = t.Result;
            return st;
        }
    }
}
