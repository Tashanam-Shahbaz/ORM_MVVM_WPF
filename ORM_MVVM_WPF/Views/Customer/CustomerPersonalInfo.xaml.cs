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
using ORM_MVVM_WPF.ViewModels;
namespace ORM_MVVM_WPF.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerPersonalInfo.xaml
    /// </summary>
    public partial class CustomerPersonalInfo : UserControl
    {
        private CustomerPersonalInfoViewModel _customerPIVM;
        public CustomerPersonalInfo()
        {
            InitializeComponent();
            _customerPIVM = new CustomerPersonalInfoViewModel();
            DataContext = _customerPIVM;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerPIEdit cPIEdit = new CustomerPIEdit(_customerPIVM);
            cPIEdit.Show();
        }
    }
}
