using SharedSpace.DomainObjects;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharedSpace.CustomControls
{
	public class MultiLevelListView : View
	{
		public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(List<ExpandableListItem>), typeof(MultiLevelListView), null, BindingMode.TwoWay);
		public static readonly BindableProperty ChildSelectedCommandProperty = BindableProperty.Create(nameof(ChildSelectedCommand), typeof(ICommand), typeof(MultiLevelListView), null, BindingMode.TwoWay);
		public static readonly BindableProperty GroupBackColorProperty = BindableProperty.Create(nameof(GroupBackColor), typeof(string), typeof(MultiLevelListView), "", BindingMode.TwoWay);
		public static readonly BindableProperty ChildBackColorProperty = BindableProperty.Create(nameof(ChildBackColor), typeof(string), typeof(MultiLevelListView), "", BindingMode.TwoWay);

		public string GroupBackColor
		{
			get => (string)GetValue(GroupBackColorProperty);
			set
			{
				SetValue(GroupBackColorProperty, value);
			}
		}

		public string ChildBackColor
		{
			get => (string)GetValue(ChildBackColorProperty);
			set
			{
				SetValue(ChildBackColorProperty, value);
			}
		}

		public List<ExpandableListItem> Items
		{
			get => (List<ExpandableListItem>)GetValue(ItemsProperty);
			set
			{
				SetValue(ItemsProperty, value);
			}
		}

		public ICommand ChildSelectedCommand
		{
			get => (ICommand)GetValue(ChildSelectedCommandProperty);
			set
			{
				SetValue(ChildSelectedCommandProperty, value);
			}
		}
	}
}
