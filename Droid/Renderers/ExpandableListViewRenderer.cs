
using System.ComponentModel;
using Android.App;
using Android.Widget;
using SharedSpace.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MultiLevelListView), typeof(SharedSpace.Droid.Renderers.ExpandableListViewRenderer))]

namespace SharedSpace.Droid.Renderers
{
	public class ExpandableListViewRenderer : ViewRenderer<MultiLevelListView, ExpandableListView>,
																						ExpandableListView.IOnGroupExpandListener,
																						ExpandableListView.IOnGroupCollapseListener,
																						ExpandableListView.IOnChildClickListener
	{
		private int _lastExpandedGroup = -1;

		public ExpandableListViewRenderer(Android.Content.Context context): base(context)
		{

		}

		public bool OnChildClick(ExpandableListView parent, Android.Views.View clickedView, int groupPosition, int childPosition, long id)
		{
			var adaptor = Control.ExpandableListAdapter as ExpandableListViewDataAdaptor;
			var dataItem = adaptor.DataList[groupPosition].ChildItems[childPosition];
			if (!dataItem.HasItemClickAssigned)
			{
				dataItem.ItemClicked += DataItem_ItemClicked;
			}
			
			dataItem.HasItemClickAssigned = true;
			dataItem.HandleClickCallBack.Invoke(parent, new ExpandableListViewEventArgs("ChildSelectedCommand") { GroupPosition = groupPosition, ChildPosition = childPosition });
			return false;
		}

		private void DataItem_ItemClicked(object sender, System.EventArgs e)
		{
			OnElementPropertyChanged(sender, (ExpandableListViewEventArgs)e);
		}

		public void OnGroupCollapse(int groupPosition)
		{
			// Grab the Group object
			var groupView = Control.GetChildAt(groupPosition);

			// If it's not a Group object continue until we find one.
			while (groupView?.Id == Resource.Id.layoutChild)
			{
				groupView = Control.GetChildAt(++groupPosition);
				if (groupView == null)
				{
					break;
				}
			}

			// Set the group indicator to arrow down
			if (groupView?.Id == Resource.Id.groupView)
			{
				var imgView = groupView.FindViewById<ImageView>(Resource.Id.imgView) ?? new ImageView(context: Context);
				imgView.SetImageResource(Resource.Mipmap.arrow_down_darkgrey);
			}
		}

		public void OnGroupExpand(int groupPosition)
		{
			if(Control != null)
			{
				if(_lastExpandedGroup == -1)
				{
					_lastExpandedGroup = groupPosition;
				}

				else if(_lastExpandedGroup != groupPosition)
				{
					Control.CollapseGroup(_lastExpandedGroup);
					_lastExpandedGroup = groupPosition;
				}

				// Grab the Group object
				var groupView = Control.GetChildAt(groupPosition);

				// If it's not a Group object continue until we find one.
				while(groupView?.Id == Resource.Id.layoutChild)
				{
					groupView = Control.GetChildAt(++groupPosition);
					if(groupView == null)
					{
						break;
					}
				}

				// Set the group indicator to arrow up
				if (groupView?.Id == Resource.Id.groupView)
				{
					var imgView = groupView.FindViewById<ImageView>(Resource.Id.imgView) ?? new ImageView(Context);
					imgView.SetImageResource(Resource.Mipmap.arrow_up_darkgrey);
				}
			}
		}

		protected override void OnElementChanged(ElementChangedEventArgs<MultiLevelListView> e)
		{
			base.OnElementChanged(e);

			var control = new ExpandableListView(this.Context);
			if (e.NewElement != null)
			{
				control.SetAdapter(new ExpandableListViewDataAdaptor(this.Context as Activity, e.NewElement.Items));
				control.SetGroupIndicator(null);
				control.SetOnGroupExpandListener(this);
				control.SetOnGroupCollapseListener(this);
				control.SetOnChildClickListener(this);
			}

			SetNativeControl(control);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if(e.PropertyName == MultiLevelListView.ItemsProperty.PropertyName)
			{
				Control.SetAdapter(new ExpandableListViewDataAdaptor(this.Context as Activity, Element.Items));
			}
			if(e.PropertyName == MultiLevelListView.ChildSelectedCommandProperty.PropertyName)
			{
				var selectedItem = Element.Items[((ExpandableListViewEventArgs)e).GroupPosition].ChildItems[((ExpandableListViewEventArgs)e).ChildPosition];
				Element.ChildSelectedCommand?.Execute(selectedItem);
				
			}
		}


	}

	class ExpandableListViewEventArgs : PropertyChangedEventArgs
	{
		public ExpandableListViewEventArgs(string propertyName) : base(propertyName)
		{
		}

		public int GroupPosition { get; set; }
		public int ChildPosition { get; set; }
	}
}