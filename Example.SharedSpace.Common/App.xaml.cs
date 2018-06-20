using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.SharedSpace.Common
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent ();
			MainPage = Bootstrapper.CreateMainPage();
		}
	}
}