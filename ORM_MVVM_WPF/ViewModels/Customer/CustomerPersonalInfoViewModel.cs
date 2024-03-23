using ORM_MVVM_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ORM_MVVM_WPF.ViewModels.Customer
{
    public class CustomerPersonalInfoViewModel : BaseViewModel
    {
        private int _id;
        private string _userName;
        private string _email;
        private string _password;
        private CustomerType _customerType;
        List<User> users;
        public CustomerPersonalInfoViewModel()
        {
            Bind();
        }
        public int PIId
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(PIId));
            }
        }
        public string PIUserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(PIUserName));
                }
            }
        }
        public string PIEmail
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(PIEmail));
                }

            }
        }
        public string PIPassword
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(PIPassword));
                }
                
            }
        }
        public CustomerType PIUserCategory
        {
            get { return _customerType; }
            set
            {
                if (_customerType != value)
                {
                    _customerType = value;
                    OnPropertyChanged(nameof(PIUserCategory));
                }

            }
        }
        private void Bind()
        {
            users = Serialization.DeSerializeList<User>();
            Models.Customer customer = (Models.Customer)User.AuthUser;
            PIId = customer.CustomerID;
            PIUserName = customer.Username;
            PIEmail = customer.Email;
            PIPassword = customer.Password;
            PIUserCategory = customer.CustomerType;
        }

        public bool EditPI()
        {
            try
            {

                Models.Customer customer = new Models.Customer();

                customer.Username = string.IsNullOrEmpty(PIUserName) ? customer.Username : PIUserName;
                customer.Email = string.IsNullOrEmpty(PIEmail) ? customer.Email : PIEmail;
                customer.Password = string.IsNullOrEmpty(PIPassword) ? customer.Password : PIPassword;
                customer.CustomerType = PIUserCategory;
                customer.CustomerID = PIId;
                
                var index = users.OfType<Models.Customer>().ToList().FindIndex(x => x.CustomerID == customer.CustomerID);
                if (index != -1)
                {
                    users[index] = customer;
                }
                else
                {
                    return false;
                }
                User.AuthUser = customer;
                Serialization.SerializeList(users);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }

}
