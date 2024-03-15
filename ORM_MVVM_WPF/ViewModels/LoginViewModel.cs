using ORM_MVVM_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
            
        }

        private string _userEmail;
        private string _userPassword;
        private User _user;

        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }
       
        public User User
        {
            get { return _user; }
            set 
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        
        public void LoginAction()
        {
            User = null;
            try
            {
                List<User> users = Serialization.DeSerializeList<User>();
                User = users.FirstOrDefault(search_user => search_user.Email  == UserEmail && search_user.Password == UserPassword);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);  
            }
            User.AuthUser = User;

        }
        public void LogoutAction()
        {
            //For Session Reset
            User = null;
            UserEmail = null;
            UserPassword = null;
        }
    }


}
