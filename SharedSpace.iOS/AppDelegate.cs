using Foundation;
using SharedSpace.iOS.Lib;
using UIKit;

namespace SharedSpace.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public static double DeviceWidth = UIScreen.MainScreen.Bounds.Width * 2;
		public static double DeviceHeight = UIScreen.MainScreen.Bounds.Height * 2;

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Xamarin.Forms.Forms.Init();
			// We only need a reference to SharedSpace.Droid.Lib.ExpandableListViewRenderer. 
			// The following line ensures that we add a reference to ExpandableListViewRenderer. 
			ExpandableListViewRenderer.Init();
			LoadApplication(new App());
			return base.FinishedLaunching(application, launchOptions);
		}
	}
}


