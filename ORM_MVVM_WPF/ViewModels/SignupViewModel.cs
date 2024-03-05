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
        public string UserEmail2
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail2));
            }
        }
        public string UserPassword2
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                OnPropertyChanged(nameof(UserPassword2));
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
        public bool SinupAction(string name, string email ,string password, string depart)
        {
            try
            {
                List<User> users = Serialization.DeSerializeList<User>();
                Admin admin = new Admin();

                admin.Username = name;
                admin.Email = email;
                admin.Password = password;
                admin.Department = depart;
                users.Add(admin);

                Serialization.SerializeList(users);
                return true;
            }
            catch 
            {
                return false;
            }     
        }

        public bool SinupAction(string name, string email, string password, CustomerType type)
        {
            string a = UserName;
            Console.WriteLine(a);
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
                customer.Username = name;
                customer.Email = email;
                customer.Password = password;
                customer.CustomerType = type;
                users.Add(customer);

                Serialization.SerializeList(users);
                return true;
            }
            catch 
            {
                return false;
            }
           
        }

        public bool SinupAction(string name, string email, string password, SellerType type)
        {
            try 
            {
                List<User> users = Serialization.DeSerializeList<User>();
                Seller seller = new Seller();

                if (users.OfType<Seller>().Any())
                {
                    seller.SellerID = users.OfType<Seller>().Max(user => user.SellerID) + 1;
                }
                else
                {
                    seller.SellerID = 1;
                }

                seller.Username = name;
                seller.Email = email;
                seller.Password = password;
                seller.SellerType = type;

                users.Add(seller);

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
