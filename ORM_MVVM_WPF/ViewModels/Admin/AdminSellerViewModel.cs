using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_MVVM_WPF.Models;

namespace ORM_MVVM_WPF.ViewModels.Admin
{
    public class AdminSellerViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Seller> _sellers;
        private List<Models.Seller> _sellerList;
        private List<User> _user;

        //Variable for Filters
        private string _sellerUsername;
        private string _sellerEmail;
        private SellerType _sellerType;
        private SellerApprovalStatus _sellerApprovalStatus;

        public AdminSellerViewModel()
        {
            Bind();
        }
        public ObservableCollection<Models.Seller> SellersOC
        {
            get { return _sellers; }
            set
            {
                _sellers = value;
                OnPropertyChanged(nameof(SellersOC));
                CalculateSerialNumbers();
            }
        }

        public string TextUsername
        {
            get { return _sellerUsername; }
            set
            {
                if (_sellerUsername != value)
                {
                    _sellerUsername = value;
                    OnPropertyChanged(nameof(TextUsername));
                    FilterOOC();
                }

            }
        }
        
        public string TextEmail
        {
            get { return _sellerEmail; }
            set {
                if (_sellerEmail!= value)
                {
                    _sellerEmail = value;
                    OnPropertyChanged(nameof(TextEmail));
                    FilterOOC();
                }

            }
        }
        public SellerType ComboSellerType
        {
            get { return _sellerType; }
            set
            {
                if (_sellerType != value)
                {
                    _sellerType = value;
                    FilterOOC();
                    OnPropertyChanged(nameof(ComboSellerType));
                }
            }
        }
        public SellerApprovalStatus ComboApprovalStatus
        {
            get { return _sellerApprovalStatus; }
            set 
            {
                if (_sellerApprovalStatus != value)
                {
                    _sellerApprovalStatus = value;
                    FilterOOC();
                    OnPropertyChanged(nameof(ComboApprovalStatus));
                }
            }
        }
        //Filter Region

        private void FilterOOC()
        {
            Func<Models.Seller, bool> func = s =>
            ( string.IsNullOrEmpty(_sellerUsername) || s.Username.Contains(_sellerUsername)) &&
            ( string.IsNullOrEmpty(_sellerEmail)    || s.Username.Contains(_sellerEmail))    &&
            ( _sellerType == SellerType.All || s.SellerType == _sellerType)                  &&
            (_sellerApprovalStatus == SellerApprovalStatus.All || s.ApprovalStatus == _sellerApprovalStatus);


            SellersOC = new ObservableCollection<Models.Seller>(_sellerList.Where(func));
        }

        private void Bind()
        {
            _sellerList = Serialization.DeSerializeList<Models.Seller>();
            SellersOC = new ObservableCollection<Models.Seller>(_sellerList);
            _user = Serialization.DeSerializeList<User>();
            CalculateSerialNumbers();
        }
        private void Save()
        {
            Serialization.SerializeList(_sellerList);
            SellersOC = new ObservableCollection<Models.Seller>(_sellerList);
        }
        private void CalculateSerialNumbers()
        {
            int serialNumber = 1;
            foreach (var item in _sellerList)
            {
                item.SerialNumber = serialNumber++;
            }
        }

        public bool RejectSeller(int id)
        {
            try 
            {
                var seller = _sellerList.FirstOrDefault(x => x.SellerID == id);
                if (seller == null)
                    return false;
                seller.ApprovalStatus = SellerApprovalStatus.Rejected;
                Save();
                              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;

        }
        public bool ApproveSeller(int id)
        {
            try
            {
                var seller = _sellerList.FirstOrDefault(x => x.SellerID == id);
                if (seller == null)
                    return false;
                seller.ApprovalStatus = SellerApprovalStatus.Accepted;
                seller.SellerID = _user.OfType<Models.Seller>().Count() + 1;
                
                _user.Add(seller);
                Serialization.SerializeList(_user);
               
                Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;

        }   
    }

}
