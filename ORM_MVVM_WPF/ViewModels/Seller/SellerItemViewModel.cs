using ORM_MVVM_WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MVVM_WPF.ViewModels.Seller
{
    public class SellerItemViewModel : BaseViewModel
    {
        private List<Item> itemsList;
        private List<Item> filterSellerItemsList;
        private ObservableCollection<Item> _itemObservableCollection;
        private bool isItemGridVisible;
        private bool isLabelVisible;

        public SellerItemViewModel()
        {
            BindItem();
        }
        public ObservableCollection<Item> ItemObservableCollection
        {
            get { return _itemObservableCollection; }
            set
            {
                _itemObservableCollection = value;
                UpdateSerialNumbersAndVisibility();
                OnPropertyChanged(nameof(ItemObservableCollection));
            }
        }
        public bool IsItemGridVisible
        {
            get { return isItemGridVisible; }
            set
            {
                isItemGridVisible = value;
                OnPropertyChanged(nameof(IsItemGridVisible));
            }
        }

        public bool IsLabelVisible
        {
            get { return isLabelVisible; }
            set
            {
                isLabelVisible = value;
                OnPropertyChanged(nameof(IsLabelVisible));
            }
        }

        public bool AddItem(string name, string description, int price, int size, string material)
        {

            ItemCloth cloth = new ItemCloth();

            if (itemsList.OfType<ItemCloth>().Any())
            {
                cloth.Id = itemsList.OfType<ItemCloth>().Max(item => item.Id) + 1;
            }
            else
            {
                cloth.Id = 1;
            }
            cloth.Name = name;
            cloth.Description = description;
            cloth.Size = size;
            cloth.Price = price;
            cloth.Material = material;
            cloth.SellerID = ((Models.Seller)User.AuthUser).SellerID;
            itemsList.Add(cloth);
            ItemObservableCollection.Add(cloth);
            UpdateSerialNumbersAndVisibility();
            Serialization.SerializeList(itemsList);
            return true;
        }
        public bool AddItem(string name, string description, int price, Brand type)
        {
            ItemElectronic itemElectronic = new ItemElectronic();

            if (itemsList.OfType<ItemElectronic>().Any())
            {
                itemElectronic.Id = itemsList.OfType<ItemElectronic>().Max(item => item.Id) + 1;
            }
            else
            {
                itemElectronic.Id = 1;
            }

            itemElectronic.Name = name;
            itemElectronic.Description = description;
            itemElectronic.Brand = type;
            itemElectronic.Price = price;
            itemElectronic.SellerID = ((Models.Seller)User.AuthUser).SellerID;
            itemsList.Add(itemElectronic);
            ItemObservableCollection.Add(itemElectronic);
            UpdateSerialNumbersAndVisibility();
            Serialization.SerializeList(itemsList);
            return true;

        }

        public bool DeleteItem(object selectedItems)
        {
            try
            {
                IEnumerable<Item> enumerableSelectedItems = (selectedItems as IEnumerable)?.OfType<Item>();
                foreach (var item in enumerableSelectedItems.ToList())
                {
                    ItemObservableCollection.Remove(item);
                    itemsList.Remove(item);

                }
                UpdateSerialNumbersAndVisibility();
                Serialization.SerializeList(itemsList);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void BindItem()
        {
            itemsList = Serialization.DeSerializeList<Item>();
            filterSellerItemsList = itemsList.Where(i => i.SellerID == ((Models.Seller)User.AuthUser).SellerID).ToList();
            ItemObservableCollection = new ObservableCollection<Item>(filterSellerItemsList);
        }

        private void UpdateSerialNumbersAndVisibility()
        {
            int serialNumber = 1;
            foreach (var item in _itemObservableCollection)
            {
                item.SerialNumber = serialNumber++;
            }

            IsItemGridVisible = _itemObservableCollection.Count > 0;
            IsLabelVisible = _itemObservableCollection.Count == 0;
        }
    }
}
