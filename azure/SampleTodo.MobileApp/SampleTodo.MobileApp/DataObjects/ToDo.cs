using Microsoft.Azure.Mobile.Server;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleTodo.MobileApp.DataObjects
{
    [Table("ToDo")]
    public class ToDo : EntityData
    {
        public string Text { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public bool Completed { get; set; }
    }
}