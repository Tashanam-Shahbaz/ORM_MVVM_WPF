using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.ViewModels;
using ORM_MVVM_WPF.Views.Admin;
using ORM_MVVM_WPF.Views.Customer;
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

namespace ORM_MVVM_WPF.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        LoginViewModel _login;
        User _user;
        public Login()
        {
            InitializeComponent();
            _login = new LoginViewModel();
            this.DataContext = _login;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
                return;
            _user = _login.LoginAction();
            string Usertype = _user?.GetType().Name;
            Window mainWindow = Window.GetWindow(this);

            switch (Usertype)
            {
                case ("Admin"):
                    AdminDashboard adminDashboard = new AdminDashboard();
                    adminDashboard.Show();
                    mainWindow.Close();

                    break;

                case ("Customer"):
                    CustomerDashboard customerDashboard = new CustomerDashboard();
                    customerDashboard.Show();
                    mainWindow.Close();
                    break;

                case ("Seller"):
                    break;

                default:
                    break;

            }


        }
        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(UserEmail.Text) ||
                string.IsNullOrWhiteSpace(UserPassword.Text)
             )
            {
                ShowIncompleteFieldsMessage();
                return false;
            }
            return true;
        }

        private void ShowIncompleteFieldsMessage()
        {
            MessageBox.Show("Please fill in all the required fields.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
