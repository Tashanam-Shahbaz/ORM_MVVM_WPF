using ORM_MVVM_WPF.ViewModels.Seller;
using ORM_MVVM_WPF.Views.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ORM_MVVM_WPF.Models;    

namespace ORM_MVVM_WPF.Views.Seller
{
    /// <summary>
    /// Interaction logic for SellerItemView.xaml
    /// </summary>
    public partial class SellerItemView : UserControl
    {
        SellerItemViewModel _viewModel;
        public SellerItemView()
        {
            InitializeComponent();
            _viewModel = new SellerItemViewModel();
            this.DataContext = _viewModel;

            //FilterBy.ItemsSource = typeof(Item).GetProperties().Select((o) => o.Name);
            FilterBy.ItemsSource = new List<string> { "Id", "Name", "Type" };
            ItemGrid.Items.Filter = GetFilter();
        }
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            SellerAddItem addNewItemAdminView = new SellerAddItem(_viewModel);
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


        }


    }


}
