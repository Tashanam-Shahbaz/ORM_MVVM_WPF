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

        public bool SinupAction(string name, string email ,string password, string depart)
        {
            try
            {
                List<User> users = Serialization.DeSerializeList();
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
            try
            {
                List<User> users = Serialization.DeSerializeList();
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
                List<User> users = Serialization.DeSerializeList();
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
