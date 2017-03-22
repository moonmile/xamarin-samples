using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CoreGraphics;

namespace SampleTodo.iOS
{
    public partial class MasterViewController : UITableViewController
    {
        List<ToDo> items;
        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            // NavigationItem.LeftBarButtonItem = EditButtonItem;

            items = new List<ToDo>();
            items.Add(new ToDo() { Id = 1, Text = "item no.1" });
            items.Add(new ToDo() { Id = 2, Text = "item no.2" });
            items.Add(new ToDo() { Id = 3, Text = "item no.3" });

            TableView.Source = new DataSource(this, items);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void AddNewItem(object sender, EventArgs args)
        {
            var item = new ToDo() { Id = items.Count + 1, Text = DateTime.Now.ToString() };
            items.Insert(0, item);

            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = items[indexPath.Row];

                ((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
            }
        }

        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("Cell");
            List<ToDo> items;
            readonly MasterViewController controller;

            public DataSource(MasterViewController controller, List<ToDo> items)
            {
                this.controller = controller;
                this.items = items;
            }
            // Customize the number of sections in the table view.
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }
            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return items.Count;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
                cell.TextLabel.Text = items[indexPath.Row].ToString();
                return cell;
            }

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return true;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            {
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    // Delete the row from the data source.
                    items.RemoveAt(indexPath.Row);
                    controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            }
        }
        /// <summary>
        /// 追加ボタンをタップする
        /// </summary>
        /// <param name="sender"></param>
        partial void UIBarButtonItem106_Activated(UIBarButtonItem sender)
        {
            AddNewItem(sender, null);
        }
    }
    public class ToDo
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
}

