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
using System.Windows.Shapes;

namespace ORM_MVVM_WPF.Views.Customer
{
    /// <summary>
    /// Interaction logic for CustomerPIEdit.xaml
    /// </summary>
    public partial class CustomerPIEdit : Window
    {
        private CustomerPersonalInfoViewModel _vm;
        public CustomerPIEdit(CustomerPersonalInfoViewModel viewModel)
        {
            InitializeComponent();
            _vm = viewModel;
            DataContext = _vm;
        }

        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            bool result =_vm.EditPI();
            if (result)
            {
                MessageBox.Show("Edit Successful");
                this.Close();
            }
            else
            {
                MessageBox.Show("Edit Failed");
            }
        }
    }
}
