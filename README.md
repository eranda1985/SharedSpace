# SharedSpace
A Xamarin Forms expandable list for iOS &amp; Android, providing a single group to be expanded at a time.    

The aim of this project is to create a cross-platform expandable list which enables data-binding within your view model. This was inspired by the work of [Jay Shukla](https://github.com/shuklajay/CollapseListView-in-xamarin.forms) and a big thank you goes to him.   

![demo](sharedspace-expandable-list-resize.gif)    ![demo](sharedspace-expandable-list-android-resize.gif)  

### Usage  
This is also available as a nuget package in (https://www.nuget.org/packages/SharedSpace.ExpandableList/).  

In your XAML add a reference to SharedSpace.CustomControls.  
Set the bindable attributes *Item* & *ChildSelectedCommand*. The values for these must be available in your view model. 
The following is an example XAML

```
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:customControls="clr-namespace:SharedSpace.CustomControls;assembly=SharedSpace"
             x:Class="Example.SharedSpace.Common.Features.MainPage"
             
             BackgroundColor="{StaticResource BackgroundColor}">
  <ContentPage.Content>
    <StackLayout Spacing="0">
      <customControls:MultiLevelListView Items="{Binding ListItems}" ChildSelectedCommand="{Binding ItemSelected}" VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand"/>
    </StackLayout>
  </ContentPage.Content>
</ContentPage>
```  


In your view model class add a reference to *SharedSpace.DomainObjects*.   
Create a property of type *ExpandableListCollection* and set this as the data binding context for *Item* property in XAML.  
Create a Command property and set this as the binding context for *ChildSelectedCommand* in XAML.  
Following is an example view model with these settings.   
```csharp
public ExpandableListCollection ListItems { get => _listItems; set { _listItems = value; RaisePropertyChanged("ListItems"); } }

public Command ItemSelected => new Command(async (selectedItem) =>
{
  // Describe your logic here when hit on a List item
  var item = selectedItem as ExpandableListItem;
  await App.Current.MainPage.DisplayAlert("You selected an item", item.Name, "Cancel");
});
```
    
