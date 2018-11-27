
using System.ComponentModel;
using Android.App;
using Android.Widget;
using SharedSpace.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MultiLevelListView), typeof(SharedSpace.Droid.Lib.ExpandableListViewRenderer))]

namespace SharedSpace.Droid.Lib
{
    public class ExpandableListViewRenderer : ViewRenderer<MultiLevelListView, ExpandableListView>,
                                                                                        ExpandableListView.IOnGroupExpandListener,
                                                                                        ExpandableListView.IOnGroupCollapseListener,
                                                                                        ExpandableListView.IOnChildClickListener
    {
        private int _lastExpandedGroup = -1;
        private ExpandableListViewDataAdaptor _expandableListViewDataAdaptor = null;

        public ExpandableListViewRenderer()
        {

        }

        public static void Init()
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
            Element.Items[groupPosition].IsExpanded = false;
        }

        public void OnGroupExpand(int groupPosition)
        {
            if (Control != null)
            {
                if (_lastExpandedGroup == -1)
                {
                    _lastExpandedGroup = groupPosition;
                }

                else if (_lastExpandedGroup != groupPosition)
                {
                    Control.CollapseGroup(_lastExpandedGroup);
                    _lastExpandedGroup = groupPosition;
                }

                Element.Items[groupPosition].IsExpanded = true;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MultiLevelListView> e)
        {
            base.OnElementChanged(e);

            var control = new ExpandableListView(this.Context);
            if (e.NewElement != null)
            {
                _expandableListViewDataAdaptor = new ExpandableListViewDataAdaptor(this.Context as Activity, e.NewElement);
                control.SetAdapter(_expandableListViewDataAdaptor);
                control.SetGroupIndicator(null);
                control.SetOnGroupExpandListener(this);
                control.SetOnGroupCollapseListener(this);
                control.SetOnChildClickListener(this);
            }

            SetNativeControl(control);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // In Xamarin Forms > 3.0 this method is called when leaving the current view which in 
            // turn results in setting the adapter object even if the view has been destroyed. 
            // So explicitly return this method when Items are null;
            if (sender.GetType() == typeof(MultiLevelListView))
            {
                var multiViewCtl = (MultiLevelListView)sender;
                if (multiViewCtl.Items == null)
                {
                    return;
                }
            }

            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == MultiLevelListView.ItemsProperty.PropertyName)
            {
                _expandableListViewDataAdaptor = new ExpandableListViewDataAdaptor(this.Context as Activity, Element);

                Control.SetAdapter(_expandableListViewDataAdaptor);
            }
            if (e.PropertyName == MultiLevelListView.ChildSelectedCommandProperty.PropertyName)
            {
                var selectedItem = Element.Items[((ExpandableListViewEventArgs)e).GroupPosition].ChildItems[((ExpandableListViewEventArgs)e).ChildPosition];
                Element.ChildSelectedCommand?.Execute(selectedItem);

            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_expandableListViewDataAdaptor != null)
            {
                _expandableListViewDataAdaptor.Dispose();
                _expandableListViewDataAdaptor = null;

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