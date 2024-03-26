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
    }
}
