using ORM_MVVM_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {

        public SignupViewModel() { }

        private string _userName;
        private string _userEmail;
        private string _userPassword;
        private string _selectedUserType;
        private string _selectedCustomerType;
        private string _selectedSellerType;
        private string _adminDepart;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }
        public string UserPasssword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                OnPropertyChanged(nameof(UserPasssword));
            }
        }

        public string SelectedUserType
        {
            get { return _selectedUserType; }
            set
            {
                _selectedUserType = value;
                OnPropertyChanged(nameof(SelectedUserType));
            }
        }
        public string SelectedCustomerType
        {
            get { return _selectedCustomerType; }
            set
            {
                _selectedCustomerType = value;
                OnPropertyChanged(nameof(SelectedCustomerType));
            }
        }
        public string SelectedSellerType
        {
            get { return _selectedSellerType; }
            set
            {
                _selectedSellerType = value;
                OnPropertyChanged(nameof(SelectedSellerType));
            }
        }
        public string AdminDepartment
        {
            get { return _adminDepart; }
            set
            {
                _adminDepart = value;
                OnPropertyChanged(nameof(AdminDepartment));
            }
        }

        public bool SinupAction(string depart)
        {
            try
            {
                List<User> users = Serialization.DeSerializeList<User>();
                Admin admin = new Admin();

                admin.Username = UserName; // Using ViewModel property
                admin.Email = UserEmail; // Using ViewModel property
                admin.Password = UserPasssword; // Using ViewModel property
                admin.Department = depart;
                users.Add(admin);
                User.AuthUser = admin;
                Serialization.SerializeList(users);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool SinupAction(CustomerType type)
        {
            try
            {
                List<User> users = Serialization.DeSerializeList<User>();
                Models.Customer customer = new Models.Customer();

                if (users.OfType<Models.Customer>().Any())
                {
                    customer.CustomerID = users.OfType<Models.Customer>().Max(user => user.CustomerID) + 1;
                }
                else
                {
                    customer.CustomerID = 1;
                }
                customer.Username = UserName; // Using ViewModel property
                customer.Email = UserEmail; // Using ViewModel property
                customer.Password = UserPasssword; // Using ViewModel property
                customer.CustomerType = type;
                users.Add(customer);
                User.AuthUser = customer;
                Serialization.SerializeList(users);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SinupAction(SellerType type)
        {
            try
            {
                List<User> users = Serialization.DeSerializeList<User>();
                Models.Seller seller = new Models.Seller();

                if (users.OfType<Models.Seller>().Any())
                {
                    seller.SellerID = users.OfType<Models.Seller>().Max(user => user.SellerID) + 1;
                }
                else
                {
                    seller.SellerID = 1;
                }

                seller.Username = UserName; // Using ViewModel property
                seller.Email = UserEmail; // Using ViewModel property
                seller.Password = UserPasssword; // Using ViewModel property
                seller.SellerType = type;
                
                users.Add(seller);
                User.AuthUser = seller;
                Serialization.SerializeList(users);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
   }

