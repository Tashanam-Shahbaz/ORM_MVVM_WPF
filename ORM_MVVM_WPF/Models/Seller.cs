using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.Models
{
    public class Seller : User
    {

        public int SellerID { get; set; }
        public string CompanyName { get; set; }
        public SellerType SellerType { get; set; }

        public override string Role => GetRole();
        public override string GetRole()
        {
            return "seller";
        }

    }

    public enum SellerType
    {
        Retailer,
        Wholesaler,
        Manufacturer,
        Dropshipper,
        Franchisee
    }
}
