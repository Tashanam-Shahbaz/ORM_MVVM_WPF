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
    /// Interaction logic for AdminOrderManageView.xaml
    /// </summary>
    public partial class AdminOrderView : UserControl
    {
        private AdminOrderViewModel _adminOVM;
        public AdminOrderView()
        {
            InitializeComponent();
            _adminOVM = new AdminOrderViewModel();
            DataContext = _adminOVM;
        }

        private void DeliverOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("Are you sure you want to deliver items?"
                , "Delivary Confirmation", MessageBoxButton.OKCancel);

            if (msg != MessageBoxResult.OK)
                return;

            var button = sender as Button;

            if (button == null)
                return;

            int id = (int)button.CommandParameter;
            _adminOVM.DeliverOrder(id);


        }
    }
}
