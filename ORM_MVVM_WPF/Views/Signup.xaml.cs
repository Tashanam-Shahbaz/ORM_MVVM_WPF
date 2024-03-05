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
using ORM_MVVM_WPF.ViewModels;

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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedUserType.SelectedItem == null)
                return;

            switch (_signup.SelectedUserType)
            {
                case "Admin":
                    LabelDpartment.Visibility = Visibility.Visible;
                    AdminDepartment.Visibility = Visibility.Visible;
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
                    LabelDpartment.Visibility = Visibility.Collapsed;
                    AdminDepartment.Visibility = Visibility.Collapsed;
                    LabelSellerType.Visibility = Visibility.Collapsed;
                    SelectedSellerType.Visibility = Visibility.Collapsed;
                    break;

                case "Seller":
                    LabelSellerType.Visibility = Visibility.Visible;
                    SelectedSellerType.Visibility = Visibility.Visible;
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
            UserType selectedUserType;
            Enum.TryParse(SelectedUserType.SelectedItem?.ToString(), out selectedUserType);

            switch (selectedUserType)
            {
                case UserType.Admin:
                    result = _signup.SinupAction(AdminDepartment.Text);
                    break;

                case UserType.Customer:
            
                    CustomerType type;
                    Enum.TryParse(SelectedCustomerType.SelectedItem.ToString(), out type);
                    result = _signup.SinupAction(type);
                    break;

                case UserType.Seller:
                    SellerType sellertype;
                    Enum.TryParse(SelectedSellerType.SelectedItem.ToString(), out sellertype);
                    result = _signup.SinupAction(sellertype);
                    break;
            }
               
            if (result)
                MessageBox.Show("User Register Successfully!");
            else 
                MessageBox.Show("Oops! Registration failed. Please try again later.","Registration failed", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

    }
}
