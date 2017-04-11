using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CoreGraphics;
using SampleTodo.iOS.Models;

namespace SampleTodo.iOS
{
    public partial class MasterViewController : UITableViewController
    {
        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            // NavigationItem.LeftBarButtonItem = EditButtonItem;

            var lst = new List<ToDo>();
            lst.Add(new ToDo() { Id = 1, Text = "item no.1", DueDate = new DateTime(2017, 5, 1), CreatedAt = new DateTime(2017, 3, 1) });
            lst.Add(new ToDo() { Id = 2, Text = "item no.2", DueDate = new DateTime(2017, 5, 3), CreatedAt = new DateTime(2017, 3, 2) });
            lst.Add(new ToDo() { Id = 3, Text = "item no.3", DueDate = new DateTime(2017, 5, 2), CreatedAt = new DateTime(2017, 3, 3) });
            lst.Add(new ToDo() { Id = 4, Text = "item no.4", DueDate = null, CreatedAt = new DateTime(2017, 3, 4) });
            items = new ToDoFiltableCollection(lst);

            TableView.Source = new DataSource(this, items);
        }

        // 表示するデータ
        ToDoFiltableCollection items;

        // 設定
        Setting appSetting = new Setting()
        {
            DispCompleted = true,
            SortOrder = 0,              // 作成日順
        };

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		/// <summary>
		/// セグエの実行時
		/// </summary>
		/// <param name="segue"></param>
		/// <param name="sender"></param>
		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			/// 項目を選択したとき
			if (segue.Identifier == "showDetail")
			{
				var indexPath = TableView.IndexPathForSelectedRow;
				var item = items[indexPath.Row];
				((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
			}
			else if (segue.Identifier == "showDetailForAdd")
			{
				var item = new ToDo()
				{
					Id = items.Count + 1,
					Text = "New ToDo",
					DueDate = null,         // 期限なし
					Completed = false,
					CreatedAt = DateTime.Now
				};
				((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
			}
		}

		[Action("UnwindToMasterView:")]
		public void UnwindToMasterView(UIStoryboardSegue segue)
		{
			
		}

        /// <summary>
        /// 新規ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void AddNewItem(object sender, EventArgs args)
        {

            // ビューを更新
            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
        }

        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("Cell");
            ToDoFiltableCollection items;
            readonly MasterViewController controller;

            public DataSource(MasterViewController controller, ToDoFiltableCollection items)
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
				cell.TextLabel.Text = items[indexPath.Row].Text;
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
    }
}

