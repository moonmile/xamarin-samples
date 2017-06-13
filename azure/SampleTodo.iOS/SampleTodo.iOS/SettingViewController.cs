
using System;
using System.Collections.Generic;
using System.Drawing;

using Foundation;
using UIKit;
using SampleTodoXForms.Models;

namespace SampleTodo.iOS
{
	public partial class SettingViewController : UIViewController
	{
		public Setting AppSetting { get; set; }

		public SettingViewController(IntPtr handle) : base(handle)
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.


			// 表示順をクリックした時にピッカーを表示
			var lst = new List<string>();
			lst.Add("作成日順");
			lst.Add("項目名順");
			lst.Add("期日順");
			var vm = new OrderViewModel(lst);
			var pkOrder = new UIPickerView();
			pkOrder.Model = vm;
			// キーボードとしてPicker を指定する
			textOrder.InputView = pkOrder;
			// Picker の変更時にラベルの表示を変える
			vm.ValueChanged += (s, e) => {
				int index = vm.SelectedIndex;
				textOrder.Text = lst[index];
				AppSetting.SortOrder = index;
			};

			swDispCompleted.ValueChanged += (sender, e) => {
				AppSetting.DispCompleted = swDispCompleted.On;
			};

			// 画面に初期値を設定
			this.swDispCompleted.On = AppSetting.DispCompleted;
			this.textOrder.Text = lst[AppSetting.SortOrder];


			// 後からジェスチャーを追加する
			var tap = new UITapGestureRecognizer(() =>
			{
				textOrder.ResignFirstResponder();
			});
			this.View.AddGestureRecognizer(tap);

		}

        class OrderViewModel : UIPickerViewModel
		{
			private List<string> _myItems;
			protected int selectedIndex = 0;
			public Action<object, EventArgs> ValueChanged;
			public int SelectedIndex
			{
				get
				{
					return selectedIndex;
				}
			}

			public OrderViewModel(List<string> items)
			{
				_myItems = items;
			}

			public string SelectedItem
			{
				get { return _myItems[selectedIndex]; }
			}

			public override nint GetComponentCount(UIPickerView picker)
			{
				return 1;
			}

			public override nint GetRowsInComponent(UIPickerView picker, nint component)
			{
				return _myItems.Count;
			}

			public override string GetTitle(UIPickerView picker, nint row, nint component)
			{
				return _myItems[(int)row];
			}

			public override void Selected(UIPickerView picker, nint row, nint component)
			{
				selectedIndex = (int)row;
				if (ValueChanged != null)
				{
					this.ValueChanged(this, new EventArgs());
				}
			}
		}

	}
}