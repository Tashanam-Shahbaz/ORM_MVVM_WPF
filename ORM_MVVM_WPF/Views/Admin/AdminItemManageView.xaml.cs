using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.ViewModels;
using ORM_MVVM_WPF.ViewModels.AdminViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ORM_MVVM_WPF.ViewModels.Items;

namespace ORM_MVVM_WPF.Views.Admin
{

    /// <summary>
    /// Interaction logic for AdminItemManageView.xaml
    /// </summary>
    public partial class AdminItemManageView : UserControl
    {
        ItemViewModel _viewModel;
        public AdminItemManageView()
        {
            InitializeComponent();
            _viewModel = new ItemViewModel();
            this.DataContext = _viewModel;

            //FilterBy.ItemsSource = typeof(Item).GetProperties().Select((o) => o.Name);
            FilterBy.ItemsSource = new List<string> { "Id" , "Name" , "Type" };
            ItemGrid.Items.Filter = GetFilter();

        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AddNewItemAdminViewWindow addNewItemAdminView = new AddNewItemAdminViewWindow(_viewModel);
            addNewItemAdminView.Show();
        }

        private void SearchItemType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemGrid.Items.Filter = GetFilter();

        }
        private void SearchItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FilterTextBox.Text == null)
            {
                ItemGrid.Items.Filter = null;
            }
            else
            {
                ItemGrid.Items.Filter = GetFilter();
            }

        }
        public Predicate<object> GetFilter()
        {
            switch (FilterBy.SelectedItem as string)
            {
                case "Id":
                    return IdFilter;

                case "Name":
                    return NameFilter;

                case "Type":
                    return TypeFilter;
            }
            return NameFilter;
        }

        private bool IdFilter(object obj)
        {
            var FilterObj = obj as Item;
            if (FilterObj != null)
            {
                int filterId;
                if (int.TryParse(FilterTextBox.Text, out filterId))
                {
                    return FilterObj.Id == filterId;
                }
            }
            return false;
        }
        private bool NameFilter(object obj)
        {
            var FilterObj = obj as Item;
            if (FilterObj != null)
            {
                return FilterObj.Name.IndexOf(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            }
            return false;
                
        }
        private bool TypeFilter(object obj)
        {
            var FilterObj = obj as Item;
            return FilterObj.Type.IndexOf(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to delete.");
                return; 
            }
            _viewModel.DeleteItem(ItemGrid.SelectedItems);
            //foreach (var selectedItem in ItemGrid.SelectedItems)
            //{
            //    Item item = selectedItem as Item;
            //    if (item != null)
            //    {

            //        //ItemObservableCollection.Remove(item);
            //        //itemsList.Remove(item);
            //        //Serialization.SerializeList(itemsList);

            //    }
            

        }


    }


}
