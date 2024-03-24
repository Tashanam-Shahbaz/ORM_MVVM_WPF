using ORM_MVVM_WPF.ViewModels;
using ORM_MVVM_WPF.Views.Admin;
using ORM_MVVM_WPF.Views.Customer;
using ORM_MVVM_WPF.Views.Seller;
using System.Windows;
using System.Windows.Controls;

namespace ORM_MVVM_WPF.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        LoginViewModel _login;
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
            _login.LoginAction();
            string Usertype = _login.User?.GetType().Name;
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
                    SellerDashboard sellerDashboard = new SellerDashboard();
                    sellerDashboard.Show();
                    mainWindow.Close();
                    break;

                default:
                    ShowLoginFailedMessage();
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
        private void ShowLoginFailedMessage()
        {
            MessageBox.Show("Something went wrong during login. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
