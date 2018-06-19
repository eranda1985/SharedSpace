using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using SharedSpace;
using Xamarin.Forms;

namespace SharedSpace.Droid
{
	[Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Forms.Init(this, savedInstanceState);
			
			LoadApplication(new App());
		}

		protected override void OnResume()
		{
			base.OnResume();
		}
	}
}

