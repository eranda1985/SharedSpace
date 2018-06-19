using CoreGraphics;
using System;
using UIKit;

namespace SharedSpace.iOS.Lib
{
	public class ExpandableListCell: UITableViewCell
	{
		public int Height = 100;
		public int ParentItemLeftPadding = 20;
		public int ChildItemLeftPadding = 45;
		public int FontSize = 17;

		public static double DeviceWidth = UIScreen.MainScreen.Bounds.Width * 2;
		public static double DeviceHeight = UIScreen.MainScreen.Bounds.Height * 2;

		public float Width = (int)(Math.Min(DeviceWidth, DeviceHeight) / 2);

		UIView borderSeparator { get; set; }

		UILabel lblName { get; set; }
		UILabel lblDesc { get; set; }
		
		public UIButton btnApply { get; set; }

		public UIImageView imgIcon { get; set; }


		public bool IsSelected { get; set; }

		public string Name
		{
			set
			{
				lblName.Text = value;
			}
		}

		public string DescriptionText
		{
			private get { return lblDesc.Text; }
			set
			{
				lblDesc.Text = value;
			}
		}

		/// <summary>
		/// Creates a new child cell for the expandable list
		/// </summary>
		/// <param name="style"></param>
		/// <param name="identifier"></param>
		/// <param name="tblFrame"></param>
		public ExpandableListCell(UITableViewCellStyle style, string identifier, CGRect tblFrame) : base(style, identifier)
		{
			if (tblFrame != null)
			{
				Width = (float)tblFrame.Width;
			}

			BackgroundColor = UIColor.Clear;

			ContentView.Frame = new CGRect(0, 0, Width, Height);
			ContentView.BackgroundColor = UIColor.FromRGB(242, 242, 242);
			UIView SubContainerView = new UIView(ContentView.Frame);
			SubContainerView.ClipsToBounds = true;
			SubContainerView.AutoresizingMask = UIViewAutoresizing.All;
			ContentView.AutoresizingMask = UIViewAutoresizing.All;
			borderSeparator = new UIView(new CGRect(0, Height - 1, Width, 1));
			borderSeparator.BackgroundColor = UIColor.LightGray;
			borderSeparator.Alpha = 0.3f;
			borderSeparator.AutoresizingMask = UIViewAutoresizing.All;

			lblName = new UILabel(new CGRect(ParentItemLeftPadding, ContentView.Frame.Y, ContentView.Frame.Width - 50, ContentView.Frame.Height / 2));
			lblName.TextColor = UIColor.DarkGray;
			lblName.Lines = 0; // This'll wrap text
			lblName.Font = UIFont.BoldSystemFontOfSize(15);
			lblName.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			lblDesc = new UILabel(new CGRect(ParentItemLeftPadding, ContentView.Frame.Y + ContentView.Frame.Height / 2 + 1, ContentView.Frame.Width - 50, ContentView.Frame.Height / 2));
			lblDesc.TextColor = UIColor.DarkGray;
			lblDesc.Lines = 0; // This'll wrap text
			lblDesc.Font = UIFont.SystemFontOfSize(13);
			lblDesc.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			imgIcon = new UIImageView(new CGRect(ContentView.Frame.Width - 20,0,20,Height));
			imgIcon.ContentMode = UIViewContentMode.Center;
			imgIcon.Image = UIImage.FromBundle("chevron_right_lightgrey");
			imgIcon.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
					
			SubContainerView.AddSubview(lblName);
			SubContainerView.AddSubview(lblDesc);
			SubContainerView.AddSubview(borderSeparator);
			SubContainerView.AddSubview(imgIcon);

			ContentView.AddSubview(SubContainerView);
		}
	}
}