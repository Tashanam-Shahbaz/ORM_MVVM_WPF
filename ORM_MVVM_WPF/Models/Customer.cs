using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.Models
{
    public class Customer : User
    {
        public int CustomerID { get; set; }
        public CustomerType CustomerType { get; set; }
        public override string Role => GetRole();
        public override string GetRole()
        {
            return "customer";
        }

        public Customer()
        {
        }
    }

    public enum CustomerType
    {
        regular,
        premium
    }
}
