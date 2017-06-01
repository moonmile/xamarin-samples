using SampleTodoXForms.Models;
using System;
using UIKit;
using Foundation;

namespace SampleTodo.iOS
{
	public partial class DetailViewController : UIViewController
	{


		protected DetailViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		// 編集時のデータ
		ToDo item;
		// 前画面に返すため
		public ToDo Item
		{
			get
			{
				return item;
			}
		}

		/// <summary>
		/// Sets the detail item.
		/// </summary>
		/// <param name="item">New detail item.</param>
		public void SetDetailItem(ToDo item)
		{
			// データを受け取る。キャンセルを可能にするためコピーを作る
			this.item = item.Copy();
		}

		void ConfigureView()
		{
			// データを画面に設定する
			this.textId.Text = item.Id.ToString();
			this.textText.Text = item.Text;
			this.swDue.On = item.DueDate != null;
			this.textDue.Text = item.StrDueDate;
			this.textDue.Hidden = item.DueDate == null;
			this.swCompleted.On = item.Completed;
			this.textCreateAt.Text = item.CreatedAt.ToString("yyyy-MM-dd hh:mm");

			// 期日の表示切り替え
			this.swDue.ValueChanged += SwDue_ValueChanged;

			// 期日をクリックした時にカレンダー表示
			var pkDue = new UIDatePicker();
			// キーボードとして DatePicker を指定する
			pkDue.Mode = UIDatePickerMode.Date;
			if (item.DueDate != null)
			{
				// 初期値を設定しておく
				var baseDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
				pkDue.Date = NSDate.FromTimeIntervalSinceReferenceDate((item.DueDate.Value - baseDate).TotalSeconds);
			}
			this.textDue.InputView = pkDue;
			// DatePicker 変更時にラベルの表示を変える
			pkDue.ValueChanged += (sender, e) =>
			{
				// NSDate から DateTime へ変換する
				var baseDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
				item.DueDate = baseDate.AddSeconds(pkDue.Date.SecondsSinceReferenceDate);
				this.textDue.Text = item.StrDueDate;
			};
			// storyboard でジェスチャーを追加する場合は、AddTarget で追加する。
			this.tapGesture.AddTarget(() =>
			{
				this.textDue.ResignFirstResponder();
			});
		}

		/// <summary>
		/// 期日の表示/非表示
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void SwDue_ValueChanged(object sender, EventArgs e)
		{
			if (this.swDue.On == true)
			{
				this.textDue.Hidden = false;
				if (item.DueDate == null) item.DueDate = DateTime.Now;
				this.textDue.Text = item.StrDueDate;
			}
			else
			{
				this.textDue.Hidden = true;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			// 表示時に画面を構成する
			ConfigureView();
		}

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
			if (segue.Identifier == "unwindFromDetail")
			{
				// 画面からデータを取り込み
				item.Text = this.textText.Text;
				if (this.swDue.On == true)
				{
					item.DueDate = DateTime.Parse(this.textDue.Text);
				}
				else
				{
					item.DueDate = null;
				}
				item.Completed = this.swCompleted.On;
			}
		}
	}
}

