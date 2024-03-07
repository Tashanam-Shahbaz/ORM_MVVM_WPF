using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using ORM_MVVM_WPF.Models;

namespace ORM_MVVM_WPF.ViewModels.AdminViewModel
{
    public class AdminManageItemViewModel : BaseViewModel
    {
        private List<Item> itemsList;
        private ObservableCollection<Item> _itemObservableCollection;

        public AdminManageItemViewModel()
        {
            BindItem();
        }
        public ObservableCollection<Item> ItemObservableCollection
        {
            get { return _itemObservableCollection; }
            set
            {
                _itemObservableCollection = value;
                OnPropertyChanged(nameof(ItemObservableCollection));
            }
        }

        public bool AddItem(string name,string description,int price ,int  size , string material)
        {

            ItemCloth cloth = new ItemCloth();

            if (itemsList.OfType<ItemCloth>().Any())
            {
                cloth.Id = itemsList.OfType<ItemCloth>().Max( item => item.Id ) + 1;
            }
            else 
            { 
            cloth.Id=1;
            }
            cloth.Name = name;
            cloth.Description = description;
            cloth.Size = size;
            cloth.Price = price;
            cloth.Material = material;
            itemsList.Add(cloth);
            ItemObservableCollection.Add(cloth);
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
            itemsList.Add(itemElectronic);
            ItemObservableCollection.Add(itemElectronic);
            Serialization.SerializeList(itemsList);
            return true;

        }

        public void SearchOnType(string type)
        {
            switch (type)
            {
                case "cloth":
                    ItemObservableCollection = new ObservableCollection<Item>(itemsList.Where(item => item.GetType().Name == "ItemCloth"));
                    break;
                case "electronic":
                    ItemObservableCollection = new ObservableCollection<Item>(itemsList.Where(item => item.GetType().Name == "ItemElectronic"));
                    break;
                default:
                    ItemObservableCollection = new ObservableCollection<Item>(itemsList);
                    break;

            }
        }

        public void SearchOnName(string name)
        {
            ItemObservableCollection = new ObservableCollection<Item>(itemsList.Where(item => item.Name.ToLower().Contains(name)));
        }

        private void BindItem()
        {
            itemsList = Serialization.DeSerializeList<Item>();
            ItemObservableCollection = new ObservableCollection<Item>(itemsList);
        }
    }


}
