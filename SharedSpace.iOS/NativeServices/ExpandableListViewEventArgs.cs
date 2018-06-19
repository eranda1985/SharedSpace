using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace SharedSpace.iOS.NativeServices
{
	public class ExpandableListViewEventArgs : PropertyChangedEventArgs
	{
		public ExpandableListViewEventArgs(string propertyName) : base(propertyName)
		{
		}

		public int GroupPosition { get; set; }
		public int ChildPosition { get; set; }
	}
}