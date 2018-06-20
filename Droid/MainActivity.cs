using Android.App;
using Android.OS;
using SharedSpace.Droid.Lib;
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

			// We only need a reference to SharedSpace.Droid.Lib.ExpandableListViewRenderer. 
			// The following line ensures that we add a reference to ExpandableListViewRenderer. 
			ExpandableListViewRenderer.Init();


			LoadApplication(new App());
		}

		protected override void OnResume()
		{
			base.OnResume();
		}
	}
}

