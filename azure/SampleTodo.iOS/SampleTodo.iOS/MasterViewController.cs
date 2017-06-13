using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CoreGraphics;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Linq;
using SampleTodoXForms.Models;

namespace SampleTodo.iOS
{
    public partial class MasterViewController : UITableViewController
    {
        // Azure Mobile Service に接続する
        private MobileServiceClient client;
        private IMobileServiceTable<ToDo> todoTable;
        // URL of the mobile app backend.
        const string applicationURL = @"https://sampletodomobileapp.azurewebsites.net";

        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            // Azure Mobile Service を使う
            client = new MobileServiceClient(applicationURL);
            // ToDo テーブルを更新対象にする
            todoTable = client.GetTable<ToDo>();
            items = new List<ToDo>();
            TableView.Source = new DataSource(this, items);
            await RefreshItemsFromTableAsync();
        }

        /// <summary>
        /// Azure Mobile Service に接続して ToDo データを取得する
        /// </summary>
        /// <returns></returns>
        private async Task RefreshItemsFromTableAsync()
        {
            try
            {
                List<ToDo> list;
                if (setting.DispCompleted == true)
                {
                    list = await todoTable.ToListAsync();
                }
                else
                {
                    // 未完了の項目のみ取得する
                    list = await todoTable.Where(x => x.Completed == false).ToListAsync();
                }
                // 表示順を変える
                switch (setting.SortOrder)
                {
                    case 0: // 作成日順/ID順
                        list = list.OrderByDescending(x => x.CreatedAt).ToList();
                        break;
                    case 1: // 項目名順
                        list = list.OrderBy(x => x.Text).ToList();
                        break;
                    case 2: // 期日順
                        list = list.OrderBy(x => x.DueDate).ToList();
                        break;
                }

                items.Clear();
                foreach (var it in list)
                {
                    items.Add(it);
                }
                // 表示を更新する
                TableView.ReloadData();
            }
            catch (Exception ex)
            {

            }
        }

        // 表示するデータ
        List<ToDo> items;

        // 設定
        Setting setting = new Setting()
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
                var item = ToDo.CreateNew();
                ((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
			}
			else if (segue.Identifier == "showSetting")
			{
				((SettingViewController)segue.DestinationViewController).AppSetting = setting;
			}
		}

		[Action("UnwindToMasterView:")]
		public async void UnwindToMasterView(UIStoryboardSegue segue)
		{
			if (segue.Identifier == "unwindFromDetail")
			{
				Console.WriteLine("UnwindToMasterView in Master from Detail");
				// 詳細画面から戻る場合
				var vc = segue.SourceViewController as DetailViewController;
				if (vc != null)
				{
                    await UpdateItem(vc.Item);
                }
            }
			if (segue.Identifier == "unwindFromSetting")
			{
				Console.WriteLine("UnwindToMasterView in Master from Setting");
				// 設定画面から戻る場合
				var vc = segue.SourceViewController as SettingViewController;
				if (vc != null)
				{
					this.setting = vc.AppSetting;
                    // 表示を更新する
                    await RefreshItemsFromTableAsync();
                }
            }
		}
        public async Task UpdateItem(ToDo item)
        {
            if (item.Id == "")
            {
                // 新規作成の場合
                await todoTable.InsertAsync(item);
            }
            else
            {
                // 更新の場合
                await todoTable.UpdateAsync(item);
            }
            // 表示を更新する
            await RefreshItemsFromTableAsync();
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
				// var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
				var cell = tableView.DequeueReusableCell(CellIdentifier);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);
				}
				var item = items[indexPath.Row];
				cell.TextLabel.Text = item.StrDueDate;
				cell.DetailTextLabel.Text = item.Text;
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

