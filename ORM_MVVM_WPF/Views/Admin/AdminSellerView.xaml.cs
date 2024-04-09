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
using ORM_MVVM_WPF.ViewModels.Admin; 

namespace ORM_MVVM_WPF.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminSellerView.xaml
    /// </summary>
    public partial class AdminSellerView : UserControl
    {
        private AdminSellerViewModel _adminSellerVM;
        public AdminSellerView()
        {
            InitializeComponent();
            _adminSellerVM = new AdminSellerViewModel();
            DataContext = _adminSellerVM;
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            if (button == null)
                return;
            int id = (int)button.CommandParameter;
            bool result = _adminSellerVM.RejectSeller(id);
            string msg = result ? "Seller deleted successfully." : 
                                  "Unable to delete seller. Please try again later.";
            MessageBox.Show(msg);
        }
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            if (button == null)
                return;
            int id = (int)button.CommandParameter;
            bool result = _adminSellerVM.ApproveSeller(id);
            string msg = result ? "Seller approved successfully." : 
                                  "Failed to approve seller. There was an issue encountered. Please try again later.";
            MessageBox.Show(msg);

        }


    }
}
