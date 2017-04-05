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

namespace SampleTodo.Droid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Detail);

            var id = this.Intent.GetIntExtra("id",0);
            var text = this.Intent.GetStringExtra("text");

            var text2 = FindViewById<TextView>(Resource.Id.textId);
            text2.Text = id.ToString();
            var text4 = FindViewById<TextView>(Resource.Id.textText);
            text4.Text = text;
        }
    }
}