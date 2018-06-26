using SharedSpace.DomainObjects;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Example.SharedSpace.Common.Features
{
	public class MainPageModel : FreshMvvm.FreshBasePageModel
	{
		private object _initData = null;
		private ExpandableListCollection _listItems = null;

		public ExpandableListCollection ListItems { get => _listItems; set { _listItems = value; RaisePropertyChanged("ListItems"); } }

		public string GroupBackColor { get; set; } = "#FF4081";

		public Command ItemSelected => new Command(async (selectedItem) =>
		{
			// Describe your logic here when hit on a List item
			var item = selectedItem as ExpandableListItem;
			await App.Current.MainPage.DisplayAlert("You selected an item", item.Name, "Cancel");
		});

		public MainPageModel()
		{
			_listItems = new ExpandableListCollection();
		}

		public override void Init(object initData)
		{
			base.Init(initData);
			_initData = initData;
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);
			LoadData();
		}

		private void LoadData()
		{
			ListItems.Clear();
			ListItems = GetMockRecordings();
		}

		private ExpandableListCollection GetMockRecordings()
		{
			var temp =  new List<ExpandableListItem>
			{
				new ExpandableListItem
				{
					Name = "Group 1",
					ChildItems = new List<ExpandableListItem>
					{
						new ExpandableListItem
						{
							Name = "Group1-Child 1", Description = "Some description goes here"
						}
					}
				},
				new ExpandableListItem
				{
					Name = "Group 2",
					ChildItems = new List<ExpandableListItem>
					{
						new ExpandableListItem
						{
							Name = "Group2-Child 1", Description = "Some description goes here"
						},
						new ExpandableListItem
						{
							Name = "Group2-Child 2", Description = "Some description goes here"
						}
					}
				},
				new ExpandableListItem
				{
					Name = "Group 3",
					ChildItems = new List<ExpandableListItem>
					{
						new ExpandableListItem
						{
							Name = "Group3-Child 1", Description = "Some description goes here"
						},
						new ExpandableListItem
						{
							Name = "Group3-Child 2", Description = "Some description goes here"
						}
					}
				}
			};

			var res = new ExpandableListCollection();
			res.AddRange(temp);
			return res;
		}
	}
}
