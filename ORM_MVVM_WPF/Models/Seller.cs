using ORM_MVVM_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ORM_MVVM_WPF.Models
{
    public class Seller : User , INotifyPropertyChanged
    {
        private int sellerID;
        private int sNo;
        private string companyName;
        private SellerType sellerType;
        private  bool isVerified;
        public Seller()
        {
            IsVerified = false;
        }

        [XmlIgnore]
        public int SerialNumber
        {
            get { return sNo; }
            set
            {
                sNo = value;
                OnPropertyChanged(nameof(SerialNumber));
            }
        }   
        public int SellerID 
        {
            get { return sellerID; }
            set
            {
                sellerID = value;
                OnPropertyChanged(nameof(SellerID));
            }
        }

        public string CompanyName 
        {
            get { return companyName; }
            set
            {
                companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }   
        }
        public SellerType SellerType {
            get { return sellerType;  }
            set
            {
                sellerType = value;
                OnPropertyChanged(nameof(SellerType));
            }
        }
        public bool IsVerified
        {
            get { return isVerified; }
            set
            {
                isVerified = value;
                OnPropertyChanged(nameof(IsVerified));
            }
        }

        public override string Role => GetRole();
        public override string GetRole()
        {
            return "seller";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
