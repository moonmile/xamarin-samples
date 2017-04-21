using SampleTodoXForms.Helpers;
using SampleTodoXForms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTodoXForms.ViewModels
{
    /// <summary>
    /// MainPageのViewModelクラス
    /// </summary>
    public class MainViewModel : ObservableObject 
    {
        public ObservableCollection<ToDo> Items { get; set; }
    }
}
