using System.ComponentModel;
using CoreGraphics;
using SharedSpace.CustomControls;
using SharedSpace.iOS.NativeServices;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MultiLevelListView), typeof(SharedSpace.iOS.Renderers.ExpandableListViewiOSRenderer))]
namespace SharedSpace.iOS.Renderers
{
	/// <summary>
	/// Custom renderer for expandable list view. 
	/// </summary>
	public class ExpandableListViewiOSRenderer : ViewRenderer<MultiLevelListView, UITableView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<MultiLevelListView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				SetNativeControl(new UITableView
				{
					BackgroundColor = UIColor.FromRGB(243, 239, 236),
					RowHeight = 100,
					AutoresizingMask = UIViewAutoresizing.All,
					SeparatorStyle = UITableViewCellSeparatorStyle.None,
					Bounces = true,
					BouncesZoom = true,
					ScrollEnabled = true,
					SectionFooterHeight = 0,
					SectionHeaderHeight = 70,
					TableHeaderView = new UIView(new CGRect(0, 0, 100, 70)),
					ContentInset = new UIEdgeInsets(-70, 0, 0, 0)
				});
			}
			if (e.NewElement != null)
			{
				var dataSource = new ExpandableListDataSource(e.NewElement);
				Control.Source = dataSource;
			}

		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == MultiLevelListView.ItemsProperty.PropertyName)
			{
				var dataSource = new ExpandableListDataSource(Element, this);
				Control.Source = dataSource;
				Control.ReloadData();
				SetNeedsDisplay();
			}

			if(e.PropertyName == MultiLevelListView.ChildSelectedCommandProperty.PropertyName)
			{
				var eventArgs = e as ExpandableListViewEventArgs;
				var dataItem = Element.Items[eventArgs.GroupPosition].ChildItems[eventArgs.ChildPosition];
				Element.ChildSelectedCommand.Execute(dataItem);
			}
		}

		public void PropertyChangedCallBack(object sender, PropertyChangedEventArgs e)
		{
			this.OnElementPropertyChanged(sender, e);
		}
	}
}