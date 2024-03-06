using System;
using System.Windows;
using System.Windows.Controls;
using ORM_MVVM_WPF.Models;
using ORM_MVVM_WPF.ViewModels;
using ORM_MVVM_WPF.Views.Admin;
using ORM_MVVM_WPF.Views.Customer;

namespace ORM_MVVM_WPF.Views
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : UserControl
    {
        SignupViewModel _signup;
        public Signup()
        {
            InitializeComponent();
            _signup = new SignupViewModel();
            DataContext = _signup;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedUserType.SelectedItem == null)
                return;

            switch (_signup.SelectedUserType)
            {
                case "Admin":
                    LabelDpartment.Visibility = Visibility.Visible;
                    AdminDepartment.Visibility = Visibility.Visible;

                    // Hide other fields
                    LabelCustomerType.Visibility = Visibility.Collapsed;
                    SelectedCustomerType.Visibility = Visibility.Collapsed;

                    LabelCompanyName.Visibility = Visibility.Collapsed;
                    TextCompanyName.Visibility = Visibility.Collapsed;

                    LabelSellerType.Visibility = Visibility.Collapsed;
                    SelectedSellerType.Visibility = Visibility.Collapsed;
                    break;

                case "Customer":
                    LabelCustomerType.Visibility = Visibility.Visible;
                    SelectedCustomerType.Visibility = Visibility.Visible;

                    // Hide other fields
                    LabelDpartment.Visibility = Visibility.Collapsed;
                    AdminDepartment.Visibility = Visibility.Collapsed;

                    LabelSellerType.Visibility = Visibility.Collapsed;
                    SelectedSellerType.Visibility = Visibility.Collapsed;
                    break;

                case "Seller":
                    LabelSellerType.Visibility = Visibility.Visible;
                    SelectedSellerType.Visibility = Visibility.Visible;

                    // Hide other fields
                    LabelCustomerType.Visibility = Visibility.Collapsed;
                    SelectedCustomerType.Visibility = Visibility.Collapsed;
                    LabelDpartment.Visibility = Visibility.Collapsed;
                    AdminDepartment.Visibility = Visibility.Collapsed;
                    break;

            }
        }
        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
                return;

            bool result = false;
            Enum.TryParse(SelectedUserType.SelectedItem?.ToString(),  out UserType selectedUserType);
            Window mainWindow = Window.GetWindow(this);

            switch (selectedUserType)
            {
                case UserType.Admin:
                    result = _signup.SinupAction(AdminDepartment.Text);
                    if (result)
                    {
                        AdminDashboard adminDashboard = new AdminDashboard();
                        adminDashboard.Show();
                        mainWindow.Close();
                    }
                    else
                        ShowSignUpFailedMessage();

                    break;

                case UserType.Customer:
                    Enum.TryParse(SelectedCustomerType.SelectedItem.ToString(), out CustomerType customertype);
                    result = _signup.SinupAction(customertype);
                    if (result)
                    {
                        CustomerDashboard customerDashboard = new CustomerDashboard();
                        customerDashboard.Show();
                        mainWindow.Close();
                    }
                    else
                        ShowSignUpFailedMessage();
                    break;

                case UserType.Seller:
                    Enum.TryParse(SelectedSellerType.SelectedItem.ToString(), out SellerType sellertype);
                    result = _signup.SinupAction(sellertype);
                    break;
            }

            if (result)
                MessageBox.Show("User Register Successfully!");
            else
                ShowSignUpFailedMessage();

        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(_signup.UserName) ||
                string.IsNullOrWhiteSpace(_signup.UserEmail) ||
                string.IsNullOrWhiteSpace(_signup.UserPasssword) ||
                string.IsNullOrWhiteSpace(ConfirmUserPasssword.Text) ||
                SelectedUserType.SelectedItem == null)
            {
                ShowIncompleteFieldsMessage();
                return false;
            }

            UserType selectedUserType;
            if (!Enum.TryParse(_signup.SelectedUserType, out selectedUserType))
            {
                return false;
            }

            switch (selectedUserType)
            {
                case UserType.Admin:
                    if (string.IsNullOrWhiteSpace(_signup.AdminDepartment))
                    {
                        ShowIncompleteFieldsMessage();
                        return false;
                    }
                    break;

                case UserType.Customer:
                    if (_signup.SelectedCustomerType == null)
                    {
                        ShowIncompleteFieldsMessage();
                        return false;
                    }
                    break;

                case UserType.Seller:
                    if (_signup.SelectedSellerType == null)
                    {
                        ShowIncompleteFieldsMessage();
                        return false;
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        private void ShowIncompleteFieldsMessage()
        {
            MessageBox.Show("Please fill in all the required fields.", "Incomplete Information", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ShowSignUpFailedMessage()
        {
            MessageBox.Show("Something went wrong during sign up. Please try again.", "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
