using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using SharedSpace.CustomControls;
using SharedSpace.DomainObjects;
using UIKit;

namespace SharedSpace.iOS.Lib
{
	public class ExpandableListDataSource : UITableViewSource
	{
		private List<ExpandableListItem> _items = new List<ExpandableListItem>();
		private ExpandableListCell _nativeCell = null;
		private int _lastExpandedSection = -1;
		private ExpandableListViewRenderer _expandableListViewiOSRenderer;

		public ExpandableListDataSource(MultiLevelListView expandableListView)
		{
			_items = expandableListView.Items;
		}

		public ExpandableListDataSource(MultiLevelListView expandableListView, ExpandableListViewRenderer expandableListViewiOSRenderer) : this(expandableListView)
		{
			this._expandableListViewiOSRenderer = expandableListViewiOSRenderer;
		}

		/// <summary>
		/// Method to create a child row
		/// </summary>
		/// <param name="tableView"></param>
		/// <param name="indexPath"></param>
		/// <returns></returns>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// Logic to create a child row and set the text. 
			var item = _items[indexPath.Section].ChildItems[indexPath.Row];
			var cell = tableView.DequeueReusableCell(nameof(UITableViewCell));
			if(cell != null)
			{
				return cell;
			}

			_nativeCell = new ExpandableListCell(UITableViewCellStyle.Default, nameof(ExpandableListCell), tableView.Frame)
			{
				Name = item.Name,
				DescriptionText = item.Description
			};
			return _nativeCell;
		}

		/// <summary>
		/// Returns the child list count for a given group
		/// </summary>
		/// <param name="tableview"></param>
		/// <param name="section"></param>
		/// <returns></returns>
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if((int)section != _lastExpandedSection)
			{
				// Allow only one section expanded
				_items[(int)section].IsExpanded = false;
			}
			return _items[(int)section].IsExpanded ? (_items[(int)section].ChildItems != null ? _items[(int)section].ChildItems.Count : 0) : 0;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return (nint)_items.Count;
		}


		/// <summary>
		/// Gets the header height
		/// </summary>
		/// <param name="tableView"></param>
		/// <param name="section"></param>
		/// <returns></returns>
		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 70;/*_nativeCell!=null ? _nativeCell.Height : 70;*/
		}

		/// <summary>
		/// Called upon tapping a child row
		/// </summary>
		/// <param name="tableView"></param>
		/// <param name="indexPath"></param>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var rowItem = _items[indexPath.Section].ChildItems[indexPath.Row];
			if(rowItem != null)
			{
				tableView.ReloadSections(NSIndexSet.FromIndex(indexPath.Section), UITableViewRowAnimation.Fade);
			}
			tableView.DeselectRow(indexPath, true);
			if (!rowItem.HasItemClickAssigned)
			{
				rowItem.ItemClicked += RowItem_ItemClicked;
			}
			rowItem.HasItemClickAssigned = true;
			rowItem.HandleClickCallBack.Invoke(tableView, new ExpandableListViewEventArgs("ChildSelectedCommand") { GroupPosition = indexPath.Section, ChildPosition = indexPath.Row });
		}

		private void RowItem_ItemClicked(object sender, EventArgs e)
		{
			var eventArgs = e as ExpandableListViewEventArgs;
			this._expandableListViewiOSRenderer.PropertyChangedCallBack(sender, eventArgs);
		}

		/// <summary>
		/// Get a custom view for the header 
		/// </summary>
		/// <param name="tableView"></param>
		/// <param name="section"></param>
		/// <returns></returns>
		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var height = 70;/*_nativeCell != null ? _nativeCell.Height : 70;*/
			var fontSize = _nativeCell != null ? _nativeCell.FontSize : 17;
			var groupSection = new UIButton(new CGRect(0, 0, tableView.Frame.Width, height));

			groupSection.TitleEdgeInsets = new UIEdgeInsets(groupSection.TitleEdgeInsets.Top, 20, groupSection.TitleEdgeInsets.Bottom, groupSection.TitleEdgeInsets.Right);
			groupSection.AutoresizingMask = UIViewAutoresizing.All;

			// Set the group image icon as per its state
			UIImageView imgView = groupSection.Subviews.OfType<UIImageView>().FirstOrDefault();
			if (_items[(int)section].IsExpanded)
			{
				if (imgView == null)
				{
					imgView = new UIImageView(UIImage.FromBundle("arrow_up_darkgrey"))
					{
						Frame = new CGRect(groupSection.Frame.Width - 20, 0, 20, height),
						AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin,
						ContentMode = UIViewContentMode.Center
					};
					groupSection.AddSubview(imgView);
				}
				else
				{
					imgView.Image = UIImage.FromBundle("arrow_up_darkgrey");
				}
			}
			else
			{
				if (imgView == null)
				{
					imgView = new UIImageView(UIImage.FromBundle("arrow_down_darkgrey"))
					{
						Frame = new CGRect(groupSection.Frame.Width - 20, 0, 20, height),
						AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin,
						ContentMode = UIViewContentMode.Center
					};
					groupSection.AddSubview(imgView);
				}
				else
				{
					imgView.Image = UIImage.FromBundle("arrow_down_darkgrey");
				}
			}

			var bottomBorder = new UIView(new CGRect(0, height - 1, tableView.Frame.Width, 1))
			{
				BackgroundColor = UIColor.LightGray,
				Alpha = 0.3f,
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth
			};
			groupSection.AddSubview(bottomBorder);

			groupSection.SetTitle(_items[(int)section].Name, UIControlState.Normal);
			groupSection.Font = UIFont.SystemFontOfSize(fontSize);
			groupSection.BackgroundColor = UIColor.FromRGBA(243, 239, 236, 0);
			groupSection.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			groupSection.SetTitleColor(UIColor.DarkGray, UIControlState.Normal);
			groupSection.TouchUpInside += (sender, e) =>
			{
				_items[(int)section].IsExpanded = !_items[(int)section].IsExpanded;
				_lastExpandedSection = (int)section;
				tableView.ReloadData();
			};
			return groupSection;
		}


	}
}