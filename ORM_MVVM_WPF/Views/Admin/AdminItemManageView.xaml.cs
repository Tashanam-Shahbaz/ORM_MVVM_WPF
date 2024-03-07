using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.ViewModels.AdminViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace ORM_MVVM_WPF.Views.Admin
{

    /// <summary>
    /// Interaction logic for AdminItemManageView.xaml
    /// </summary>
    public partial class AdminItemManageView : UserControl
    {
        AdminManageItemViewModel _viewModel;
        public AdminItemManageView()
        {
            InitializeComponent();
            _viewModel = new AdminManageItemViewModel();
            this.DataContext = _viewModel;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AddNewItemAdminViewWindow addNewItemAdminView = new AddNewItemAdminViewWindow(_viewModel);
            addNewItemAdminView.Show();
        }

        private void SearchItem_Click(object sender, RoutedEventArgs e)
        {
            //SearchItemName
            //SearchItemType
        }

        private void SearchItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string itemtype = SearchItemType.SelectedItem?.ToString();
            _viewModel.SearchOnType(itemtype?.ToLower());
            //    string itemtype = SearchItemType.SelectedItem?.ToString();
            //    DynamicDataGrid.Items.Clear();
            //    switch (itemtype?.ToLower())
            //    {
            //        case "cloth":
            //            var clothItems = _viewModel.ItemObservableCollection.Where(Items => Items.GetType().Name == "ItemCloth");
            //            DynamicDataGrid.Items.Add(clothItems);
            //            break;


            //        case "electronic":
            //            var electronicItems = _viewModel.ItemObservableCollection.Where(Items => Items.GetType().Name == "ItemElectronic");
            //            DynamicDataGrid.Items.Add(electronicItems);
            //            break;

            //        case "all":
            //            DynamicDataGrid.Items.Add(_viewModel.ItemObservableCollection);
            //            break;
            //    }

        }

        private void SearchItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string itemName = SearchItemName.Text.Trim().ToLower();
            _viewModel.SearchOnType(itemName);
            //    string itemType = SearchItemType.SelectedItem?.ToString()?.ToLower();
            //    DynamicDataGrid.Items.Clear(); // Clear existing items

            //    if (!string.IsNullOrEmpty(itemName))
            //    {
            //        var filteredItems = Items.Where(item => item.Name.ToLower().Contains(itemName)).ToList();
            //        var clothItems = new ObservableCollection<ItemCloth>(filteredItems.OfType<ItemCloth>());
            //        var electronicItems = new ObservableCollection<ItemElectronic>(filteredItems.OfType<ItemElectronic>());


            //        if ((string.IsNullOrEmpty(itemType) || itemType == "all") && clothItems.Any() && electronicItems.Any())
            //        {
            //            DynamicDataGrid.Items.Add(clothItems);
            //            DynamicDataGrid.Items.Add(electronicItems);
            //        }
            //        else if (itemType == "cloth" && clothItems.Any())
            //        {
            //            DynamicDataGrid.Items.Add(clothItems);
            //        }
            //        else if (itemType == "electronic" && electronicItems.Any())
            //        {
            //            DynamicDataGrid.Items.Add(electronicItems);
            //        }
            //    }
            //    else
            //    {
            //        SearchItemType_SelectionChanged(null, null);
            //    }
        }


        //private void DisplayItem_Click()
        //{
        //     = new AdminManageItemViewModel();
        //    Items = viewModel.DisplayItem();

        //    var distinctItemTypes = Items.Select(item => item.GetType()).Distinct().ToList();

        //    foreach (var itemType in distinctItemTypes)
        //    {
        //        var itemTypeList = Items.Where(item => item.GetType() == itemType).ToList();

        //        Type genericType = typeof(ObservableCollection<>).MakeGenericType(itemType);
        //        dynamic dynamicObservableCollection = Activator.CreateInstance(genericType);

        //        var addMethod = genericType.GetMethod("Add"); // Retrieve the 'Add' method

        //        foreach (var item in itemTypeList)
        //        {
        //            addMethod.Invoke(dynamicObservableCollection, new[] { item }); // Invoke 'Add' method to add item
        //        }

        //        dynamicDataGrid = new DataGrid();
        //        dynamicDataGrid.AutoGenerateColumns = true;
        //        dynamicDataGrid.ItemsSource = dynamicObservableCollection;
        //        DynamicDataGrid.Items.Add(dynamicDataGrid);
        //    }
        //}
    }


}
