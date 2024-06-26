﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.Models
{
    public class User
    {
        public static User user;
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual string Role => GetRole();
        public virtual string GetRole()
        {
            return "user";
        }
        public User()
        {
        }

        public static User AuthUser
        {
            get  { return user; }
            set  {user = value; }
        }
    }

    public enum UserType
    {
        Admin,
        Customer,
        Seller
    }
}
