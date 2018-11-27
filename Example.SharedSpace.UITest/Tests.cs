using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Example.SharedSpace.UITest
{
	[TestFixture(Platform.Android)]
	public class Tests
	{
		IApp app;
		Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

		[Test]
		public void WelcomeTextIsDisplayed()
		{
			// Tap on Group 1
			AppResult[] results = app.WaitForElement(c => c.Marked("sharedSpaceDroidGroupGroup 1"));
			Assert.IsTrue(results.Any());
			app.Tap("sharedSpaceDroidGroupGroup 1");

			// Tap on Group 2
			results = app.WaitForElement(c => c.Marked("sharedSpaceDroidGroupGroup 2"));
			Assert.IsTrue(results.Any());
			app.Tap("sharedSpaceDroidGroupGroup 2");

			// Tap on Group 3
			results = app.WaitForElement(c => c.Marked("sharedSpaceDroidGroupGroup 3"));
			Assert.IsTrue(results.Any());
			app.Tap("sharedSpaceDroidGroupGroup 3");

			// Tap on Child item
			results = app.WaitForElement(c => c.Marked("sharedSpaceDroidGroup3-Child 1"));
			Assert.IsTrue(results.Any());
			app.Tap("sharedSpaceDroidGroup3-Child 1");
		}
	}
}
