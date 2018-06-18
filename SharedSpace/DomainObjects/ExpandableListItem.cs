using System;
using System.Collections.Generic;
using System.Text;

namespace SharedSpace.DomainObjects
{
	public class ExpandableListItem
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public string ViewerLink { get; set; }

		public bool IsExpanded { get; set; }

		public bool HasItemClickAssigned { get; set; } = false;

		public List<ExpandableListItem> ChildItems { get; set; }

		public event HandleClick ItemClicked;

		public delegate void HandleClick(object sender, EventArgs e);

		public HandleClick HandleClickCallBack => (o, e) => { ItemClicked(o, e); };
	}
}
