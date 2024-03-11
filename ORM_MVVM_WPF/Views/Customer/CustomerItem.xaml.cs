using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ORM_MVVM_WPF.ViewModels.Items;
using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.ViewModels.Orders;

namespace ORM_MVVM_WPF.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerItem.xaml
    /// </summary>
    public partial class CustomerItem : UserControl
    {
        private ItemViewModel _cusIVM;
        private OrderViewModel _orderVM;
        public CustomerItem()
        {
            InitializeComponent();
            _cusIVM = new ItemViewModel();
            _orderVM = new OrderViewModel();
            this.DataContext = this._cusIVM;


            //FilterBy.ItemsSource = typeof(Item).GetProperties().Select((o) => o.Name);
            FilterBy.ItemsSource = new List<string> { "Id", "Name", "Type" };
            ItemGrid.Items.Filter = GetFilter();
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

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            _orderVM.PlaceOrder(ItemGrid.SelectedItems);
        }
    }
}
