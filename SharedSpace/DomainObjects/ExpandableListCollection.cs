using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SharedSpace.DomainObjects
{
	public class ExpandableListCollection : List<ExpandableListItem>, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
