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
                OnPropertyChanged("Sellers");
            }
        }   
        private void Bind()
        {
            _sellerList = Serialization.DeSerializeList<Models.Seller>();
            SellersOC = new ObservableCollection<Models.Seller>(_sellerList);
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

        public bool DeleteSeller(int id)
        {
            try 
            {
                var seller = _sellerList.FirstOrDefault(x => x.SellerID == id);
                if (seller == null)
                    return false;
                _sellerList.Remove(seller);
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
                seller.IsVerified = true;
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
