using SampleTodo.iOS.Models;
using System;

using UIKit;

namespace SampleTodo.iOS
{
	public partial class DetailViewController : UIViewController
	{

		public ToDo DetailItem { get; set; }

		protected DetailViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		ToDo item;

		/// <summary>
		/// Sets the detail item.
		/// </summary>
		/// <param name="item">New detail item.</param>
		public void SetDetailItem(ToDo item)
		{
			// データを受け取る
			this.item = item;
			// データを画面に設定する
			this.textId.Text = item.Id.ToString();
			this.textText.Text = item.Text;
			this.swDue.On = item.DueDate != null;
			this.textDue.Text = item.StrDueDate;
			this.textDue.Hidden = item.DueDate == null;
			this.swCompleted.On = item.Completed;
			this.textCreateAt.Text = item.CreatedAt.ToString("yyyy-MM-dd hh:mm");

			// 期日の表示切り替え
			// 期日をクリックした時にカレンダー表示

		}

		void ConfigureView()
		{
			/*
            if (label1 == null) return;

            // Update the user interface for the detail item
            label1.Text = this.DetailItem.Id.ToString();
            text1.Text = this.DetailItem.Text;
			*/
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			ConfigureView();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
		/// <summary>
		/// 保存ボタンをタップする
		/// </summary>
		/// <param name="sender">Sender.</param>
		partial void BtnSave_Activated(UIBarButtonItem sender)
		{
			// 画面からデータを取り込み
			item.Text = this.textText.Text = item.Text;
			if (this.swDue.On == true)
			{
				item.DueDate = DateTime.Parse(this.textDue.Text);
			}
			else
			{
				item.DueDate = null;
			}
			item.Completed = this.swCompleted.On;
			// 前の画面に戻る
			this.NavigationController.PopViewController(true);
		}
	}
}

