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
using ORM_MVVM_WPF.ViewModels.Seller;

namespace ORM_MVVM_WPF.Views.Seller
{
    /// <summary>
    /// Interaction logic for SellerOrderView.xaml
    /// </summary>
    public partial class SellerOrderView : UserControl
    {
        private SellerOrderViewModel _sellerOVM;
        public SellerOrderView()
        {
            InitializeComponent();
            _sellerOVM = new SellerOrderViewModel();
            DataContext = _sellerOVM;
        }
        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("Are you sure you want to proceed with the payment?", "Payment Confirmation", MessageBoxButton.OKCancel);

            if (msg == MessageBoxResult.OK)
            {
                var button = sender as Button;
                if (button == null)
                    return;
                int id = (int)button.CommandParameter;
                _sellerOVM.PayOrder(id);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
