using System.ComponentModel;

namespace SharedSpace.iOS.Lib
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