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
        public Signup()
        {
            InitializeComponent();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password) ||
                UserTypeComboBox.SelectedItem == null)
            {
                ShowIncompleteFieldsMessage();
                return false;
            }

            UserType selectedUserType;
            if (!Enum.TryParse(UserTypeComboBox.SelectedItem?.ToString(), out selectedUserType))
            {
                return false;
            }

            switch (selectedUserType)
            {
                case UserType.Admin:
                    if (string.IsNullOrWhiteSpace(TextDepartment.Text))
                    {
                        ShowIncompleteFieldsMessage();
                        return false;
                    }
                    break;

                case UserType.Customer:
                    if (OptionCustomerType.SelectedItem == null)
                    {
                        ShowIncompleteFieldsMessage();
                        return false;
                    }
                    break;

                case UserType.Seller:
                    if (OptionSellerType.SelectedItem == null)
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
            if (UserTypeComboBox.SelectedItem != null)
            {
                UserType selectedUserType;
                string selectedUserTypeString = UserTypeComboBox.SelectedItem.ToString();
                Enum.TryParse(selectedUserTypeString, out selectedUserType);


                if (selectedUserType == UserType.Admin)
                {
                    LabelDpartment.Visibility = Visibility.Visible;
                    TextDepartment.Visibility = Visibility.Visible;

                    // Hide other fields
                    LabelCustomerType.Visibility = Visibility.Collapsed;
                    OptionCustomerType.Visibility = Visibility.Collapsed;

                    LabelCompanyName.Visibility = Visibility.Collapsed;
                    TextCompanyName.Visibility = Visibility.Collapsed;

                    LabelSellerType.Visibility = Visibility.Collapsed;
                    OptionSellerType.Visibility = Visibility.Collapsed;

                }
                else if (selectedUserType == UserType.Customer)
                {
                    LabelCustomerType.Visibility = Visibility.Visible;
                    OptionCustomerType.Visibility = Visibility.Visible;

                    // Hide other fields
                    LabelDpartment.Visibility = Visibility.Collapsed;
                    TextDepartment.Visibility = Visibility.Collapsed;

                    LabelSellerType.Visibility = Visibility.Collapsed;
                    OptionSellerType.Visibility = Visibility.Collapsed;
                }
                else if (selectedUserType == UserType.Seller)
                {

                    LabelSellerType.Visibility = Visibility.Visible;
                    OptionSellerType.Visibility = Visibility.Visible;


                    // Hide other fields
                    LabelCustomerType.Visibility = Visibility.Collapsed;
                    OptionCustomerType.Visibility = Visibility.Collapsed;
                    LabelDpartment.Visibility = Visibility.Collapsed;
                    TextDepartment.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                bool result = false;
                UserType selectedUserType;
                Enum.TryParse(UserTypeComboBox.SelectedItem?.ToString(), out selectedUserType);

                SignupViewModel signup = new SignupViewModel();

                if (selectedUserType == UserType.Admin)
                {
                    result = signup.SinupAction(NameTextBox.Text, EmailTextBox.Text, PasswordBox.Password, TextDepartment.Text);
                }
                else if (selectedUserType == UserType.Customer)
                {
                    CustomerType type;
                    Enum.TryParse(OptionCustomerType.SelectedItem.ToString(), out type);
                    result = signup.SinupAction(NameTextBox.Text, EmailTextBox.Text, PasswordBox.Password, type);
                }
                else if (selectedUserType == UserType.Seller)
                {

                    SellerType type;
                    Enum.TryParse(OptionSellerType.SelectedItem.ToString(), out type);
                    result = signup.SinupAction(NameTextBox.Text, EmailTextBox.Text, PasswordBox.Password, type);
                }
                if (result)
                {
                    MessageBox.Show("User Register Successfully!");
                }
                else 
                {
                    MessageBox.Show("Oops! Registration failed. Please try again later.","Registration failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

    }
}
