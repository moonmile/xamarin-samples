using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SampleTodo.Droid.Models;

namespace SampleTodo.Droid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        ToDo item;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Detail);

            // データを受け取る
            var data = Intent.GetStringExtra("data");
            item = Newtonsoft.Json.JsonConvert.DeserializeObject<ToDo>(data);

            // 画面に設定する
            var textid = FindViewById<TextView>(Resource.Id.textId);
            textid.Text = item.Id.ToString();
            var editText = FindViewById<EditText>(Resource.Id.editText);
            editText.Text = item.Text;
            var editDue = FindViewById<EditText>(Resource.Id.editDue);
            var swDue = FindViewById<Switch>(Resource.Id.swDue);
            swDue.Checked = item.DueDate != null;
            editDue.Text = item.StrDueDate;
            editDue.Visibility = item.DueDate == null ? ViewStates.Gone : ViewStates.Visible;
            swDue.Click += SwDue_Click; 


            var swCompleted = FindViewById<Switch>(Resource.Id.swCompleted);
            swCompleted.Checked = item.Completed;
            var textCreateAt = FindViewById<TextView>(Resource.Id.textCreateAt);
            textCreateAt.Text = item.CreatedAt.ToString("yyyy-MM-dd hh:mm");

            // 期日をクリックしたときにカレンダーを表示
            editDue.Click += EditDue_Click;
            var btnSave = FindViewById<Button>(Resource.Id.buttonSave);
            btnSave.Click += BtnSave_Click;
        }

        /// <summary>
        /// 保存ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 画面からデータを取り込む
            var editText = FindViewById<EditText>(Resource.Id.editText);
            item.Text = editText.Text ;
            var editDue = FindViewById<EditText>(Resource.Id.editDue);
            var swDue = FindViewById<Switch>(Resource.Id.swDue);
            if ( swDue.Checked == true )
            {
                // 期日指定あり
                item.DueDate = DateTime.Parse(editDue.Text);
            }
            else
            {
                // 期日指定なし
                item.DueDate = null;
            }
            var swCompleted = FindViewById<Switch>(Resource.Id.swCompleted);
            item.Completed = swCompleted.Checked;

            var intent = new Intent();
            // 結果をシリアライズして渡す
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            intent.PutExtra("data", data);
            SetResult(Result.Ok, intent);
            Finish();
        }

        /// <summary>
        /// 戻るボタンをタップ
        /// </summary>
        public override void OnBackPressed()
        {
            SetResult(Result.Canceled);
            Finish();
        }

        /// <summary>
        /// 期日の表示/非表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwDue_Click(object sender, EventArgs e)
        {
            var swDue = FindViewById<Switch>(Resource.Id.swDue);
            var editDue = FindViewById<EditText>(Resource.Id.editDue);
            editDue.Visibility = swDue.Checked == false ? ViewStates.Gone : ViewStates.Visible;
            if ( item.DueDate == null )
            {
                item.DueDate = DateTime.Now;
                editDue.Text = item.StrDueDate;
            }
        }

        private void EditDue_Click(object sender, EventArgs e)
        {
            var tr = FragmentManager.BeginTransaction();
            var picker = new DatePickerDialogFragment();
            picker.Date = item.DueDate.Value;
            picker.OnOk = dt =>
            {
                item.DueDate = dt;
                var editDue = FindViewById<EditText>(Resource.Id.editDue);
                editDue.Text = item.StrDueDate;
            };
            picker.Show(FragmentManager, "datePicker");
        }

        class DatePickerDialogFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
        {
            public Action<DateTime> OnOk;
            public DateTime Date;

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                var picker = new DatePickerDialog(Activity, this, Date.Year, Date.Month, Date.Day);
                return picker;
            }

            /// <summary>
            /// 日付が選択されたときの処理
            /// </summary>
            /// <param name="view"></param>
            /// <param name="year"></param>
            /// <param name="monthOfYear"></param>
            /// <param name="dayOfMonth"></param>
            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                if ( this.OnOk != null )
                {
                    this.Date = new DateTime(year, monthOfYear, dayOfMonth);
                    OnOk(Date);
                }
            }
        }
    }


}