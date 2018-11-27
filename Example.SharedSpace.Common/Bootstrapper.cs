using Example.SharedSpace.Common.Features;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Example.SharedSpace.Common
{
    public class Bootstrapper
    {
		public static Page CreateMainPage()
		{
            //var page = FreshPageModelResolver.ResolvePageModel<MediatorPageModel>();
            var welcomePage = FreshPageModelResolver.ResolvePageModel<WelcomePageModel>();

            var stack = new FreshNavigationContainer(welcomePage, "Welcome");

            return stack;
		}
	}
}
