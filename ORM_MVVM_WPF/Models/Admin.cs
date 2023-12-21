using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.Models
{
    public class Admin : User
    {
        public string Department { get; set; }

        public override string Role => GetRole();

        public override string GetRole()
        {
            return "admin";
        }
        public Admin()
        {

        }
    }
}
