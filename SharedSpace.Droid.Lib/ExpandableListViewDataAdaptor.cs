﻿using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using SharedSpace.DomainObjects;

namespace SharedSpace.Droid.Lib
{
	public class ExpandableListViewDataAdaptor : BaseExpandableListAdapter
	{
		public Activity FormsContext { get; set; }
		public List<ExpandableListItem> DataList { get; set; }

		public ExpandableListViewDataAdaptor(Activity context, List<ExpandableListItem> list)
		{
			FormsContext = context;
			DataList = list;
		}

		public override int GroupCount => DataList.Count;

		public override bool HasStableIds => true;

		public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
		{
			throw new NotImplementedException();
		}

		public override long GetChildId(int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override int GetChildrenCount(int groupPosition)
		{
			return DataList[groupPosition].ChildItems != null ? DataList[groupPosition].ChildItems.Count : 0;
			
		}

		public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if(row == null)
			{
				row = FormsContext.LayoutInflater.Inflate(Resource.Layout.ExpandableListChild, null);
			}
			List<ExpandableListItem> newValue = new List<ExpandableListItem>();
			newValue = DataList[groupPosition].ChildItems;
			row.FindViewById<TextView>(Resource.Id.txtTitle).Text = newValue[childPosition].Name;
			row.FindViewById<TextView>(Resource.Id.txtDesc).Text = newValue[childPosition].Description;
			return row;
			
		}

		public override Java.Lang.Object GetGroup(int groupPosition)
		{
			throw new NotImplementedException();
		}

		public override long GetGroupId(int groupPosition)
		{
			return groupPosition;
		}

		public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			View groupRow = convertView;
			if (groupRow == null)
			{
				groupRow = FormsContext.LayoutInflater.Inflate(Resource.Layout.ExpandableListGroup, null);
			}
			groupRow.FindViewById<TextView>(Resource.Id.txtView).Text = DataList[groupPosition].Name;
			return groupRow;
		}

		public override bool IsChildSelectable(int groupPosition, int childPosition)
		{
			return true;
		}
	}
}