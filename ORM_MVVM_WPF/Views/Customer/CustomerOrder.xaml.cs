using ORM_MVVM_WPF.ViewModels.Customer;
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

namespace ORM_MVVM_WPF.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerOrder.xaml
    /// </summary>
    public partial class CustomerOrder : UserControl
    {
        private CustomerOrderViewModel _orderVM;  
        public CustomerOrder()
        {
            InitializeComponent();
            _orderVM = new CustomerOrderViewModel();
            DataContext = _orderVM;
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("Are you sure you want to proceed with the payment?", "Payment Confirmation", MessageBoxButton.OKCancel);

            if (msg == MessageBoxResult.OK )
            {
                var button = sender as Button;
                if (button == null)
                    return;
                int id = (int)button.CommandParameter;
                _orderVM.PayOrder(id);
            }

        }
        private void ConfirmCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("Are you sure you want to confirm that your order has been delivered?", "Delivery Confirmation", MessageBoxButton.OKCancel);

            if (msg == MessageBoxResult.OK)
            {
                var button = sender as Button;
                if (button == null)
                    return;
                int id = (int)button.CommandParameter;
                _orderVM.ConfirmDelivery(id);
            }

        }

    }
}
