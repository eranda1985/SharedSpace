using FreshMvvm;
using SharedSpace.Features;
using Xamarin.Forms;

namespace SharedSpace
{
	public class Bootstrapper
	{
		public Bootstrapper()
		{

		}

		public static Page CreateMainPage()
		{
			var page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
			return page;
		}
	}
}
